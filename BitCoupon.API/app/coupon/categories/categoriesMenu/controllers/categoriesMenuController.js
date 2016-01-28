(function () {

    angular.module("couponModule")
        .controller("CategoriesMenuController", ["CouponService", function (CouponService) {

            var ctrl = this;
            ctrl.service = CouponService


        }]);

})();