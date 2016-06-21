var commonDataService = function ($http, $window, $q, $sce) {
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
        return $http.post("/workorder/Save", workorder, {
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

    var currentPage = 0;

    this.getWorkordesPaged = function (model) {
        //var deferred = $q.defer();
        //deferred.resolve("");
        //return deferred.promise;
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


    this.getPaginationList = function (num) {

        var pagesNum = num;
        var paginationList = [];
        if (currentPage == undefined) {
            currentPage = 0;
        }
      
        if (currentPage + 6 < pagesNum) {
            for (var i = currentPage; i < currentPage + 5; i++) {
                var name = i + 1;
                paginationList.push({
                    name: $sce.trustAsHtml(String(name)),
                    link: i
                });
            }
            paginationList.push({
                name: $sce.trustAsHtml('...'),
                link: pagesNum
            });
            paginationList.push({
                name: $sce.trustAsHtml(String(pagesNum)),
                link: pagesNum - 1
            });
        } else {
            paginationList.push({
                name: $sce.trustAsHtml('1'),
                link: 0
            });
            paginationList.push({
                name: $sce.trustAsHtml('...'),
                link: pagesNum
            });
            for (var i = pagesNum-6; i < pagesNum; i++) {
                var name = i + 1;
                paginationList.push({
                    name: $sce.trustAsHtml(String(name)),
                    link: i
                });
            }
        }

        if (pagesNum > 2) {
            return paginationList;
        } else {
            return false;
        }
    }

    this.getCurrentPageNum = function () {
            if (currentPage == undefined) {
                currentPage = 0;
            }
            return currentPage;
    },

    this.getPrevPage = function () {
        if (currentPage > 0)
            currentPage--;
        else {
            return false;
        }
        return currentPage;
    }
    this.getSelectedPage = function (num) {
        currentPage = num;
    }

    this.getNextPage = function (num) {
        if (currentPage < num)
            currentPage++;
        else {
            return false;
        }
        return currentPage;
    }



}
commonDataService.$inject = ['$http', '$window', '$q', '$sce'];
