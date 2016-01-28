(function () {

    angular.module("accountModule")
    .directive("coupCouponUser", function () {
        return {
            templateUrl: '/app/account/profile/templates/couponUser.html',
            controller: 'BuyerInfoCtrl as buyerInfoCtrl'
        }
    });

})();