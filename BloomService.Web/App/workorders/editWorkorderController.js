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
        $scope.lookups.Customers.selected = $scope.editableWorkOrder.ARCustomer;
        $scope.lookups.Locations.selected = $scope.editableWorkOrder.Location;
        $scope.calltype = $scope.editableWorkOrder.CallType;
        $scope.calldate = $scope.editableWorkOrder.CallDate;
        $scope.lookups.Problems.selected = $scope.editableWorkOrder.Problem;
        $scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheet;
        $scope.lookups.Employes.selected = $scope.editableWorkOrder.Employee;
        $scope.lookups.Equipment.selected = $scope.editableWorkOrder.Equipment;
        //$scope.lookups.Hours.selected = $scope.editableWorkOrder.Contact;
        $scope.nottoexceed = $scope.editableWorkOrder.NottoExceed;
        $scope.locationcomments = $scope.editableWorkOrder.Area;
        $scope.customerpo = $scope.editableWorkOrder.CustomerPO;
        $scope.permissiocode = $scope.editableWorkOrder.PermissionCode;
        $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PayMethod;
    });

	this.saveWorkOrder = function() {
		console.log("saveWorkOrder");
	};

	commonDataService.getWorkorder($stateParams.id).then(function (response) {
	    return $scope.editableWorkOrder = response.data;
	});

 

    //$scope.locations = ["1",  "2", "3", "4"];


};
editWorkorderController.$inject = ["$scope", "$stateParams", "commonDataService", "state"];