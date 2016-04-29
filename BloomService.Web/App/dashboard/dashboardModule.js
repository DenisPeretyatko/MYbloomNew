/**
 * dashboard - module
 */
"use strict";
 
var dashboardModuleConfig = function($stateProvider) {
    $stateProvider
        .state('manager.dashboard', {
            url: "/dashboard",
            templateUrl: "/app/dashboard/views/dashboard.html",
            controller: "dashboardController",
            data: { pageTitle: 'Example view' },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                    	{
                            files: ['public/js/plugins/footable/footable.all.min.js', 'public/css/plugins/footable/footable.core.css']
                        },
                        {
                            name: 'ui.footable',
                            files: ['public/js/plugins/footable/angular-footable.js']
                        },
                        {
                            name: 'ui.event',
                            files: ['public/js/plugins/uievents/event.js']
                        },
                        {
                            name: 'ui.map',
                            files: ['public/js/plugins/uimaps/ui-map.js']
                        },
                        {
                            serie: true,
                            name: 'angular-flot',
                            files: [ 'public/js/plugins/flot/jquery.flot.js', 'public/js/plugins/flot/jquery.flot.time.js', 'public/js/plugins/flot/jquery.flot.tooltip.min.js', 'public/js/plugins/flot/jquery.flot.spline.js', 'public/js/plugins/flot/jquery.flot.resize.js', 'public/js/plugins/flot/jquery.flot.pie.js', 'public/js/plugins/flot/curvedLines.js', 'public/js/plugins/flot/angular-flot.js', 'public/js/plugins/flot/jquery.flot.categories.js' ]
                        }
                    ]);
                }
            }
        });
}
dashboardModuleConfig.$inject = ["$stateProvider"]; 

angular
    .module('bloom.dashboard', [])
    .config(dashboardModuleConfig)
    .controller('dashboardController', dashboardController)
