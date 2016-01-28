(function(){
    
    ///Factory for coupons (connects to web api controller for coupons)
    angular.module("couponModule")
    .factory("CouponFactory", ["$resource", function ($resource) {
        return $resource("/api/couponsapi/:id", null,
            {
                'update': { method: "put" },
                'buyCoupon': {method: "get", url: "/api/buycoupons/:id"}
            });
    }]);

})();

