var workorderController = function ($scope, $timeout, $sce, commonDataService, state) {
    var model = {
        Index: 0,
        Search: '',
        Column: '',
        Direction: false
    };
     var currentPage = 0;
     $scope.sorting = "date";
    $scope.increase = false;
    $scope.Search = "";

    commonDataService.getWorkordesPaged(model).then(function (response) {
        $scope.workorders = response.data.WorkordersList;
        $scope.pagesCount = response.data.CountPage;
        $scope.paginationList = $scope.getPagList(response.data.CountPage);
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
        $scope.getSelectedPage(page);
        state.UpdateWorkordersList(model).then(function() {
            $scope.workorders = state.getWorkordersList().WorkordersList;
            $scope.pagesCount = state.getWorkordersList().CountPage;
            $scope.paginationList = $scope.getPagList(state.getWorkordersList().CountPage);
        });

    }

    $scope.CurrentPageNum = function () {
        return $scope.getCurrentPageNum();
    }
    $scope.PrevPage = function () {
        $scope.ShowPage($scope.getPrevPage());
    }

    $scope.NextPage = function () {
        $scope.ShowPage($scope.getNextPage($scope.pagesCount));
    }

    
   
    $scope.changeSorting = function (data) {
        if ($scope.sorting != data) {
            $scope.sorting = data;
            $scope.increase = false;
        } else {
            $scope.increase = !$scope.increase;
        }
        $scope.ShowPage($scope.getCurrentPageNum());
       
    }
  
    $scope.$watch('searchStr', function (tmpStr) {
            $scope.Search = tmpStr;
            $scope.ShowPage();
        
    });

    $scope.getPagList = function (num) {

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

    $scope.getCurrentPageNum = function () {
            if (currentPage == undefined) {
                currentPage = 0;
            }
            return currentPage;
    },

    $scope.getPrevPage = function () {
        if (currentPage > 0)
            currentPage--;
        else {
            return false;
        }
        return currentPage;
    }
    $scope.getSelectedPage = function (num) {
        currentPage = num;
    }

    $scope.getNextPage = function (num) {
        if (currentPage < num)
            currentPage++;
        else {
            return false;
        }
        return currentPage;
    }


    $scope.workorders = state.getWorkordersList().WorkordersList;
    $scope.pagesCount = state.getWorkordersList().CountPage;
    $scope.getPaginationList = $scope.getPagList(state.getWorkordersList().CountPage);

};
workorderController.$inject = ["$scope", "$timeout", '$sce', "commonDataService", "state"];