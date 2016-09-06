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
      function showError() {
        requests--;
        if (!requests) {
            $rootScope.$broadcast("responseError");
        }
    }

    return {
        'request': function (config) {
            show();
            return $q.when(config);
        }, 'response': function (response) {
            hide();
            return $q.when(response);
        }, 'responseError': function (rejection) {
            showError();
            return $q.reject(rejection);
        }
    };
}

loadingBarService.$inject = ["$q", "$rootScope"];


var loadingBarConfig = function($httpProvider) {
    $httpProvider.interceptors.push(loadingBarService);
}

loadingBarConfig.$inject = ["$httpProvider"];