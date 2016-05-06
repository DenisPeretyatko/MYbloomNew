/**
 * techniciansController - controller
 */

var technicianController = function($scope, $timeout, commonDataService) {
    commonDataService.getTechnicians().then(function(response){
    	$scope.technicians = response.data;
    	$timeout(function() {
        	$(".footable").trigger("footable_redraw");
    	}, 100);
    });
};
technicianController.$inject = ["$scope", "$timeout", "commonDataService"];