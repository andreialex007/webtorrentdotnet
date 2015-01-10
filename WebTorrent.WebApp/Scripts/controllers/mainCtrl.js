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

        $scope.isActive = function (item) {
            return item.url == window.location.pathname;
        };

        switch (window.location.pathname) {
            case "/downloading":
                $scope.torrents = torrentSvc.getDownloading();
                break;
            case "/completed":
                $scope.torrents = torrentSvc.getCompleted();
                break;
            case "/checking":
                $scope.torrents = torrentSvc.getChecking();
                break;
            case "/paused":
                $scope.torrents = torrentSvc.getPaused();
                break;
            case "/all":
                $scope.torrents = torrentSvc.getAll();
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
