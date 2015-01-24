﻿define([
    "Scripts/signalr/connectionInit"
], function (connectionInit) {
    'use strict';

    function mainCtrl($scope, $routeParams, torrentSvc) {

        $scope.torrents = [];

        $scope.getTorrentsFunction = function () {
            console.log("getTorrentsFunction");
        }

        $scope.updateTorrentInfoFunction = function () {
            console.log("update torrent info");
        }

        $scope.menuItems = [
            { name: "All", url: "/all", icon: "fa-cloud" },
            { name: "Downloading", url: "/downloading", icon: "fa-download" },
            { name: "Completed", url: "/completed", icon: "fa-thumbs-up" },
            { name: "Checking", url: "/checking", icon: "fa-check-square" },
            { name: "Paused", url: "/paused", icon: "fa-pause" }
        ];

        $scope.fileChanged = function (event) {

            var xhr = new XMLHttpRequest();
            var formData = new FormData();
            if (event.target.files.length != 1)
                return;

            formData.append("file", event.target.files[0]);
            xhr.open("POST", "/api/torrents/upload/", true);
            xhr.send(formData);
            xhr.addEventListener("load", function () {
                console.log("file uploaded");
            }, false);
        };

        $scope.deleteTorrent = function () {
            var selectedTorrent = $.grep($scope.torrents, function (x) { return x.selected == true; })[0];
            torrentSvc.deleteTorrent(selectedTorrent.Id, function () {
            });
        }

        $scope.pauseTorrent = function () {
            var selectedTorrent = $.grep($scope.torrents, function (x) { return x.selected == true; })[0];
            torrentSvc.pauseTorrent(selectedTorrent.Id, function () {
            });
        }
        $scope.startTorrent = function () {
            var selectedTorrent = $.grep($scope.torrents, function (x) { return x.selected == true; })[0];
            torrentSvc.startTorrent(selectedTorrent.Id, function () {
            });
        }
        $scope.stopTorrent = function () {
            var selectedTorrent = $.grep($scope.torrents, function (x) { return x.selected == true; })[0];
            torrentSvc.stopTorrent(selectedTorrent.Id, function () {
            });
        }

        //#region active menu item

        $scope.setActive = function (item) {
            $($scope.torrents).each(function (i, el) {
                el.selected = false;
            });
            item.selected = true;
        }

        $scope.isActive = function (item) {
            return item.url == window.location.pathname;
        };

        //#endregion

        var successFunc = function (items) {
            var selectedIndex = 0;
            var selectedItem = $.grep($scope.torrents, function (x) { return x.selected === true; })[0];
            if (selectedItem) {
                selectedIndex = $.inArray(selectedItem, $scope.torrents);
            }

            if ($scope.torrents.length != items.length) {
                $scope.torrents = items;
            } else {
                $.extend($scope.torrents, items);
            }

            if ($scope.torrents.length == 0)
                return;

            try {
                $scope.torrents[selectedIndex].selected = true;
            } catch (e) {
                debugger;
            }
        };

        switch (window.location.pathname) {
            case "/downloading":
                $scope.getTorrentsFunction = function () {
                    torrentSvc.getDownloading(successFunc);
                }
                break;
            case "/completed":
                $scope.getTorrentsFunction = function () {
                    torrentSvc.getCompleted(successFunc);
                }
                break;
            case "/checking":
                $scope.getTorrentsFunction = function () {
                    torrentSvc.getChecking(successFunc);
                }
                break;
            case "/paused":
                $scope.getTorrentsFunction = function () {
                    torrentSvc.getPaused(successFunc);
                }
                break;
            case "/all":
                $scope.getTorrentsFunction = function () {
                    torrentSvc.getAll(successFunc);
                }
                break;
            default:
                throw new Exception("correct path not provided");
        };

        $scope.updateTorrentInfoFunction = function () {

            var selectedItem = $.grep($scope.torrents, function (x) { return x.selected === true; })[0];

            if (!selectedItem)
                return;

            torrentSvc.getTorrent(selectedItem.Id, function (data) {
                $scope.infoTabs = data;
            });
        }

        $(window).trigger("resize");
        setTimeout(function () {
            Metronic.init();
        }, 70);

        $scope.getTorrentsFunction();
        connectionInit($scope.getTorrentsFunction, $scope.updateTorrentInfoFunction);
    }

    return mainCtrl;
});
