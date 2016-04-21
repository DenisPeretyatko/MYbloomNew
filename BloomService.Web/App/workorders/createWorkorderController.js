/**
 * createWorkorderController - controller
 */

var createWorkorderController = function ($scope, $stateParams, $state, state, commonDataService) {
    $scope.obj = {}
    $scope.customer = '';
    $scope.location = '';
	$scope.calltype = '';
	$scope.obj.calldate = '';
	$scope.problem = '';
	$scope.ratesheet = '';
	$scope.emploee = '';
	$scope.equipment = '';
	$scope.estimatehours = '';
	$scope.obj.nottoexceed = '';
	$scope.obj.locationcomments = '';
	$scope.obj.customerpo = '';
	$scope.obj.permissiocode = '';
	$scope.paymentmethods = '';
    $scope.lookups = state.lookups;

    $scope.$watch(function () { return state.lookups; }, function () {
        $scope.lookups = state.lookups;
    });

    $scope.createWorkOrder = function () {
        var equipment;
        if ($scope.lookups.Equipment.selected == null) {
            equipment = "0";
        } else {
            equipment = $scope.lookups.Equipment.selected.Equipment;
        }
        var workorder = {
            Customer: $scope.lookups.Customers.selected.Customer,
            Location: $scope.lookups.Locations.selected.Location,
            Calltype: $scope.lookups.Calltypes.selected.CallType,
            Calldate: $scope.obj.calldate,
            Problem: $scope.lookups.Problems.selected.Problem,
            Ratesheet: $scope.lookups.RateSheets.selected,
            Emploee: $scope.lookups.Employes.selected.Employee,
            Equipment: equipment,
            Estimatehours: $scope.lookups.Hours.selected.Repair,
            Nottoexceed: $scope.obj.nottoexceed,
            Locationcomments: $scope.obj.locationcomments,
            Customerpo: $scope.obj.customerpo,
            Permissiocode: $scope.obj.permissiocode,
            Paymentmethods: $scope.lookups.PaymentMethods.selected
        };
        
        commonDataService.createWorkorder(workorder).then(function (response) {
            if (response.data == 'success')
                $state.go('manager.workorder.list');
        });
    };

};
createWorkorderController.$inject = ["$scope", "$stateParams", "$state", "state", "commonDataService"];