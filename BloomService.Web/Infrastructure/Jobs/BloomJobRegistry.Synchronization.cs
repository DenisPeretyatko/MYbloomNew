using BloomService.Domain.Entities.Concrete;
using System;
using System.Collections;
using System.Linq;
using BloomService.Domain.Extensions;
using System.Collections.Generic;

namespace BloomService.Web.Infrastructure.Jobs
{
    public partial class BloomJobRegistry
    {
        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            if (selfValue is IList && toValue is IList)
                            {
                                if (PublicInstancePropertiesEqual(selfValue, toValue))
                                {
                                    continue;
                                }
                                return false;
                            }
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }

        public void Synchronization()
        {
            //Sync sage and mongoDb
            Schedule(() =>
            {
                lock (_syncSageLock)
                {
                    GetEntities(); //2min
                    try //Add: 2sec Update: 0.5sec
                    {
                        var rateSheetList = _repository.GetAll<SageRateSheet>().ToList();
                        foreach (var entity in RateSheetArray.Entities)
                        {
                            var mongoEntity = rateSheetList.SingleOrDefault(x => x.RATESHEETNBR == entity.RATESHEETNBR);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(mongoEntity, entity, "Id"))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRateSheet {0}", ex);
                    }

                    try
                    { //Add: 1sec Update: 0.126sec
                        var permissionCodeList = _repository.GetAll<SagePermissionCode>().ToList();
                        foreach (var entity in PermissionCodesArray.Entities)
                        {
                            var mongoEntity =
                                permissionCodeList.SingleOrDefault(x => x.PERMISSIONCODE == entity.PERMISSIONCODE);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(mongoEntity, entity, "Id"))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePermissionCode {0}", ex);
                    }

                    var workOrderLocationAccordance = _proxy.GetAccordance();
                    var workOrderList = _repository.GetAll<SageWorkOrder>().ToList();
                    var workorderItems = _repository.GetAll<SageWorkOrderItem>().ToList();
                    var numberOfQueries = 10;
                    var lastWo = WorkOrders.Entities.Max(x => x.WorkOrder);
                    var currentQuery = 0;
                    var querySize = lastWo / 10;
                    try //Add: 80min Update: 8min 30sec
                    {
                        while (currentQuery < numberOfQueries)
                        {
                            currentQuery++;
                            var first = (currentQuery - 1) * querySize + 1;
                            var last = currentQuery * querySize;
                            var result = _proxy.GetDiapasonItems(first, last);

                            foreach (var entity in result.Entities)
                            {
                                var mongoEntity =
                                    workorderItems.SingleOrDefault(
                                        x =>
                                            x.WorkOrder == entity.WorkOrder &&
                                            x.WorkOrderItem == entity.WorkOrderItem);
                                if (mongoEntity == null)
                                    _repository.Add(entity);
                                else
                                {
                                    if (PublicInstancePropertiesEqual(mongoEntity, entity, "Id"))
                                        continue;

                                    entity.Id = mongoEntity.Id;
                                    _repository.Update(entity);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageWorkOrderItems {0}", ex);
                    }

                    try //Add:30min Update: 4min
                    {
                        workorderItems = _repository.GetAll<SageWorkOrderItem>().ToList();
                        foreach (var workOrder in WorkOrders.Entities)
                        {
                            var mongoEntity = workOrderList.SingleOrDefault(x => x.WorkOrder == workOrder.WorkOrder);

                            if (mongoEntity == null)
                            {
                                var result = workorderItems.Where(x => x.WorkOrder == workOrder.WorkOrder);
                                if (result != null)
                                {
                                    workOrder.WorkOrderItems = result;
                                }
                                var notesResult =
                                  WorkOrderNotes.Entities.Where(x => x.TRANSNBR == workOrder.WorkOrder).ToList();
                                if (notesResult.Count != 0)
                                {
                                    workOrder.WorkNotes = notesResult;
                                }
                                workOrder.LocationNumber =
                                    workOrderLocationAccordance.Entities.Single(
                                        x => x.WRKORDNBR == workOrder.WorkOrder)
                                        .SERVSITENBR;
                                _repository.Add(workOrder);
                            }
                            else
                            {
                                var woiResult =
                                    workorderItems.Where(x => x.WorkOrder == workOrder.WorkOrder).ToList();
                                if (woiResult != null && woiResult.Count != 0)
                                {
                                    workOrder.WorkOrderItems = woiResult;
                                }

                                var notesResult =
                                    WorkOrderNotes.Entities.Where(x => x.TRANSNBR == workOrder.WorkOrder).ToList();
                                if (notesResult.Count != 0)
                                {
                                    workOrder.WorkNotes = notesResult;
                                }

                                workOrder.Id = mongoEntity.Id;
                                workOrder.Status = mongoEntity.Status;
                                workOrder.AssignmentId = mongoEntity.AssignmentId;
                                workOrder.Latitude = mongoEntity.Latitude;
                                workOrder.Longitude = mongoEntity.Longitude;
                                workOrder.ScheduleDate = mongoEntity.ScheduleDate;
                                workOrder.Color = mongoEntity.Color;
                                workOrder.EmployeeId = mongoEntity.EmployeeId;
                                workOrder.LocationNumber =
                                    workOrderLocationAccordance.Entities.Single(
                                        x => x.WRKORDNBR == workOrder.WorkOrder)
                                        .SERVSITENBR;
                                if (PublicInstancePropertiesEqual(workOrder, mongoEntity) &&
                                    workOrder.LocationNumber == mongoEntity.LocationNumber)
                                    continue;
                                _repository.Update(workOrder);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageWorkOrder {0}", ex);
                    }

                    try
                    { //Add: 1 sec Update: 0.1sec
                        var callTypeList = _repository.GetAll<SageCallType>().ToList();
                        foreach (var entity in CallTypeArray.Entities)
                        {
                            var mongoEntity = callTypeList.SingleOrDefault(x => x.CallType == entity.CallType);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                if (PublicInstancePropertiesEqual(entity, mongoEntity, "Id"))
                                    continue;
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync  SageCallType {0}", ex);
                    }

                    try
                    {
                        var employeeList = _repository.GetAll<SageEmployee>().ToList();
                        foreach (var entity in Technicians)
                        {
                            var mongoEntity = employeeList.SingleOrDefault(x => x.Employee == entity.Employee);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                entity.Color = mongoEntity.Color;
                                entity.Picture = mongoEntity.Picture;
                                entity.AvailableDays = mongoEntity.AvailableDays;
                                entity.IsAvailable = mongoEntity.IsAvailable;
                                entity.IosDeviceToken = mongoEntity.IosDeviceToken;
                                entity.Alias = mongoEntity.Alias;
                                entity.Name = mongoEntity.Name;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEmployee {0}", ex);
                    }

                    try
                    {//Add: 50min Update: 4min
                        var workordersList = _repository.GetAll<SageWorkOrder>().ToList();
                        var assigmentsList = _repository.GetAll<SageAssignment>().ToList();

                        var employeeList = _repository.GetAll<SageEmployee>().ToList();

                        foreach (var assigment in Assignments.Entities)
                        {
                            var mongoEntity =
                                assigmentsList.SingleOrDefault(x => x.Assignment == assigment.Assignment);
                            var workorder = workordersList.SingleOrDefault(x => x.WorkOrder == assigment.WorkOrder);
                            if (mongoEntity == null)
                            {
                                if (!string.IsNullOrEmpty(assigment.Employee))
                                {
                                    var employee = employeeList.SingleOrDefault(e => e.Name == assigment.Employee);
                                    assigment.EmployeeId = employee?.Employee ?? 0;

                                    var assignmentDate =
                                        assigment.ScheduleDate.Value.Add(((DateTime)assigment.StartTime).TimeOfDay);

                                    assigment.Start = assignmentDate.ToString();
                                    assigment.End =
                                        assignmentDate.AddHours(assigment.EstimatedRepairHours.AsDouble())
                                            .ToString();
                                    assigment.Color = employee?.Color ?? "";
                                    if (workorder != null)
                                    {
                                        assigment.Customer = workorder.ARCustomer;
                                        assigment.Location = workorder.Location;
                                        if (workorder.ScheduleDate != assignmentDate ||
                                            workorder.Color != assigment.Color ||
                                            workorder.EmployeeId != assigment.EmployeeId)
                                        {
                                            workorder.ScheduleDate = assignmentDate;
                                            workorder.Color = assigment.Color;
                                            workorder.EmployeeId = assigment.EmployeeId;
                                            _repository.Update(workorder);
                                        }
                                    }
                                }
                                else
                                {
                                    if (assigment.Employee == "" && workorder != null &&
                                        workorder.AssignmentId != assigment.Assignment)
                                    {
                                        workorder.AssignmentId = assigment.Assignment;
                                        _repository.Update(workorder);
                                    }
                                }
                                _repository.Add(assigment);
                            }
                            else
                            {
                                assigment.Id = mongoEntity.Id;
                                assigment.EmployeeId = mongoEntity.EmployeeId;
                                assigment.Start = mongoEntity.Start;
                                assigment.End = mongoEntity.End;
                                assigment.Color = mongoEntity.Color;
                                assigment.Customer = mongoEntity.Customer;
                                assigment.Location = mongoEntity.Location;
                                if (PublicInstancePropertiesEqual(assigment, mongoEntity))
                                    continue;
                                _repository.Update(assigment);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageAssignment {0}", ex);
                    }

                    try
                    {//Add: 3min Update: 3sec
                        var equipmentList = _repository.GetAll<SageEquipment>().ToList();
                        foreach (var entity in Equipments.Entities)
                        {
                            var mongoEntity = equipmentList.SingleOrDefault(x => x.Equipment == entity.Equipment);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEquipment {0}", ex);
                    }

                    try
                    {//Add: 3 sec
                        var problemList = _repository.GetAll<SageProblem>().ToList();
                        foreach (var entity in Problems.Entities)
                        {
                            var mongoEntity = problemList.SingleOrDefault(x => x.Problem == entity.Problem);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageProblem {0}", ex);
                    }

                    try
                    {//Add: 11 sec
                        var sageRepairList = _repository.GetAll<SageRepair>().ToList();
                        foreach (var entity in Repairs.Entities)
                        {
                            var mongoEntity = sageRepairList.SingleOrDefault(x => x.Repair == entity.Repair);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRepair {0}", ex);
                    }
                    try
                    {//Add: 1.5 min Update: 9sec
                        var workorderList = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
                        var tmpLocations = workorderList.Select(x => x.LocationNumber).ToList();
                        var locationList =
                            _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Location)).ToList();

                        _locationsCache = _repository.GetAll<SageLocationCache>().ToList();

                        var utcNow = DateTime.UtcNow;
                        foreach (var entity in locationList)
                        {
                            var mongoEntity = _locationsCache.SingleOrDefault(x => x.Location == entity.Location);
                            if (mongoEntity == null)
                            {
                                _locationService.ResolveLocation(entity);
                            }
                            else
                            {
                                var wts = (TimeSpan)(utcNow - mongoEntity.ResolvedDate);
                                if (wts.TotalDays >= 30)
                                {
                                    _locationService.ResolveLocation(entity, mongoEntity);
                                    _repository.Update(entity);
                                }
                                else
                                {
                                    if (entity.Latitude == mongoEntity.Latitude &&
                                        entity.Longitude == mongoEntity.Longitude &&
                                        entity.Address == mongoEntity.Address) continue;
                                    entity.Latitude = mongoEntity.Latitude;
                                    entity.Longitude = mongoEntity.Longitude;
                                    entity.Address = mongoEntity.Address;
                                    _repository.Update(entity);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocationCache {0}", ex);
                    }
                    try//Add:8min Update: 11sec
                    {
                        _locationsCache = _repository.GetAll<SageLocationCache>().ToList();
                        var locationsList = _repository.GetAll<SageLocation>().ToList();
                        var workorderList = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
                        foreach (var entity in Locations.Entities)
                        {
                            var mongoEntity = locationsList.SingleOrDefault(x => x.Location == entity.Location);
                            var currentCacheLocation =
                                _locationsCache.SingleOrDefault(x => x.Location == entity.Location);

                            if (mongoEntity == null)
                            {
                                var woBylocation =
                                    workorderList.Where(x => x.Location == entity.Name && x.Status == "Open");
                                var hasOpenWorkOrder = woBylocation.Any();
                                if (hasOpenWorkOrder)
                                {
                                    //var currentCacheLocation = locationsCache.SingleOrDefault(x => x.Location == entity.Location);
                                    if (currentCacheLocation != null)
                                    {
                                        entity.Longitude = currentCacheLocation.Longitude;
                                        entity.Latitude = currentCacheLocation.Latitude;
                                    }
                                }
                                foreach (var wo in woBylocation)
                                {
                                    wo.Latitude = entity.Latitude;
                                    wo.Longitude = entity.Longitude;
                                    _repository.Update(wo);
                                }
                                _repository.Add(entity);
                            }
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (currentCacheLocation != null)
                                {
                                    entity.Longitude = currentCacheLocation.Longitude;
                                    entity.Latitude = currentCacheLocation.Latitude;
                                    if (PublicInstancePropertiesEqual(entity, mongoEntity) &&
                                        mongoEntity.Longitude == currentCacheLocation.Longitude &&
                                        mongoEntity.Latitude == currentCacheLocation.Latitude)
                                        continue;
                                }
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocation {0}", ex);
                    }

                    try
                    { //Add: 4min 20sec Update: 14sec
                        var customersList = _repository.GetAll<SageCustomer>().ToList();
                        foreach (var entity in Customers.Entities)
                        {
                            var mongoEntity = customersList.SingleOrDefault(x => x.Customer == entity.Customer);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageCustomer {0}", ex);
                    }

                    try
                    {//Add 2min 40 sec Update: 4sec
                        var partsList = _repository.GetAll<SagePart>().ToList();
                        foreach (var entity in Parts.Entities)
                        {
                            var mongoEntity = partsList.SingleOrDefault(x => x.Part == entity.Part);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePart {0}", ex);
                    }
                }
            }).ToRunNow()
            .AndEvery(_settings.SynchronizationDelay)
            .Minutes();
        }
    }
}