define([
     "Scripts/controllers/baseCtrl"
], function (baseCtrl) {
    'use strict';

    return function ($scope, $routeParams, torrentSvc) {

        //Наследуемся от базового
        baseCtrl.call(this, $scope, $routeParams);

//        $scope.


        console.log("users controller");
    };
});
