(function(){
    ///Login controller for account
    angular.module("accountModule")
        .controller("LoginController", ["AccountService", function (AccountService) {

            var ctrl = this;
            ctrl.user = {};
            ctrl.service = AccountService;
            ctrl.login = login;

            ///cals login function from account service
            function login() {
                AccountService.login({ username: ctrl.user.Username, password: ctrl.user.Password });
            };

        }]);

})();