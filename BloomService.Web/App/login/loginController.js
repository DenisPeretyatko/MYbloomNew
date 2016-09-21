/**
 * loginController - controller
 */

var loginController = function ($scope, $rootScope, $state, commonDataService, commonHub, state, $q) {
    $scope.obj = {};
    var logoPath = global.BasePath + '/images/logo.png';
    $(".main-logo").css({ "background-image": 'url(' + logoPath + ')' });
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    $scope.obj.username = 'testofficeuser';
    $scope.obj.password = 'sageDEV!!';
    $scope.obj.message = '';
    $scope.isDisabled = false;
    $scope.login = (function () {
        if ($scope.isDisabled == false) {
            $scope.isDisabled = true;
            commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function(response) {
                if (response.access_token != null && response.access_token != "" && response.access_token != undefined) {
                    window.localStorage.setItem('Token', response.access_token);
                    commonHub.LoginHub();
                    $scope.isDisabled = false;
                    $state.go('manager.dashboard');
                } else {
                    $scope.isDisabled = false;
                    $scope.obj.message = 'Incorrect login or password';
                };
            });
        }
    });
};
loginController.$inject = ["$scope", "$rootScope", "$state", "commonDataService", "commonHub", "state", "$q"];