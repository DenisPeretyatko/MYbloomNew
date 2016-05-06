/**
 * editTechnicianController - controller
 */

var editTechnicianController = function ($scope, $stateParams, $state, commonDataService) {
    $scope.technician = {};
    $scope.events = [];

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    commonDataService.getTechnician($stateParams.id).then(function (response) {
        $scope.technician = response.data;

        $scope.notAvailable = $scope.technician.IsAvailable;
        
        if ($scope.technician.AvailableDays != null) {
            angular.forEach($scope.technician.AvailableDays, function (value, key) {
                if (value != null) {
                    $scope.events.push({
                        start: new Date(value.Start),
                        end: new Date(value.End),
                        resourceId: value.ResourceId,
                        title: value.Title,
                    });
                }
            });
        } else {
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
        }

        if ($scope.technician.Picture != null) {
            $("#avatar").attr('src', $scope.technician.Picture);
        }
    });
    

    $scope.alertOnEventClick = function (event, allDay, jsEvent, view) {
        $scope.alertMessage = (event.title + ': Clicked ');
    };

    $scope.alertOnDrop = function (event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view) {
        angular.forEach($scope.events, function (value, key) {
            if (value._id == event._id) {
                value.start = event.start;
                value.end = event.end;
            }
        });
        $scope.alertMessage = (event.title + ': Droped to make dayDelta ' + dayDelta);
    };

    $scope.alertOnResize = function (event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view) {
        angular.forEach($scope.events, function (value, key) {
            if (value._id == event._id) {
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
            defaultView: 'timelineDay',
            height: 280,
            resourceAreaWidth: 250,
            editable: true,
            businessHours: true,
            eventOverlap: false,
            disableDragging: true,
            header: {
                left: '',
                center: '',
                right: ''
            },
            eventLimit: true,
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            events: $scope.eventSources,
            resourceLabelText: 'Availability',
            resources: [
				{ id: 'Sun', title: 'Sunday' },
				{ id: 'Mon', title: 'Monday' },
				{ id: 'Tue', title: 'Tuesday' },
				{ id: 'Wed', title: 'Wednesday' },
				{ id: 'Thu', title: 'Thursday' },
				{ id: 'Fri', title: 'Friday' },
				{ id: 'Sat', title: 'Saturday' }
            ],
            timezone: 'local'
        }
    };

    /* Event sources array */
    $scope.eventSources = [$scope.events];    

    $scope.saveTechnician = function () {
        var technician = {
            Id: $stateParams.id,
            AvailableDays: $scope.events,
            IsAvailable: $scope.notAvailable,
            Picture: $scope.file
        };
        commonDataService.editTechnician(technician).then(function (response) {
            if (response.data == 'success')
                $state.go('manager.technician.list');
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
};
editTechnicianController.$inject = ["$scope", "$stateParams", "$state", "commonDataService"];