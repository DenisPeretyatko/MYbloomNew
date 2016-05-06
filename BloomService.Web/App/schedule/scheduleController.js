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
            events: $scope.events,
            eventRender: function(event, element) {
                var columns = event.title.split("/");
                var workorder = columns[0];

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
                    var columns = event.title.split("/");
                    var innerHtml = "";
                    columns.forEach(function(item, i) {
                        innerHtml += "<td>" + columns[i] + "</td>";
                    });
                    var el = $("<tr class='drag fc-event'>").appendTo('.footable tbody').html(innerHtml);
                    el.draggable({
                        zIndex: 999,
                        revert: true, 
                        revertDuration: 0 
                    });
                    el.data('event', { title: event.title, id: event.id, stick: true });
                }
            },
            resourceLabelText: 'Technicians',
            resources: $scope.resources,
            timezone: 'local',
            forceEventDuration: true,
        }
    };

    var isEventOverDiv = function (x, y) {
        var external_events = $('.dragdpor_section');
        var offset = external_events.offset();
        offset.right = external_events.width() + offset.left;
        offset.bottom = external_events.height() + offset.top;

        if (x >= offset.left
            && y >= offset.top
            && x <= offset.right
            && y <= offset.bottom) { return true; }
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
                    avatarUrl : value.avatarUrl
                });
            }
        }, $scope.resources);
    });

    commonDataService.getSchedule().then(function(response) {
        var schedule = response.data;
        $scope.unassignedWorkorders = unassignedWO;//schedule.UnassignedWorkorders; --- real data
        $scope.assigments = schedule.Assigments;
        angular.forEach($scope.assigments, function (value, key) {
            if (value != null) {
                this.push({
                    id: value.Assignment,
                    resourceId: value.EmployeeId,
                    title: value.Center,
                    start: value.Start,
                    end: value.End,
                    durationEditable: false,
                    editable: false
                });
            }
        }, $scope.events);

        $timeout(function () {
            $('.drag').each(function () {
                var textTitle = '';
                $(this).find('td').each(function() {
                    textTitle += $(this).text() + '/';
                });
                var startDate = new Date();
                var endDate = new Date(startDate);


                $(this).data('event', {
                    title: textTitle,
                    start: startDate,
                    end: endDate.setHours(startDate.getHours() + parseInt($(this).find('td').last().text())),
                    durationEditable: false,
                    stick: true,
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
    //------------------------ test data for unassigned work order --------------------------------------
    var unassignedWO = [
        {
            "WorkOrder": 11245,
            "CallType": "Routine Leak T & M",
            "Problem": "Curb",
            "EstimatedRepairHours": 3,
            "Priority": "Normal",
            "Agreement": 0,
            "AgreemntPeriod": 0,
            "AgreemntPeriodSpecified": true,
            "Center": "Bloom Roofing Systems, Inc - Service",
            "Area": "Michigan",
            "Name": "Target - Warren - #2544",
            "Contact": "MOD",
            "DateEntered": "28-04-2016",
            "DateEnteredSpecified": true,
            "TimeEntered": "/Date(-62135550351000)/",
            "TimeEnteredSpecified": true,
            "EnteredBy": "Kris",
            "Employee": "Alley; Christopher",
            "DateRun": "",
            "DateComplete": "",
            "TimeComplete": "/Date(-62135578800000)/",
            "TimeCompleteSpecified": true,
            "CompletedBy": 0,
            "CompletedBySpecified": true,
            "Status": "Open",
            "CustomerPO": "",
            "QuoteExpirationDate": "",
            "TaxatCenter": "No",
            "Amount": 0,
            "AmountSpecified": true,
            "SalesTaxAmount": 0,
            "SalesTaxAmountSpecified": true,
            "AmountBilled": 0,
            "AmountBilledSpecified": true,
            "TotalCost": 0,
            "TotalCostSpecified": true,
            "LeadSource": "",
            "Comments": "This is a test work order!!!",
            "Equipment": 0,
            "EquipmentSpecified": true,
            "WorkOrderType": "Service",
            "PayMethod": "Bill Out",
            "PreventiveMaintenance": "No",
            "RateSheet": "Standard Call Rate",
            "SalesEmployee": "Kyle Menard",
            "JobSaleProduct": "",
            "EstimatedPartsCost": 0,
            "EstimatedPartsCostSpecified": true,
            "EstimatedLaborCost": 0,
            "EstimatedLaborCostSpecified": true,
            "EstimatedMiscCost": 0,
            "EstimatedMiscCostSpecified": true,
            "ActualPartsCost": 0,
            "ActualPartsCostSpecified": true,
            "ActualLaborCost": 0,
            "ActualLaborCostSpecified": true,
            "ActualMiscCost": 0,
            "ActualMiscCostSpecified": true,
            "ActualLaborHours": 0,
            "ActualLaborHoursSpecified": true,
            "AlternateWorkOrderNbr": "",
            "Lead": 0,
            "LeadSpecified": true,
            "Misc": "",
            "CallDate": "/Date(1453525200000)/",
            "CallDateSpecified": true,
            "CallTime": "/Date(-62135528400000)/",
            "CallTimeSpecified": true,
            "JCJob": "",
            "JCExtra": "",
            "Location": "Target - Warren - #2544",
            "ARCustomer": "ACP",
            "Department": "Service",
            "NonBillable": "No",
            "ChargeBillto": "",
            "PermissionCode": "uyg",
            "SalesTaxBilled": 0,
            "InvoiceDate": "",
            "DateClosed": "",
            "NottoExceed": "Blah blah blah.",
            "AgreemntPeriCustomer": null
        },
        {
            "WorkOrder": 11246,
            "CallType": "Routine Leak T & M",
            "Problem": "Roof Leak",
            "EstimatedRepairHours": 1,
            "Priority": "Normal",
            "Agreement": 0,
            "AgreemntPeriod": 0,
            "AgreemntPeriodSpecified": true,
            "Center": "Bloom Roofing Systems, Inc - Service",
            "Area": "Michigan",
            "Name": "Beyster Land Company",
            "Contact": "Pierre",
            "DateEntered": "28-04-2016",
            "DateEnteredSpecified": true,
            "TimeEntered": "/Date(-62135547407000)/",
            "TimeEnteredSpecified": true,
            "EnteredBy": "Kris",
            "Employee": "O'Sullivan; Thomas",
            "DateRun": "",
            "DateComplete": "",
            "TimeComplete": "/Date(-62135578800000)/",
            "TimeCompleteSpecified": true,
            "CompletedBy": 0,
            "CompletedBySpecified": true,
            "Status": "Open",
            "CustomerPO": "ws",
            "QuoteExpirationDate": "",
            "TaxatCenter": "No",
            "Amount": 0,
            "AmountSpecified": true,
            "SalesTaxAmount": 0,
            "SalesTaxAmountSpecified": true,
            "AmountBilled": 0,
            "AmountBilledSpecified": true,
            "TotalCost": 0,
            "TotalCostSpecified": true,
            "LeadSource": "",
            "Comments": "ws",
            "Equipment": 0,
            "EquipmentSpecified": true,
            "WorkOrderType": "Service",
            "PayMethod": "Check",
            "PreventiveMaintenance": "No",
            "RateSheet": "Standard Call Rate",
            "SalesEmployee": "Kyle Menard",
            "JobSaleProduct": "",
            "EstimatedPartsCost": 0,
            "EstimatedPartsCostSpecified": true,
            "EstimatedLaborCost": 0,
            "EstimatedLaborCostSpecified": true,
            "EstimatedMiscCost": 0,
            "EstimatedMiscCostSpecified": true,
            "ActualPartsCost": 0,
            "ActualPartsCostSpecified": true,
            "ActualLaborCost": 0,
            "ActualLaborCostSpecified": true,
            "ActualMiscCost": 0,
            "ActualMiscCostSpecified": true,
            "ActualLaborHours": 0,
            "ActualLaborHoursSpecified": true,
            "AlternateWorkOrderNbr": "",
            "Lead": 0,
            "LeadSpecified": true,
            "Misc": "",
            "CallDate": "/Date(1459828800000)/",
            "CallDateSpecified": true,
            "CallTime": "/Date(-62135578800000)/",
            "CallTimeSpecified": true,
            "JCJob": "",
            "JCExtra": "",
            "Location": "Beyster Land Company",
            "ARCustomer": "NBS",
            "Department": "Service",
            "NonBillable": "No",
            "ChargeBillto": "",
            "PermissionCode": "ws",
            "SalesTaxBilled": 0,
            "InvoiceDate": "",
            "DateClosed": "",
            "NottoExceed": "ws",
            "AgreemntPeriCustomer": null
        }
    ];

};
scheduleController.$inject = ["$scope", "$interpolate", "$timeout", "commonDataService"];