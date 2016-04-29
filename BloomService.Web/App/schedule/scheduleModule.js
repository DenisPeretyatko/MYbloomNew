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
                            serie: true,
                            insertBefore: '#loadBefore',
                            files: ['public/js/plugins/moment/moment.min.js', 'public/css/plugins/fullcalendar/fullcalendar.css','public/js/plugins/fullcalendar/fullcalendar.min.js', 'public/css/plugins/fullcalendar-scheduler/scheduler.min.css','public/js/plugins/fullcalendar-scheduler/scheduler.min.js']
                        },
                        {
                            name: 'ui.calendar',
                            files: ['public/js/plugins/fullcalendar/calendar.js']
                        },
                        {
                            name: 'ui.switchery',
                            files: ['public/css/plugins/switchery/switchery.css','public/js/plugins/switchery/switchery.js','public/js/plugins/switchery/ng-switchery.js']
                        },
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