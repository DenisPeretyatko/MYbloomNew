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
                    GetEntities(); 
                    try 
                    {
                        var newRateSheets = new List<SageRateSheet>();
                        var updateRateSheets = new List<SageRateSheet>();
                        var rateSheetList = _repository.GetAll<SageRateSheet>().ToList();
                        foreach (var entity in RateSheetArray.Entities)
                        {
                            var mongoEntity = rateSheetList.SingleOrDefault(x => x.RATESHEETNBR == entity.RATESHEETNBR);
                            if (mongoEntity == null)
                                newRateSheets.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(mongoEntity, entity, "Id"))
                                    continue;
                                updateRateSheets.Add(entity);
                            }
                        }
                        if (newRateSheets.Count > 0)
                            _repository.AddMany(newRateSheets);
                        if (updateRateSheets.Count > 0)
                            _repository.UpdateMany(updateRateSheets);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRateSheet {0}", ex);
                    }

                    try
                    { 
                        var updatePermissionCodes = new List<SagePermissionCode>();
                        var newPermissionCodes = new List<SagePermissionCode>();
                        var permissionCodeList = _repository.GetAll<SagePermissionCode>().ToList();
                        foreach (var entity in PermissionCodesArray.Entities)
                        {
                            var mongoEntity =
                                permissionCodeList.SingleOrDefault(x => x.PERMISSIONCODE == entity.PERMISSIONCODE);
                            if (mongoEntity == null)
                                newPermissionCodes.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(mongoEntity, entity, "Id"))
                                    continue;
                                updatePermissionCodes.Add(entity);
                            }
                        }
                        if (newPermissionCodes.Count > 0)
                            _repository.AddMany(newPermissionCodes);
                        if (updatePermissionCodes.Count > 0)
                            _repository.UpdateMany(updatePermissionCodes);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePermissionCode {0}", ex);
                    }

                    var newWorkOrderItems = new List<SageWorkOrderItem>();
                    var updateWorkOrderItems = new List<SageWorkOrderItem>();
                    var newWorkOrders = new List<SageWorkOrder>();
                    var updateWorkOrders = new List<SageWorkOrder>();
                    var workOrderLocationAccordance = _proxy.GetAccordance();
                    var workOrderList = _repository.GetAll<SageWorkOrder>().ToList();
                    var workorderItems = _repository.GetAll<SageWorkOrderItem>().ToList();
                    var numberOfQueries = 10;
                    var lastWo = WorkOrders.Entities.Max(x => x.WorkOrder);
                    var currentQuery = 0;
                    var querySize = lastWo / 10;
                    try 
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
                                    newWorkOrderItems.Add(entity);
                                else
                                {
                                    if (PublicInstancePropertiesEqual(mongoEntity, entity, "Id"))
                                        continue;

                                    entity.Id = mongoEntity.Id;
                                    updateWorkOrderItems.Add(entity);
                                }
                            }
                        }
                        if (newWorkOrderItems.Count > 0)
                            _repository.AddMany(newWorkOrderItems);
                        if (updateWorkOrderItems.Count > 0)
                            _repository.UpdateMany(updateWorkOrderItems);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageWorkOrderItems {0}", ex);
                    }

                    try 
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
                                newWorkOrders.Add(workOrder);
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
                                updateWorkOrders.Add(workOrder);
                            }
                        }
                        if (newWorkOrders.Count > 0)
                            _repository.AddMany(newWorkOrders);
                        if (updateWorkOrders.Count > 0)
                            _repository.UpdateMany(updateWorkOrders);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageWorkOrder {0}", ex);
                    }

                    try
                    { 
                        var updateCallTypes = new List<SageCallType>();
                        var newCallTypes = new List<SageCallType>();
                        var callTypeList = _repository.GetAll<SageCallType>().ToList();
                        foreach (var entity in CallTypeArray.Entities)
                        {
                            var mongoEntity = callTypeList.SingleOrDefault(x => x.CallType == entity.CallType);
                            if (mongoEntity == null)
                                newCallTypes.Add(entity);
                            else
                            {
                                if (PublicInstancePropertiesEqual(entity, mongoEntity, "Id"))
                                    continue;
                                entity.Id = mongoEntity.Id;
                                updateCallTypes.Add(entity);
                            }
                        }
                        if (newCallTypes.Count > 0)
                            _repository.AddMany(newCallTypes);
                        if (updateCallTypes.Count > 0)
                            _repository.UpdateMany(updateCallTypes);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync  SageCallType {0}", ex);
                    }

                    try
                    {
                        var updateEmployees = new List<SageEmployee>();
                        var newEmployees = new List<SageEmployee>();
                        var employeeList = _repository.GetAll<SageEmployee>().ToList();
                        foreach (var entity in Technicians)
                        {
                            var mongoEntity = employeeList.SingleOrDefault(x => x.Employee == entity.Employee);
                            if (mongoEntity == null)
                                newEmployees.Add(entity);
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
                                updateEmployees.Add(entity);
                            }
                        }
                        if (newEmployees.Count > 0)
                            _repository.AddMany(newEmployees);
                        if (updateEmployees.Count > 0)
                            _repository.UpdateMany(updateEmployees);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEmployee {0}", ex);
                    }

                    try
                    {
                        updateWorkOrders = new List<SageWorkOrder>();
                        var updateAssignments = new List<SageAssignment>();
                        var newAssignments = new List<SageAssignment>();
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
                                newAssignments.Add(assigment);
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
                                updateAssignments.Add(assigment);
                            }
                        }
                        if (newAssignments.Count > 0)
                            _repository.AddMany(newAssignments);
                        if (updateAssignments.Count > 0)
                            _repository.UpdateMany(updateAssignments);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageAssignment {0}", ex);
                    }

                    try
                    {
                        var updateEquipments = new List<SageEquipment>();
                        var newEquipments = new List<SageEquipment>();
                        var equipmentList = _repository.GetAll<SageEquipment>().ToList();
                        foreach (var entity in Equipments.Entities)
                        {
                            var mongoEntity = equipmentList.SingleOrDefault(x => x.Equipment == entity.Equipment);
                            if (mongoEntity == null)
                                newEquipments.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                updateEquipments.Add(entity);
                            }
                        }
                        if (newEquipments.Count > 0)
                            _repository.AddMany(newEquipments);
                        if (updateEquipments.Count > 0)
                            _repository.UpdateMany(updateEquipments);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEquipment {0}", ex);
                    }

                    try
                    {
                        var updateProblems = new List<SageProblem>();
                        var newProblems = new List<SageProblem>();
                        var problemList = _repository.GetAll<SageProblem>().ToList();
                        foreach (var entity in Problems.Entities)
                        {
                            var mongoEntity = problemList.SingleOrDefault(x => x.Problem == entity.Problem);
                            if (mongoEntity == null)
                                newProblems.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                updateProblems.Add(entity);
                            }
                        }
                        if (newProblems.Count > 0)
                            _repository.AddMany(newProblems);
                        if (updateProblems.Count > 0)
                            _repository.UpdateMany(updateProblems);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageProblem {0}", ex);
                    }

                    try
                    {
                        var updateRepairs = new List<SageRepair>();
                        var newRepairs = new List<SageRepair>();
                        var sageRepairList = _repository.GetAll<SageRepair>().ToList();
                        foreach (var entity in Repairs.Entities)
                        {
                            var mongoEntity = sageRepairList.SingleOrDefault(x => x.Repair == entity.Repair);
                            if (mongoEntity == null)
                                newRepairs.Add(entity);                           
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                updateRepairs.Add(entity);
                            }
                        }
                        if (newRepairs.Count > 0)
                            _repository.AddMany(newRepairs);
                        if (updateRepairs.Count > 0)
                            _repository.UpdateMany(updateRepairs);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRepair {0}", ex);
                    }
                    try
                    {
                        var updateLocation = new List<SageLocation>();
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
                                    updateLocation.Add(entity);
                                }
                                else
                                {
                                    if (entity.Latitude == mongoEntity.Latitude &&
                                        entity.Longitude == mongoEntity.Longitude &&
                                        entity.Address == mongoEntity.Address) continue;
                                    entity.Latitude = mongoEntity.Latitude;
                                    entity.Longitude = mongoEntity.Longitude;
                                    entity.Address = mongoEntity.Address;
                                    updateLocation.Add(entity);
                                }
                            }
                        }
                        if (updateLocation.Count > 0)
                            _repository.UpdateMany(updateLocation);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocationCache {0}", ex);
                    }
                    try
                    {
                        var updateLocations = new List<SageLocation>();
                        updateWorkOrders = new List<SageWorkOrder>();
                        var newLocations = new List<SageLocation>();
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
                                    updateWorkOrders.Add(wo);
                                }                               
                                newLocations.Add(entity);
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
                                updateLocations.Add(entity);
                            }
                        }
                        if (newLocations.Count > 0)
                            _repository.AddMany(newLocations);
                        if (updateLocations.Count > 0)
                            _repository.UpdateMany(updateLocations);
                        if (updateWorkOrders.Count > 0)
                            _repository.UpdateMany(updateWorkOrders);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocation {0}", ex);
                    }

                    try
                    { 
                        var updateCustomers = new List<SageCustomer>();
                        var newCustomers = new List<SageCustomer>();
                        var customersList = _repository.GetAll<SageCustomer>().ToList();
                        foreach (var entity in Customers.Entities)
                        {
                            var mongoEntity = customersList.SingleOrDefault(x => x.Customer == entity.Customer);
                            if (mongoEntity == null)
                                newCustomers.Add(entity);                            
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                updateCustomers.Add(entity);
                            }
                        }
                        if (newCustomers.Count > 0)
                            _repository.AddMany(newCustomers);
                        if (updateCustomers.Count > 0)
                            _repository.UpdateMany(updateCustomers);
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageCustomer {0}", ex);
                    }

                    try
                    {
                        var updateParts = new List<SagePart>();
                        var newParts = new List<SagePart>();
                        var partsList = _repository.GetAll<SagePart>().ToList();
                        foreach (var entity in Parts.Entities)
                        {
                            var mongoEntity = partsList.SingleOrDefault(x => x.Part == entity.Part);
                            if (mongoEntity == null)
                                newParts.Add(entity);                            
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                if (PublicInstancePropertiesEqual(entity, mongoEntity))
                                    continue;
                                updateParts.Add(entity);
                            }
                        }
                        if (newParts.Count > 0)
                            _repository.AddMany(newParts);
                        if (updateParts.Count > 0)
                            _repository.UpdateMany(updateParts);
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