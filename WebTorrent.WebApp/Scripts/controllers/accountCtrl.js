define([
], function () {
    'use strict';

    return function ($scope, $routeParams, torrentSvc) {

        $scope.commonErrors = [];

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

        $scope.hasErrors = function () {
            return $scope.passwordHasError() || $scope.userNameHasError() || $scope.commonErrors.length > 0;
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
                $scope.passwordErrors = $.grep(data.Errors, function (x) { return x.PropertyName == "Password"; });
                $scope.userNameErrors = $.grep(data.Errors, function (x) { return x.PropertyName == "Name"; });
                if (data.ValidationSummary) {
                    $scope.commonErrors = [data.ValidationSummary];
                }
            });

        };

        console.log("account controller");

    };
});
