/**
 * techniciansController - controller
 */

var technicianController = function ($scope, state) {
    $scope.technicians = state.technicians;
    $scope.sortType = '';
    $scope.sortDirection = true;
      $scope.changeSorting = function (data) {
         if ($scope.sortType != data) {
             $scope.sortType = data;
             $scope.sortDirection = true;
        } else {
             $scope.sortDirection = !$scope.sortDirection;
         }
     }
};
technicianController.$inject = ["$scope", "state"];