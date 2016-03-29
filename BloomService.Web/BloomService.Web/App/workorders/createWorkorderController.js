/**
 * createWorkorderController - controller
 */

var createWorkorderController = function($scope, $stateParams, state, commonDataService) {

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

    $scope.createWorkOrder = function() {
    	console.log("createWorkOrder");
    };

};
createWorkorderController.$inject = ["$scope", "$stateParams", "state", "commonDataService"];