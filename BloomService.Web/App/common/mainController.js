/**
 * mainController - controller
 */

var mainController = function ($scope, commonDataService, state) {
    $scope.userName = 'Nick Saroki';
    $scope.notifications = [];
    $scope.notifications = state.notifications;
    $scope.allnotifications = false;

    $scope.$watch(function () { return state.notifications; }, function () {
        angular.forEach(state.notifications, function (notification) {
            notification.time = moment(notification.time).fromNow();
        });
        $scope.notifications = state.notifications;
    });

    $scope.showAlerts = function() {
        $scope.allnotifications = true;
    }

    $scope.hideAlerts = function () {
        $scope.allnotifications = false;
    }
};

mainController.$inject = ["$scope", "commonDataService", "state"];