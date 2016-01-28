(function(){

    angular.module("couponModule")
        .controller("DeleteController", ["CouponService", function (CouponService) {

            var ctrl = this;
            ctrl.service = CouponService;

        }]);

})();