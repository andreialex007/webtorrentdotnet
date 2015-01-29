define([
     "Scripts/controllers/baseCtrl",
     "Scripts/models/userItem",
     "Scripts/models/editUserModal"
], function (
    baseCtrl,
    userItem,
    editUserModal
    ) {
    'use strict';

    return function ($scope, $routeParams, torrentSvc) {

        //Наследуемся от базового
        baseCtrl.call(this, $scope, $routeParams);

        $scope.users = [
            new userItem({
                id: "1",
                name: "name",
                email: "email",
                role: "role"
            }),
            new userItem({
                id: "1",
                name: "name",
                email: "email",
                role: "role"
            }),
            new userItem({
                id: "1",
                name: "name",
                email: "email",
                role: "role"
            })
        ];
        $scope.editUserModal = new editUserModal();
        $scope.editUserModal.onSaved = function() {
            $scope.users.push(new userItem({
                id: $scope.editUserModal.userId,
                name: $scope.editUserModal.userName,
                email: $scope.editUserModal.email,
                role: $scope.editUserModal.selectedRole.name
            }));

            $scope.editUserModal.userId = 0;
            $scope.editUserModal.userName = "";
            $scope.editUserModal.email = "";
            $scope.editUserModal.name = "";

        }
        $scope.addUser = function () {
            $scope.editUserModal.show();
            console.log("add user");
        }


        console.log("users controller");
    };
});
