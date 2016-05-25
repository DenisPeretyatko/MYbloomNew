/**
 * mapController - controller
 */
"use strict";

var mapController = function ($scope, $http, $compile, $interpolate, commonDataService, state) {

    $scope.mapOptions = googleMapOptions;
    $scope.trucks = [];
    $scope.workorders = [];
    $scope.truckMarkers = [];
    $scope.obj = {};
    $scope.obj.mapDate = new Date();

    var tooltip = $interpolate("<div><h1 class='firstHeading'>{{Name}}</h1><div>{{Location}}</div></div>");
    var tooltipWO = $interpolate("<div><h1 class='firstHeading'>{{WorkOrder}}</h1><div>{{Location}}<br/>{{Problem}}<br/>{{CallType}}</div></div>");

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
            title:  workorder.WorkOrder
          });

          marker.addListener('click', function() {
            var infowindow = new google.maps.InfoWindow({
               content: content
            });
            infowindow.open($scope.locationMap, marker);
          });
        });
    });

    $scope.$watch(function () { return state.trucks; }, function () {
        angular.forEach($scope.truckMarkers, function (marker) { marker.setMap(null); });
        angular.forEach($scope.trucks, function (truck) {

          var content = tooltip(truck);

          var pos = {
              lat: parseFloat(truck.Latitude),
              lng: parseFloat(truck.Longitude)
          }

          var marker = new google.maps.Marker({
            position: pos,
            map: $scope.locationMap,
            icon: "/public/images/technician2.png",
            title: truck.Employee
          });
          $scope.truckMarkers.push(marker);
          marker.addListener('click', function() {
            var infowindow = new google.maps.InfoWindow({
               content: content
            });
            infowindow.open($scope.locationMap, marker);
          });
        });
    });

    commonDataService.getTrucks().then(function(response) {
        $scope.trucks = response.data;
    });

    $scope.$watch(function () { return $scope.obj.mapDate; }, function () {
        var model = {
            DateWorkOrder: new Date($scope.obj.mapDate)
        }
        commonDataService.getLocations(model).then(function (response) {
            $scope.workorders = response.data;
        });
    });
};
mapController.$inject = ["$scope", "$http", "$compile", "$interpolate", "commonDataService", "state"];