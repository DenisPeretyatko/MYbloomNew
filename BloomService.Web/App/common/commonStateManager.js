var commonStateManager = function (commonDataService) {
        this.profile = {};
        this.statistic = [];
        this.notifications = [];
        this.workorders = [];
        this.trucks = [];
        this.assigments = [];
        this.technicians = [];
        this.lookups = {};
        this.locations = {};

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

    commonDataService.getLookups().then(function (response) {
        return _this.lookups = response.data;
    });

    commonDataService.getLocations().then(function (response) {
        return _this.locations = response.data;
    });

    commonDataService.getNotifications().then(function (response) {
        return _this.notifications = response.data;
    });
    //------------- Test SignalR for notifications -------------------
    //setInterval(changeNotifications, 3000);
    //function changeNotifications() {
    //    var notification = $.connection.notificationsHub;

        //notification.client.updateNotifications = function (notif) {
        //    _this.notifications = notif;
        //};

        //$.connection.hub.start().done(function () {
        //    notification.server.getNotifications(_this.notifications);
        //});
    //}
    //------------- Test SignalR for locations -------------------
    //setInterval(changeLocations, 1000);
    //function changeLocations() {
    //    var location = $.connection.locationHub;

        //location.client.updateLocations = function (locations) {
        //    _this.locations = locations;
        //};

        //$.connection.hub.start().done(function () {
        //    location.server.getLocations();
        //});
    //}

    var connection = $.connection.bloomServiceHub;
    connection.client.createAssignment = function (model) {
        _this.assigments.push(model);
    };

    return _this;
}
commonStateManager.$inject = ["commonDataService"];