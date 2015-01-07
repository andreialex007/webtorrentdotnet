define([], function (app) {
    'use strict';

    function mainCtrl($scope) {
        $scope.TestVar = "myTestVar";
    }

//    mainCtrl.$inject = ['$scope'];


    return mainCtrl;
});
