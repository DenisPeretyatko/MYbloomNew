using BloomService.Domain.Entities.Concrete;
using System;
using System.Linq;
using BloomService.Domain.Extensions;

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
                        foreach (var entity in rateSheetArray.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageRateSheet>(x => x.RATESHEETNBR == entity.RATESHEETNBR).SingleOrDefault();
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
                        foreach (var entity in permissionCodesArray.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SagePermissionCode>(x => x.PERMISSIONCODE == entity.PERMISSIONCODE).SingleOrDefault();
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
                        foreach (var workOrder in workOrders.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == workOrder.WorkOrder).SingleOrDefault();
                            if (mongoEntity == null)
                            {
                                var result = _proxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                                if (result != null)
                                {
                                    workOrder.WorkOrderItems = result.Entities;

                                }
                                _repository.Add(workOrder);
                            }
                            else
                            {

                                var woiResult = _proxy.GetWorkorderItemsByWorkOrderId(workOrder.WorkOrder);
                                if (woiResult != null)
                                {
                                    workOrder.WorkOrderItems = woiResult.Entities;
                                }

                                var notesResult = _proxy.GetNotes(workOrder.WorkOrder);
                                if (notesResult != null)
                                {
                                    workOrder.WorkNotes = notesResult.Entities;
                                }

                                workOrder.Id = mongoEntity.Id;
                                workOrder.Status = mongoEntity.Status;
                                workOrder.AssignmentId = mongoEntity.AssignmentId;
                                workOrder.Latitude = mongoEntity.Latitude;
                                workOrder.Longitude = mongoEntity.Longitude;

                                workOrder.ScheduleDate = mongoEntity.ScheduleDate;
                                workOrder.Color = mongoEntity.Color;
                                workOrder.EmployeeId = mongoEntity.EmployeeId;
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
                        foreach (var entity in callTypeArray.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageCallType>(x => x.CallType == entity.CallType).SingleOrDefault();
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
                        foreach (var entity in emploees)
                        {
                            var mongoEntity = _repository.SearchFor<SageEmployee>(x => x.Employee == entity.Employee).SingleOrDefault();
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
                        foreach (var assigment in assignments.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageAssignment>(x => x.Assignment == assigment.Assignment).SingleOrDefault();
                            var workorder = _repository.SearchFor<SageWorkOrder>(x => x.WorkOrder == assigment.WorkOrder).SingleOrDefault();
                            if (mongoEntity == null)
                            {
                                if (!string.IsNullOrEmpty(assigment.Employee))
                                {
                                    var employee = _repository.SearchFor<SageEmployee>(e => e.Name == assigment.Employee).SingleOrDefault();
                                    assigment.EmployeeId = employee != null ? employee.Employee : 0;

                                    var assignmentDate = assigment.ScheduleDate.Value.Add(((DateTime)assigment.StartTime).TimeOfDay);

                                    assigment.Start = assignmentDate.ToString();
                                    assigment.End = assignmentDate.AddHours(assigment.EstimatedRepairHours.AsDouble()).ToString();
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
                        foreach (var entity in equipments.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageEquipment>(x => x.Equipment == entity.Equipment).SingleOrDefault();
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
                        foreach (var entity in problems.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageProblem>(x => x.Problem == entity.Problem).SingleOrDefault();
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
                        foreach (var entity in repairs.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageRepair>(x => x.Repair == entity.Repair).SingleOrDefault();
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
                        foreach (var entity in locations.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageLocation>(x => x.Location == entity.Location).SingleOrDefault();

                            if (mongoEntity == null)
                            {
                                var woBylocation = _repository.SearchFor<SageWorkOrder>(x => x.Location == entity.Name && x.Status == "Open");
                                var hasOpenWorkOrder = woBylocation.Any();
                                if (hasOpenWorkOrder)
                                    _locationService.ResolveLocation(entity);
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
                                entity.Latitude = mongoEntity.Latitude;
                                entity.Longitude = mongoEntity.Longitude;
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
                        foreach (var entity in customers.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SageCustomer>(x => x.Customer == entity.Customer).SingleOrDefault();
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
                        foreach (var entity in parts.Entities)
                        {
                            var mongoEntity = _repository.SearchFor<SagePart>(x => x.Part == entity.Part).SingleOrDefault();
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