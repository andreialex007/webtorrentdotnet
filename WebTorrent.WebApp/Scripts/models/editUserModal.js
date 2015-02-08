define([], function () {
    return function () {
        var self = {};

        self.userId = 0;
        self.userName = "";
        self.email = "";
        self.password = "";

        self.isValid = function () {
            return !!self.errorsString;
        };
        self.errorsString = "";

        self.avaliableRoles = [
        {
            name: "Administrator",
            value: "1"
        },
        {
            name: "User",
            value: "2"
        }
        ];

        self.selectedRole = self.avaliableRoles[1];

        self.visible = false;
        self.hide = function () {
            self.userId = 0;
            self.userName = "";
            self.email = "";
            self.name = "";
            self.password = "";
            self.errorsString = "";
            self.visible = false;
        }

        self.show = function () {
            self.visible = true;
        }

        self.save = function () {
            self.validate();
            if (!self.isValid()) {
                self.onSaved();
                self.hide();
            }
        }

        self.onSaved = function () { };

        self.validate = function () {
            self.errorsString = "";
            if (!self.userName) {
                self.errorsString += "\r\n" + "Имя пользователя обязательно для заполнения";
            }

            if (!validateEmail(self.email)) {
                self.errorsString += "\r\n" + "Необходимо указать корректный email";
            }

            if (self.userId == 0 && !self.password) {
                self.errorsString += "\r\n" + "Необходимо указать пароль";
            }
        }

        function validateEmail(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        }

        return self;
    };

});