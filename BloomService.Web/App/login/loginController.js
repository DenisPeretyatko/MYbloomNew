/**
 * loginController - controller
 */

var loginController = function($scope, $rootScope, $state, commonDataService, commonHub, state, $q) {
    $scope.obj = {};
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    $scope.obj.username = 'testofficeuser';
    $scope.obj.password = 'sageDEV!!';
    $scope.obj.message = '';
    var paginationModel = {
        Index: 0,
        Search: '',
        Column: '',
        Direction: true
    }
    $scope.login = (function () {
        commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function(response) {
            if (response.access_token != null && response.access_token != "" && response.access_token != undefined) {
                window.localStorage.setItem('Token', response.access_token);
                commonHub.LoginHub();

                debugger;
                if (state.alreadyLoaded == false ) { //|| state.notificationTime == undefined || state.lookups == undefined || state.notifications == undefined
                    $q.all([commonDataService.getLookups(), commonDataService.getWorkordesPaged(paginationModel), commonDataService.getTechnicians(), commonDataService.getTrucks()]).then(function (values) { //, commonDataService.getLocations(),
                       
                        $rootScope.notifications = values[0].data.Notifications;
                        state.notifications = values[0].data.Notifications;
                        state.notificationTime = values[0].data.NotificationTime;
                        state.lookups = values[0].data;
                        //state.locations = values[1].data;
                        state.workorders = values[1].data;
                        state.technicians = values[2].data;
                        $rootScope.trucks = values[3].data;
                        state.trucks = values[3].data;
                        $state.go('manager.dashboard');
                    });
                } 
                else  $state.go('manager.dashboard');
            }
            else {
                $scope.obj.message = 'Incorrect login or password';
            };
        });
    });
};
loginController.$inject = ["$scope", "$rootScope", "$state", "commonDataService", "commonHub", "state", "$q"];