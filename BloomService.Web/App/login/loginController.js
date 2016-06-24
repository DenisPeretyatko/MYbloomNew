/**
 * loginController - controller
 */

var loginController = function ($scope, $state, commonDataService, commonHub) {
    $scope.obj = {};
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    $scope.obj.username = 'testofficeuser';
    $scope.obj.password = 'sageDEV!!';
    $scope.obj.message = '';
    $scope.login = (function () {
        commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function (response) {
            if (response.access_token != null && response.access_token != "" && response.access_token != undefined  ) {
                window.localStorage.setItem('Token', response.access_token);
                commonHub.LoginHub();
                $state.go('manager.dashboard');
            }
            else {
                $scope.obj.message = 'Incorrect login or password';
            }
        })
    });
};
loginController.$inject = ["$scope", "$state", "commonDataService", "commonHub"];