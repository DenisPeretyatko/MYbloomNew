/**
 * editWorkorderController - controller
 */

var editWorkorderController = function ($scope, $stateParams, commonDataService, state) {

    $scope.customer = '';
    $scope.location = '';
    $scope.calltype = '';
    $scope.calldate = '';
    $scope.problem = '';
    $scope.ratesheet = '';
    $scope.emploee = '';
    $scope.equiment = '';
    $scope.estimatehours = '';
    $scope.nottoexceed = '';
    $scope.locationcomments = '';
    $scope.customerpo = '';
    $scope.permissiocode = '';
    $scope.paymentmethods = '';
    $scope.lookups = state.lookups;

    $scope.$watch(function () { return state.lookups; }, function () {
        $scope.lookups = state.lookups;
    });

	this.saveWorkOrder = function() {
		console.log("saveWorkOrder");
    };

    //$scope.locations = ["1",  "2", "3", "4"];


};
editWorkorderController.$inject = ["$scope", "$stateParams", "commonDataService", "state"];