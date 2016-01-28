(function () {

    angular.module("accountModule")
    .directive("coupBoughtCouponDisplay", function () {
        return {
            templateUrl: '/app/account/profile/templates/boughtCouponDisplay.html',
            controller: 'BuyerInfoCtrl as buyerInfoCtrl'
        }
    });

})();