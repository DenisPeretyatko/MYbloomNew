/**
 * mapController - controller
 */
"use strict";

var mapController = function ($rootScope, $scope, $location, $state, $http, $compile, $interpolate, commonDataService, state) {
    var antiCache = new Date().getTime();
    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workordersView = [];
    $scope.truckMarkers = [];
    $scope.workorderMarkers = [];
    $scope.obj = {};
    $scope.obj.mapDate = new Date();
    $scope.showAll = false;
    $scope.showUnassigned = false;
    $scope.activeTechnicans = [];
    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

    $scope.$watch(function () { return state.technicians; }, function () {
        angular.forEach(state.technicians, function (value, key) {
            if (value != null && value.IsAvailable) {
                $scope.activeTechnicans.push({ Name: value.Name, Color: value.Color })
            }
        });
    });

    $scope.showAllLocations = function () {
        var tempWorkordersView = [];
        if ($scope.showUnassigned)
        $scope.showUnassigned = false;
        if ($scope.showAll == true) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                tempWorkordersView.push(value);
            });
        } else {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD') && value.Employee) {
                    tempWorkordersView.push(value);
                }
            });
        }
        $scope.workordersView = tempWorkordersView;
    }

    $scope.showUnassignedWorkorders = function () {
        if ($scope.showAll)
        $scope.showAll = false;
        var tempWorkordersView = [];
        if ($scope.showUnassigned == true) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (value.WorkOrder.Status == 'Open' && value.Employee===0)
                    tempWorkordersView.push(value);
            });
        } else {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD') && value.Employee) {
                    tempWorkordersView.push(value);
                }
            });
        }
        $scope.workordersView = tempWorkordersView;
    }

    $scope.$watchCollection(function () { return $scope.workordersView; }, function () {
        angular.forEach($scope.workorderMarkers, function (marker) { marker.setMap(null); });
        angular.forEach($scope.workordersView, function (value) {

            var content = tooltipWO(value.WorkOrder);
            var pos = {
                lat: parseFloat(value.WorkOrder.Latitude),
                lng: parseFloat(value.WorkOrder.Longitude)
            }
            //var icon = (value.Color == null || value.Color == "") ? "/public/images/workorder.png" : "/Public/workorder/" + value.Employee + ".png?anti_cache=" + value.Color;
            var icon = (value.Color == null || value.Color == "") ? global.BasePath + "/images/workorder.png" : global.BasePath + "/workorder/" + value.Employee + ".png?anti_cache=" + antiCache;
            var marker = new google.maps.Marker({
                position: pos,
                map: $scope.locationMap,
                icon: icon,
                title: value.WorkOrder.WorkOrder.toString()
            });
            $scope.workorderMarkers.push(marker);
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
        });
    });

    $rootScope.$watchCollection(function () { return $rootScope.trucks; }, function () {
        angular.forEach($scope.truckMarkers, function (marker) { marker.setMap(null); });
        angular.forEach($rootScope.trucks, function (truck) {

            var content = tooltip(truck);
            var pos = {
                lat: parseFloat(truck.Latitude),
                lng: parseFloat(truck.Longitude)
            }
            if (pos.lat !== 0 && pos.lng !== 0) {
                //var icon = truck.Color == null ? "/public/images/technician.png" : "/public/technician/" + truck.Employee + ".png?anti_cache=" + truck.Color;
                var icon = truck.Color == null ? global.BasePath + "/images/technician.png" : global.BasePath + "/technician/" + truck.Employee + ".png?anti_cache=" + antiCache;
                var marker = new google.maps.Marker({
                    position: pos,
                    map: $scope.locationMap,
                    icon: icon,
                    title: truck.Name.toString()
                });
                $scope.truckMarkers.push(marker);
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
                    $state.go("manager.technician.edit", { id: truck.Id });
                });
            }
        });
    });

    var tempWorkordersView = [];
    angular.forEach($rootScope.workorders, function (value, key) {
        if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
            tempWorkordersView.push(value);
        }
    });
    $scope.workordersView = tempWorkordersView;

    $scope.$watch(function () { return $scope.obj.mapDate; }, function () {
        if ($scope.showAll == false) {
            $scope.workordersView = [];
            var tempWorkordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    tempWorkordersView.push(value);
                }
            });
            $scope.workordersView = tempWorkordersView;
        } else {
            $scope.workordersView = angular.copy($rootScope.workorders);
        }
    });

    $scope.$watchCollection(function () { return $rootScope.workorders; }, function () {
        if ($scope.showAll == false) {
            var tempWorkordersView = [];
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    tempWorkordersView.push(value);
                }
            });
            $scope.workordersView = tempWorkordersView;
        } else {
            $scope.workordersView = angular.copy($rootScope.workorders);
        }
    });

};
mapController.$inject = ["$rootScope", "$scope", "$location", "$state", "$http", "$compile", "$interpolate", "commonDataService", "state"];