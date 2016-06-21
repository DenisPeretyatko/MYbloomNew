/**
 * techniciansController - controller
 */

var workorderController = function ($scope, $timeout, commonDataService) {

    var model = {
        Index: 0,
        Search: '',
        Column: '',
        Direction: true
    };
    $scope.sorting = "";
    $scope.increase = true;
    $scope.Search = "";

    commonDataService.getWorkordesPaged(model).then(function (response) {
        $scope.workorders = response.data.WorkordersList;
        $scope.pagesCount = response.data.CountPage;
        $scope.getPaginationList = commonDataService.getPaginationList(response.data.CountPage);
    });
    

    $scope.Parse = function (value) {
        return new Date(parseInt(value.substr(6)));
    };

    $scope.ShowPage = function (page) {
        if (page == $scope.pagesCount) return true;
        model.Index = page;
        model.Column = $scope.sorting;
        model.Direction = $scope.increase;
        model.Search = $scope.Search;
        commonDataService.getSelectedPage(page);
        commonDataService.getWorkordesPaged(model).then(function (response) {
            $scope.workorders = response.data.WorkordersList;
            $scope.pagesCount = response.data.CountPage;
            $scope.getPaginationList = commonDataService.getPaginationList(response.data.CountPage);
        });
    }

    $scope.CurrentPageNum = function () {
        return commonDataService.getCurrentPageNum();
    }
    $scope.PrevPage = function () {
        $scope.ShowPage(commonDataService.getPrevPage());
    }

    $scope.NextPage = function () {
        $scope.ShowPage(commonDataService.getNextPage($scope.pagesCount));
    }

    //var updateData = function () {
    //    $scope.workorders = commonDataService.workorders;
    //}

    //commonDataService.registrationCallback(updateData);

   
    $scope.changeSorting = function (data) {
        if ($scope.sorting != data) {
            $scope.sorting = data;
            $scope.increase = true;
        } else {
            $scope.increase = !$scope.increase;
        }
        $scope.ShowPage(commonDataService.getCurrentPageNum());
       
    }
  
    // Instantiate these variables outside the watch
    $scope.$watch('searchStr', function (tmpStr) {
            $scope.Search = tmpStr;
            $scope.ShowPage();
        
    });
};
workorderController.$inject = ["$scope", "$timeout", "commonDataService"];