/**
 * scheduleController - controller
 */

var scheduleController = function($scope, commonDataService) {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    // Events
    $scope.events = [
        {title: 'All Day Event',start: new Date(y, m, d)},        
    ];


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
            height: 400,
            defaultView: 'agendaWeek',
            editable: true,
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize
        }
    };

    /* Event sources array */
    $scope.eventSources = [$scope.events];

    commonDataService.getTechnicians().then(function(response){
        $scope.activeTechnicians = response.data;
    });

    commonDataService.getSchedule().then(function(response) {
        var schedule = response.data;
        $scope.unassignedWorkorders = schedule.unassignedWorkorders;
        $scope.assigments = schedule.assigments;
    });
};
scheduleController.$inject = ["$scope", "commonDataService"];