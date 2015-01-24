define([
    "signalr",
    "signalr-proxies"
], function () {
    return function (updateListFunc, updateTorrentInfoFunc) {
        var self = {};
        var client = $.connection.AppHub.client;
        client.updateTorrents = function (data) {
            updateListFunc();
            updateTorrentInfoFunc();
        }
        $.connection.hub.start();

        return self;
    };
});