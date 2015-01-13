define([
    "Scripts/models/torrentItem",
    "Scripts/models/torrentInfoTab"
], function (torrentItem, torrentInfoTab) {
    'use strict';

    function torrentSvc($resource) {
        var self = {};

        self.getDownloading = function (success) {
            $resource("/api/torrents/downloading").query({}, success);
        }

        self.getChecking = function (success) {
            $resource("/api/torrents/checking").query({}, success);
        }

        self.getPaused = function (success) {
            $resource("/api/torrents/paused").query({}, success);
        }

        self.getCompleted = function (success) {
            $resource("/api/torrents/completed").query({}, success);
        }

        self.getAll = function (success) {
            $resource("/api/torrents").query({}, success);
        };

        self.getTorrentData = function (id) {
            ///<summary>Получает данные  для вкладок о торренте</summary>

            return [
                torrentInfoTab({
                    name: "Common",
                    properties: {
                        SavePath: {
                            name: "Save path",
                            value: "sdcard/torrent"
                        }
                    }
                }),
                torrentInfoTab({
                    name: "Data",
                    properties: {
                        SavePath: {
                            name: "Save path",
                            value: "sdcard/torrent"
                        }
                    }
                }),
                torrentInfoTab({
                    name: "Trackers",
                    properties: [
                        {
                            name: "Save path",
                            value: "sdcard/torrent"
                        },
                        {
                            name: "Total size",
                            value: "49.9 MB"
                        },
                        {
                            name: "Pieces",
                            value: "100 x 512 kb"
                        },
                        {
                            name: "Avaliability",
                            value: "100.0%"
                        },
                        {
                            name: "Created on",
                            value: "2011-02-25"
                        },
                        {
                            name: "Hash",
                            value: "sksldjflsdjflsdjlwioeuroiwjlkvmsdkl"
                        },
                        {
                            name: "Created with",
                            value: "mktorrent 1.0"
                        },
                        {
                            name: "Comment",
                            value: "Torrent created and tracked by"
                        }
                    ]
                })
            ];
        }

        return self;
    }

    //    config.$inject = ['$routeProvider'];

    return torrentSvc;
});