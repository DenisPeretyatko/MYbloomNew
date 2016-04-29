/**
 *  Commmon module configuration. Contains all directives and common services
 */

var commonModuleConfig = function($stateProvider) {
    $stateProvider
        .state('bloom', {
            abstract: true,
            url: "/index",
            templateUrl: "/app/common/views/content.html",
        });
}
commonModuleConfig.$inject = ["$stateProvider"];

angular
    .module('bloom.common', [])
    .directive('pageTitle', pageTitle)
    .directive('sideNavigation', sideNavigation)
    .directive('iboxTools', iboxTools)
    .directive('minimalizaSidebar', minimalizaSidebar)
    .directive('iboxToolsFullScreen', iboxToolsFullScreen)
    .directive('back', backButton)
    .constant('flotChartOptions', flotChartOptions)
    .constant('googleMapStyles', googleMapStyles)
    .constant('googleMapOptions', googleMapOptions)
    .controller('mainController', mainController)
    .service('commonDataService', commonDataService)
    .service('notify', commonNotificationService)
    .service('state', commonStateManager)
    .config(commonModuleConfig);
    