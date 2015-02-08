define([
    "Scripts/models/userItem",
    "Scripts/models/torrentInfoTab"
], function (userItem, torrentInfoTab) {
    'use strict';

    function userSvc($resource) {
        var self = {};

        self.getUsers = function (success) {
            $resource("/api/users").query({}, function (data) {
                success($.map(data, function (x) { return new userItem(x); }));
            });
        }

        self.getUser = function (id, success) {
            var userResource = $resource('/api/users/:id');
            userResource.get({ id: id }, success);
        }

        self.deleteUser = function (id, success) {
            if (confirm("Вы действительно хотите удалить пользователя?")) {
                var userResource = $resource('/api/users/:id');
                userResource.delete({ id: id }, success);
            }
        }

        self.saveUser = function (userData, success) {
            ///<summary>Сохраняет пользователя</summary>
            ///<param name="userData">Данные пользователя для сохранения</param>
            ///<param name="success">Коллбэк после сохранения</param>

            var User = $resource('/api/users/update');
            var user = new User();
            $.extend(user, userData);
            user.$save(success);
        }
        return self;
    }

    return userSvc;
});