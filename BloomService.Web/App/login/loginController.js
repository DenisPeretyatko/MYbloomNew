/**
 * loginController - controller
 */

 var loginController = function ($scope, $state, commonDataService) {
     $scope.obj = {};
     $scope.obj.username = 'testtechuser';
     $scope.obj.password = 'sageDEV!!';

     $scope.login = (function () {
         commonDataService.getToken($scope.obj.username, $scope.obj.password).success(function (response) {
             if (response == 'success')
                 $state.go('manager.dashboard');
         })
     });
    
 };
 loginController.$inject = ["$scope", "$state", "commonDataService"];