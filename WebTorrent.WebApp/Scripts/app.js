require([
        'Metronic/init',
        'Scripts/libs/angular-1.3.3/angular-modules',
        'Scripts/controllers/mainCtrl',
        "Scripts/services/torrentSvc"
], function (init, modules, mainCtrl, torrentSvc) {
    "use strict";

    window.
    app = angular.module('app', ['ngRoute' /* , 'ngResource', 'ngGrid' */]);
    app.config(function ($routeProvider, $locationProvider) {

        $locationProvider.html5Mode(true);
        var mainCtrlConfig = { templateUrl: 'Templates/default.html', controller: 'mainCtrl' };
        $routeProvider
            .when("/all", mainCtrlConfig)
            .when("/downloading", mainCtrlConfig)
            .when("/checking", mainCtrlConfig)
            .when("/paused", mainCtrlConfig)
            .when("/completed", mainCtrlConfig)
            .otherwise({ redirectTo: '/all' });

    });
    app.factory('torrentSvc', torrentSvc);
    app.controller('mainCtrl', mainCtrl);

    angular.bootstrap(document, ['app']);

});