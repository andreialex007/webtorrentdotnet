define([], function () {
    return function () {
        return {
            restrict: 'A',
            link: function ($scope, element, atttributes) {
                $scope.$watch(atttributes.ngVisible, function (visible) {
                    element.css('display', visible ? 'block' : 'none');
                });
            }
        }
    };
});