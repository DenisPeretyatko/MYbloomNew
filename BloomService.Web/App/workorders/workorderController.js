/**
 * techniciansController - controller
 */

var workorderController = function ($scope, $timeout, commonDataService) {

    commonDataService.getWorkorderPage(1).then(function (response) {
        $scope.workorders = response.data;
        $timeout(function () {
            $(".footable").trigger("footable_redraw");
        }, 100);
    });

    $scope.currentPage = 1;
    $scope.itemsPerPage = 12;
    $scope.numberOfPages = 0;
    $scope.firstPage = function () {
        return $scope.currentPage == 1;
    }

    commonDataService.countPage().then(function (response) {
        $scope.numberOfPages = response.data.CountPage;
    });

    $scope.lastPage = function () {
        var lastPageNum = $scope.numberOfPages;
        return $scope.currentPage == lastPageNum;
    }

    $scope.startingItem = function () {
        return $scope.currentPage * $scope.itemsPerPage;
    }

    $scope.pageBack = function () {
        $scope.currentPage = $scope.currentPage - 1;
        commonDataService.getWorkorderPage($scope.currentPage).then(function (response) {
            $scope.workorders = response.data;
        })
    }

    $scope.pageForward = function () {
        $scope.currentPage = $scope.currentPage - 1 + 2;
        commonDataService.getWorkorderPage($scope.currentPage).then(function (response) {
            $scope.workorders = response.data;
        })
    }

    $scope.checkIfEnterKeyWasPressed = function ($event) {
        var keyCode = $event.which || $event.keyCode;
        if (keyCode === 13) {
            commonDataService.getWorkorderPage($scope.currentPage).then(function (response) {
                $scope.workorders = response.data;
            })
        }
    };

    //$scope.change = function runScript(e) {
    //    if (e.keyCode == 13) {
    //        commonDataService.getWorkorderPage($scope.currentPage).then(function (response) {
    //        $scope.workorders = response.data;
    //        }
    //    }
    //}

    $scope.Parse = function (value) {
        return new Date(parseInt(value.substr(6)));
    };

};
workorderController.$inject = ["$scope", "$timeout", "commonDataService"];