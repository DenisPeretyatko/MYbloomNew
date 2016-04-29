var commonDataService = function($http) {

	this.getWorkorders = function() {
		return $http.get("/Workorder");
	}

	this.getWorkorder = function (id) {
	    return $http.get("/Workorder/"+id);
	}

	this.createWorkorder = function(workorder){
	    return $http.post("/workorder/Create", workorder);
	}

	this.saveWorkorder = function (workorder) {
	    return $http.post("/workorder/Save", workorder);
	}

	this.assignWorkorder = function(assignment){
	    $http.post("/Schedule/Assignments/Create", assignment);
	}

	this.getDashboard = function() {
	    return $http.get("/Dashboard");
	}
	this.getLookups = function () {
	    return $http.get("/Dashboard/Lookups");
	}

	this.getLocations = function() {
	    return $http.get("/Location");
	}

	this.getTrucks = function() {
	    return $http.get("/Location/Trucks");
	}

	this.getSchedule = function() {
	    return $http.get("/Schedule");
	}

	this.getTechnicians = function () {
	    return $http.get("/Technician");
	}

	this.EditTechnician = function (technician) {
	    $http.post("/Technician/Save", technician);
	}

	this.getNotifications = function () {
	    return $http.get("/Dashboard/SendNotification");
	}
}
commonDataService.$inject = ['$http'];
