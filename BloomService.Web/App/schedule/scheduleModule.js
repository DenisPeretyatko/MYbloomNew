/**
 * schedule - module
 */

 var scheduleModuleConfig = function($stateProvider) {
    $stateProvider
        .state('manager.schedule', {
            url: "/schedule",
            templateUrl: "/app/schedule/views/schedule.html",
            controller: "scheduleController",
            data: { pageTitle: 'Example view' },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            insertBefore: '#loadBefore',
                            files: ['public/css/plugins/fullcalendar/fullcalendar.css','public/js/plugins/fullcalendar/fullcalendar.min.js']
                        },
                        {
                            name: 'ui.calendar',
                            files: ['public/js/plugins/fullcalendar/calendar.js']
                        }
                    ]);
                }
            }
        });
}
scheduleModuleConfig.$inject = ["$stateProvider"]; 

angular
    .module('bloom.schedule', [])
    .config(scheduleModuleConfig)
    .controller('scheduleController', scheduleController)