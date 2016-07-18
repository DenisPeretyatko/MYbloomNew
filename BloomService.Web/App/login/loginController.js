/**
 * loginController - controller
 */

var loginController = function($scope, $rootScope, $state, commonDataService, commonHub, state) {
    $scope.obj = {};
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    $scope.obj.username = 'testofficeuser';
    $scope.obj.password = 'sageDEV!!';
    $scope.obj.message = '';
    $scope.login = (function () {
        commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function (response) {
            if (response.access_token != null && response.access_token != "" && response.access_token != undefined) {
                window.localStorage.setItem('Token', response.access_token);
                commonHub.LoginHub();

                commonDataService.getTechnicians().then(function (response) {
                    state.technicians = response.data;
                });

                commonDataService.getLookups().then(function (response) {
                    $rootScope.notifications = response.data.Notifications;
                    state.notificationTime = response.data.NotificationTime;
                    state.lookups = response.data;
                });

                commonDataService.getLocations().then(function (response) {
                    state.locations = response.data;
                });
                var paginationModel = {
                    Index: 0,
                    Search: '',
                    Column: '',
                    Direction: true
                };
                commonDataService.getWorkordesPaged(paginationModel).then(function (response) {
                    state.workorders = response.data;
                });
                commonDataService.getTechnicians().then(function (response) {
                    state.technicians = response.data;
                });

                commonDataService.getTrucks().then(function (response) {
                    $rootScope.trucks = response.data;
                    state.trucks = response.data;
                });

                $state.go('manager.dashboard');
            }
            else {
                $scope.obj.message = 'Incorrect login or password';
            }
        })
    });
};
loginController.$inject = ["$scope", "$rootScope", "$state", "commonDataService", "commonHub", "state"];