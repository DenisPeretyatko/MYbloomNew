/**
 * editWorkorderController - controller
 */

var editWorkorderController = function ($scope, $stateParams, $state, $compile, commonDataService, state) {

    $scope.obj = {}
    $scope.customer = "";
    $scope.location = "";
    $scope.calltype = "";
    $scope.obj.calldate = "";
    $scope.problem = "";
    $scope.ratesheet = "";
    $scope.emploee = "";
    $scope.equipment = [];
    $scope.estimatehours = "";
    $scope.obj.nottoexceed = "";
    $scope.obj.locationcomments = "";
    $scope.obj.customerpo = "";
    $scope.obj.permissiocode = "";
    $scope.paymentmethods = "";
    $scope.lookups = state.lookups;
    $scope.EquipType = ["Labor", "Parts"];
    $scope.equipmentList = [];
    
    $scope.$watch(function () { return state.lookups; }, function () {
       $scope.lookups = state.lookups;

       if (state.lookups.Equipment !== undefined) {
           var equipment = {
               equip: $scope.lookups.Equipment,
               equipType: $scope.EquipType,
               description: $scope.lookups.Parts,
               empl: $scope.lookups.Employes,
               date: $scope.obj.data,
               cost: 0.00,
               biled: 0.00,
               rate: 0.0000,
               isEditing: true,
               labor: $scope.lookups.Hours,
               parts: $scope.lookups.Parts
           }
           $scope.equipment.push(equipment);
       }
       var workorder = $scope.editableWorkOrder;
       $scope.lookups.Customers.selected = {};
       $scope.lookups.Customers.selected = workorder.CustomerObj;
       $scope.lookups.Locations.selected = {};
       $scope.lookups.Locations.selected = workorder.LocationObj;
       $scope.lookups.Calltypes.selected = workorder.CalltypeObj;
        $scope.obj.calldate = workorder.CallDate;
        $scope.lookups.Problems.selected = workorder.ProblemObj;
        //$scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheet;
        $scope.lookups.RateSheets.selected = workorder.RateSheetObj;
        $scope.lookups.Employes.selected = workorder.EmployeeObj;
        $scope.lookups.Hours.selected = workorder.HourObj;
        $scope.obj.nottoexceed = workorder.NottoExceed;
        $scope.obj.locationcomments = workorder.Comments;
        $scope.obj.customerpo = workorder.CustomerPO;
        //$scope.obj.permissiocode = workorder.PermissionCode;
        $scope.lookups.PermissionCodes.selected = workorder.PermissionCodeObj;
        $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PayMethod;
    });

    

    $scope.saveWorkOrder = function () {
        var workorder = {
            WorkOrder: $scope.editableWorkOrder.WorkOrder,
            Id: $stateParams.id,
            Customer: $scope.lookups.Customers.selected == null ? "" : $scope.lookups.Customers.selected.description.Customer,
            Location: $scope.lookups.Locations.selected == null ? "" : $scope.lookups.Locations.selected.description.Location,
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
	        if (response.data.success == true)
	            $state.go("manager.workorder.list");
	    });
    };

	commonDataService.getWorkorder($stateParams.id).then(function (response) {
	    return $scope.editableWorkOrder = response.data;
	});

	commonDataService.getWorkorderPictures($stateParams.id).then(function (response) {
	    return $scope.pictures = response.data;
	});

	$scope.editRow = function (item, index) {
	    item.isEditing = true;

	    item.equip = angular.copy($scope.lookups.Equipment);
	    item.equip.selected = $scope.lookups.Equipment.selected;
        	    
	    item.equipType = angular.copy($scope.EquipType);
	    item.equipType.selected = $scope.EquipType.selected;

	    var selectedDesc =$scope.lookups.Parts.find(function (element) {
	        return element.Description === item.description;
        });

	    if (item.description != '' && selectedDesc != undefined) {
	        item.description = angular.copy($scope.lookups.Parts);
	        item.description.selected = $scope.lookups.Parts.selected.description;

	        item.parts = angular.copy($scope.lookups.Parts);
	        item.parts.selected = $scope.lookups.Parts.selected.description;
	    }
	    else {
	        item.description = angular.copy($scope.lookups.Hours);
	        item.description.selected = $scope.lookups.Hours.selected.description;

	        item.labor = angular.copy($scope.lookups.Hours);
	        item.labor.selected = $scope.lookups.Hours.selected.description;
	    }

	    item.empl = angular.copy($scope.lookups.Employes);
	    item.empl.selected = $scope.lookups.Employes.selected;

	    item.cost = parseFloat(item.cost);
	    item.biled = parseFloat(item.biled);
	    item.rate = parseFloat(item.rate);
	    item.date = new Date(item.date);
	}
 
	$scope.saveRow = function (item, index) {
	    if (item.equip.selected != undefined && item.equipType.selected != undefined && item.empl.selected != undefined && item.date != undefined && item.description != undefined) {
	        item.equip = item.equip.selected.EquipmentType;
	        item.equipType = item.equipType.selected;
	        if (item.equipType == 'Labor') {
	            item.description = item.labor.selected.description.Description;
	        }
	        else {
	            item.description = item.parts.selected.description.Description;
	        }

	        item.empl = item.empl.selected.Name;
	        var date = new Date(item.date);	        
	        item.date = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
	        item.isEditing = false;
	        item.cost = parseFloat(item.cost);
	        item.biled = parseFloat(item.biled);
	        item.rate = parseFloat(item.rate);

	        if (index == ($scope.equipment.length - 1)) {
	            var equipment = {
	                equip: angular.copy($scope.lookups.Equipment),
	                equipType: angular.copy($scope.EquipType),
	                empl: angular.copy($scope.lookups.Employes),
	                description: angular.copy($scope.lookups.Parts),
	                date: $scope.obj.data,
	                isEditing: true,
	                cost: 0.00,
	                biled: 0.00,
	                rate: 0.0000,
	                labor: angular.copy($scope.lookups.Hours),
	                parts: angular.copy($scope.lookups.Parts)
	            }
	            $scope.equipment.push(equipment);
	        }
	    }
    }

    $scope.deleteRow = function ($event, item) {
        var el = angular.element($event.target);
        el.parent().parent().remove();
        $scope.equipment.splice($scope.equipment.indexOf(item), 1);
    }
    //$scope.locations = ["1",  "2", "3", "4"];

};
editWorkorderController.$inject = ["$scope", "$stateParams", "$state", "$compile", "commonDataService", "state"];