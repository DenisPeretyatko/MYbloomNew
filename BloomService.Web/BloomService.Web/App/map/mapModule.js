/**
 * map - module
 */
"use strict";

var mapModuleConfig = function($stateProvider) {
    $stateProvider
        .state('manager.map', {
            url: "/map",
            templateUrl: "/app/map/views/map.html",
            controller: "mapController",
            data: { pageTitle: 'Example view' },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'ui.event',
                            files: ['public/js/plugins/uievents/event.js']
                        },
                        {
                            name: 'ui.map',
                            files: ['public/js/plugins/uimaps/ui-map.js']
                        },
                    ]);
                }
            }
        });
}
mapModuleConfig.$inject = ["$stateProvider"]; 

angular
    .module('bloom.map', [])
    .config(mapModuleConfig)
    .controller('mapController', mapController)