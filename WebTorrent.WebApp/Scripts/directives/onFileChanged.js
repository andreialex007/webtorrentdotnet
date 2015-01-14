define([], function () {
    return function () {
        return {
            restrict: 'A',
            link: function ($scope, element, atttributes) {
                element.on("click", function () {
                    var fileInput = element.parent().find("input[type='file']")[0];
                    fileInput.click();
                });
                element.next().on("change", function (event) {
                    $scope.fileChanged(event);
                });
            }
        }
    };
});