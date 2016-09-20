using BloomService.Domain.Entities.Concrete;
using System;
using System.Linq;
using BloomService.Domain.Extensions;
using MongoDB.Driver.Builders;

namespace BloomService.Web.Infrastructure.Jobs
{
    public partial class BloomJobRegistry
    {
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
                        var rateSheetList = _repository.GetAll<SageRateSheet>().ToList();
                        foreach (var entity in rateSheetArray.Entities)
                        {
                            var mongoEntity = rateSheetList.SingleOrDefault(x => x.RATESHEETNBR == entity.RATESHEETNBR);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRateSheet {0}", ex);
                    }

                    try
                    {
                        var permissionCodeList = _repository.GetAll<SagePermissionCode>().ToList();
                        foreach (var entity in permissionCodesArray.Entities)
                        {
                            var mongoEntity =
                                permissionCodeList.SingleOrDefault(x => x.PERMISSIONCODE == entity.PERMISSIONCODE);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePermissionCode {0}", ex);
                    }

                    try
                    {
                        var workOrderLocationAccordance = _proxy.GetAccordance();
                        var workOrderList = _repository.GetAll<SageWorkOrder>().ToList();
                        foreach (var workOrder in workOrders.Entities)
                        {
                            var mongoEntity = workOrderList.SingleOrDefault(x => x.WorkOrder == workOrder.WorkOrder);

                            if (mongoEntity == null)
                            {
                                var result = _proxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                                if (result != null)
                                {
                                    workOrder.WorkOrderItems = result.Entities;
                                }
                                //var result = workOrderItems.Entities.Where(x => x.WorkOrder == workOrder.WorkOrder).ToList();
                                //if (result.Count != 0)
                                //{
                                //    workOrder.WorkOrderItems = result;
                                //}

                                workOrder.LocationNumber =
                                    workOrderLocationAccordance.Entities.Single(x => x.WRKORDNBR == workOrder.WorkOrder)
                                        .SERVSITENBR;
                                _repository.Add(workOrder);
                            }
                            else
                            {
                                var woiResult = _proxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                                if (woiResult != null)
                                {
                                    workOrder.WorkOrderItems = woiResult.Entities;
                                }

                                //var woiResult = workOrderItems.Entities.Where(x => x.WorkOrder == workOrder.WorkOrder).ToList();
                                //if (woiResult.Count != 0)
                                //{
                                //    workOrder.WorkOrderItems = woiResult;
                                //}

                                var notesResult =
                                    workOrderNotes.Entities.Where(x => x.TRANSNBR == workOrder.WorkOrder).ToList();
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
                                    workOrderLocationAccordance.Entities.Single(x => x.WRKORDNBR == workOrder.WorkOrder)
                                        .SERVSITENBR;
                                _repository.Update(workOrder);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageWorkOrder {0}", ex);
                    }

                    try
                    {
                        var callTypeList = _repository.GetAll<SageCallType>().ToList();
                        foreach (var entity in callTypeArray.Entities)
                        {
                            var mongoEntity = callTypeList.SingleOrDefault(x => x.CallType == entity.CallType);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
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
                        foreach (var entity in emploees)
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
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEmployee {0}", ex);
                    }

                    try
                    {
                        var workordersList = _repository.GetAll<SageWorkOrder>().ToList();
                        var assigmentsList = _repository.GetAll<SageAssignment>().ToList();

                        var employeeList = _repository.GetAll<SageEmployee>().ToList();

                        foreach (var assigment in assignments.Entities)
                        {
                            var mongoEntity = assigmentsList.SingleOrDefault(x => x.Assignment == assigment.Assignment);
                            var workorder = workordersList.SingleOrDefault(x => x.WorkOrder == assigment.WorkOrder);
                            if (mongoEntity == null)
                            {
                                if (!string.IsNullOrEmpty(assigment.Employee))
                                {
                                    var employee = employeeList.SingleOrDefault(e => e.Name == assigment.Employee);
                                    assigment.EmployeeId = employee != null ? employee.Employee : 0;

                                    var assignmentDate =
                                        assigment.ScheduleDate.Value.Add(((DateTime)assigment.StartTime).TimeOfDay);

                                    assigment.Start = assignmentDate.ToString();
                                    assigment.End =
                                        assignmentDate.AddHours(assigment.EstimatedRepairHours.AsDouble()).ToString();
                                    assigment.Color = employee?.Color ?? "";
                                    if (workorder != null)
                                    {
                                        assigment.Customer = workorder.ARCustomer;
                                        assigment.Location = workorder.Location;
                                        workorder.ScheduleDate = assignmentDate;
                                        workorder.Color = assigment.Color;
                                        workorder.EmployeeId = assigment.EmployeeId;
                                        _repository.Update(workorder);
                                    }
                                }
                                else
                                {
                                    if (assigment.Employee == "" && workorder != null)
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
                                _repository.Update(assigment);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageAssignment {0}", ex);
                    }

                    try
                    {
                        var equipmentList = _repository.GetAll<SageEquipment>().ToList();
                        foreach (var entity in equipments.Entities)
                        {
                            var mongoEntity = equipmentList.SingleOrDefault(x => x.Equipment == entity.Equipment);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageEquipment {0}", ex);
                    }

                    try
                    {
                        var problemList = _repository.GetAll<SageProblem>().ToList();
                        foreach (var entity in problems.Entities)
                        {
                            var mongoEntity = problemList.SingleOrDefault(x => x.Problem == entity.Problem);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageProblem {0}", ex);
                    }

                    try
                    {
                        var sageRepairList = _repository.GetAll<SageRepair>().ToList();
                        foreach (var entity in repairs.Entities)
                        {
                            var mongoEntity = sageRepairList.SingleOrDefault(x => x.Repair == entity.Repair);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageRepair {0}", ex);
                    }
                    try
                    {
                        var workorderList = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
                        var tmpLocations = workorderList.Select(x => x.LocationNumber).ToList();
                        var locationList = _repository.SearchFor<SageLocation>(x => tmpLocations.Contains(x.Location)).ToList();

                        locationsCache = _repository.GetAll<SageLocationCache>().ToList();

                        var utcNow = DateTime.UtcNow;
                        foreach (var entity in locationList)
                        {
                            var mongoEntity = locationsCache.SingleOrDefault(x => x.Location == entity.Location);
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
                                }
                                else
                                {
                                    entity.Latitude = mongoEntity.Latitude;
                                    entity.Longitude = mongoEntity.Longitude;
                                    entity.Address = mongoEntity.Address;
                                }
                                _repository.Update(entity);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocationCache {0}", ex);
                    }
                    try
                    {
                        locationsCache = _repository.GetAll<SageLocationCache>().ToList();
                        var locationsList = _repository.GetAll<SageLocation>().ToList();
                        var workorderList = _repository.SearchFor<SageWorkOrder>(x => x.Status == "Open").ToList();
                        foreach (var entity in locations.Entities)
                        {
                            var mongoEntity = locationsList.SingleOrDefault(x => x.Location == entity.Location);
                            var currentCacheLocation = locationsCache.SingleOrDefault(x => x.Location == entity.Location);

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
                                }
                                //entity.Latitude = mongoEntity.Latitude;
                                //entity.Longitude = mongoEntity.Longitude;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageLocation {0}", ex);
                    }

                    try
                    {
                        var customersList = _repository.GetAll<SageCustomer>().ToList();
                        foreach (var entity in customers.Entities)
                        {
                            var mongoEntity = customersList.SingleOrDefault(x => x.Customer == entity.Customer);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SageCustomer {0}", ex);
                    }

                    try
                    {
                        var partsList = _repository.GetAll<SagePart>().ToList();
                        foreach (var entity in parts.Entities)
                        {
                            var mongoEntity = partsList.SingleOrDefault(x => x.Part == entity.Part);
                            if (mongoEntity == null)
                                _repository.Add(entity);
                            else
                            {
                                entity.Id = mongoEntity.Id;
                                _repository.Update(entity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.ErrorFormat("Can`t sync SagePart {0}", ex);
                    }
                }
            }).ToRunNow().AndEvery(_settings.SynchronizationDelay).Minutes();
        }
    }
}