/**
 * editWorkorderController - controller
 */

var editWorkorderController = function ($scope, $stateParams, $state, $compile, $interpolate, commonDataService, state) {

    $scope.mapOptions = googleMapOptions;
    $scope.obj = {}
    $scope.customer = "";
    $scope.location = "";
    $scope.calltype = "";
    $scope.obj.calldate = new Date();
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
    $scope.obj.data = new Date();
    var customerChanged = false;
    $scope.Rate = 0;


    $scope.$watch(function () { return state.lookups; }, function () {
        $scope.lookups = state.lookups;

        if (state.lookups.Equipment !== undefined) {
            var equipment = {
                equipType: angular.copy($scope.EquipType),
                empl: angular.copy($scope.lookups.Employes),
                description: angular.copy($scope.lookups.Parts),
                date: $scope.obj.data,
                isEditing: true,
                cost: 0.00,
                biled: 0.00,
                rate: 0.0000,
                labor: angular.copy($scope.lookups.Hours),
                parts: angular.copy($scope.lookups.Parts),
                part: ""
            }
            $scope.equipment.push(equipment);
        }
        if ($scope.editableWorkOrder !== undefined && $scope.lookups !== undefined) {
            $scope.lookups.Customers.selected = $scope.editableWorkOrder.CustomerObj;
            $scope.lookups.Locations.selected = $scope.editableWorkOrder.LocationObj;
            $scope.lookups.Calltypes.selected = $scope.editableWorkOrder.CalltypeObj;
            $scope.obj.calldate = $scope.editableWorkOrder.CallDate;
            $scope.lookups.Problems.selected = $scope.editableWorkOrder.ProblemObj;
            $scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheetObj;
            $scope.lookups.Employes.selected = $scope.editableWorkOrder.EmployeeObj;
            $scope.lookups.Hours.selected = $scope.editableWorkOrder.HourObj;
            $scope.obj.nottoexceed = $scope.editableWorkOrder.NottoExceed;
            $scope.obj.locationcomments = $scope.editableWorkOrder.Comments;
            $scope.obj.customerpo = $scope.editableWorkOrder.CustomerPO;
            $scope.lookups.PermissionCodes.selected = $scope.editableWorkOrder.PermissionCodeObj;
            $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PaymentMethodObj;
        }
    });

    $scope.$watch(function () { return $scope.editableWorkOrder }, function () {
        if ($scope.editableWorkOrder !== undefined && $scope.lookups !== undefined) {
            $scope.lookups.Customers.selected = $scope.editableWorkOrder.CustomerObj;
            $scope.lookups.Locations.selected = $scope.editableWorkOrder.LocationObj;
            $scope.lookups.Calltypes.selected = $scope.editableWorkOrder.CalltypeObj;
            $scope.obj.calldate = $scope.editableWorkOrder.CallDate;
            $scope.lookups.Problems.selected = $scope.editableWorkOrder.ProblemObj;
            $scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheetObj;
            $scope.lookups.Employes.selected = $scope.editableWorkOrder.EmployeeObj;
            $scope.lookups.Hours.selected = $scope.editableWorkOrder.HourObj;
            $scope.obj.nottoexceed = $scope.editableWorkOrder.NottoExceed;
            $scope.obj.locationcomments = $scope.editableWorkOrder.Comments;
            $scope.obj.customerpo = $scope.editableWorkOrder.CustomerPO;
            $scope.lookups.PermissionCodes.selected = $scope.editableWorkOrder.PermissionCodeObj;
            $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PaymentMethodObj;
        }
    });

    $scope.saveWorkOrder = function () {
        $scope.equipment.pop();
        var equipment = [];
        angular.forEach($scope.equipment, function (value, key) {
            if (value != null) {
                equipment.push({
                    WorkDate: value.date,
                    Type: value.equipType,
                    Description: value.description,
                    Employee: value.empl,
                    CostQty: value.cost,
                    BiledQty: value.biled,
                    Rate: value.rate,
                    Part: parseInt(value.part)
                });
            }
        });
        var workorder = {
            WorkOrder: $scope.editableWorkOrder.WorkOrder,
            Id: $stateParams.id,
            Customer: $scope.lookups.Customers.selected == null ? "" : $scope.lookups.Customers.selected.Customer,
            Location: $scope.lookups.Locations.selected == null ? "" : $scope.lookups.Locations.selected.Location,
            Calltype: $scope.lookups.Calltypes.selected == null ? "" : $scope.lookups.Calltypes.selected.CallType,
            Calldate: $scope.obj.calldate,
            Problem: $scope.lookups.Problems.selected == null ? "" : $scope.lookups.Problems.selected.Problem,
            Ratesheet: $scope.lookups.RateSheets.selected == null ? "" : $scope.lookups.RateSheets.selected.RATESHEETNBR,
            Emploee: $scope.lookups.Employes.selected == null ? "" : $scope.lookups.Employes.selected.Employee,
            Estimatehours: $scope.lookups.Hours.selected == null ? "0" : $scope.lookups.Hours.selected.Repair,
            Nottoexceed: $scope.obj.nottoexceed,
            Locationcomments: $scope.obj.locationcomments,
            Customerpo: $scope.obj.customerpo,
            Permissiocode: $scope.lookups.PermissionCodes.selected == null ? "" : $scope.lookups.PermissionCodes.selected.DESCRIPTION,
            Paymentmethods: $scope.lookups.PaymentMethods.selected == null ? "" : $scope.lookups.PaymentMethods.selected.Value,
            WorkOrder: $scope.editableWorkOrder.WorkOrder,
            Equipment: equipment,
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

    $scope.setRate = function (selected,item) {
        if (item.equipType.selected == 'Parts' || item.equipType.selected == undefined) {
            item.rate = parseFloat(selected.$select.selected.Level1Price);
        }
        else {
            item.rate = 85.0000;
        }
    };


    $scope.editRow = function (item, index) {
        item.isEditing = true;

        var type = item.equipType
        item.equipType = angular.copy($scope.EquipType);
        item.equipType.selected = type;

        if (item.equipType.selected == 'Labor') {
            var selectedDesc = $scope.lookups.Hours.find(function (element) {
                return element.Description === item.description;
            });
            item.description = angular.copy($scope.lookups.Hours);
            item.description.selected = selectedDesc;

            item.labor = angular.copy($scope.lookups.Hours);
            item.labor.selected = selectedDesc;
        }
        else {
            var selectedDesc = $scope.lookups.Parts.find(function (element) {
                return element.Description === item.description;
            });
            item.description = angular.copy($scope.lookups.Parts);
            item.description.selected = selectedDesc;

            item.parts = angular.copy($scope.lookups.Parts);
            item.parts.selected = selectedDesc;
            item.part = selectedDesc.Part;
        }

        var selectedEmpl = $scope.lookups.Employes.find(function (element) {
            return element.Name === item.empl;
        });
        item.empl = angular.copy($scope.lookups.Employes);
        item.empl.selected = selectedEmpl;

        item.cost = parseFloat(item.cost);
        item.biled = parseFloat(item.biled);
        item.rate = parseFloat(item.rate);
        item.date = new Date(item.date);
    }

    $scope.saveRow = function (item, index) {
        if (item.equipType.selected != undefined && item.empl.selected != undefined && item.date != undefined && item.description != undefined) {
            item.equipType = item.equipType.selected;
            if (item.equipType == 'Labor') {
                item.description = item.labor.selected.Description;
            }
            else {
                item.description = item.parts.selected.Description;
                item.part = item.parts.selected.Part;
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
                    equipType: angular.copy($scope.EquipType),
                    empl: angular.copy($scope.lookups.Employes),
                    description: angular.copy($scope.lookups.Parts),
                    date: $scope.obj.data,
                    isEditing: true,
                    cost: 0.00,
                    biled: 0.00,
                    rate: 0.0000,
                    labor: angular.copy($scope.lookups.Hours),
                    parts: angular.copy($scope.lookups.Parts),
                    part: ""
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

    $scope.$watch(function () {
        return $scope.lookups.Customers != undefined ? $scope.lookups.Customers.selected != undefined ? $scope.lookups.Customers.selected : "" : "";
    }, function () {
        if ($scope.lookups.Customers != undefined && $scope.lookups.Customers.selected != undefined && !customerChanged) {
            var customer = $scope.lookups.Customers.selected.Customer;
            var request = "{'customer':'" + customer + "'}";
            commonDataService.locationsByCustomer(request).then(function (response) {
                var selLocation = {};
                if ($scope.lookups.Locations.selected != undefined && $scope.lookups.Locations.selected.ARCustomer == $scope.lookups.Customers.selected.Customer) {
                    selLocation = $scope.lookups.Locations.selected;
                }
                $scope.lookups.Locations = response.data.length > 0 ? response.data : [];
                $scope.lookups.Locations.selected = selLocation;
            });
        }
    });

    $scope.$watch(function () {
        return $scope.lookups.Locations != undefined ? $scope.lookups.Locations.selected != undefined ? $scope.lookups.Locations.selected : "" : ""
    }, function () {
        if ($scope.lookups.Locations != undefined && $scope.lookups.Locations.selected != undefined && $scope.lookups.Customers.selected == undefined) {
            var arCustomer = $scope.lookups.Locations.selected.ARCustomer;
            var request = "{'arcustomer':'" + arCustomer + "'}";
            customerChanged = true;
            commonDataService.customerByLocation(request).then(function (response) {
                $scope.lookups.Customers.selected = response.data;
            });
        }
        customerChanged = false;
    });

    $scope.displayLocation = function (lat, lng, picture, woNumber) {
        var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Id}}. {{Image}}</h1><div>{{Description}}</div></div>");
        var content = tooltip(picture);
        var pos = {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        }

        var marker = new google.maps.Marker({
            position: pos,
            map: $scope.locationMap,
            icon: "/public/images/workorder.png",
            title: woNumber
        });
        marker.addListener('click', function () {
            var infowindow = new google.maps.InfoWindow({
                content: content
            });
            infowindow.open($scope.locationMap, marker);
        });       
    };

    $('#myModal').on('shown.bs.modal', function () {
        google.maps.event.trigger($scope.locationMap, 'resize');
    })

};
editWorkorderController.$inject = ["$scope", "$stateParams", "$state", "$compile", "$interpolate", "commonDataService", "state"];