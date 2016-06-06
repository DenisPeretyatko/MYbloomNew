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

	$scope.Parse = function (value) {
	    return new Date(parseInt(value.substr(6)));
	};
		
};
workorderController.$inject = ["$scope", "$timeout", "commonDataService"];