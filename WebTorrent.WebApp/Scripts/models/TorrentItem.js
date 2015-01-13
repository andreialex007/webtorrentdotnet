define([], function () {

    function torrentItem(json) {
        var self = $.extend(true, {}, json);
        self.selected = false;
        return self;
    }

    return torrentItem;

});