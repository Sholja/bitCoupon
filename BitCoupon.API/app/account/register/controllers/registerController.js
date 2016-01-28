(function(){

    ///Controller for register user
    angular.module("accountModule")
        .controller("RegisterController", ["AccountService", function (AccountService) {
            var ctrl = this;
            ctrl.service = AccountService;

            var ctrl = this;

            ctrl.user = {};
            ctrl.user.AvatarUrl = "http://res.cloudinary.com/dvnnqotru/image/upload/v1445558026/Andy_rksodp.png";
            ctrl.register = register;

            ///cals register function from account service
            function register() {
                ctrl.service.register(ctrl.user);
            };
        }]);

})();