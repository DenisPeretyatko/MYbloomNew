var commonStateManager = function ($rootScope, commonDataService, commonHub, $q, $state) {
    var _this = {
        profile: this.profile,
        statistic: this.statistic,
        notifications: this.notifications,
        workorders: this.workorders,
        trucks: this.trucks,
        assigments: this.assigments,
        technicians: this.technicians,
        lookups: this.lookups,
        locations: this.locations,
        notificationTime: this.notificationTime,
        alreadyLoaded: this.alreadyLoaded
    }

    this.profile = _this.profile;
    this.statistic = _this.statistic;
    this.notifications = _this.notifications;
    this.workorders = _this.workorders;
    this.trucks = _this.trucks;
    this.assigments = _this.assigments;
    this.technicians = _this.technicians;
    this.lookups = _this.lookups;
    this.locations = _this.locations;
    this.notificationTime = _this.notificationTime;
    this.alreadyLoaded = _this.alreadyLoaded;
    _this.alreadyLoaded = false;

    $rootScope.notifications = [];
    $rootScope.workorders = [];
    $rootScope.unavailableTechniciansIds = [];
    $rootScope.AddedImage = [];
    $rootScope.updatedSageWorkOrder = [];
    $rootScope.updatedTechnican = [];

    var model = {
        Index: 0,
        Search: '',
        Column: '',
        Direction: false
    };


    var connection = commonHub.GetConnection();

    if (window.localStorage.getItem('Token') != null && window.localStorage.getItem('Token') != "") {
        $state.go('manager.dashboard');
        $q.all([commonDataService.getLookups(), commonDataService.getLocations(), commonDataService.getTechnicians(), commonDataService.getTrucks(), commonDataService.getWorkordesPaged(model)]).then(function (values) {
            $rootScope.notifications = values[0].data.Notifications;
            _this.notifications = values[0].data.Notifications;
            _this.notificationTime = values[0].data.NotificationTime;
            _this.lookups = values[0].data;
            _this.locations = values[1].data;
            $rootScope.workorders = values[1].data;
            _this.technicians = values[2].data;
            $rootScope.trucks = values[3].data;
            _this.trucks = values[3].data;
            _this.workorders = values[4].data;
            _this.alreadyLoaded = true;
        });
        _this.alreadyLoaded = true;
    }

    connection.client.UpdateSageWorkOrder = function (model) {
        $rootScope.updatedSageWorkOrder = model;
        $rootScope.$digest();
    };

    connection.client.UpdateWorkOrderPicture = function (image) {
        $rootScope.addedImage = image;
        $rootScope.$digest();
    };

    connection.client.UpdateWorkOrder = function (workorder) {
        angular.forEach(_this.workorders, function (value, key) {
            if (value.WorkOrder === workorder.WorkOrder) {
                commonDataService.getWorkorder(value.Id).then(function (response) {
                    delete _this.workorders[key];
                    _this.workorders[key] = response.data;
                });
            }
        });
    };

    connection.client.UpdateTechnician = function (technician) {
        $rootScope.updatedTechnican = technician;
        angular.forEach(_this.technicians, function (value, key) {
            if (value.Employee == technician.Id) {
                commonDataService.getTechnician(value.Id).then(function (response) {
                    delete _this.technicians[key];
                    _this.technicians[key] = response.data;
                    if (technician.IsAvailable) {
                        var isExist = false;
                        angular.forEach(_this.lookups.Employes, function (value, key) {
                            if (value.Employee == technician.Id) {
                                isExist = true;
                                delete _this.lookups.Employes[key];
                                _this.lookups.Employes[key] = response.data;
                            }
                        });
                        if (!isExist) {
                            _this.lookups.Employes.push(response.data);
                        }
                    } else {
                        angular.forEach(_this.lookups.Employes, function (value, key) {
                            if (value.Employee == technician.Id) {
                                _this.lookups.Employes.splice(key, 1);
                            }
                        });
                    }
                });
            }
        });
        angular.forEach($rootScope.workorders, function (value, key) {
            if (value.Employee == technician.Id) {
                $rootScope.workorders[key].Color = technician.Color;
            }
        });
        if ($rootScope.unavailableTechniciansIds.includes(technician.Id) && technician.IsAvailable) {
            $rootScope.unavailableTechniciansIds.splice($rootScope.unavailableTechniciansIds.indexOf(technician.Id), 1);
        }
        else if ($rootScope.unavailableTechniciansIds.includes(technician.Id) === false && technician.IsAvailable === false) {
            $rootScope.unavailableTechniciansIds.unshift(technician.Id);
        }

        $rootScope.$digest();
    };

    connection.client.updateTechnicianLocation = function (technician) {
        angular.forEach($rootScope.trucks, function (value, key) {
            if (value.Employee == technician.Employee) {
                delete $rootScope.trucks[key];
                $rootScope.trucks[key] = technician;
            }
        });
    };

    connection.client.SendNotification = function (notification) {
        $rootScope.notifications.unshift(notification);
        $rootScope.notifications = $rootScope.notifications.slice(0, 9);

    };


    connection.client.CreateAssignment = function (model) {
        var isExist = false;
        angular.forEach(_this.locations, function (value, key) {
            if (value.WorkOrder.WorkOrder == model.WorkOrder.WorkOrder && value.Employee == model.Employee) {
                isExist = true;
                _this.locations[key] = value;
            }
        });
        if (isExist) {
            angular.forEach($rootScope.workorders, function (value, key) {
                if (value.WorkOrder.WorkOrder == model.WorkOrder.WorkOrder && value.Employee == model.Employee) {
                    $rootScope.workorders[key] = model;
                }
            });
        } else {
            $rootScope.workorders.unshift(model);
        }
        $rootScope.$broadcast('addedAssignment', {
  item: model 
});
        $rootScope.$digest();
    };

    connection.client.DeleteAssigment = function (model) {
        angular.forEach(_this.locations, function (value, key) {
            if (value.WorkOrder.WorkOrder == model.WorkOrder) {
                _this.locations.splice(key, 1);
            }
        });
        angular.forEach($rootScope.workorders, function (value, key) {
            if (value.WorkOrder.WorkOrder == model.WorkOrder) {
                $rootScope.workorders.splice(key, 1);
            }
        });
        $rootScope.$broadcast('deletedAssignment', {
            item: model
        });
        $rootScope.$digest();
    };
    connection.client.ShowAlert = function (model) {
        swal({
            title: model.Title,
            text: model.Message,
            type: model.Type,
            confirmButtonColor: '#df4242'
        }
        );
    }

    $.connection.hub.start().done(function () { });
    return _this;
}
commonStateManager.$inject = ["$rootScope", "commonDataService", "commonHub", "$q", "$state"];