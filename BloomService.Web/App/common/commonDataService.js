var commonDataService = function ($http, $window) {
    var vm = this;
    this.Token = '';
    this.UserName = '';
    vm.workorders = null;

    var observCallback = [];
    this.registrationCallback = function (callback) {
        observCallback.push(callback);
    }
    var notifyObservers = function () {
        angular.forEach(observCallback, function (callback) {
            callback();
        });
    }

    this.getWorkorders = function () {
        return $http.get("/Workorder", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getWorkorder = function (id) {
        return $http.get("/Workorder/" + id, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getWorkorderPictures = function (id) {
        return $http.get("/Workorderpictures/" + id, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.editComment = function (model) {
       return $http.post("/EditComment", model, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getWorkorderPage = function (index, search) {
        return $http.get("/WorkorderPage?index=" + index + "&searchString=" + search, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.countPage = function (search) {
        return $http.get("/WorkorderPageCount/" + search, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.createWorkorder = function (workorder) {
        return $http.post("/workorder/Create", workorder, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.saveWorkorder = function (workorder) {
        return $http.put("/workorder/Save", workorder, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.assignWorkorder = function (assignment) {
        $http.post("/Schedule/Assignments/Create", assignment, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.unAssignWorkorder = function (assignment) {
        $http.post("/Schedule/Assignments/Delete", assignment, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.markAsReviewed = function (workorderId) {
        return $http.post("/workorder/markAsReviewed", workorderId, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getDashboard = function (map) {
        return $http.post("/Dashboard", map, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }
    this.getLookups = function () {
        return $http.get("/Dashboard/Lookups", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getLocations = function (map) {
        return $http.post("/Location", map, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getTrucks = function () {
        return $http.get("/Location/Trucks", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getSchedule = function () {
        return $http.get("/Schedule", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getTechnician = function (id) {
        return $http.get("/Technician/" + id, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }


    this.getTechnicians = function () {
        return $http.get("/Technician", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.editTechnician = function (technician) {
        return $http.post("/Technician/Save", technician, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getNotifications = function () {
        return $http.get("/Dashboard/SendNotification", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getToken = function (user, pass) {
        var data = "username=" + user + "&password=" + pass;
        angular.element($window).on;
        window.localStorage.setItem('UserName', user);
        return $http.post('Login', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    }



    this.getWorkordesPaged = function (model) {
        return $http.post("/WorkorderPage", model, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    };

    this.countPagePagination = function () {
        return $http.get("/WorkorderPageCount/", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.locationsByCustomer  = function (customer) {
        return $http.post("Workorder/LocationsByCustomer", customer, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    };

    this.customerByLocation = function (location) {
        return $http.post("Workorder/CustomerByLocation", location, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    };

    this.updateNotificationTime = function () {
        return $http.post("/Dashboard/UpdateNotificationTime", "", {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    };

    this.addNote = function (model) {
        return $http.post("/WorkOrder/AddNote", model, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

     this.editNote = function (model) {
        return $http.post("/WorkOrder/EditNote", model, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.deleteNote = function (model) {
        return $http.post("/WorkOrder/DeleteNote", model, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }

    this.getNotes = function (id) {
        return $http.get("/WorkOrder/GetNotes/" + id, {
            headers: {
                'Authorization': 'bearer ' + window.localStorage.getItem('Token')
            }
        });
    }
}
commonDataService.$inject = ['$http', '$window'];
