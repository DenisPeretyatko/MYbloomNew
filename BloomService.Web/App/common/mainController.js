/**
 * mainController - controller
 */

var mainController = function($scope, commonDataService, state) {
    $scope.userName = 'Nick Saroki';

    commonDataService.getLookups().then(function (response) {
        state.lookups = response.data;
    });
};

mainController.$inject = ["$scope", "commonDataService", "state"];