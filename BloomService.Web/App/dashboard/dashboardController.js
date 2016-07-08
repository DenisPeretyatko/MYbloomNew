/**
 * dashboardController - controller
 */
"use strict";

var dashboardController = function ($rootScope, $scope, $interpolate, commonDataService, state) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workorders = [];
    $scope.truckMarkers = [];
    $scope.mapDate = new Date();
    $scope.sortType = 'DateEntered';
    $scope.sortDirection = true;
    $scope.showAll = false;
    $scope.workordersView = [];
    $scope.workorderMarkers = [];

    $scope.changeSorting = function (data) {
        if ($scope.sortType != data) {
            $scope.sortType = data;
            $scope.sortDirection = true;
        } else {
            $scope.sortDirection = !$scope.sortDirection;
        }
    }


    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

    $scope.Parse = function (value) {
        return new Date(parseInt(value.substr(6)));
    };

    $scope.showAllLocations = function () {
       if ($scope.showAll == true) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                    $scope.workordersView.push(value.WorkOrder);
            });
        } else {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
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

    $scope.$watchCollection(function () { return $rootScope.workorders; }, function () {
        $scope.workordersView = [];
        if ($scope.showAll == false) {          
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value.WorkOrder);
                }
            });
        } else {
            angular.forEach($rootScope.workorders, function (value, key) {
                $scope.workordersView.push(value.WorkOrder);
            });
        }
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

    commonDataService.getTrucks().then(function (response) {
        $scope.trucks = response.data;
    });
    var model = {
        DateWorkOrder: new Date($scope.mapDate)
    }
    commonDataService.getLocations().then(function (response) {
        $scope.workordersView = [];
        $rootScope.workorders = response.data;
        angular.forEach($rootScope.workorders, function (value, key) {
            if (moment(value.DateEntered).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
                $scope.workordersView.push(value.WorkOrder);
            }
        });
    });


    commonDataService.getDashboard(model).then(function (response) {
        var dashboard = response.data;
        $scope.listworkorders = dashboard.WorkOrders;
        $scope.chartData = dashboard.Chart;
        $scope.flotChartOptions = flotChartOptions;
    });

};
dashboardController.$inject = ["$rootScope", "$scope", "$interpolate", "commonDataService", "state"];