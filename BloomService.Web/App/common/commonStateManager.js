var commonStateManager = function (commonDataService) {
        this.profile = {};
        this.statistic = [];
        this.notifications = [];
        this.workorders = [];
        this.trucks = [];
        this.assigments = [];
        this.technicians = [];
        this.lookups = {};
    
        var _this = {
            profile: this.profile,
            statistic: this.statistic,
            notifications: this.notifications,
            workorders: this.workorders,
            trucks: this.trucks,
            assigments: this.assigments,
            technicians: this.technicians,
            lookups: this.lookups,
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

    return _this;
}
commonStateManager.$inject = ["commonDataService"];