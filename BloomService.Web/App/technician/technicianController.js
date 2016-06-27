/**
 * techniciansController - controller
 */

var technicianController = function ($scope, state) {
    $scope.technicians = state.getTechniciansList();
};
technicianController.$inject = ["$scope", "state"];