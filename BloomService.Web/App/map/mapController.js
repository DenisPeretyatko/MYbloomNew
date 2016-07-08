/**
 * mapController - controller
 */
"use strict";

var mapController = function ($rootScope, $scope, $http, $compile, $interpolate, commonDataService, state) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    //$rootScope.workorders = [];
    $scope.workordersView = [];
    $scope.truckMarkers = [];
    $scope.workorderMarkers = [];
    $scope.obj = {};
    $scope.obj.mapDate = new Date();
    $scope.showAll = false;
    $rootScope.workorders = [];

    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

    $scope.showAllLocations = function() {
        if ($scope.showAll == true) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                    $scope.workordersView.push(value.WorkOrder);
            });
        } else {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value.WorkOrder);
                }
            });
        }
    }

    $scope.$watchCollection(function () { return $scope.workordersView; }, function () {
        angular.forEach($scope.workorderMarkers, function (marker) { marker.setMap(null); });
        angular.forEach($scope.workordersView, function (workorder) {

            var content = tooltipWO(workorder);
            var pos = {
                lat: parseFloat(workorder.Latitude),
                lng: parseFloat(workorder.Longitude)
            }

            var marker = new google.maps.Marker({
                position: pos,
                map: $scope.locationMap,
                icon: "/public/images/workorder.png",
                title: workorder.WorkOrder
            });
            $scope.workorderMarkers.push(marker);
            marker.addListener('click', function () {
                var infowindow = new google.maps.InfoWindow({
                    content: content
                });
                infowindow.open($scope.locationMap, marker);
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
            var icon = truck.Color == null ? "/public/images/technician.png" : "/public/technician/" + truck.Employee + ".png";
            var marker = new google.maps.Marker({
                position: pos,
                map: $scope.locationMap,
                icon: icon,
                title: truck.Name
            });
            $scope.truckMarkers.push(marker);
            marker.addListener('click', function () {
                var infowindow = new google.maps.InfoWindow({
                    content: content
                });
                infowindow.open($scope.locationMap, marker);
            });
        });
    });

    commonDataService.getLocations().then(function (response) {
        $rootScope.workorders = response.data;
        angular.forEach($rootScope.workorders, function (value, key) {
            if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                $scope.workordersView.push(value.WorkOrder);
            }
        });
    });

    $scope.$watch(function () { return $scope.obj.mapDate; }, function () { 
        if ($scope.showAll == false) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value.WorkOrder);
                }
            });
        } else {
            $scope.workordersView = $rootScope.workorders.WorkOrder;
        }
    });

    $scope.$watchCollection(function () { return $rootScope.workorders; }, function () {
        if ($scope.showAll == false) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.obj.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value.WorkOrder);
                }
            });
        } else {
            $scope.workordersView = $rootScope.workorders.WorkOrder;
        }
    });

};
mapController.$inject = ["$rootScope", "$scope", "$http", "$compile", "$interpolate", "commonDataService", "state"];