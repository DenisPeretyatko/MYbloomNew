/**
 * Bloom service angular application
 */
var mainModuleConfig = function($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
    $urlRouterProvider.otherwise("/login");

    $ocLazyLoadProvider.config({
        // Set to true if you want to see what and when is dynamically loaded
        debug: true
    });

    $stateProvider
        .state('login', {
            url: "/login",
            templateUrl: "/app/login/views/login.html",
            data: { pageTitle: 'Example view' }
        })
        .state('manager', {
            url: "/manager",
            templateUrl: "/app/common/views/content.html",
            data: { pageTitle: 'BS' }
        });
}
mainModuleConfig.$inject = ["$stateProvider", "$urlRouterProvider", "$ocLazyLoadProvider"];


(function () {
    angular.module('bloom', [
        'ui.router',                    // Routing
        'oc.lazyLoad',                  // ocLazyLoad
        'ui.bootstrap',                 // Ui Bootstrap
        'bloom.common',				    // Commmon
        'bloom.dashboard', 				// Dashboard
        'bloom.map',                    // Map
        'bloom.schedule',               // Schedule
        'bloom.technician',             // Technician

        'bloom.workorder',              // Workorder
        'bloom.login'                   // Login
    ])
    .config(mainModuleConfig)
    .run(function($rootScope, $state) {
        $rootScope.$state = $state;
    });    
})();
