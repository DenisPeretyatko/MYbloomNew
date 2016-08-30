/**
 * editWorkorderController - controller
 */

var editWorkorderController = function ($scope, $rootScope, $stateParams, $state, $compile, $interpolate, commonDataService, state) {

    $rootScope.updatedSageWorkOrder = [];
    $rootScope.addedImage = [];
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
    $scope.obj.hours = '';
    $scope.paymentmethods = "";
    $scope.lookups = state.lookups;
    $scope.EquipType = ["Labor", "Parts"];
    $scope.equipmentList = [];
    $scope.obj.data = new Date();
    $scope.Rate = 0;
    $scope.isEditNote = false;
    $scope.noteObj = {};
    $scope.noteObj.Subject = "";
    $scope.noteObj.Note = "";
    $scope.workOrderNotes = [];

    $scope.$watch(function () { return state.lookups; }, function () {
        $scope.lookups = state.lookups;

        if ($scope.editableWorkOrder !== undefined && $scope.lookups !== undefined) {
            $scope.getWOItems();

            $scope.lookups.Customers.selected = $scope.editableWorkOrder.CustomerObj;
            $scope.lookups.Locations.selected = $scope.editableWorkOrder.LocationObj;
            $scope.lookups.Calltypes.selected = $scope.editableWorkOrder.CalltypeObj;
            $scope.obj.calldate = $scope.editableWorkOrder.CallDate;
            $scope.lookups.Problems.selected = $scope.editableWorkOrder.ProblemObj;
            $scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheetObj;
            $scope.lookups.Employes.selected = $scope.editableWorkOrder.EmployeeObj;
            //$scope.lookups.Hours.selected = $scope.editableWorkOrder.HourObj;
            $scope.obj.hours = $scope.editableWorkOrder.EstimatedRepairHours;
            $scope.obj.nottoexceed = $scope.editableWorkOrder.NottoExceed;
            $scope.obj.locationcomments = $scope.editableWorkOrder.Comments;
            $scope.obj.customerpo = $scope.editableWorkOrder.CustomerPO;
            $scope.lookups.PermissionCodes.selected = $scope.editableWorkOrder.PermissionCodeObj;
            $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PaymentMethodObj;
            $scope.lookups.Status.selected = $scope.editableWorkOrder.StatusObj;

            commonDataService.getWorkorderPictures($scope.editableWorkOrder.WorkOrder).then(function (response) {
                return $scope.pictures = response.data;
            });
        }
    });

    $scope.$watch(function () { return $scope.editableWorkOrder }, function () {
        if ($scope.editableWorkOrder !== undefined && $scope.lookups !== undefined && $scope.lookups.Customers != undefined) {
            $scope.getWOItems();

            $scope.lookups.Customers.selected = $scope.editableWorkOrder.CustomerObj;
            $scope.lookups.Locations.selected = $scope.editableWorkOrder.LocationObj;
            $scope.lookups.Calltypes.selected = $scope.editableWorkOrder.CalltypeObj;
            $scope.obj.calldate = $scope.editableWorkOrder.CallDate;
            $scope.lookups.Problems.selected = $scope.editableWorkOrder.ProblemObj;
            $scope.lookups.RateSheets.selected = $scope.editableWorkOrder.RateSheetObj;
            $scope.lookups.Employes.selected = $scope.editableWorkOrder.EmployeeObj;
            //$scope.lookups.Hours.selected = $scope.editableWorkOrder.HourObj;
            $scope.obj.hours = $scope.editableWorkOrder.EstimatedRepairHours;
            $scope.obj.nottoexceed = $scope.editableWorkOrder.NottoExceed;
            $scope.obj.locationcomments = $scope.editableWorkOrder.Comments;
            $scope.obj.customerpo = $scope.editableWorkOrder.CustomerPO;
            $scope.lookups.PermissionCodes.selected = $scope.editableWorkOrder.PermissionCodeObj;
            $scope.lookups.PaymentMethods.selected = $scope.editableWorkOrder.PaymentMethodObj;
            $scope.lookups.Status.selected = $scope.editableWorkOrder.StatusObj;

            commonDataService.getWorkorderPictures($scope.editableWorkOrder.WorkOrder).then(function (response) {
                return $scope.pictures = response.data;
            });
        }
    });

    $scope.$watchCollection(function () {
        return $rootScope.addedImage;
    }, function () {
        if ($rootScope.addedImage != undefined && $scope.editableWorkOrder != undefined) {
            if ($rootScope.addedImage.WorkOrder == $scope.editableWorkOrder.WorkOrder) {
                $scope.pictures = $rootScope.addedImage;
            }
        }
    });

    $scope.$watchCollection(function () {
        return $rootScope.updatedSageWorkOrder;
    }, function () {
        if ($rootScope.updatedSageWorkOrder != undefined && $scope.editableWorkOrder != undefined) {
            if ($rootScope.updatedSageWorkOrder.WorkOrder == $scope.editableWorkOrder.WorkOrder) {
                $scope.editableWorkOrder = $rootScope.updatedSageWorkOrder;
            }
        }
    });

    $scope.getWOItems = function () {
        if ($scope.editableWorkOrder !== undefined && $scope.editableWorkOrder.WorkOrderItems !== undefined && $scope.lookups !== undefined) {
            if ($scope.editableWorkOrder.WorkOrderItems != null) {
                var dBWOItem = [];
                angular.forEach($scope.editableWorkOrder.WorkOrderItems, function (value, key) {
                    var laborsList = angular.copy($scope.lookups.Hours);
                    laborsList.selected = $scope.lookups.Hours.find(function (element) {
                        return element.Description === value.Description;
                    });
                    var partsList = angular.copy($scope.lookups.Parts);
                    partsList.selected = $scope.lookups.Parts.find(function (element) {
                        return element.PartNumber + " " + element.Description === value.Description;
                    });
                    var part = partsList.selected != undefined ? parseInt(partsList.selected.Part) : 0;

                    if (value != null) {
                        dBWOItem.push({
                            equipType: value.ItemType,
                            empl: value.Employee,
                            description: value.Description,
                            date: value.WorkDate.substring(0, 10),
                            isEditing: false,
                            cost: value.CostQuantity,
                            biled: value.Quantity,
                            rate: value.UnitSale,
                            labor: laborsList,
                            parts: partsList,
                            part: part,
                            woItem: value.WorkOrderItem,
                            laborItem: laborsList.selected,
                            amount: value.TotalSale,
                            WOId: value.WorkOrder
                        });
                    }
                });
                $scope.equipment = dBWOItem;
            }

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
                part: "",
                woItem: '',
                laborItem: "",
                amount: 0.0000,
                WOId: $scope.editableWorkOrder.WorkOrder
            }
            $scope.equipment.push(equipment);
        } else {
            if ($scope.lookups !== undefined)
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
                    woItem: '',
                    laborItem: "",
                    amount: 0.0000,
                    WOId: $scope.editableWorkOrder.WorkOrder
                }
            $scope.equipment.push(equipment);
        }
    }

    $scope.saveWorkOrder = function () {
        var equipment = [];
        angular.forEach($scope.equipment, function (value, key) {
            if (value != null && key < $scope.equipment.length - 1) {
                equipment.push({
                    WorkDate: value.date,
                    Type: value.equipType,
                    Description: value.description,
                    Employee: value.empl,
                    CostQty: value.cost,
                    BiledQty: value.biled,
                    Rate: value.rate,
                    Part: parseInt(value.part),
                    WorkOrderItem: value.woItem,
                    LaborItem: value.laborItem,
                    WorkOrder: value.WOId
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
            Estimatehours: $scope.obj.hours, //$scope.lookups.Hours.selected == null ? "" : $scope.lookups.Hours.selected.Repair,
            Nottoexceed: $scope.obj.nottoexceed,
            Locationcomments: $scope.obj.locationcomments,
            Customerpo: $scope.obj.customerpo,
            Permissiocode: $scope.lookups.PermissionCodes.selected == null ? "" : $scope.lookups.PermissionCodes.selected.DESCRIPTION,
            Paymentmethods: $scope.lookups.PaymentMethods.selected == null ? "" : $scope.lookups.PaymentMethods.selected.Value,
            WorkOrder: $scope.editableWorkOrder.WorkOrder,
            Equipment: equipment,
            Status: $scope.lookups.Status.selected == null ? "" : $scope.lookups.Status.selected.Value,
            JCJob: $scope.lookups.Employes.selected == null ? "" : $scope.lookups.Employes.selected.JCJob,
            Notes: $scope.workOrderNotes
        };

        commonDataService.saveWorkorder(workorder).then(function (response) {
            if (response.data.success == true)
                $state.go("manager.workorder.list");
        });
    };

    $scope.markAsReviewed = function () {
        var workorderId = "{'workorderId':'" + $scope.editableWorkOrder.WorkOrder + "'}";
        commonDataService.markAsReviewed(workorderId).then(function (response) {
            if (response.data.success == false)
                alert("Failed to mark as reviewed");
        });
    }


    commonDataService.getWorkorder($stateParams.id).then(function (response) {
        $scope.editableWorkOrder = response.data;
        commonDataService.getNotes(response.data.WorkOrder).then(function (result) {
            if (result.data != "")
                $scope.workOrderNotes = result.data;
            else
                $scope.workOrderNotes = [];
        });
    });

    $scope.setRate = function (selected, item) {
        if (item.equipType.selected == 'Parts' || item.equipType.selected == undefined) {
            item.rate = parseFloat(selected.$select.selected.Level1Price);
        } else {
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
        } else {
            var selectedDesc = $scope.lookups.Parts.find(function (element) {
                return element.PartNumber + " " + element.Description === item.description;
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
                item.laborItem = item.labor.selected;
            } else {
                item.description = item.parts.selected.PartNumber + " " + item.parts.selected.Description;
                item.part = item.parts.selected.Part;
            }

            item.empl = item.empl.selected.Name;
            var date = new Date(item.date);
            item.date = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
            item.isEditing = false;
            item.cost = item.cost != NaN ? parseFloat(item.cost) : 0;
            item.biled = parseFloat(item.biled);
            item.rate = parseFloat(item.rate);
            item.amount = parseFloat(item.amount);

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
                    part: "",
                    woItem: "",
                    amount: 0.0000,
                    WOId: $scope.editableWorkOrder.WorkOrder
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


    $scope.displayLocation = function (lat, lng, picture, woNumber) {
        var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Id}}. {{Image}}</h1><div>{{Description}}</div></div>");
        var content = tooltip(picture);
        var pos = {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        }
        $scope.locationMap.setZoom(100);
        $scope.locationMap.setMapTypeId(google.maps.MapTypeId.SATELLITE);

        var marker = new google.maps.Marker({
            position: pos,
            map: $scope.locationMap,
            icon: "/public/images/workorder.png",
            title: woNumber
        });
        var infowindow = new google.maps.InfoWindow({
            content: content
        });
        marker.addListener('mouseover', function () {
            infowindow.open($scope.locationMap, marker);
        });
        marker.addListener('mouseout', function () {
            infowindow.close();
        });
        marker.addListener('click', function () {
            $state.go("manager.workorder.edit", { id: value.WorkOrder.Id });
        });
        $('#myModal').on('shown.bs.modal', function () {
            google.maps.event.trigger($scope.locationMap, 'resize');
            $scope.locationMap.setCenter(new google.maps.LatLng(pos.lat, pos.lng));
            $("#modalImg").attr('src', 'public/images/' + $scope.editableWorkOrder.WorkOrder + '/' + picture.BigImage);
            $("#modalComment").text(picture.Description);
        });
        $('#myModal').on('hidden.bs.modal', function () {
            marker.setMap(null);
        });
    };

    $scope.setEstimateHour = function (selected) {
        $scope.obj.hours = parseFloat(selected.$select.selected.EstimatedRepairHours);
    };

    $scope.saveComment = function (comment, wo, id) {
        var model = {
            comment: comment,
            workOrder: wo,
            id: id
        };
        commonDataService.editComment(model).then(function (response) {
            if (response.data.success == false)
                alert("Failed to edit comment");
        });
    }

    $scope.addNote = function (wo) {
        if ($scope.noteObj.Subject && $scope.noteObj.Note) {
            var model = {
                WorkOrderId: wo,
                NoteId: null,
                SubjectLine: $scope.noteObj.Subject,
                Text: $scope.noteObj.Note
            };
            $scope.workOrderNotes.push(model);
            $scope.noteObj.Subject = "";
            $scope.noteObj.Note = "";
        }
    }

    $scope.editNote = function (index, note) {
        $scope.isEditNote = true;
        $scope.editableNote = note;
        $scope.noteObj.Subject = note.SubjectLine;
        $scope.noteObj.Note = note.Text;
    }

    $scope.saveNote = function () {
        if (!$scope.isEditNote) //cancel
        {
            $scope.isEditNote = false;
            $scope.noteObj.Subject = "";
            $scope.noteObj.Note = "";
        } else { //save
            if ($scope.noteObj.Subject && $scope.noteObj.Note) {
                angular.forEach($scope.workOrderNotes, function (value, key) {
                    if (value == $scope.editableNote) {
                        value.SubjectLine = $scope.noteObj.Subject;
                        value.Text = $scope.noteObj.Note;
                    }
                });
                $scope.isEditNote = false;
                $scope.noteObj.Subject = "";
                $scope.noteObj.Note = "";
            }
        }
    }

    $scope.deleteNote = function ($event, item) {
        var el = angular.element($event.target);
        el.parent().parent().remove();
        $scope.workOrderNotes.splice($scope.workOrderNotes.indexOf(item), 1);
    }

    $scope.editRow = function (item, index) {
        item.isEditing = true;
    };
}
editWorkorderController.$inject = ["$scope", "$rootScope", "$stateParams", "$state", "$compile", "$interpolate", "commonDataService", "state"];