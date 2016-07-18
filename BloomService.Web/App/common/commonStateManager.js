﻿var commonStateManager = function ($rootScope, commonDataService, commonHub) {
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
        notificationTime: this.notificationTime
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

    $rootScope.notifications = [];
    $rootScope.workorders = [];
    $rootScope.unavailableTechniciansIds = [];

  

    var connection = commonHub.GetConnection();

    if (window.localStorage.getItem('Token') != null && window.localStorage.getItem('Token') != "") {
        var paginationModel = {
            Index: 0,
            Search: '',
            Column: '',
            Direction: true
        };
        if (_this.notificationTime == undefined || _this.lookups == undefined || _this.notifications == undefined) {
            commonDataService.getLookups().then(function(response) {
                $rootScope.notifications = response.data.Notifications;
                _this.notifications = response.data.Notifications;
                _this.notificationTime = response.data.NotificationTime;
                return _this.lookups = response.data;
            });
        }
        if (_this.locations == undefined) {
            commonDataService.getLocations().then(function(response) {
                return _this.locations = response.data;
            });
        }
        if (_this.workorders == undefined) {
            commonDataService.getWorkordesPaged(paginationModel).then(function(response) {
                return _this.workorders = response.data;
            });
        }
        if (_this.technicians == undefined) {
            commonDataService.getTechnicians().then(function(response) {
                return _this.technicians = response.data;
            });
        }
        if (_this.trucks == undefined) {
            commonDataService.getTrucks().then(function(response) {
                $rootScope.trucks = response.data;
                return _this.trucks = response.data;
            });
        }
    }

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
        angular.forEach(_this.technicians, function (value, key) {
            if (value.Employee === technician.Id) {
                commonDataService.getTechnician(value.Id).then(function(response) {
                    delete _this.technicians[key];
                    _this.technicians[key] = response.data;
                });
            } 
        });
        if ($rootScope.unavailableTechniciansIds.includes(technician.Id) && technician.IsAvailable) {
            $rootScope.unavailableTechniciansIds.splice($rootScope.unavailableTechniciansIds.indexOf(technician.Id), 1);
        }
        else if ($rootScope.unavailableTechniciansIds.includes(technician.Id) === false && technician.IsAvailable === false) {
            $rootScope.unavailableTechniciansIds.unshift(technician.Id);
        }

    };

    connection.client.updateTechnicianLocation = function (technician) {
        angular.forEach($rootScope.trucks, function (value, key) {
            if (value.Employee === technician.Employee) {
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
        $rootScope.workorders.unshift(model);
        $rootScope.$digest();
    };

    connection.client.DeleteAssigment = function (model) {
        angular.forEach($rootScope.workorders, function(value, key) {
            if (value.WorkOrder.WorkOrder == model.WorkOrder) {
                $rootScope.workorders.splice(key, 1);
            }
        });
        $rootScope.$digest();
    
    };

    $.connection.hub.start().done(function () { });
    return _this;
}
commonStateManager.$inject = ["$rootScope", "commonDataService", "commonHub"];