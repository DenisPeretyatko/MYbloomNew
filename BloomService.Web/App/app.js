/**
 * Bloom service angular application
 */
var mainModuleConfig = function($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
    $urlRouterProvider.otherwise("/manager");

    $ocLazyLoadProvider.config({
        // Set to true if you want to see what and when is dynamically loaded
        debug: true
    });

    $stateProvider
        .state('manager', {
            url: "/manager",
            templateUrl: "/app/login/views/login.html",
            data: { pageTitle: 'Example view' }
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
