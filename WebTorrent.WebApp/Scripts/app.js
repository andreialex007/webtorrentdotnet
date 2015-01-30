require([
        'Metronic/init',
        'Scripts/libs/angular-1.3.3/angular-modules',
        'Scripts/controllers/mainCtrl',
        'Scripts/controllers/usersCtrl',
        'Scripts/controllers/accountCtrl',
        "Scripts/services/torrentSvc",
        "Scripts/services/userSvc",
        "Scripts/filters/reverse",
        "Scripts/directives/onFileChanged",
        "Scripts/directives/ngVisible"
], function (
    init,
    modules,
    mainCtrl,
    usersCtrl,
    accountCtrl,
    torrentSvc,
    userSvc,
    reverseFilter,
    onFileChanged,
    ngVisible
    ) {
    "use strict";

    window.
    app = angular.module('app', ['ngRoute', 'ngResource' /* , 'ngResource', 'ngGrid' */]);
    app.config(function ($routeProvider, $locationProvider) {

        $locationProvider.html5Mode(true);
        var mainCtrlConfig = { templateUrl: 'Templates/default.html', controller: 'mainCtrl' };
        $routeProvider
            .when("/all", mainCtrlConfig)
            .when("/downloading", mainCtrlConfig)
            .when("/checking", mainCtrlConfig)
            .when("/paused", mainCtrlConfig)
            .when("/completed", mainCtrlConfig)
            .when("/users", { templateUrl: 'Templates/users.html', controller: 'usersCtrl' })
            .otherwise({ redirectTo: '/all' });
    });

    app.filter('reverse', reverseFilter);
    app.factory('torrentSvc', torrentSvc);
    app.factory('userSvc', userSvc);
    app.directive("onFileChanged", onFileChanged);
    app.directive("ngVisible", ngVisible);
    app.controller('mainCtrl', mainCtrl);
    app.controller('usersCtrl', usersCtrl);
    app.controller('accountCtrl', accountCtrl);

    angular.bootstrap(document, ['app']);

    $(".login-button").click();

});