define([
    "Scripts/models/torrentItem",
    "Scripts/models/torrentInfoTab"
], function (torrentItem, torrentInfoTab) {
    'use strict';

    function torrentSvc() {
        var self = {};

        self.getDownloading = function () {
            return [
                new torrentItem({
                    id: 5,
                    percentage: 25,
                    name: "vcredist_x64.exe",
                    isChecked: true
                }),
                new torrentItem({
                    id: 15,
                    percentage: 88,
                    name: "[NNM-Club.me]_Klyuchi dlya ESET NOD32, Kaspersky, Avast, Dr.Web, Avira 25.12.14",
                    isChecked: true
                })
            ];
        }

        self.getChecking = function () {
            return [
                new torrentItem({
                    id: 114,
                    percentage: 71,
                    name: "torbrowser-install-4.0.2_en-US.exe",
                    isChecked: true
                })
            ];
        }

        self.getPaused = function () {
            return [
               new torrentItem({
                   id: 114,
                   percentage: 5,
                   name: "16_prajs_zabor_mramor_iz_betona (3).doc",
                   isChecked: true
               })
            ];
        }

        self.getCompleted = function () {
            return [
                new torrentItem({
                    id: 1,
                    percentage: 50,
                    name: "Ubuntu-12.04.1-server-i386.iso",
                    isChecked: false
                }),
                new torrentItem({
                    id: 2,
                    percentage: 100,
                    name: "Dsl-4.4.exe",
                    isChecked: false
                }),
                new torrentItem({
                    id: 3,
                    percentage: 75,
                    name: "Interstellar CAMRip",
                    isChecked: false
                })
            ];
        }

        self.getAll = function () {
            ///<summary>Получает список всех торрентов данного пользователя</summary>

            return self.getChecking()
                .concat(self.getDownloading())
                .concat(self.getCompleted())
                .concat(self.getPaused());
        };

        self.getTorrentData = function (id) {
            ///<summary>Получает данные  для вкладок о торренте</summary>

            return [
                torrentInfoTab({
                    name: "Trackers",
                    properties: {
                        SavePath: {
                            name: "Save path",
                            value: "sdcard/torrent"
                        },
                        TotalSize: {
                            name: "Total size",
                            value: "49.9 MB"
                        },
                        Pieces: {
                            name: "Pieces",
                            value: "100 x 512 kb"
                        },
                        Avaliability: {
                            name: "Avaliability",
                            value: "100.0%"
                        },
                        CreatedOn: {
                            name: "Created on",
                            value: "2011-02-25"
                        },
                        Hash: {
                            name: "Hash",
                            value: "sksldjflsdjflsdjlwioeuroiwjlkvmsdkl"
                        },
                        CreatedWith: {
                            name: "Created with",
                            value: "mktorrent 1.0"
                        },
                        Comment: {
                            name: "Comment",
                            value: "Torrent created and tracked by"
                        },
                    }
                }),
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
                })
            ];
        }

        return self;
    }

    //    config.$inject = ['$routeProvider'];

    return torrentSvc;
});