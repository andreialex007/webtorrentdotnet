define([], function () {
    return function (json) {
        var self = $.extend(true, {}, json);
        self.delete = function() {
            console.log("delete");
        };
        self.edit = function() {
            console.log("edit");
        };
        return self;
    };

});