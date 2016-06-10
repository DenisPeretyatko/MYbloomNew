/**
 * loginController - controller
 */

var loginController = function ($scope, $state, commonDataService, $window) {
    $scope.obj = {};
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    $scope.obj.username = 'testofficeuser';
    $scope.obj.password = 'sageDEV!!';
    $scope.obj.message = '';
    $scope.login = (function () {
        commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function (response) {
            if (response.Type == 2) {
                window.localStorage.setItem('Token', response.access_token);
                $state.go('manager.dashboard');
            }
            else {
                $scope.obj.message = 'Incorrect login or password';
            }
        })
    });

};
loginController.$inject = ["$scope", "$state", "commonDataService"];