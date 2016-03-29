/**
 * editWorkorderController - controller
 */

var editWorkorderController = function($scope, $stateParams, commonDataService) {

	this.saveWorkOrder = function() {
		console.log("saveWorkOrder");
    };

    //$scope.locations = ["1",  "2", "3", "4"];


};
editWorkorderController.$inject = ["$scope", "$stateParams", "commonDataService"];