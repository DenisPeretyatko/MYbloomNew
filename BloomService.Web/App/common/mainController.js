/**
 * mainController - controller
 */

var mainController = function ($scope, $rootScope, commonDataService, state, $window, commonHub) {
    angular.element(window).on;
    $scope.userName = window.localStorage.getItem('UserName');
    $scope.notificationsCount = 0;
    $scope.allnotifications = false;
    $scope.canReadaAll = true;
    $rootScope.notifications = [];
    $scope.notificationsCopy = [];
    var canReadMessagesCount = 3;
    var previousNotificationCount = 0;
   

    var convertDate = function (value) {
        if (typeof value == "string") {
            var date = value.split(' ');
            var dateText = date[0];
            var timeText = date[1];
            dateText = dateText.split('-');
            for (var i = 0; i < dateText.length; i++) {
                if (dateText[i].length == 1) {
                    var tempVal = dateText[i];
                    delete dateText[i];
                    dateText[i] = '0' + tempVal;
                }
            }
            dateText[1] = parseInt(dateText[1]) - 1;
            if (dateText[1] < 10) {
                dateText[1] = '0' + dateText[1].toString();
            }
            dateText[1] = dateText[1].toString();
            timeText = timeText.split(':');
            var temp = (parseInt(dateText[1]) - 1).toString();
            return new Date(dateText[2], dateText[1], dateText[0], timeText[0], timeText[1], timeText[2], 0);
        }
        else if (value instanceof Date) {
            return value;
        } else return true;
    }

   

        $rootScope.$watchCollection(function() {
            return $rootScope.notifications;
        }, function () {
            $scope.canReadaAll = false;
             var now = new Date();
             var utcDate = new Date(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(), now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds());
            var temp = state.getNotificationTime();
            var lastDate = convertDate(temp);
            if ($scope.canReadaAll == false && $scope.notificationsCopy.length < $rootScope.notifications.length) { 
                $scope.canReadaAll = true;
                if ($rootScope.notifications.length - $scope.notificationsCopy.length <= 1 && $rootScope.notifications.length != 0)
                    if (canReadMessagesCount < 3)
                        canReadMessagesCount++;

            } 
            $scope.notificationsCount = 0;
            $scope.notificationsCopy = angular.copy($rootScope.notifications);
            angular.forEach($scope.notificationsCopy, function (value, key) {
                var convDate = convertDate(value.time);
                if (convDate > convertDate(lastDate)) {
                    $scope.notificationsCount++;
                }
                value.time = moment(value.time, 'dd-mm-yy hh:mm:ss').from(now);
            });
            $scope.notificationsCount += previousNotificationCount;
        });

    $scope.Logout = function() {
    window.localStorage.setItem('UserName', '')
    window.localStorage.setItem('Token', '');
    commonHub.LogoutHub();
    $state.go('login');
}

$scope.showAlerts = function() {
    $scope.allnotifications = true;
    $scope.canReadaAll = true;
    $scope.notificationsCount = 0;
    previousNotificationCount = 0;
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
        if ($scope.canReadaAll == true) {
            if ($scope.notificationsCount < 3 && previousNotificationCount == 0) {
                $scope.notificationsCount = 0;
                previousNotificationCount = $scope.notificationsCount;
                canReadMessagesCount = 0;
                $scope.canReadaAll = false;
            } else {

                $scope.notificationsCount = $scope.notificationsCount - canReadMessagesCount;
                previousNotificationCount = $scope.notificationsCount;
                canReadMessagesCount = 0;
                $scope.canReadaAll = false;
            }
        }
    }
}

    commonDataService.getLookups().then(function (response) {
        $rootScope.notifications = response.data.Notifications;
        state.setMongaNotificationTime(response.data.NotificationTime);
        state.setLookups(response.data);
    });
};

mainController.$inject = ["$scope", "$rootScope", "commonDataService", "state", "$window", "commonHub"];