define([], function () {
    'use strict';

    function mainCtrl($scope, $routeParams, torrentSvc) {

        $scope.menuItems = [
            { name: "All", url: "/all", icon: "fa-cloud" },
            { name: "Downloading", url: "/downloading", icon: "fa-download" },
            { name: "Completed", url: "/completed", icon: "fa-thumbs-up" },
            { name: "Checking", url: "/checking", icon: "fa-check-square" },
            { name: "Paused", url: "/paused", icon: "fa-pause" }
        ];

        $scope.setActive = function (item) {
            $($scope.torrents).each(function (i, el) {
                el.selected = false;
            });
            item.selected = true;
        }

        $scope.isActive = function (item) {
            return item.url == window.location.pathname;
        };

        var successFunc = function (items) {
            $scope.torrents = items;
            var firstItem = $($scope.torrents).first()[0];
            if (firstItem) {
                firstItem.selected = true;
            }
        };

        switch (window.location.pathname) {
            case "/downloading":
                torrentSvc.getDownloading(successFunc);
                break;
            case "/completed":
                torrentSvc.getCompleted(successFunc);
                break;
            case "/checking":
                torrentSvc.getChecking(successFunc);
                break;
            case "/paused":
                torrentSvc.getPaused(successFunc);
                break;
            case "/all":
                torrentSvc.getAll(successFunc);
                break;
            default:
                throw new Exception("correct path not provided");
        };

        $scope.infoTabs = torrentSvc.getTorrentData();

        $(window).trigger("resize");
        setTimeout(function () {
            Metronic.init();
        }, 70);
    }

    return mainCtrl;
});
