/**
 * scheduleController - controller
 */

var scheduleController = function($scope, $interpolate, $timeout, commonDataService) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    // Events
    $scope.events = [
        { title: 'All Day Event',start: new Date(y, m, d) },        
    ];

    $scope.resources = [];

    /* message on eventClick */
    $scope.alertOnEventClick = function( event, allDay, jsEvent, view ){
        $scope.alertMessage = (event.title + ': Clicked ');
    };
    /* message on Drop */
    $scope.alertOnDrop = function(event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view){
        $scope.alertMessage = (event.title +': Droped to make dayDelta ' + dayDelta);
    };
    /* message on Resize */
    $scope.alertOnResize = function(event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        $scope.alertMessage = (event.title +': Resized to make dayDelta ' + minuteDelta);
    };

    /* config object */
    $scope.uiConfig = {
        calendar:{
            schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
            now: date,
            defaultView: 'timelineDay',
            height: 400,
            resourceAreaWidth: '15%',
            editable: true,
            eventOverlap: false,
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'timelineMonth,timelineWeek,timelineDay'
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            resourceRender: function(resource, labelTds, bodyTds) {
                var cell = '<span class="client-avatar" style="height: 40px;"><img alt="image" src="{{avatarUrl}}">&nbsp;' + 
                           '<a class="client-link">{{title}}</a></span>';

                labelTds.html($interpolate(cell)(resource));
                //bodyTds.html(cell);
            },
            resourceLabelText: 'Technicians',
            resources: $scope.resources,
        }
    };

    $scope.eventSources = [$scope.events];
    $scope.resouceSources = [$scope.resources];

    commonDataService.getTechnicians().then(function(response){
        $scope.activeTechnicians = response.data;
        angular.forEach($scope.activeTechnicians, function(value, key) {
            if (value != null) {
                this.push({
                    id : value.number,
                    title : value.fullName,
                    avatarUrl : value.avatarUrl
                });
            }
        }, $scope.resources);
    });

    commonDataService.getSchedule().then(function(response) {
        var schedule = response.data;
        $scope.unassignedWorkorders = schedule.unassignedWorkorders;
        $scope.assigments = schedule.assigments;

        $timeout(function() {
            $(".footable").data('footable').redraw();
            $(".footable").data('footable').bind({
                'footable_redrawn' : function(e) {
                    $(".drag").each(function() {
                        console.log(this);

                        $(this).data("event", {
                            title: $.trim($(this).text()),
                            stick: true 
                        });

                        $(this).draggable({
                            zIndex: 999,
                            revert: 'invalid',
                            handle: 'span',
                            revertDuration: 0
                        });
                    });
                }
            });

            function resizeGhost(event, ui) {
                var helper = ui.helper;
                var element = $(event.target);
                helper.width(element.width());
                helper.height(element.height());
            }

        }, 100);
    });
};
scheduleController.$inject = ["$scope", "$interpolate", "$timeout", "commonDataService"];