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

    setInterval(setLookups, 30000);
    function setLookups() {
        return commonDataService.getLookups().then(function(response) {
            return _this.lookups = response.data;
        });
    }

    commonDataService.getLocations().then(function (response) {
        return _this.locations = response.data;
        });
    
    setInterval(changeLocations, 1000);
    function changeLocations() {
        var location = $.connection.locationHub;

        location.client.updateLocations = function (locations) {
            _this.locations = locations;
        };

        $.connection.hub.start().done(function () {
            location.server.getLocations();
        });
    }

    return _this;
}
commonStateManager.$inject = ["commonDataService"];