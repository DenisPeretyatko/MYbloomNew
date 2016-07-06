var commonStateManager = function ($rootScope, commonDataService, commonHub) {
    this.profile = {};
    this.statistic = [];
    this.notifications = [];
    this.workorders = [];
    this.trucks = [];
    this.assigments = [];
    this.technicians = [];
    this.lookups = {};
    this.locations = {};
    this.notificationTime = {}
    $rootScope.notifications = [];
    $rootScope.workorders = [];
    $rootScope.unavailableTechniciansIds = [];

    var paginationModel = {
        Index: 0,
        Search: '',
        Column: '',
        Direction: true
    };

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

    var connection = commonHub.GetConnection();

    commonDataService.getLookups().then(function (response) {
        $rootScope.notifications = response.data.Notifications;
        _this.notificationTime = response.data.NotificationTime;
        return _this.lookups = response.data;
    });

    commonDataService.getLocations().then(function (response) {
        return _this.locations = response.data;
    });

    commonDataService.getWorkordesPaged(paginationModel).then(function (response) {
        return _this.workorders = response.data;
    });
    commonDataService.getTechnicians().then(function (response) {
        return _this.technicians = response.data;
    });

    commonDataService.getTrucks().then(function (response) {
        $rootScope.trucks = response.data;
        return _this.trucks = response.data;
    });

    this.UpdateWorkordersList = function (model) {
        return commonDataService.getWorkordesPaged(model).then(function (response) {
            return _this.workorders = response.data;
        });
    }

    this.getTechniciansList = function () {
        return _this.technicians;
    }
    this.getWorkordersList = function () {
        return _this.workorders;
    }
    this.getNotificationsList = function () {
        return _this.notifications;
    }
    this.getNotificationTime = function () {
        return _this.notificationTime;
    }

    this.setNotificationTime = function () {
       return commonDataService.updateNotificationTime().then(function (response) {
            return _this.notificationTime = response.data;
        });
        //now = new Date();
        //_this.notificationTime = new Date(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(), now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds())

    }


    this.setMongaNotificationTime = function(value)
    {
        _this.notificationTime = value;
    }

    this.setLookups = function(value) {
        this.lookups = value;
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

}
commonStateManager.$inject = ["$rootScope", "commonDataService", "commonHub"];