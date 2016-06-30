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
        d = new Date();
        datetext = d.toTimeString();
        datetext = datetext.split(' ')[0]; //time
        var curr_date = d.getDate();
        var curr_month = d.getMonth() + 1;
        var curr_year = d.getFullYear();
        _this.notificationTime = curr_year + "-" + curr_month + "-" + curr_date + " " + datetext;

    }

    this.setMongaNotificationTime = function(value)
    {
        debugger;
        _this.notificationTime = value;
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
                commonDataService.getTechnician(value.Id).then(function (response) {
                    delete _this.technicians[key];
                    _this.technicians[key] = response.data;
                });
            }
        });
    };

    connection.client.SendNotification = function (notification) {
        $rootScope.notifications.unshift(notification);
    };

    connection.client.DeleteAssigment = function (model) {
        
    };

    $.connection.hub.start().done(function () { });

}
commonStateManager.$inject = ["$rootScope", "commonDataService", "commonHub"];