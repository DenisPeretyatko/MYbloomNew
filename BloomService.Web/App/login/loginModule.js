/**
 * login - module
 */
 
var loginModuleConfig = function ($stateProvider) {
    $stateProvider
        .state('manager.login', {
            url: "/login",
            templateUrl: "/app/login/views/login.html",
            controller: "loginController",
            data: { pageTitle: 'Example view' }
        });
}
dashboardModuleConfig.$inject = ["$stateProvider"];

angular
    .module('bloom.login', [])
    .config(loginModuleConfig)
    .controller('loginController', loginController);
