/**
 * scheduleController - controller
 */

var scheduleController = function($scope, $interpolate, $timeout, commonDataService) {
    var date = new Date();

    // Events
    $scope.events = [];
    var tempEvents = [];

    $scope.resources = [];

    /* message on eventClick */
    $scope.alertOnEventClick = function( event, allDay, jsEvent, view ){
        $scope.alertMessage = (event.title + ': Clicked ');
    };
    /* message on Drop */
    $scope.alertOnDrop = function(event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view){
        $scope.alertMessage = (event.title + ': Droped to make dayDelta ' + dayDelta);
        event = setTechnicianColor(event);
        $('#calendar').fullCalendar('rerenderEvents');
        saveEvent(event);
    };
    /* message on Resize */
    $scope.alertOnResize = function(event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        $scope.alertMessage = (event.title + ': Resized to make dayDelta ' + minuteDelta);
        event = setTechnicianColor(event);
        $('#calendar').fullCalendar('rerenderEvents');
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
    var setTechnicianColor = function (event) {
        var resource = $scope.resources.find(function (element) {
            return element.id === event.resourceId;
        });
        event.color = resource.color;
        return event;
    }

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
                var cell = '<div style="height: 34px;">' +
                    '<span class="client-avatar"><img alt="image" src="{{avatarUrl}}" style="height: 28px; margin: 3px;">&nbsp;</span>' +
                    '<span class="fc-cell-text">{{title}}</span>' +
                    '</div>';
                labelTds.html($interpolate(cell)(resource));
            },
            eventDragStop: function( event, jsEvent, ui, view ) {
                
                if(isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
                    var innerHtml = "<td>" + event.title + "</td>" + "<td>" +  formatDate(new Date(event.dateFoo)) + "</td>" + "<td>" + event.customerFoo + "</td>" +
                                    "<td>" + event.locationFoo + "</td>" + "<td>" + parseInt(event.hourFoo) + "</td>";
                    var el = $("<tr class='drag fc-event' style='z-index: 99'>").appendTo('.footable tbody').html(innerHtml);
                    el.draggable({
                        zIndex: 999,
                        revert: true, 
                        revertDuration: 0 
                    });
                    el.data('event', {
                        title: event.title,
                        id: event.id,
                        start: event.start,
                        end: event.end,
                        workorderId: event.workorderId,
                        description: event.description,
                        dateFoo: event.dateFoo,
                        customerFoo: event.customerFoo,
                        locationFoo: event.locationFoo,
                        hourFoo: parseInt(event.hourFoo),
                        durationEditable: false
                    });

                    $('#calendar').fullCalendar('removeEvents', event._id);

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
            },
            eventReceive: function (event) {
                event = setTechnicianColor(event);
                var date = new Date(event.start);
                event.end._d = new Date(date.setHours(date.getHours() + parseInt(event.hourFoo)));
                $('#calendar').fullCalendar('rerenderEvents');
                saveEvent(event);
            },
            resourceLabelText: 'Technicians',
            timezone: 'local',
            resources: $scope.resources,
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

    $scope.eventSources = [$scope.events];
    $scope.resouceSources = [$scope.resources];

    commonDataService.getTechnicians().then(function(response){
        $scope.activeTechnicians = response.data;
        angular.forEach($scope.activeTechnicians, function(value, key) {
            if (value != null) {
                this.push({
                    id : value.Employee,
                    title : value.Name,
                    avatarUrl: value.Picture != null ? value.Picture : "public/images/user.png",
                    color: value.Color == "" ? "" : value.Color
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
                tempEvents.push({
                    id: value.Assignment,
                    resourceId: value.EmployeeId,
                    title: value.WorkOrder,
                    start: new Date(value.Start +' UTC'),
                    end: new Date(value.End +' UTC'),
                    assigmentId: value.Assigment,
                    workorderId: value.WorkOrder,
                    description: value.Customer + spliter + value.Location,
                    dateFoo: value.DateEntered,
                    customerFoo: value.Customer,
                    locationFoo: value.Location,
                    hourFoo: value.EstimatedRepairHours,
                    color: value.Color == "" ? "" : value.Color,
                    durationEditable: false
            });
            }
        });
        $scope.events = tempEvents;
        $("#calendar").fullCalendar('addEventSource', $scope.events);

        $timeout(function () {
            $('.drag').each(function () {
                var descr = '';
                var fooElements = [];
                $(this).find('td').each(function (i, e) {
                    if (i == 2) {
                        var spliter = (e.innerText == '' || e[i + 1] == '') ? '' : '/';
                        descr += e.innerText + spliter;
                    }
                    if (i == 3) {
                        descr += e.innerText;
                    }
                    fooElements[i] = e.innerText;
                });
                var startDate = new Date();
                var endDate = new Date(startDate);

                var rows = $(this).find('td');

                $(this).data('event', {
                    title: parseInt($(this).find('td').first().text()), //textTitle,
                    start: startDate,
                    end: endDate.setHours(startDate.getHours() + parseInt(rows.last().text())),
                    workorderId: rows.first().text(),
                    description: descr,
                    dateFoo: fooElements[1],
                    customerFoo: fooElements[2],
                    locationFoo: fooElements[3],
                    hourFoo: fooElements[4],
                    durationEditable: false
            });

                $(this).draggable({
                    zIndex: 999,
                    revert: true,
                    revertDuration: 0
                });
            });
        }, 100);
    });

    function formatDate(inputdate) {
        return inputdate.getMonth() + 1 + "-" + inputdate.getDate() + "-" + inputdate.getFullYear();
    }

};
scheduleController.$inject = ["$scope", "$interpolate", "$timeout", "commonDataService"];