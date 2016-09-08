/**
 * pageTitle - Directive for set Page title - mata title
 */
function pageTitle($rootScope, $timeout) {
    return {
        link: function (scope, element) {
            var listener = function (event, toState, toParams, fromState, fromParams) {
                // Default title - load on Dashboard 1
                var title = 'Bloom Field Service Operations';
                // Create your own title pattern
                if (toState.data && toState.data.pageTitle) title = 'Bloom Field Service Operations ';
                $timeout(function () {
                    element.text(title);
                });
            };
            $rootScope.$on('$stateChangeStart', listener);
        }
    }
}

/**
 * sideNavigation - Directive for run metsiMenu on sidebar navigation
 */
function sideNavigation($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            // Call the metsiMenu plugin and plug it to sidebar navigation
            $timeout(function () {
                element.metisMenu();
            });
        }
    };
}

/**
 * iboxTools - Directive for iBox tools elements in right corner of ibox
 */
function iboxTools($timeout) {
    return {
        restrict: 'A',
        scope: true,
        templateUrl: '/app/common/views/ibox_tools.html',
        controller: function ($scope, $element) {
            // Function for collapse ibox
            $scope.showhide = function () {
                var ibox = $element.closest('div.ibox');
                var icon = $element.find('i:first');
                var content = ibox.find('div.ibox-content');
                content.slideToggle(200);
                // Toggle icon from up to down
                icon.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
                ibox.toggleClass('').toggleClass('border-bottom');
                $timeout(function () {
                    ibox.resize();
                    ibox.find('[id^=map-]').resize();
                }, 50);
            },
            // Function for close ibox
                $scope.closebox = function () {
                    var ibox = $element.closest('div.ibox');
                    ibox.remove();
                }
        }
    };
}

/**
 * iboxTools with full screen - Directive for iBox tools elements in right corner of ibox with full screen option
 */
function iboxToolsFullScreen($timeout) {
    return {
        restrict: 'A',
        scope: true,
        templateUrl: '/app/common/views/ibox_tools_full_screen.html',
        controller: function ($scope, $element) {
            // Function for collapse ibox
            $scope.showhide = function () {
                var ibox = $element.closest('div.ibox');
                var icon = $element.find('i:first');
                var content = ibox.find('div.ibox-content');
                content.slideToggle(200);
                // Toggle icon from up to down
                icon.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
                ibox.toggleClass('').toggleClass('border-bottom');
                $timeout(function () {
                    ibox.resize();
                    ibox.find('[id^=map-]').resize();
                }, 50);
            };
            // Function for close ibox
            $scope.closebox = function () {
                var ibox = $element.closest('div.ibox');
                ibox.remove();
            };
            // Function for full screen
            $scope.fullscreen = function () {
                var ibox = $element.closest('div.ibox');
                var button = $element.find('i.fa-expand');
                $('body').toggleClass('fullscreen-ibox-mode');
                button.toggleClass('fa-expand').toggleClass('fa-compress');
                ibox.toggleClass('fullscreen');
                setTimeout(function () {
                    $(window).trigger('resize');
                }, 100);
            }
        }
    };
}

/**
 * minimalizaSidebar - Directive for minimalize sidebar
*/
function minimalizaSidebar($timeout) {
    return {
        restrict: 'A',
        template: '<a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="" ng-click="minimalize()"><i class="fa fa-bars"></i></a>',
        controller: function ($scope, $element) {
            $scope.minimalize = function () {
                $("body").toggleClass("mini-navbar");
                if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
                    // Hide menu in order to smoothly turn on when maximize menu
                    $('#side-menu').hide();
                    // For smoothly turn on menu
                    setTimeout(
                        function () {
                            $('#side-menu').fadeIn(400);
                        }, 200);
                } else if ($('body').hasClass('fixed-sidebar')) {
                    $('#side-menu').hide();
                    setTimeout(
                        function () {
                            $('#side-menu').fadeIn(400);
                        }, 100);
                } else {
                    // Remove all inline style from jquery fadeIn function to reset menu state
                    $('#side-menu').removeAttr('style');
                }
            }
        }
    };
}

/**
 * backButton - return history back
*/
function backButton($window) {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            elem.bind('click', function () {
                $window.history.back();
            });
        }
    };
};

/**
 * loadingBar - loading page bar
*/
function loadingBar() {
    var directive = {
        link: link,
        scope: {

        },
        template: '<div id="cover"></div>',
        replace: true,
        restrict: 'E'
    };
    return directive;

    function link(scope, element, attrs) {

        scope.$on("ajax-start", function () {
            element.removeClass('hide');
            scope.ajaxBusy = true;
        });

        scope.$on("ajax-stop", function () {
            element.addClass('hide');
            scope.ajaxBusy = false;
        });

        scope.$on("responseError", function () {
            element.addClass('hide');
            scope.ajaxBusy = false;
        });
        scope.$on("serverError", function () {
            element.addClass('hide');
            scope.ajaxBusy = false;
        });
    }
}

function interceptor($window, $rootScope) {
    var directive = {
        link: link,
        scope: {
        },
        templateUrl: '../App/common/views/modalError.html',
        replace: true,
        restrict: 'E'
    };
    return directive;

    function link(scope) {
        var div = $('#read-more-error');
      

        scope.$on("responseError", function () {
            scope.hidden = false;
            scope.tittle = "Unexpected server error";
            scope.message = "Unexpected server error, please reload the page.";
            debugger;
            div.removeClass();
            div.addClass("ng-hide");
            if ($rootScope.rejectionError != undefined) {
                if ($rootScope.rejectionError.status != undefined)
                    div.addClass('read-more-large');
                else
                    div.addClass('read-more-little');
            }
            $("#prepended").remove();
            div.append('<div id="prepended">' + $rootScope.rejectionError.data + '</div>');

            scope.showModal = true;
            scope.ajaxBusy = true;
        });

        scope.$on("serverError", function () {
            scope.hidden = false;
            scope.tittle = "Connection server error";
            scope.message = "Connection server error, please reload the page.";
            div.removeClass();
            div.addClass("ng-hide");
            if ($rootScope.rejectionError != undefined) {
                if ($rootScope.rejectionError.status != undefined)
                    div.addClass('read-more-large');
                else
                    div.addClass('read-more-little');
            }
            $("#prepended").remove();
            div.append('<div id="prepended">' + $rootScope.rejectionError.data + '</div>');

            scope.showModal = true;
            scope.ajaxBusy = true;
        });

        scope.reloadPage = function () {
            $window.location.reload();
        }

        scope.hide = function () {
            scope.hidden = !scope.hidden;
            if ($rootScope.rejectionError.status != undefined) {
                if (scope.hidden)
                    $(".box").addClass("read-more-opened");
                else
                    $(".box").removeClass("read-more-opened");
            }
        }
    }
}
