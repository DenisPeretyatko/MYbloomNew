/**
 * workorder - module
 */

 var workorderModuleConfig = function($stateProvider) {
    $stateProvider
        .state('manager.workorder', {
            abstract: true,
            url: '/workorders',
            template: '<ui-view/>'
        })
        .state('manager.workorder.list', {
            url: "",
            templateUrl: "/app/workorders/views/workorders.html",
            controller: "workorderController",
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
        .state('manager.workorder.create', {
            url: "/create",
            templateUrl: "/app/workorders/views/createWorkorder.html",
            controller: "createWorkorderController",
            data: { pageTitle: 'Create workorder' },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'datePicker',
                            files: ['public/css/plugins/datapicker/angular-datapicker.css','public/js/plugins/datapicker/angular-datepicker.js']
                        },
                        {
                            name: 'ui.select',
                            files: ['public/css/plugins/ui-select/select.min.css', 'public/js/plugins/ui-select/select.min.js']
                        },
                        {
                            serie: true,
                            files: ['public/js/plugins/moment/moment.min.js']
                        },
                         {
                             name: 'angucomplete-alt',
                             files: ['public/js/plugins/angucomplete/angucomplete-alt.css', 'public/js/plugins/angucomplete/angucomplete-alt.min.js']
                         },
                    ]);
                }
            }
        })
        .state('manager.workorder.edit', {
            url: "/:id/edit",
            templateUrl: "/app/workorders/views/editWorkorder.html",
            controller: "editWorkorderController",
            data: { pageTitle: 'Create workorder' },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'datePicker',
                            files: ['public/css/plugins/datapicker/angular-datapicker.css','public/js/plugins/datapicker/angular-datepicker.js']
                        },
                        {
                            name: 'ui.select',
                            files: ['public/css/plugins/ui-select/select.min.css', 'public/js/plugins/ui-select/select.min.js']
                        },
                        {
                            serie: true,
                            files: ['public/js/plugins/moment/moment.min.js']
                        },
                         {
                            name: 'angucomplete-alt',
                            files: ['public/js/plugins/angucomplete/angucomplete-alt.css', 'public/js/plugins/angucomplete/angucomplete-alt.min.js']
                        },
                    ]);
                }
            }
        });
}
workorderModuleConfig.$inject = ["$stateProvider"]; 

angular
    .module('bloom.workorder', [])
    .config(workorderModuleConfig)
    .controller('workorderController', workorderController)
    .controller('createWorkorderController', createWorkorderController)
    .controller('editWorkorderController', editWorkorderController)