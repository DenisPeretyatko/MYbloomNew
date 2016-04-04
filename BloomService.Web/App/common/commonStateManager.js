var commonStateManager = function (commonDataService) {
    this.profile = {};
    this.statistic = [];
    this.notifications = [];
    this.workorders = [];
    this.trucks = [];
    this.assigments = [];
    this.technicians = [];
    this.lookups = {};
}
commonStateManager.$inject = ["commonDataService"];