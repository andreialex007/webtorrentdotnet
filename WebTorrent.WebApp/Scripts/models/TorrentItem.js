define([], function () {

    function torrentItem(json) {
        var self = $.extend(true, {}, json);
        self.selected = false;
        self.checked = false;
        return self;
    }

    return torrentItem;

});