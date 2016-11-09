
/**
 * scheduleController - controller
 */

var scheduleController = function ($rootScope, $scope, $interpolate, $timeout, $q, commonDataService) {
    var date = new Date();
    var offset = -5.0;
    var utc = date.getTime() + (date.getTimezoneOffset() * 60000);
    var serverDate = new Date(utc + (3600000 * offset));

    var remainsFullHours = function (start, end) {
        var dateDifference = end.getTime() - start.getTime();
        var remainsDate = new Date(dateDifference);
        var remainsSec = (parseInt(remainsDate / 1000));
        var remainsFullDays = (parseInt(remainsSec / (24 * 60 * 60)));
        var secInLastDay = remainsSec - remainsFullDays * 24 * 3600;
        return parseInt(secInLastDay / 3600);
    }
    // Events
    $scope.events = [];
    var tempEvents = [];
    $rootScope.unavailableTechniciansIds = [];
    $scope.resources = [];
    var prevDivState = {};

    /* message on eventClick */
    $scope.alertOnEventClick = function (event, allDay, jsEvent, view) {
        $scope.alertMessage = (event.title + ": Clicked ");
    };
    /* message on Drop */
    $scope.alertOnDrop = function (event, delta, revertFunc, jsEvent, ui, view, dayDelta, minuteDelta, allDay) {
        var now = new Date();
        var eventDate = new Date(event._start._d.getTime() + (now.getTimezoneOffset() * 60000));
        if (!$rootScope.unavailableTechniciansIds.includes(parseInt(event.resourceId)) && serverDate <= eventDate) {
            $scope.alertMessage = (event.title + ": Droped to make dayDelta " + dayDelta);
            event = setTechnicianColor(event);
            $("#calendar").fullCalendar("rerenderEvents");
            saveEvent(event);
        }
        else {
            revertFunc();
        }
    };
    /* message on Resize */
    $scope.alertOnResize = function (event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view) {
        $scope.alertMessage = (event.title + ": Resized to make dayDelta " + minuteDelta);
        event = setTechnicianColor(event);
        $("#calendar").fullCalendar("rerenderEvents");
        saveEvent(event);
    };


    var saveEvent = function (event) {
        var workorder = event.workorderId;
        var now = new Date();
        var start = new Date(event._start._d.getTime() + (now.getTimezoneOffset() * 60000));
        var end = new Date(event._end._d.getTime() + (now.getTimezoneOffset() * 60000));


        var estimate = remainsFullHours(start, end);

        var assignment = {
            ScheduleDate: start,
            Employee: event.resourceId,
            WorkOrder: workorder,
            EstimatedRepairHours: estimate,
            EndDate: end
        };

        commonDataService.assignWorkorder(assignment);
    };
    var setTechnicianColor = function (event) {
        var resource = $scope.resources.find(function (element) {
            return element.id.toString() === event.resourceId;
        });
        event.color = resource.color;
        return event;
    }

    var getBusinesHours = function () {
        var result = [];
        angular.forEach($scope.activeTechnicians, function (value, key) {
            if (value.AvailableDays != null) {
                angular.forEach(value.AvailableDays, function (item, key) {
                    var event = {
                        start: item.Start,
                        end: item.End,
                        color: '#D3D3D3',
                        rendering: 'background',
                        resourceId: value.Employee,
                        title: "Unavailable time"
                    }
                    result.push(event);
                });
            }
        });
        return result;
    }

    /* config object */
    $scope.uiConfig = {
        calendar: {
            schedulerLicenseKey: "CC-Attribution-NonCommercial-NoDerivatives",
            now: serverDate,
            defaultView: "timelineDay",
            height: 400,
            resourceAreaWidth: "15%",
            editable: true,
            timezone: "America/New_York",
            events: $scope.events,
            eventDragStart:
                function (event, element) {
                    eventBeforeDrag = event;
                },
            eventRender: function (event, element) {
                var qtip = $("div.qtip:visible");

                qtip.remove();
                element.qtip({
                    content: event.description
                });
            },
            eventConstraint: {
                start: moment(serverDate).format("YYYY-MM-DD HH:mm"),
                end: "2999-01-01" // hard coded goodness unfortunately
            },
            droppable: true,
            dragRevertDuration: 0,
            header: {
                left: "prev,next",
                center: "title",
                right: "timelineWeek,timelineDay"
            },
            drop: function () {
                $(this).remove();
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            eventOverlap: function (stillEvent, movingEvent) {
                return false;
            },
            resourceRender: function (resource, labelTds, bodyTds) {
                var cell = '<div style="height: 34px;">' +
                    '<span class="client-avatar"><img alt="image" src="{{avatarUrl}}" style="height: 28px; margin: 3px;">&nbsp;</span>' +
                    '<span class="fc-cell-text">{{title}}</span>' +
                    "</div>";
                labelTds.html($interpolate(cell)(resource));
            },
            eventDragStop: function (event, jsEvent, ui, view) {
                if (isEventOverDiv(jsEvent.clientX, jsEvent.clientY)) {
                    $('#calendar').fullCalendar('removeEvents', event._id);

                    var workorder = event.workorderId;

                    var start = new Date(event.start);
                    var end = new Date(event.end);
                    var estimate = remainsFullHours(start, end);

                    var assignment = {
                        ScheduleDate: start,
                        Employee: event.resourceId,
                        WorkOrder: workorder,
                        EstimatedRepairHours: estimate,
                        EndDate: end
                    };
                    angular.forEach($scope.events, function (value, key) {
                        if (value.workorderId == event.workorderId) {
                            $scope.events.splice(key, 1);
                            var element = {
                                ARCustomer: event.customerFoo,
                                DateEntered: formatDate(new Date(event.dateFoo)),
                                EstimatedRepairHours: parseInt(event.hourFoo),
                                Location: event.locationFoo,
                                Status: "Open",
                                WorkOrder: event.workorderId
                            }
                            $scope.unassignedWorkorders.push(element);
                        };
                    });
                    commonDataService.unAssignWorkorder(assignment);
                }
            },
            eventReceive: function (event) {
                var now = new Date();
                var eventDate = new Date(event._start._d.getTime() + (now.getTimezoneOffset() * 60000));
                if (!$rootScope.unavailableTechniciansIds.includes(parseInt(event.resourceId)) && eventDate >= serverDate) {
                    event = setTechnicianColor(event);
                    event.title = event.workorderId + " " + event.customerFoo + " " + event.locationFoo;
                    var estimate = parseInt(event.hourFoo);
                    var date = new Date(event.start);
                    event.end._d = new Date(date.setHours(date.getHours() + (estimate == 0 ? 1 : estimate > 8 ? 8 : estimate)));
                    $("#calendar").fullCalendar("rerenderEvents");
                    saveEvent(event);
                    var isExist = false;
                    angular.forEach($scope.events, function (value, key) {
                        if (value.workorderId == event.workorderId) {
                            isExist = true;
                            $scope.events[key] = event;
                        }
                    });
                    if (!isExist)
                        $scope.events.push(event);
                    angular.forEach($scope.unassignedWorkorders, function (value, key) {
                        if (value.WorkOrder == event.workorderId) {
                            $scope.unassignedWorkorders.splice(key, 1);
                        };
                    });
                } else {
                    $("#calendar").fullCalendar("removeEvents", event._id);
                    ///
                    var innerHtml = "<div class=\"table-row col-lg-1 col-md-1 col-sm-6 col-xs-6 ng-binding\">" + event.workorderId + "</div>" + "<div class=\"table-row col-lg-2 col-md-2 col-sm-6 col-xs-6 ng-binding\">" + formatDate(new Date(event.dateFoo)) + "</div>" + "<div class=\"table-row col-lg-3 col-md-3 hidden-sm hidden-xs ng-binding\">" + event.customerFoo + "</div>" +
                                 "<div class=\"table-row col-lg-4 col-md-4 hidden-sm hidden-xs ng-binding\">" + event.locationFoo + "</div>" + "<div class=\"table-row col-lg-2 col-md-2 hidden-sm hidden-xs ng-binding\">" + parseInt(event.hourFoo) + "</div>";

                    var el = $("<div class=\"drag fc-event table row table-row dragdemo\" style=\"z-index: 999; display: block\" draggable=\"true\">").appendTo("#new-row").html(innerHtml);
                    el.draggable({
                        zIndex: 999,
                        revert: true,
                        revertDuration: 0
                    });
                    el.data("event", {
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
                }
            },
            resourceLabelText: "Employees",
            resources: $scope.resources,
            forceEventDuration: true,
            dayRender: function (date, cell) {
                var expected = moment(cell.data("date")).local();
                if (expected < serverDate) {
                    $(cell).css("background-color", "#e6e6e6");
                }
            }
        }
    };

    var isEventOverDiv = function (x, y) {
        var external_events = $(".dragdpor_section");
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


    $q.all([commonDataService.getTechnicians(), commonDataService.getSchedule()]).then(function (values) {
        $scope.activeTechnicians = values[0].data;
        angular.forEach($scope.activeTechnicians, function (value, key) {
            if (value != null && value.IsAvailable) {
                this.push({
                    id: value.Employee,
                    title: value.Name,
                    avatarUrl: value.Picture != null ? value.Picture : global.BasePath + "/images/user.png",
                    color: value.Color == "" ? "" : value.Color
                });
                if (value.IsAvailable == false) {
                    $rootScope.unavailableTechniciansIds.unshift(parseInt(value.Employee));
                }
            }
        }, $scope.resources);

        var schedule = values[1].data;
        $scope.unassignedWorkorders = schedule.UnassignedWorkorders;
        angular.forEach($scope.unassignedWorkorders, function (value, key) {
            if (value != null) {
                value.DateEntered = formatDate(new Date(value.DateEntered));
            };
        });
        $scope.assigments = schedule.Assigments;
        angular.forEach($scope.assigments, function (value, key) {
            if (value != null) {
                if (value.Start !== "" && value.End !== "") {
                    var spliter = (value.Customer == "" || value.Location == "") ? "" : "/";
                    tempEvents.push({
                        id: value.Assignment,
                        resourceId: value.EmployeeId,
                        title: value.title = value.WorkOrder + " " + value.Customer + " " + value.Location,
                        start: new Date(value.Start),
                        end: new Date(value.End),
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
            }
        });

        $scope.events = tempEvents.concat(getBusinesHours());

        $timeout(function () {
            $("#calendar").fullCalendar("removeEvents");
            $("#calendar").fullCalendar("addEventSource", $scope.events);
            $("#calendar").fullCalendar("rerenderEvents");
            repaintUnavailables();

            $(".fc-timelineWeek-button").click(function () {
                repaintUnavailables();
            });
            $(".fc-timelineDay-button").click(function () {
                repaintUnavailables();
            });

            $(".drag").each(function () {
                var descr = "";
                var fooElements = [];
                var a = $(this).find(".table-row");
                $(this).find(".table-row").each(function (i, e) {
                    if (i == 2) {
                        var spliter = (e.innerText == "" || e[i + 1] == "") ? "" : "/";
                        descr += e.innerText + spliter;
                    }
                    if (i == 3) {
                        descr += e.innerText;
                    }
                    fooElements[i] = e.innerText;
                });
                var startDate = new Date();
                var endDate = new Date(startDate);

                var rows = $(this).find(".table-row");

                $(this).data("event", {
                    title: parseInt($(this).find(".table-row").first().text()), //textTitle,
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

        $(".ibox-content").on("dragstart", ".dragdemo", function (e) {
            var x = e.pageX;
            var y = e.pageY;
            var innerText = [];
            angular.forEach(this.children, function (value, key) {
                innerText[key] = $(value).text();
            });
            var element = this;
            prevDivState = this.cloneNode(true);;
            this.innerText = innerText[0] + " " + innerText[2] + " " + innerText[3];
            this.style.width = "200px";
            this.style.height = "28px";
            this.style.backgroundColor = "#e77070";
            this.style.borderColor = "#e77070";
            this.style.alignItems = "center";
            this.style.textAlign = "center";
            this.style.color = "#fff";
            this.style.fontSize = ".85em";
            this.style.opacity = ".85";
            this.style.borderRadius = "0";

            this.clientWidth = 20;
            this.offsetWidth = 20;
            this.scrollWidth = 20;
            this.style.marginLeft = (x - 275) + "px";
            this.style.marginTop = -5 + "px";
            document.getElementById("schedule").addEventListener("mouseup", function (event) {
                var innerHtml = "<div class=\"table-row col-lg-1 col-md-1 col-sm-6 col-xs-6 ng-binding\">" + innerText[0] + "</div>" + "<div class=\"table-row col-lg-2 col-md-2 col-sm-6 col-xs-6 ng-binding\">" + innerText[1] + "</div>" + "<div class=\"table-row col-lg-3 col-md-3 hidden-sm hidden-xs ng-binding\">" + innerText[2] + "</div>" +
                              "<div class=\"table-row col-lg-4 col-md-4 hidden-sm hidden-xs ng-binding\">" + innerText[3] + "</div>" + "<div class=\"table-row col-lg-2 col-md-2 hidden-sm hidden-xs ng-binding\">" + innerText[4] + "</div>";

                element.innerHTML = innerHtml;
                element.style = prevDivState.style;
                element.clientWidth = prevDivState.clientWidth;
                element.offsetWidth = prevDivState.offsetWidth;
                element.scrollWidth = prevDivState.scrollWidth;
                $("#schedule").unbind();
            });

        });


        //todo
        //$rootScope.$watchCollection(function () {
        //    return $rootScope.unavailableTechniciansIds;
        //}, function () {
        //    angular.forEach($rootScope.unavailableTechniciansIds, function (value, key) {
        //        angular.forEach($scope.resources, function (item, itemKey) {
        //            if (item.id == value) {
        //                $scope.resources.splice(itemKey, 1);
        //                $('#calendar').fullCalendar('removeResource', item.id);
        //            }
        //        });
        //        angular.forEach($scope.events, function (item, itemKey) {
        //            if (item.resourceId == value) {
        //                $scope.events.splice(itemKey, 1);
        //            }
        //        });
        //    });
        //});
    });

    function formatDate(inputdate) {
        return inputdate.getMonth() + 1 + "-" + inputdate.getDate() + "-" + inputdate.getFullYear();
    }

    var repaintUnavailables = function () {
        //angular.forEach($rootScope.unavailableTechniciansIds, function (value, key) {
        //    var event = {
        //        start: "1900-01-01T11:30:00.000Z",
        //        end: "2100-11-07T19:30:00.000Z",
        //        color: '#D3D3D3',
        //        rendering: 'background',
        //        resourceId: value,
        //        title: "Unavailable time"
        //    }
        //    $scope.events.push(event);
        //    $('#calendar').fullCalendar('renderEvent', event, true);
        //});
        angular.forEach($rootScope.unavailableTechniciansIds, function (value, key) {
            angular.forEach($scope.resources, function (item, itemKey) {
                if (item.id == value) {
                    $scope.resources.splice(itemKey, 1);
                    $('#calendar').fullCalendar('removeResource', item.id);
                }
            });
            angular.forEach($scope.events, function (item, itemKey) {
                if (item.resourceId == value) {
                    $scope.events.splice(itemKey, 1);
                }
            });
        });
    }


    $scope.changeSorting = function (data) {
        if ($scope.sortKey != data) {
            $("." + $scope.sortKey + "").removeClass("footable-sorted");
            $("." + $scope.sortKey + "").removeClass("footable-sorted-desc");
            $scope.sortKey = data;
            $scope.reverse = true;
            $("." + data + "").addClass("footable-sorted");
            $("." + data + "").removeClass("footable-sorted-desc");
        } else {
            $scope.reverse = !$scope.reverse;
            if ($("." + data + "").hasClass("footable-sorted-desc")) {
                $("." + data + "").addClass("footable-sorted");
                $("." + data + "").removeClass("footable-sorted-desc");
            }
            else {
                $("." + data + "").addClass("footable-sorted-desc");
                $("." + data + "").removeClass("footable-sorted");
            }
        }
    }

    $scope.$on('addedAssignment', function (_event, data) {
        var isExist = false;
        angular.forEach($scope.events, function (value, key) {
            if (data.item.WorkOrder.WorkOrder == value.workorderId) {
                isExist = true;
                if (value.id)
                    $("#calendar").fullCalendar('removeEvents', value.id);
                else
                    $("#calendar").fullCalendar('removeEvents', value._id);
                value.start = new Date(data.item.Start);
                value.end = new Date(data.item.End);
                value.resourceId = data.item.Employee;
                $("#calendar").fullCalendar('renderEvent', value);
            }
        });
        if (!isExist) {
            var spliter = (data.item.Customer == "" || data.item.Location == "") ? "" : "/";
            var event = {
                id: data.item.Assignment,
                resourceId: data.item.Employee,
                title: data.item.WorkOrder.WorkOrder + " " + data.item.Customer + " " + data.item.Location,
                start: new Date(data.item.Start),
                end: new Date(data.item.End),
                assigmentId: data.item.Assigment,
                workorderId: data.item.WorkOrder.WorkOrder,
                description: data.item.Customer + spliter + data.item.Location,
                dateFoo: data.item.DateEntered,
                customerFoo: data.item.Customer,
                locationFoo: data.item.Location,
                hourFoo: data.item.EstimatedRepairHours,
                color: data.item.Color == "" ? "" : data.item.Color,
                durationEditable: false
            };
            $scope.events.push(event);
            $("#calendar").fullCalendar('renderEvent', event);
        }
        angular.forEach($scope.unassignedWorkorders, function (value, key) {
            if (value.WorkOrder == data.item.WorkOrder.WorkOrder) {
                $scope.unassignedWorkorders.splice(key, 1);
            }
        });
    });

    $scope.$on('deletedAssignment', function (_event, data) {
        angular.forEach($scope.events, function (value, key) {
            if (data.item.WorkOrder == value.workorderId) {
                isExist = true;
                if (value.id)
                    $("#calendar").fullCalendar('removeEvents', value.id);
                else
                    $("#calendar").fullCalendar('removeEvents', value._id);
            }
        });
        var isExist = false;
        angular.forEach($scope.unassignedWorkorders, function (value, key) {
            if (value.WorkOrder == data.item.WorkOrder) {
                isExist = true;
            }
        });
        if (!isExist) {
            var date = new Date(data.item.DateEntered);
            data.item.DateEntered = formatDate(date);
            $scope.unassignedWorkorders.push(data.item);
        }
    });

    $scope.$on('createdUnassignedWO', function(event, data) {
        var date = new Date(data.item.DateEntered);
        data.item.DateEntered = formatDate(date);
        $scope.unassignedWorkorders.push(data.item);
    });

    //todo
    //$rootScope.$watchCollection(function () { return $rootScope.updatedTechnican; }, function () {
    //    angular.forEach($rootScope.updatedTechnican.AvailableDays, function (value, key) {
    //        var event = {
    //            start: value.start,
    //            end: value._end,
    //            color: '#D3D3D3',
    //            rendering: 'background',
    //            resourceId: $rootScope.updatedTechnican.Id,
    //            title: "Unavailable time"
    //        }
    //        $('#calendar').fullCalendar('renderEvent', event, true);
    //    });
    //    angular.forEach($scope.resources, function (value, key) {
    //        if (value.id == $rootScope.updatedTechnican.Id) {
    //            $scope.resources[key].color = $rootScope.updatedTechnican.Сolor;
    //        }
    //    });
    //});

};
scheduleController.$inject = ["$rootScope", "$scope", "$interpolate", "$timeout", "$q", "commonDataService"];