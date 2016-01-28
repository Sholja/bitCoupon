(function(){

    ///Factory for account module
    'use strict'
    angular.module("accountModule")
        .factory("AccountFactory", ["$resource", function ($resource) {
            return $resource("/api/account/:id", null, {
                "GetUser": {
                    method: "get",
                    url: "/api/account/UserInfo"
                }
            });
        }]);
})();