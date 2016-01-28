
(function () {

    ///Controller used to get all coupons form coupon service
    ///and using directive render coupons on index page
    angular.module("couponModule")
          .controller("CouponsCtrl", ["CouponService", "AccountService", function (CouponService, AccountService) {
          var coupons = this;
          coupons.service = CouponService;
          coupons.accountService = AccountService;
    }]);

    
})();



    
