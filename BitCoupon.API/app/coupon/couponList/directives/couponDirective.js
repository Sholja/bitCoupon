(function(){

    ///Coupon directive, it uses coupon temaplate to render all coupons at once
    angular.module("couponModule")
        .directive("coupCouponsList", function () {
        return {
            templateUrl: '/app/coupon/couponList/templates/coupon.cshtml',
            controller: 'CouponsCtrl as couponsCtrl'
        }
    });

})();