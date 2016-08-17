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
    $scope.sortType = 'ScheduleDate';
    $scope.sortDirection = true;
    $scope.showAll = false;
    $scope.workordersView = [];
    $scope.workorderMarkers = [];
    $scope.globalTimezone = global.TimeZone;
  
    $scope.sort = function (data) {
        if ($scope.sortKey != data) {
            $scope.sortKey = data;
            $scope.reverse = true;
        } else {
            $scope.reverse = !$scope.reverse;
        }
    }
    $scope.sort('ScheduleDate');

    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

    $scope.Parse = function (value) {
        return new Date(parseInt(value.substr(6)));
    };

    $scope.showAllLocations = function () {
       if ($scope.showAll == true) {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                    $scope.workordersView.push(value);
            });
        } else {
            $scope.workordersView = [];
            angular.forEach($rootScope.workorders, function (value, key) {
                if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
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
                if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
                    $scope.workordersView.push(value);
                }
            });
        } else {
            angular.forEach($rootScope.workorders, function (value, key) {
                $scope.workordersView.push(value);
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
            var icon = truck.Color == null ? "/public/images/technician.png" : "/public/technician/" + truck.Employee + ".png?anti_cache=" + Math.random();
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

    //commonDataService.getTrucks().then(function (response) {
    //    $scope.trucks = response.data;
    //});
    $scope.$watch(function () { return state.trucks; }, function () {
        $scope.trucks = state.trucks;
    });

    var model = {
        DateWorkOrder: new Date($scope.mapDate)
    }
    //commonDataService.getLocations().then(function (response) {
    //    $scope.workordersView = [];
    //    $rootScope.workorders = response.data;
    //    angular.forEach($rootScope.workorders, function (value, key) {
    //        if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
    //            $scope.workordersView.push(value);
    //        }
    //    });
    //});
       $scope.workordersView = [];
       // $rootScope.workorders = state.locations;
         angular.forEach($rootScope.workorders, function (value, key) {
            if (moment(value.WorkOrder.ScheduleDate).format('YYYY-MM-DD') == moment($scope.mapDate).format('YYYY-MM-DD')) {
                $scope.workordersView.push(value);
            }
        });

    commonDataService.getDashboard(model).then(function (response) {
        var dashboard = response.data;
        $scope.listworkorders = dashboard.WorkOrders;
        $scope.chartData = dashboard.Chart;
        $scope.flotChartOptions = flotChartOptions;
    });

};
dashboardController.$inject = ["$rootScope", "$scope", "$interpolate", "commonDataService", "state"];