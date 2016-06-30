/**
 * mainController - controller
 */

var mainController = function ($scope, $rootScope, commonDataService, state, $window, commonHub) {
    angular.element(window).on;
    $scope.userName = window.localStorage.getItem('UserName');
    $scope.notificationsCount = 0;
    $scope.allnotifications = false;
    $rootScope.notifications = [];

        $rootScope.$watchCollection(function() {
            return $rootScope.notifications;
        }, function () {
            var temp = state.getNotificationTime();
            $scope.notificationsCount = 0;
            $scope.notificationsCopy = angular.copy($rootScope.notifications);
            angular.forEach($scope.notificationsCopy, function (value, key) {
                if (moment(value.time, 'dd-mm-yy hh:mm:ss') > moment(temp, 'dd-mm-yy hh:mm:ss')) {
                    $scope.notificationsCount++;
                }
                value.time = moment(value.time, 'dd-mm-yy hh:mm:ss').fromNow();
            });
        });

$scope.Logout = function() {
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    commonHub.LogoutHub();
    $state.go('login');
}

$scope.showAlerts = function() {
    $scope.allnotifications = true;
    $scope.notificationsCount = 0;
}

$scope.hideAlerts = function () {
    $scope.allnotifications = false;
}
    
$scope.openNotifications = function () {
    var wrappedResult = angular.element(angular.element(document.querySelector('#dropdown')));
    if ($scope.allnotifications == true) {
        $scope.notificationsCount = 0;
    }
    if (wrappedResult.context.className != "dropdown open") {
        state.setNotificationTime();
        $scope.notificationsCopy = angular.copy($rootScope.notifications);
        commonDataService.updateNotificationTime();
        if ($scope.notificationsCount >= 3) {
            $scope.notificationsCount = $scope.notificationsCount - 3;
        } else {
            $scope.notificationsCount = 0;
        }
    }
}
};

mainController.$inject = ["$scope", "$rootScope", "commonDataService", "state", "$window", "commonHub"];