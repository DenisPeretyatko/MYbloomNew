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
            customer: $scope.lookups.Customers.selected,
            location: $scope.lookups.Locations.selected,
            calltype: $scope.lookups.Calltypes.selected,
            calldate: $scope.calldate,
            problem: $scope.lookups.Problems.selected,
            ratesheet: $scope.lookups.Ratesheets.selected,
            emploee: $scope.lookups.Employes.selected,
            equipment: $scope.lookups.Equipment.selected,
            estimatehours: $scope.lookups.Hours.selected,
            nottoexceed: $scope.nottoexceed,
            locationcomments: $scope.locationcomments,
            customerpo: $scope.customerpo,
            permissiocode: $scope.permissiocode,
            paymentmethods: $scope.lookups.PaymentMethods.selected
        };
        commonDataService.createWorkorder(workorder).then(function () {
            console.log("createWorkOrder");
        });
    };

};
createWorkorderController.$inject = ["$scope", "$stateParams", "state", "commonDataService"];