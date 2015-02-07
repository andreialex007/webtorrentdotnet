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

    return function ($scope, $routeParams, userSvc) {

        //Наследуемся от базового
        baseCtrl.call(this, $scope, $routeParams);

        userSvc.getUsers(function (data) {
            debugger;
            $scope.users = data;
        });

        //        $scope.users = [
        //            new userItem({
        //                id: "1",
        //                name: "name",
        //                email: "email",
        //                role: "role"
        //            }),
        //            new userItem({
        //                id: "1",
        //                name: "name",
        //                email: "email",
        //                role: "role"
        //            }),
        //            new userItem({
        //                id: "1",
        //                name: "name",
        //                email: "email",
        //                role: "role"
        //            })
        //        ];

        $scope.editUserModal = new editUserModal();
        $scope.editUserModal.onSaved = function () {

            debugger;

            userSvc.saveUser({
                Id: $scope.editUserModal.userId,
                Name: $scope.editUserModal.userName,
                Email: $scope.editUserModal.email,
                Password: $scope.editUserModal.password,
                Role: $scope.editUserModal.selectedRole.name
            },
            function (data) {

                $scope.users.push(new userItem({
                    Id: $scope.editUserModal.userId,
                    Name: $scope.editUserModal.userName,
                    Email: $scope.editUserModal.email,
                    Role: $scope.editUserModal.selectedRole.name
                }));

                $scope.editUserModal.userId = 0;
                $scope.editUserModal.userName = "";
                $scope.editUserModal.email = "";
                $scope.editUserModal.name = "";
                $scope.editUserModal.password = "";
            });



        }
        $scope.addUser = function () {
            $scope.editUserModal.show();
            console.log("add user");
        }

        $scope.editUser = function (user) {
            debugger;
        }


        console.log("users controller");
    };
});
