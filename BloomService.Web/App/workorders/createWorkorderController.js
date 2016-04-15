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
	$scope.equipment = '';
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

    $scope.createWorkOrder = function () {
        var workorder = {
            customer: $scope.lookups.customers.selected,
            location: $scope.lookups.locations.selected,
            calltype: $scope.lookups.calltypes.selected,
            calldate: $scope.calldate,
            problem: $scope.lookups.problems.selected,
            ratesheet: $scope.lookups.ratesheets.selected,
            emploee: $scope.lookups.employes.selected,
            equipment: $scope.lookups.equipment.selected,
            estimatehours: $scope.lookups.hours.selected,
            nottoexceed: $scope.nottoexceed,
            locationcomments: $scope.locationcomments,
            customerpo: $scope.customerpo,
            permissiocode: $scope.permissiocode,
            paymentmethods: $scope.lookups.paymentMethods.selected
        };
        commonDataService.createWorkorder(workorder).then(function () {
            console.log("createWorkOrder");
        });
    };

};
createWorkorderController.$inject = ["$scope", "$stateParams", "state", "commonDataService"];