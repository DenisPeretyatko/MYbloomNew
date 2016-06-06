/**
 * loginController - controller
 */

var loginController = function ($scope, $state, commonDataService, $window) {
    $scope.obj = {};
    $scope.obj.username = 'testtechuser';
    $scope.obj.password = 'sageDEV!!';

    $scope.login = (function () {
        commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function (response) {
            window.localStorage.setItem('Token', response.access_token);
            $state.go('manager.dashboard');
        })
    });

};
loginController.$inject = ["$scope", "$state", "commonDataService"];