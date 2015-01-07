require([
        'Metronic/init',
        'Scripts/libs/angular-1.3.3/angular-modules',
        'Scripts/controllers/mainCtrl'
], function (init, modules, mainCtrl) {

    window.app = angular.module('app', [
            // 'ngRoute', 'ngResource', 'ngGrid'
        ]
    );
    app.controller('mainCtrl', mainCtrl);
    angular.bootstrap(document, ['app']);

});