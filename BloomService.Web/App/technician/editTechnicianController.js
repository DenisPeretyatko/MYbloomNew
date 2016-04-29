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
        { resourceId: 'Sun', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) },
        { resourceId: 'Mon', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) },
        { resourceId: 'Tue', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) },
        { resourceId: 'Wed', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) },
        { resourceId: 'Thu', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) },
        { resourceId: 'Fri', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) },
        { resourceId: 'Sat', title: 'Available', start: new Date(y, m, d, 8), end: new Date(y, m, d, 18) }
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
        	schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        	now: date,
        	defaultView: 'timelineDay',
            height: 280,
            resourceAreaWidth: 250,
            editable: true,
            businessHours : true,
            eventOverlap: false,
            disableDragging : true,
            header: {
                left: '',
                center: '',
                right: ''
            },
            eventLimit: true,
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            resourceLabelText: 'Availability',
			resources: [
				{ id: 'Sun', title: 'Sunday' },
				{ id: 'Mon', title: 'Monday' },
				{ id: 'Tue', title: 'Tuesday' },
				{ id: 'Wed', title: 'Wednesday' },
				{ id: 'Thu', title: 'Thursday' },
				{ id: 'Fri', title: 'Friday' },
				{ id: 'Sat', title: 'Saturday' }
			]
        }
    };

    /* Event sources array */
    $scope.eventSources = [$scope.events];    

    $scope.saveTechnician = function() {
    	console.log("saveTechnician");
    };    
};
editTechnicianController.$inject = ["$scope", "$stateParams"];