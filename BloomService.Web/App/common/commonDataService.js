var commonDataService = function($http) {

	this.getWorkorders = function() {
		//return $http.post("/workorders");
		return $http.get("/Workorder/GetWorkorders");
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
		return $http.get("/public/mock/getDashboard.json");
	}
	this.getLookups = function () {
	    return $http.get("/Dashboard/GetLookups");
	}

	this.getLocations = function() {
		return $http.get("/public/mock/getLocations.json");
	}

	this.getTrucks = function() {
		return $http.get("/public/mock/getTrucks.json");
	}

	this.getSchedule = function() {
		return $http.get("/public/mock/getSchedule.json");
	}

	this.getTechnicians = function () {
		//return $http.get("/technicians");
	    return $http.get("/Technician/GetTechnicians");
	}

	this.saveTechnicianSchedule = function(technicianId, schedule) {
		$http.post("/technician/id");
	}
}
commonDataService.$inject = ['$http'];
