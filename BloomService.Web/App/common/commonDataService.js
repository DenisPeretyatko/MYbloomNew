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

	this.getAssigments = function() {
		return $http.get("/public/mock/getAssigments.json");
	}

	this.assignWorkorder = function(workorderId, technicianId){
		$http.post("/technician/id/workorder/id");
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

	this.saveTechnicianSchedule = function(technicianId, schedule) {
		$http.post("/technician/id");
	}

	this.getNotifications = function () {
	    return $http.get("/Dashboard/SendNotification");
	}

	this.getToken = function (username, password) {
	    return $http.get("/Authorization/Login/" + username + "/" + password);
	}
}
commonDataService.$inject = ['$http'];
