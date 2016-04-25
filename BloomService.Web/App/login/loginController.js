/**
 * loginController - controller
 */

 var loginController = function ($scope, $state, commonDataService) {
     $scope.obj = {};
     $scope.obj.username = '';
     $scope.obj.password = '';

     $scope.login = function() {
         commonDataService.getToken($scope.obj.username, $scope.obj.password).then(function (response) {
             if (response.data == 'success')
                 $state.go('manager.common');
         });
     };
 };
 loginController.$inject = ["$scope", "$state", "commonDataService"];