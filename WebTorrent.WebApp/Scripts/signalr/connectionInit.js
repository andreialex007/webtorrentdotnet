define([
    "signalr",
    "signalr-proxies"
], function () {
    return function (updateListFunc, updateTorrentInfoFunc) {
        var self = {};
        var client = $.connection.AppHub.client;
        $.connection.hub.stop();
        client.updateTorrents = function (data) {
            updateListFunc();
            updateTorrentInfoFunc();
        }
        $.connection.hub.start();

        return self;
    };
});