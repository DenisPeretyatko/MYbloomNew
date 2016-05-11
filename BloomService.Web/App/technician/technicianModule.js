/**
 * technician - module
 */

 var technicianModuleConfig = function($stateProvider) {
    $stateProvider
        .state('manager.technician', {
            abstract: true,
            url: '/technicians',
            template: '<ui-view/>'
        })
        .state('manager.technician.list', {
            url: "",
            templateUrl: "/app/technician/views/technician.html",
            controller: "technicianController",
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
                        }
                    ]);
                }
            }
        })
        .state('manager.technician.edit', {
            url: "/:id/edit",
            templateUrl: "/app/technician/views/editTechnician.html",
            controller: "editTechnicianController",
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
                        {
                            name: 'colorpicker.module',
                            files: ['public/js/plugins/colorpicker/bootstrap-colorpicker-module.js', 'public/css/plugins/colorpicker/colorpicker.css', 'public/js/plugins/colorpicker/bootstrap-colorpicker.min.js']
                        }
                    ]);
                }
            }
        });
}
technicianModuleConfig.$inject = ["$stateProvider"]; 

angular
    .module('bloom.technician', [])
    .config(technicianModuleConfig)
    .controller('technicianController', technicianController)
    .controller('editTechnicianController', editTechnicianController)