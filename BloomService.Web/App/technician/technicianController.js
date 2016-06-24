/**
 * techniciansController - controller
 */

var technicianController = function ($scope, $rootScope, $timeout, commonDataService, commonHub) {
    var connection = commonHub.GetConnection();
    commonDataService.getTechnicians().then(function(response){
        $rootScope.technicians = response.data;
    	$timeout(function() {
        	$(".footable").trigger("footable_redraw");
    	}, 100);
    });

    connection.client.UpdateTechnician = function (technician) {
        angular.forEach($rootScope.technicians, function (value, key) {
            if (value.Employee === technician.Id) {
                commonDataService.getTechnician(value.Id).then(function(response) {
                    delete $rootScope.technicians[key];
                    $rootScope.technicians[key] = response.data;
                });
            }
        });
    };
};
technicianController.$inject = ["$scope", "$rootScope", "$timeout", "commonDataService", "commonHub"];