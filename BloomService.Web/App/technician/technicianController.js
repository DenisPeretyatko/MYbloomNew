/**
 * techniciansController - controller
 */

var technicianController = function ($scope, state) {

    $scope.$watch(function () { return state.technicians; }, function () {
        $scope.technicians = state.technicians;
    });
    $scope.sortType = '';
    $scope.sortDirection = true;
    $scope.sort = function (data) {
        if ($scope.sortKey != data) {
            $scope.sortKey = data;
            $scope.reverse = true;
        } else {
            $scope.reverse = !$scope.reverse;
        }
    }
    $scope.sort('Employee');
};
technicianController.$inject = ["$scope", "state"];