/**
 * loginController - controller
 */

var loginController = function ($scope, $rootScope, $state, commonDataService, commonHub, state, $q) {
    $scope.obj = {};
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

                    if (state.alreadyLoaded == false) {
                        var model = {
                            Index: 0,
                            Search: '',
                            Column: '',
                            Direction: false
                        };
                        $q.all([commonDataService.getLookups(), commonDataService.getLocations(), commonDataService.getTechnicians(), commonDataService.getTrucks(), commonDataService.getWorkordesPaged(model)]).then(function(values) {
                            $rootScope.notifications = values[0].data.Notifications;
                            state.notifications = values[0].data.Notifications;
                            state.notificationTime = values[0].data.NotificationTime;
                            state.lookups = values[0].data;
                            state.locations = values[1].data;
                            $rootScope.workorders = values[1].data;
                            state.technicians = values[2].data;
                            $rootScope.trucks = values[3].data;
                            state.trucks = values[3].data;
                            state.workorders = values[4].data;
                            $scope.isDisabled = false;
                            $state.go('manager.dashboard');
                        });
                    } else $state.go('manager.dashboard');
                } else {
                    $scope.isDisabled = false;
                    $scope.obj.message = 'Incorrect login or password';
                };
            });
        }
    });
};
loginController.$inject = ["$scope", "$rootScope", "$state", "commonDataService", "commonHub", "state", "$q"];