/**
 * mapController - controller
 */
"use strict";

var mapController = function ($scope, $http, $compile, $interpolate, commonDataService, state) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workorders = [];
    $scope.workorders = state.locations;
    $scope.workorderMarkers = [];

    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{title}}</h1><div>{{description}}</div></div>");

    $scope.$watch(function () { return state.locations; }, function () {
        angular.forEach($scope.workorderMarkers, function(marker) { marker.setMap(null); });
        angular.forEach(state.locations, function (workorder) {
           
          var content = tooltip(workorder);

          var marker = new google.maps.Marker({
            position: workorder.location,
            map: $scope.locationMap,
            icon: workorder.icon,
            title:  workorder.title
          });
          $scope.workorderMarkers.push(marker);
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

          var content = tooltip(truck);

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

    //commonDataService.getLocations().then(function(response) {
    //  	$scope.workorders = response.data;
	//  });

    commonDataService.getTrucks().then(function(response) {
        $scope.trucks = response.data;
    });
};
mapController.$inject = ["$scope", "$http", "$compile", "$interpolate", "commonDataService", "state"];