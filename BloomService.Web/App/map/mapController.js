/**
 * mapController - controller
 */
"use strict";

var mapController = function ($rootScope, $scope, $location, $state, $http, $compile, $interpolate, commonDataService, state) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    //$rootScope.workorders = [];
    $scope.workordersView = [];
    $scope.truckMarkers = [];
    $scope.workorderMarkers = [];
    $scope.obj = {};
    $scope.obj.mapDate = new Date();
    $scope.showAll = false;
    //$rootScope.workorders = [];

    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

    $scope.showAllLocations = function() {
        if ($scope.showAll == true) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                    $scope.workordersView.push(value);
            });
        } else {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value);
                }
            });
        }
    }

    $scope.$watchCollection(function () { return $scope.workordersView; }, function () {
        angular.forEach($scope.workorderMarkers, function (marker) { marker.setMap(null); });
        angular.forEach($scope.workordersView, function (value) {

            var content = tooltipWO(value.WorkOrder);
            var pos = {
                lat: parseFloat(value.WorkOrder.Latitude),
                lng: parseFloat(value.WorkOrder.Longitude)
            }
            var icon = (value.Color == null || value.Color == "") ? "/public/images/workorder.png" : "/Public/workorder/" + value.Employee + ".png?anti_cache=" + Math.random();
            var marker = new google.maps.Marker({
                position: pos,
                map: $scope.locationMap,
                icon: icon,
                title: value.WorkOrder.WorkOrder
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
                var icon = truck.Color == null ? "/public/images/technician.png" : "/public/technician/" + truck.Employee + ".png?anti_cache=" + Math.random();
                var marker = new google.maps.Marker({
                    position: pos,
                    map: $scope.locationMap,
                    icon: icon,
                    title: truck.Name
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

    //commonDataService.getLocations().then(function (response) {
    //    $rootScope.workorders = response.data;
        angular.forEach($rootScope.workorders, function (value, key) {
            if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                $scope.workordersView.push(value);
            }
        });
    //});

    $scope.$watch(function () { return $scope.obj.mapDate; }, function () { 
        if ($scope.showAll == false) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value);
                }
            });
        } else {
            $scope.workordersView = angular.copy($rootScope.workorders);
        }
    });

    $scope.$watchCollection(function () { return $rootScope.workorders; }, function () {
        if ($scope.showAll == false) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value);
                }
            });
        } else {
            $scope.workordersView = angular.copy($rootScope.workorders);
        }
    });

};
mapController.$inject = ["$rootScope", "$scope", "$location", "$state", "$http", "$compile", "$interpolate", "commonDataService", "state"];