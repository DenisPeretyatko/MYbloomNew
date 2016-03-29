/**
 * dashboardController - controller
 */
 "use strict";

var dashboardController = function ($scope, commonDataService) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workoders = [];

    $scope.$watch("workoders", function(workoders){
        angular.forEach(workoders, function (workoder){  

          var marker = new google.maps.Marker({
            position: workoder.location,
            map: $scope.locationMap,
            icon: workoder.icon,
            title: workoder.title
          });

          marker.addListener('click', function() {
            var infowindow = new google.maps.InfoWindow({
               content: 'workoder'
            });
            infowindow.open($scope.locationMap, marker);
          });
        });
    });

    $scope.$watch("trucks", function(trucks){
        angular.forEach(trucks, function (truck){  

          var marker = new google.maps.Marker({
            position: truck.location,
            map: $scope.locationMap,
            icon: truck.icon,
            title: truck.title
          });

          marker.addListener('click', function() {
            var infowindow = new google.maps.InfoWindow({
               content: 'truck'
            });
            infowindow.open($scope.locationMap, marker);
          });
        });
    });

    commonDataService.getLocations().then(function(response) {
        $scope.workoders = response.data;
      });

    commonDataService.getTrucks().then(function(response) {
        $scope.trucks = response.data;
    });

    commonDataService.getDashboard().then(function(response) {
        var dashboard = response.data;
        $scope.workorders = dashboard.workorders;
        $scope.chartData = dashboard.chart;
        $scope.flotChartOptions = flotChartOptions;
    });
};
dashboardController.$inject = ["$scope", "commonDataService"];