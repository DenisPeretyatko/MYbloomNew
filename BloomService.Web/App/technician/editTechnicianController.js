/**
 * editTechnicianController - controller
 */

var editTechnicianController = function ($scope, $stateParams, $state, commonDataService) {
    $scope.technician = {};
    $scope.events = [];
    $scope.createdEvents = [];
    $scope.color = '';
    $scope.obj = {};
    $scope.obj.notAvailable = undefined;

   
    var date = new Date();

    commonDataService.getTechnician($stateParams.id).then(function (response) {
        $scope.technician = response.data;

        $scope.obj.notAvailable = $scope.technician.IsAvailable;
       
         var span = $('.switchery');
        if ($scope.obj.notAvailable == true) {
            span[0].style = "box-shadow: rgb(237, 85, 101) 0px 0px 0px 16px inset; border-color: rgb(237, 85, 101); transition: border 0.4s, box-shadow 0.4s, background-color 1.2s; background-color: rgb(237, 85, 101)";
            span[0].firstChild.style = "left: 20px; transition: left 0.2s;";
        } else {
            span[0].style = "box-shadow: rgb(223, 66, 66) 0px 0px 0px 0px inset; border-color: rgb(223, 66, 66); transition: border 0.4s, box-shadow 0.4s;";
            span[0].firstChild.style = "left: 0px; transition: left 0.2s;";
        }

        if ($scope.technician.AvailableDays != null) {
            angular.forEach($scope.technician.AvailableDays, function (value, key) {
                $scope.events.push({
                    start: new Date(value.Start),
                    end: new Date(value.End),
                    title: value.Title
                });
            });
        }

        if ($scope.technician.Picture != null) {
            $("#avatar").attr('src', $scope.technician.Picture);
        }

        if ($scope.technician.Color != null) {
            $('#colorId .input-group-addon i').css('backgroundColor', $scope.technician.Color);
        }
    });
    

    $scope.alertOnEventClick = function (event, allDay, jsEvent, view) {
        $scope.alertMessage = (event.title + ': Clicked ');
    };

    $scope.alertOnDrop = function (event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view) {
        angular.forEach($scope.events, function (value, key) {
            if (value.__id == event.__id) {
                value.start = event.start;
                value.end = event.end;
            }
        });
        $scope.alertMessage = (event.title + ': Droped to make dayDelta ' + dayDelta);
    };

    $scope.alertOnResize = function (event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view) {
        angular.forEach($scope.events, function (value, key) {
            if (value.__id == event.__id) {
                value.start = event.start;
                value.end = event.end;
            }
        });
        $scope.alertMessage = (event.title + ': Resized to make dayDelta ' + minuteDelta);
    };

    /* config object */
    $scope.uiConfig = {
        calendar: {
            schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
            now: date,
            defaultView: 'agendaWeek',
            height: 630,
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar
            dragRevertDuration: 0,
            eventReceive: function(event) {
                $scope.events.push({ title: event.title, start: event.start, end: event.end });
            },
            eventDragStop: function(event, jsEvent, ui, view) {

                if (isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
                    $('.calendar').fullCalendar('removeEvents', event._id);
                    angular.forEach($scope.events, function (value, key) {
                        if (value.__id == event.__id) {
                            $scope.events.splice($scope.events.indexOf(value), 1);
                        }
                    });
                }
            },
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'agendaWeek'
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            events: $scope.events,
            timezone: 'local',
            forceEventDuration: true
        }
    };

    /* Event sources array */
    $scope.eventSources = $scope.events;    

    $scope.saveTechnician = function () {
        $scope.file = $("#avatar").attr('src');
        var technician = {
            Id: $stateParams.id,
            AvailableDays: $scope.events.concat($scope.createdEvents),
            IsAvailable: $scope.obj.notAvailable,
            Picture: $scope.file,
            Color: $scope.color
        };


        commonDataService.editTechnician(technician).then(function (response) {
            if (response.statusText == 'OK') {
                $state.go('manager.technician.list');
            }
        });
       
    };

    $scope.uploadFile = function (files) {
        if (files && files[0]) {
            var fr = new FileReader();
            fr.onload = function (e) {
                $("#avatar").attr('src', e.target.result);
                $scope.file = e.target.result;
            };
            fr.readAsDataURL(files[0]);
        }
    };

    $('#colorId').colorpicker().on('changeColor', function(e) {
            $scope.color = e.color.toHex();
    });

    var isEventOverDiv = function (x, y) {
        var external_events = $('#external-events-listing');
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

    $('#external-events-listing div').each(function () {
        $(this).data('event', {
            title: 'Not available'
        });

        $(this).draggable({
            zIndex: 999,
            revert: true,
            revertDuration: 0
        });
    });

};
editTechnicianController.$inject = ["$scope", "$stateParams", "$state", "commonDataService"];