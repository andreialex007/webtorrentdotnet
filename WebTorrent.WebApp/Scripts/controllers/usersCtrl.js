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

        function updateUsers() {
            userSvc.getUsers(function (data) {
                $scope.users = data;
            });
        }

        function cleanUpModal() {
            $scope.editUserModal.userId = 0;
            $scope.editUserModal.userName = "";
            $scope.editUserModal.email = "";
            $scope.editUserModal.name = "";
            $scope.editUserModal.password = "";
        }

        updateUsers();

        $scope.editUserModal = new editUserModal();
        $scope.editUserModal.onSaved = function () {

            userSvc.saveUser({
                Id: $scope.editUserModal.userId,
                Name: $scope.editUserModal.userName,
                Email: $scope.editUserModal.email,
                Password: $scope.editUserModal.password,
                Role: $scope.editUserModal.selectedRole.name
            },
            function (data) {

                updateUsers();
                cleanUpModal();

            });
        }
        $scope.addUser = function () {
            $scope.editUserModal.show();
            console.log("add user");
        }

        $scope.editUser = function (user) {

            $scope.editUserModal.userId = user.Id;
            $scope.editUserModal.userName = user.Name;
            $scope.editUserModal.email = user.Email;
            $scope.editUserModal.selectedRole = $.grep($scope.editUserModal.avaliableRoles, function (x) { return x.name == user.Role; })[0];
            $scope.editUserModal.show();

        }

        $scope.deleteUser = function (user) {
            userSvc.deleteUser(user.Id, function () {
                updateUsers();
                cleanUpModal();
            });
        }

        console.log("users controller");
    };
});
