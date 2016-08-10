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

    $scope.$watch(function () { return state.lookups; }, function () {
        $scope.lookups = state.lookups;
        if ($scope.lookups != undefined && $scope.lookups.Customers != undefined) {
            $scope.lookups.Customers = state.lookups.Customers;
            $scope.lookups.Locations = state.lookups.Locations;
            $scope.lookups.Customers.selected = '';
            $scope.lookups.Locations.selected = '';
            $scope.lookups.Calltypes.selected = '';
            $scope.obj.calldate;
            $scope.lookups.Problems.selected  = '';
            $scope.lookups.RateSheets.selected  = '';
            $scope.lookups.Employes.selected  = '';
            $scope.lookups.Equipment.selected = '';
            $scope.lookups.Hours.selected = '';
            $scope.obj.nottoexceed = '';
            $scope.obj.locationcomments = '';
            $scope.obj.customerpo = '';
            $scope.lookups.PermissionCodes.selected  = '';
            $scope.lookups.PaymentMethods.selected = '';
        }
    });

    $scope.createWorkOrder = function () {
        var workorder = {
            Customer: $scope.lookups.Customers.selected == null ? "" : $scope.lookups.Customers.selected.Customer,
            Location: $scope.lookups.Locations.selected == null ? "" : $scope.lookups.Locations.selected.Location,
            Calltype: $scope.lookups.Calltypes.selected == null ? "" : $scope.lookups.Calltypes.selected.CallType,
            Calldate: $scope.obj.calldate,
            Problem: $scope.lookups.Problems.selected == null ? "" : $scope.lookups.Problems.selected.Problem,
            Ratesheet: $scope.lookups.RateSheets.selected == null ? "" : $scope.lookups.RateSheets.selected.DESCRIPTION.trim(),
            Emploee: $scope.lookups.Employes.selected == null ? "" : $scope.lookups.Employes.selected.Employee,
            Equipment: $scope.lookups.Equipment.selected == null ? "0" : $scope.lookups.Equipment.selected.Equipment,
            Estimatehours: $scope.lookups.Hours.selected == null ? "" : $scope.lookups.Hours.selected.Repair,
            Nottoexceed: $scope.obj.nottoexceed,
            Locationcomments: $scope.obj.locationcomments,
            Customerpo: $scope.obj.customerpo,
            Permissiocode: $scope.lookups.PermissionCodes.selected == null ? "" : $scope.lookups.PermissionCodes.selected.DESCRIPTION,
            Paymentmethods: $scope.lookups.PaymentMethods.selected == null ? "" : $scope.lookups.PaymentMethods.selected.Method,
            JCJob: $scope.lookups.Employes.selected == null ? "" : $scope.lookups.Employes.selected.JCJob,
        };

        commonDataService.createWorkorder(workorder).then(function (response) {
            if (response.data.success == true)
                $state.go('manager.workorder.list');
        });
    };


    $scope.setCustomer = function (selected) {
        var customer = selected.$select.selected.Customer;
        var request = "{'customer':'" + customer + "'}";
        commonDataService.locationsByCustomer(request).then(function (response) {
            var selLocation = {};
            if ($scope.lookups.Locations.selected != undefined && $scope.lookups.Locations.selected.ARCustomer == $scope.lookups.Customers.selected.Customer) {
                selLocation = $scope.lookups.Locations.selected;
            }
            $scope.lookups.Locations = response.data.length > 0 ? response.data : [];
            $scope.lookups.Locations.selected = selLocation;
        });
    };
    
    $scope.setLocation = function (selected) {
        var arCustomer = selected.$select.selected.ARCustomer;
        var request = "{'arcustomer':'" + arCustomer + "'}";                       
        customerChanged = true;
        commonDataService.customerByLocation(request).then(function (response) {
            $scope.lookups.Customers.selected = response.data;
        });    
    };

};
createWorkorderController.$inject = ["$scope", "$stateParams", "$state", "state", "commonDataService"];