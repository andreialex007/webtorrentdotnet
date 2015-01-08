define([], function () {

    function torrentItem(json) {
        var self = $.extend(true, {}, json);
        return self;
    }

    return torrentItem;
});