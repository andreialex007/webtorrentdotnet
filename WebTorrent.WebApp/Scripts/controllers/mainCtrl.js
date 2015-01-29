define([
    "Scripts/signalr/connectionInit",
    "Scripts/controllers/baseCtrl"
], function (
    connectionInit,
    baseCtrl) {
    'use strict';

    function mainCtrl($scope, $routeParams, torrentSvc) {

        //Наследуемся от базового
        baseCtrl.call(this, $scope, $routeParams);

        $scope.torrents = [];

        $scope.searchText = "";

        $scope.filteredTorrents = function () {
            return $.grep($scope.torrents, function (x) { return x.Name.toLowerCase().indexOf($scope.searchText.toLowerCase()) > -1; });
        }

        $scope.getTorrentsFunction = function () {
            console.log("getTorrentsFunction");
        }

        $scope.updateTorrentInfoFunction = function () {
            console.log("update torrent info");
        }

       

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

        $scope.anyChecked = function () {
            return $.grep($scope.torrents, function (x) { return x.checked === true; }).length > 0;
        }

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
                var checkedItems = $.map($.grep($scope.torrents, function (x) { return x.checked === true; }), function (x) { return x.Id; });
                $.extend($scope.torrents, items);
                $(checkedItems).each(function (index, element) {
                    var found = $.grep($scope.torrents, function (x) { return x.Id == element; })[0];
                    found.checked = true;
                });
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
