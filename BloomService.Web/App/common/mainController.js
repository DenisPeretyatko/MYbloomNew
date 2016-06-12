/**
 * mainController - controller
 */

var mainController = function ($scope, commonDataService, state, $window) {
    angular.element(window).on;
    $scope.userName = window.localStorage.getItem('UserName')
    $scope.notifications = [];
    $scope.notifications = state.notifications;
    $scope.allnotifications = false;

    $scope.$watch(function () { return state.notifications; }, function () {
        angular.forEach(state.notifications, function (notification) {
            notification.time = moment(notification.time).fromNow();
        });
        $scope.notifications = state.notifications;
    });

    $scope.Logout = function() {
        window.localStorage.setItem('UserName', '')
        window.localStorage.setItem('Token', '');
        $state.go('login');
    }

    $scope.showAlerts = function() {
        $scope.allnotifications = true;
    }

    $scope.hideAlerts = function () {
        $scope.allnotifications = false;
    }
};

mainController.$inject = ["$scope", "commonDataService", "state"];