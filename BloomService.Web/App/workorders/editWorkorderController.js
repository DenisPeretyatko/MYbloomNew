/**
 * editWorkorderController - controller
 */

var editWorkorderController = function ($scope, $stateParams, $state, commonDataService, state) {

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
        var workorder = $scope.editableWorkOrder;
        $scope.lookups.Customers.selected = $scope.lookups.Customers.find(function (element) {
            return element.Name === workorder.ARCustomer;
        });
        $scope.lookups.Locations.selected = $scope.lookups.Locations.find(function (element) {
            return element.Name === workorder.Location;
        });
        $scope.lookups.Calltypes.selected = $scope.lookups.Calltypes.find(function (element) {
            return element.Description === workorder.CallType;
        });
        $scope.obj.calldate = workorder.CallDate;
        $scope.lookups.Problems.selected = $scope.lookups.Problems.find(function (element) {
            return element.Description === workorder.Problem;
        });
        $scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheet;
        $scope.lookups.Employes.selected = $scope.lookups.Employes.find(function (element) {
            return element.Name === workorder.Employee;
        });
        $scope.lookups.Equipment.selected = $scope.lookups.Equipment.find(function (element) {
            return element.Equipment === workorder.Equipment;
        });
        $scope.lookups.Hours.selected = $scope.lookups.Hours.find(function (element) {
            return element.Description === workorder.EstimatedRepairHours;
        });
        $scope.obj.nottoexceed = workorder.NottoExceed;
        $scope.obj.locationcomments = workorder.Area;
        $scope.obj.customerpo = workorder.CustomerPO;
        $scope.obj.permissiocode = workorder.PermissionCode;
        $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PayMethod;
    });

    

    $scope.saveWorkOrder = function () {
        var workorder = {
            Id: $stateParams.id,
            Customer: $scope.lookups.Customers.selected == null ? "" : $scope.lookups.Customers.selected.Customer,
            Location: $scope.lookups.Locations.selected == null ? "" : $scope.lookups.Locations.selected.Location,
            Calltype: $scope.lookups.Calltypes.selected == null ? "" : $scope.lookups.Calltypes.selected.CallType,
	        Calldate: $scope.obj.calldate,
	        Problem: $scope.lookups.Problems.selected == null ? "" : $scope.lookups.Problems.selected.Problem,
	        Ratesheet: $scope.lookups.RateSheets.selected == null ? "" : $scope.lookups.RateSheets.selected,
	        Emploee: $scope.lookups.Employes.selected == null ? "" : $scope.lookups.Employes.selected.Employee,
	        Equipment: $scope.lookups.Equipment.selected == null ? "" : $scope.lookups.Equipment.selected.Equipment,
	        Estimatehours: $scope.lookups.Hours.selected == null ? "0" : $scope.lookups.Hours.selected.Repair,
	        Nottoexceed: $scope.obj.nottoexceed,
	        Locationcomments: $scope.obj.locationcomments,
	        Customerpo: $scope.obj.customerpo,
	        Permissiocode: $scope.obj.permissiocode,
	        Paymentmethods: $scope.lookups.PaymentMethods.selected == null ? "" : $scope.lookups.PaymentMethods.selected,
            WorkOrder: $scope.editableWorkOrder.WorkOrder
	    };

	    commonDataService.saveWorkorder(workorder).then(function (response) {
	        if (response.data == 'success')
	            $state.go('manager.workorder.list');
	    });
    };

	commonDataService.getWorkorder($stateParams.id).then(function (response) {
	    return $scope.editableWorkOrder = response.data;
	});

	commonDataService.getWorkorderPictures($stateParams.id).then(function (response) {
	    return $scope.pictures = response.data;
	});
    
    //$scope.locations = ["1",  "2", "3", "4"];

};
editWorkorderController.$inject = ["$scope", "$stateParams", "$state", "commonDataService", "state"];