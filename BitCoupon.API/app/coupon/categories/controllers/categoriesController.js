(function () {

    angular.module("couponModule")
    .controller("CategoriesController", ["CouponService", function (CouponService) {

        var ctrl = this;
        ctrl.service = CouponService

    }]);

})();