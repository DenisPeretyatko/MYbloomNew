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

    return _this;
}
commonStateManager.$inject = ["commonDataService"];