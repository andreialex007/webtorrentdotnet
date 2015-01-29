define([
], function () {
    'use strict';

    return function ($scope, $routeParams, torrentSvc) {

        $scope.menuItems = [
           { name: "All", url: "/all", icon: "fa-cloud" },
           { name: "Downloading", url: "/downloading", icon: "fa-download" },
           { name: "Completed", url: "/completed", icon: "fa-thumbs-up" },
           { name: "Checking", url: "/checking", icon: "fa-check-square" },
           { name: "Paused", url: "/paused", icon: "fa-pause" }
        ];

        console.log("base controller");
    };
});
