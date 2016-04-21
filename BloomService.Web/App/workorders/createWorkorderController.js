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
            Customer: $scope.lookups.Customers.selected.Name,
            Location: $scope.lookups.Locations.selected.Name,
            Calltype: $scope.lookups.Calltypes.selected.Description,
            Calldate: $scope.calldate,
            Problem: $scope.lookups.Problems.selected.Description,
            Ratesheet: $scope.lookups.RateSheets.selected,
            Emploee: $scope.lookups.Employes.selected.Name,
            Equipment: $scope.lookups.Equipment.selected.EquipmentType,
            Estimatehours: $scope.lookups.Hours.selected,
            Nottoexceed: $scope.nottoexceed,
            Locationcomments: $scope.locationcomments,
            Customerpo: $scope.customerpo,
            Permissiocode: $scope.permissiocode,
            Paymentmethods: $scope.lookups.PaymentMethods.selected
        };
        commonDataService.createWorkorder(workorder).then(function () {
            console.log("createWorkOrder");
        });
    };

};
createWorkorderController.$inject = ["$scope", "$stateParams", "state", "commonDataService"];