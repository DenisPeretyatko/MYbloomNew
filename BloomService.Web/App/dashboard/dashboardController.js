/**
 * dashboardController - controller
 */
 "use strict";

 var dashboardController = function ($scope, $interpolate, commonDataService) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workorders = [];
    $scope.truckMarkers = [];

    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

    $scope.Parse = function (value) {
        return new Date(parseInt(value.substr(6)));
    };

    $scope.$watch(function () { return $scope.workorders; }, function () {
        angular.forEach($scope.workorders, function (workorder) {

            var content = tooltipWO(workorder);

            var pos = {
                lat: parseFloat(workorder.Latitude),
                lng: parseFloat(workorder.Longitude)
            }

            var marker = new google.maps.Marker({
                position: pos,
                map: $scope.locationMap,
                icon: "/public/images/workorder1.png",
                title: workorder.WorkOrder
            });

            marker.addListener('click', function () {
                var infowindow = new google.maps.InfoWindow({
                    content: content
                });
                infowindow.open($scope.locationMap, marker);
            });
        });
    });

    $scope.$watch(function () { return $scope.trucks; }, function () {
        angular.forEach($scope.truckMarkers, function (marker) { marker.setMap(null); });
        angular.forEach($scope.trucks, function (truck) {

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
        DateWorkOrder: new Date()
    }
    commonDataService.getLocations(model).then(function (response) {
        $scope.workorders = response.data;
    });


    commonDataService.getDashboard().then(function(response) {
        var dashboard = response.data;
        $scope.listworkorders = dashboard.WorkOrders;
        $scope.chartData = dashboard.Chart;
        $scope.flotChartOptions = flotChartOptions;
    });
};
 dashboardController.$inject = ["$scope", "$interpolate", "commonDataService"];