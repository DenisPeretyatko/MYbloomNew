var loadingBarService = function($q, $rootScope) {
    var requests = 0;
   

    function show() {
        if (!requests) {
            $rootScope.$broadcast("ajax-start");
        }
        requests++;
    }
    function hide() {
        requests--;
        if (!requests) {
            $rootScope.$broadcast("ajax-stop");
        }
    }
      function responseError() {
            $rootScope.$broadcast("responseError");
    }

    function serverError() {
            $rootScope.$broadcast("serverError");
    }

    return {
        'request': function (config) {
            show();
            return $q.when(config);
        }, 'response': function (response) {
            if (response.data.success == false) {
                $rootScope.rejectionError = {};
                $rootScope.rejectionError.data = {};
                $rootScope.rejectionError.data = response.data.message;
                $rootScope.$broadcast("responseError");
            }
            hide();
            return $q.when(response);
        }, 'responseError': function (rejection) {
            requests = 0;
            $rootScope.rejectionError = rejection;
           // var start = $rootScope.rejectionError.data.indexOf("<style>");
            //var end = $rootScope.rejectionError.data.indexOf("</style>");
            $rootScope.rejectionError.data = $rootScope.rejectionError.data.replace($rootScope.rejectionError.data.substring(start, end), "");
            if (rejection.status == 404 || rejection.status == 408 || rejection.status == 504 || rejection.status == 0)
                serverError();
            else
                responseError();

            return $q.reject(rejection);
        }
    };
}

loadingBarService.$inject = ["$q", "$rootScope"];


var loadingBarConfig = function($httpProvider) {
    $httpProvider.interceptors.push(loadingBarService);
}

loadingBarConfig.$inject = ["$httpProvider"];