/**
 * mainController - controller
 */

var mainController = function ($scope, $rootScope, commonDataService, state, $window, commonHub) {
    angular.element(window).on;
    $scope.userName = window.localStorage.getItem('UserName');
    $scope.notificationsCount = 0;
    $scope.allnotifications = false;
    $rootScope.notifications = [];
    now = new Date();
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
            
            timeText = timeText.split(':');
            return new Date(dateText[2], parseInt(dateText[1]) - 1, dateText[0], timeText[0], timeText[1], timeText[2], 0);
        } else return true;
    }

    var utcDate = new Date(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate(), now.getUTCHours(), now.getUTCMinutes(), now.getUTCSeconds());
        $rootScope.$watchCollection(function() {
            return $rootScope.notifications;
        }, function () {
            var temp = state.getNotificationTime();
            var lastDate = convertDate(temp);
            $scope.notificationsCount = 0;
            $scope.notificationsCopy = angular.copy($rootScope.notifications);
            angular.forEach($scope.notificationsCopy, function (value, key) {
                // if (moment(value.time, 'dd-mm-yy hh:mm:ss') > moment(temp, 'dd-mm-yy hh:mm:ss')) {
                if (convertDate(value.time) > lastDate) {
                    $scope.notificationsCount++;
                }
                value.time = moment(value.time, 'dd-mm-yy hh:mm:ss').from(utcDate); //.fromNow();
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
      //  $scope.notificationsCopy = angular.copy($rootScope.notifications);
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