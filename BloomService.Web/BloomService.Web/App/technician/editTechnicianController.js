/**
 * editTechnicianController - controller
 */

var editTechnicianController = function($scope, $stateParams) {

	var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    $scope.notAvailable = true;

    // Events
    $scope.events = [
        {title: 'All Day Event',start: new Date(y, m, d)},
        {title: 'All Day Event',start: new Date(y, m, d + 1)},
        {title: 'All Day Event',start: new Date(y, m, d + 2)},
        {title: 'All Day Event',start: new Date(y, m, d + 3)}
    ];

    $scope.alertOnEventClick = function( event, allDay, jsEvent, view ){
        $scope.alertMessage = (event.title + ': Clicked ');
    };

    $scope.alertOnDrop = function(event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view){
        $scope.alertMessage = (event.title +': Droped to make dayDelta ' + dayDelta);
    };

    $scope.alertOnResize = function(event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view ){
        $scope.alertMessage = (event.title +': Resized to make dayDelta ' + minuteDelta);
    };

    /* config object */
    $scope.uiConfig = {
        calendar:{
        	defaultView: 'timelineDay',
            height: 550,
            editable: true,
            businessHours : true,
            header: {
                left: '',
                center: '',
                right: ''
            },
            eventLimit: true,
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize
        }
    };

    /* Event sources array */
    $scope.eventSources = [$scope.events];    

    $scope.saveTechnician = function() {
    	console.log("saveTechnician");
    };    
};
editTechnicianController.$inject = ["$scope", "$stateParams"];