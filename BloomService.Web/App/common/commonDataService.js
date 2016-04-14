var commonDataService = function($http) {

	this.getWorkorders = function() {
		//return $http.post("/workorders");
		return $http.get("/Workorder");
	}

	this.saveWorkorder = function(workorder){
		$http.post("/workorder");
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
		//return $http.get("/technicians");
	    return $http.get("/Technician");
	}

	this.saveTechnicianSchedule = function(technicianId, schedule) {
		$http.post("/technician/id");
	}

	this.getNotifications = function () {
	    return $http.get("/Dashboard/SendNotification");
	}
}
commonDataService.$inject = ['$http'];
