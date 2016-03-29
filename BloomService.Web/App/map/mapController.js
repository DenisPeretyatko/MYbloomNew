/**
 * mapController - controller
 */
"use strict";

var mapController = function($scope, $http, $compile, $templateCache, commonDataService) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workorders = [];

    $http.get('/app/map/views/tooltip.html').success(function (response) {
        $templateCache.put('tooltip.html', response);
    });

    var tooltipTemplate = $templateCache.get('tooltip.html');
    var tooltip = $compile(tooltipTemplate);

    $scope.$watch("workorders", function(workorders){
        angular.forEach(workorders, function (workorder){  

          var content = tooltip(workorder).html();

          var marker = new google.maps.Marker({
            position: workorder.location,
            map: $scope.locationMap,
            icon: workorder.icon,
            title:  workorder.title
          });

          marker.addListener('click', function() {
            var infowindow = new google.maps.InfoWindow({
               content: content
            });
            infowindow.open($scope.locationMap, marker);
          });
        });
    });

    $scope.$watch("trucks", function(trucks){
        angular.forEach(trucks, function (truck){  

          var content = tooltip(truck).html();

          var marker = new google.maps.Marker({
            position: truck.location,
            map: $scope.locationMap,
            icon: truck.icon,
            title: truck.title
          });

          marker.addListener('click', function() {
            var infowindow = new google.maps.InfoWindow({
               content: content
            });
            infowindow.open($scope.locationMap, marker);
          });
        });
    });

    commonDataService.getLocations().then(function(response) {
      	$scope.workorders = response.data;
	  });

    commonDataService.getTrucks().then(function(response) {
        $scope.trucks = response.data;
    });
};
mapController.$inject = ["$scope", "$http", "$compile", "$templateCache", "commonDataService"];