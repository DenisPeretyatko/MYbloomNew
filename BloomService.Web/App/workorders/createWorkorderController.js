/**
 * createWorkorderController - controller
 */

var createWorkorderController = function ($scope, $stateParams, $state, state, commonDataService) {
    $scope.obj = {}
    $scope.customer = '';
    $scope.location = '';
    $scope.calltype = '';
    $scope.obj.calldate = new Date();
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
    $scope.initCustomer = "";
    $scope.initLocation = "";
    $scope.selectedCustomer = "";
    $scope.selectedLocation = "";

    $scope.$watch(function () { return state.lookups; }, function () {
        $scope.lookups = state.lookups;
    });

    $scope.createWorkOrder = function () {
        var workorder = {
            Customer: $scope.initCustomer != null ? $scope.initCustomer.Customer : $scope.lookups.Customers.selected.description.Customer,
            Location: $scope.initLocation != null ? $scope.initLocation.Location : $scope.lookups.Locations.selected.description.Location,
            Calltype: $scope.lookups.Calltypes.selected == null ? "" : $scope.lookups.Calltypes.selected.CallType,
            Calldate: $scope.obj.calldate,
            Problem: $scope.lookups.Problems.selected == null ? "" : $scope.lookups.Problems.selected.Problem,
            Ratesheet: $scope.lookups.RateSheets.selected == null ? "" : $scope.lookups.RateSheets.selected.RATESHEETNBR,
            Emploee: $scope.lookups.Employes.selected == null ? "" : $scope.lookups.Employes.selected.Employee,
            Equipment: $scope.lookups.Equipment.selected == null ? "0" : $scope.lookups.Equipment.selected.Equipment,
            Estimatehours: $scope.lookups.Hours.selected == null ? "" : $scope.lookups.Hours.selected.Repair,
            Nottoexceed: $scope.obj.nottoexceed,
            Locationcomments: $scope.obj.locationcomments,
            Customerpo: $scope.obj.customerpo,
            Permissiocode: $scope.lookups.PermissionCodes.selected == null ? "" : $scope.lookups.PermissionCodes.selected.DESCRIPTION,
            Paymentmethods: $scope.lookups.PaymentMethods.selected
        };

        commonDataService.createWorkorder(workorder).then(function (response) {
            if (response.data.success == true)
                $state.go('manager.workorder.list');
        });
    };

    $scope.$watch(function () { return $scope.selectedCustomer; }, function () {
        var customer = $scope.selectedCustomer.description == null ? $scope.selectedCustomer.originalObject : $scope.selectedCustomer.description;
        var request = "{'customer':'" + customer +"'}";
        commonDataService.locationsByCustomer(request).then(function (response) {
             $scope.lookups.Locations = [response.data];
        });
    });

    $scope.$watch(function () { return $scope.selectedLocation; }, function () {
        var location = $scope.selectedLocation.description == null ? $scope.selectedLocation.originalObject.ARCustomer : $scope.selectedLocation.description.ARCustomer;
        var request = "{'location':'" + location + "'}";
        commonDataService.customerByLocation(request).then(function (response) {
            $scope.lookups.Customers = [response.data];
            $scope.initCustomer = response.data;
        });
    });
};
createWorkorderController.$inject = ["$scope", "$stateParams", "$state", "state", "commonDataService"];