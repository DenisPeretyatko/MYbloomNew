/**
 * scheduleController - controller
 */

var scheduleController = function($scope, $interpolate, $timeout, commonDataService) {
    var date = new Date();

    // Events
    $scope.events = [];

    $scope.resources = [];

    /* message on eventClick */
    $scope.alertOnEventClick = function( event, allDay, jsEvent, view ){
        $scope.alertMessage = (event.title + ': Clicked ');
    };
    /* message on Drop */
    $scope.alertOnDrop = function(event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view){
        $scope.alertMessage = (event.title + ': Droped to make dayDelta ' + dayDelta);
        saveEvent(event);
    };
    /* message on Resize */
    $scope.alertOnResize = function(event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        $scope.alertMessage = (event.title + ': Resized to make dayDelta ' + minuteDelta);
        saveEvent(event);
    };


    var saveEvent = function(event) {
        var workorder = event.title;

        var start = new Date(event.start);
        var end = new Date(event.end);
        var estimate = end.getHours() - start.getHours();

        var assignment = {
            ScheduleDate: start,
            Employee: event.resourceId,
            WorkOrder: workorder,
            EstimatedRepairHours: estimate,
            EndDate: end,
        };
        commonDataService.assignWorkorder(assignment);
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
            events: $scope.events,
            eventRender: function (event, element) {
                element.qtip({
                    content: event.description
                });
                $('#calendar').find("div[style='height: 34px;']").each(function (i, el) {
                    $(this).css('height', '28px');
                });
                $('#calendar').find("div[style='height: 8px;']").each(function (i, el) {
                    $(this).css('height', '28px');
                });
               
            },
            droppable: true, // this allows things to be dropped onto the calendar
            dragRevertDuration: 0,
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'timelineWeek,timelineDay'
            },
            drop: function() {
                $(this).remove();
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            resourceRender: function(resource, labelTds, bodyTds) {
                var cell = '<span class="client-avatar" style="height: 40px;"><img alt="image" src="{{avatarUrl}}">&nbsp;' + 
                           '<a class="client-link">{{title}}</a></span>';

                labelTds.html($interpolate(cell)(resource));
                //bodyTds.html(cell);
                $('#calendar').find("div[style='height: 34px;']").each(function (i, el) {
                    $(this).css('height', '28px');
                });
            },
            eventDragStop: function( event, jsEvent, ui, view ) {
                
                if(isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
                    $('#calendar').fullCalendar('removeEvents', event._id);
                    var innerHtml = "<td>" + event.title + "</td>" + "<td>" + event.dateFoo + "</td>" + "<td>" + event.customerFoo + "</td>" +
                                    "<td>" + event.locationFoo + "</td>" + "<td>" + parseInt(event.hourFoo) + "</td>";
                    var el = $("<tr class='drag fc-event'>").appendTo('.footable tbody').html(innerHtml);
                    el.draggable({
                        zIndex: 999,
                        revert: true, 
                        revertDuration: 0 
                    });
                    el.data('event', { title: event.title, id: event.id, stick: true });
                    

                    var workorder = event.title;

                    var start = new Date(event.start);
                    var end = new Date(event.end);
                    var estimate = end.getHours() - start.getHours();

                    var assignment = {
                        ScheduleDate: start,
                        Employee: event.resourceId,
                        WorkOrder: workorder,
                        EstimatedRepairHours: estimate,
                        EndDate: end,
                    };
                    commonDataService.unAssignWorkorder(assignment);
                }
                $('#calendar').find("div[style='height: 34px;']").each(function (i, el) {
                    $(this).css('height', '28px');
                });
                $('#calendar').find("div[style='height: 8px;']").each(function (i, el) {
                    $(this).css('height', '28px');
                });
           
            },
            eventReceive: function(event) {
                 saveEvent(event);
            },
            resourceLabelText: 'Technicians',
            resources: $scope.resources,
            //timezone: 'local',
            forceEventDuration: true,
        }
    };

    var isEventOverDiv = function (x, y) {
        var external_events = $('.dragdpor_section');
        var offset = external_events.offset();
        offset.right = external_events.width() + offset.left;
        offset.bottom = external_events.height() + offset.top;
        var scroll = $(window).scrollTop();

        if (x >= offset.left
            && y + scroll >= offset.top
            && x <= offset.right
            && y + scroll <= offset.bottom) { return true; }
        return false;

    }

    $scope.eventSources = $scope.events;
    $scope.resouceSources = [$scope.resources];

    commonDataService.getTechnicians().then(function(response){
        $scope.activeTechnicians = response.data;
        angular.forEach($scope.activeTechnicians, function(value, key) {
            if (value != null) {
                this.push({
                    id : value.Employee,
                    title : value.Name,
                    avatarUrl : value.Picture != null? value.Picture : "public/images/user.png"
                });
            }
        }, $scope.resources);
    });

    commonDataService.getSchedule().then(function(response) {
        var schedule = response.data;
        $scope.unassignedWorkorders = schedule.UnassignedWorkorders;
        angular.forEach($scope.unassignedWorkorders, function(value, key) {
            if (value != null) {
                value.DateEntered = formatDate(new Date(value.DateEntered));
            };
        });
        $scope.assigments = schedule.Assigments;
        angular.forEach($scope.assigments, function (value, key) {
            if (value != null) {
                var spliter = (value.Customer == '' || value.Location == '') ? '' : '/';
                $scope.events.push({
                    id: value.Assignment,
                    resourceId: value.EmployeeId,
                    title: value.WorkOrder,
                    start: value.Start,
                    end: value.End,
                    assigmentId: value.Assigments,
                    workorderId: value.WorkOrder,
                    description: value.Customer + spliter + value.Location,
                    dateFoo: value.DateEntered,
                    customerFoo: value.Customer,
                    locationFoo: value.Location,
                    hourFoo: value.EstimatedRepairHours
                });
            }
        });

        $timeout(function () {
            $('.drag').each(function () {
                var descr = '';
                $(this).find('td').each(function (i, e) {
                    if (i == 2) {
                        var spliter = (e.innerText == '' || e[i + 1] == '') ? '' : '/';
                        descr += e.innerText + spliter;
                    }
                    if (i == 3) {
                        descr += e.innerText;
                    }
                });
                var startDate = new Date();
                var endDate = new Date(startDate);

                var rows = $(this).find('td');

                $(this).data('event', {
                    title: parseInt($(this).find('td').first().text()), //textTitle,
                    start: startDate,
                    end: endDate.setHours(startDate.getHours() + parseInt(rows.last().text())),
                    workorderId: rows.first().text(),
                    description: descr
            });

                $(this).draggable({
                    zIndex: 999,
                    revert: true,
                    revertDuration: 0
                });
            });

            function resizeGhost(event, ui) {
                var helper = ui.helper;
                var element = $(event.target);
                helper.width(element.width());
                helper.height(element.height());
            }

        }, 100);
    });

    function formatDate(inputdate) {
        return inputdate.getMonth() + 1 + "-" + inputdate.getDate() + "-" + inputdate.getFullYear();
    }

};
scheduleController.$inject = ["$scope", "$interpolate", "$timeout", "commonDataService"];