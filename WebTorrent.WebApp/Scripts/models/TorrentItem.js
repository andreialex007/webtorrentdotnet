define([], function () {

    function torrentItem(json) {
        var self = $.extend(true, {}, json);
        self.selected = false;
        self.checked = false;
        self.downloadVisible = self.DownloadingPercentage == 100;
        return self;
    }

    return torrentItem;

});