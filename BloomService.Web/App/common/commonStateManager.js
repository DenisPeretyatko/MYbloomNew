var commonStateManager = function (commonDataService, commonHub) {
        this.profile = {};
        this.statistic = [];
        this.notifications = [];
        this.workorders = [];
        this.trucks = [];
        this.assigments = [];
        this.technicians = [];
        this.lookups = {};
        this.locations = {};

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
            locations: this.locations
        }

         var connection = commonHub.GetConnection();
        
    commonDataService.getLookups().then(function (response) {
        return _this.lookups = response.data;
    });

    commonDataService.getLocations().then(function (response) {
        return _this.locations = response.data;
    });

    commonDataService.getNotifications().then(function (response) {
        return _this.notifications = response.data;
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

    this.getTechniciansList = function() {
        return _this.technicians;
    }
    this.getWorkordersList = function () {
        return _this.workorders;
    }

    connection.client.UpdateWorkOrder = function (workorder) { 
        alert("WorkOrder SignalR");
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
    };
      $.connection.hub.start().done(function () { });

}
commonStateManager.$inject = ["commonDataService", "commonHub"];