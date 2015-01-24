define([
    "Scripts/models/torrentItem",
    "Scripts/models/torrentInfoTab"
], function (torrentItem, torrentInfoTab) {
    'use strict';

    function torrentSvc($resource) {
        var self = {};

        self.getDownloading = function (success) {
            $resource("/api/torrents/downloading").query({}, function (data) {
                success($.map(data, function (x) { return new torrentItem(x); }));
            });
        }

        self.getChecking = function (success) {
            $resource("/api/torrents/checking").query({}, function (data) {
                success($.map(data, function (x) { return new torrentItem(x); }));
            });
        }

        self.getPaused = function (success) {
            $resource("/api/torrents/paused").query({}, function (data) {
                success($.map(data, function (x) { return new torrentItem(x); }));
            });
        }

        self.getCompleted = function (success) {
            $resource("/api/torrents/completed").query({}, function (data) {
                success($.map(data, function (x) { return new torrentItem(x); }));
            });
        }

        self.getAll = function (success) {
            $resource("/api/torrents/all").query({}, function (data) {
                success($.map(data, function (x) { return new torrentItem(x); }));
            });
        };

        self.getTorrent = function (id, success) {
            var torrentResource = $resource('/api/torrents/:id');
            torrentResource.get({ id: id }, success);
        }

        self.deleteTorrent = function (id, success) {
            var torrentResource = $resource('/api/torrents/:id');
            torrentResource.delete({ id: id }, success);
        }

        self.startTorrent = function (id, success) {
            var torrentResource = $resource('/api/torrents/command/:id/:command');
            torrentResource.get({ id: id, command: "start" }, success);
        }
        self.stopTorrent = function (id, success) {
            var torrentResource = $resource('/api/torrents/command/:id/:command');
            torrentResource.get({ id: id, command: "stop" }, success);
        }
        self.pauseTorrent = function (id, success) {
            var torrentResource = $resource('/api/torrents/command/:id/:command');
            torrentResource.get({ id: id, command: "pause" }, success);
        }

        self.getTorrentData = function (id, success) {
            ///<summary>Получает данные  для вкладок о торренте</summary>

            var torrentResource = $resource('/api/torrents/:id');
            torrentResource.get({ id: id }, function (data) {
                var filesInfoTab = new torrentInfoTab(data.FilesInfo);
                var peersInfoTab = new torrentInfoTab(data.PeersInfo);
                var torrentInfoTabItem = new torrentInfoTab(data.TorrentInfo);
                var trackersInfoTab = new torrentInfoTab(data.TrackersInfo);
                success([
                    filesInfoTab, peersInfoTab, torrentInfoTabItem, trackersInfoTab
                ]);
            });
        }

        return self;
    }

    return torrentSvc;
});