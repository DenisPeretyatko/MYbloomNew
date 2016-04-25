/**
 * login - module
 */
 
var loginModuleConfig = function ($stateProvider) {
    $stateProvider
        //.state('manager.login', {
        //    url: "/login",
        //    templateUrl: "/app/login/views/login.html",
        //    controller: "loginController",
        //    data: { pageTitle: 'Example view' }
        //})
        .state('bloom', {
            abstract: true,
            url: "/index",
            templateUrl: "/app/login/views/login.html",
        });
}
dashboardModuleConfig.$inject = ["$stateProvider"];

angular
    .module('bloom.login', [])
    .config(loginModuleConfig)
    .controller('loginController', loginController);
