/**
 *  Commmon module configuration. Contains all directives and common services
 */

"use strict";

var commonModuleConfig = function ($stateProvider) {
    $stateProvider
        .state('manager.common', {
            url: "/common",
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
    .directive('loadingBar', loadingBar) 
    .directive('interceptor', interceptor)
    .constant('flotChartOptions', flotChartOptions)
    .constant('googleMapStyles', googleMapStyles)
    .constant('googleMapOptions', googleMapOptions)
    .controller('mainController', mainController)
    .service('commonDataService', commonDataService)
    .service('notify', commonNotificationService)
    .service('state', commonStateManager)
    .service('commonHub', commonHub)
     .service('loadingBarService', loadingBarService)
    .config(commonModuleConfig)
     .config(loadingBarConfig);


