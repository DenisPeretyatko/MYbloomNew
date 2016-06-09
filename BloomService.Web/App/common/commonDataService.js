var commonDataService = function($http, $window) {

    this.Token = '';
    this.UserName = '';

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

	this.getWorkorderPage = function (index) {
	    return $http.get("/WorkorderPage/" + index, {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.countPage = function (index) {
	    return $http.get("/WorkorderPageCount", {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.createWorkorder = function(workorder){
	    return $http.post("/workorder/Create", workorder, {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.saveWorkorder = function (workorder) {
	    return $http.post("/workorder/Save", workorder, {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.assignWorkorder = function(assignment){
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

	this.getDashboard = function() {
	    return $http.get("/Dashboard", { headers: {
	        'Authorization': 'bearer ' + window.localStorage.getItem('Token')}
});
	}
	this.getLookups = function () {
	    return $http.get("/Dashboard/Lookups", {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.getLocations = function(map) {
	    return $http.post("/Location", map, {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.getTrucks = function() {
	    return $http.get("/Location/Trucks", {
	        headers: {
	            'Authorization': 'bearer ' + window.localStorage.getItem('Token')
	        }
	    });
	}

	this.getSchedule = function() {
	    return $http.get("/Schedule", {
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

	this.getTechnician = function (id) {
	    return $http.get("/Technician/" + id, {
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
	    var data = "grant_type=password&username=" + user + "&password=" + pass;
	    angular.element($window).on;
	    window.localStorage.setItem('UserName', user);
	    return $http.post('apimobile/Token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
	}
}
commonDataService.$inject = ['$http'];
