define([], function () {
    'use strict';

    function mainCtrl($scope, $routeParams, torrentSvc) {

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

    }

    return mainCtrl;
});
