/**
 * techniciansController - controller
 */

var workorderController = function($scope, $timeout, commonDataService) {  

	commonDataService.getWorkorders().then(function(response) {
		$scope.workorders = response.data;
		$timeout(function() {
        	$(".footable").trigger("footable_redraw");
    	}, 100);
	});
		
};
workorderController.$inject = ["$scope", "$timeout", "commonDataService"];