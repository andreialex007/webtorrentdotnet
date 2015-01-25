define([
], function () {
    'use strict';

    return function ($scope, $routeParams, torrentSvc) {

        $scope.userName = "";
        $scope.userNameErrors = [];
        $scope.userNameHasError = function () {
            return $scope.userNameErrors.length > 0;
        }


        $scope.password = "";
        $scope.passwordErrors = [];
        $scope.passwordHasError = function () {
            return $scope.passwordErrors.length > 0;
        }

        $scope.rememberMe = false;
        $scope.login = function () {

            $.ajax({
                type: "POST",
                url: "account/login",
                data: JSON.stringify({ name: $scope.userName, password: $scope.password, isPersistent: $scope.rememberMe }),
                dataType: "json",
                contentType: "application/json"
            }).done(function (data) {

                debugger;

                if (data.HasErrors) {

                } else {

                }

               
            });

        };

        console.log("account controller");

    };
});
