(function(){

    ///Directive for expired coupons
    angular.module("couponModule")
        .directive("coupExpiredCoupon", function(){
        
        return{
        templateUrl: '/app/coupon/couponList/templates/expiredCoupon.html',
        controller: 'CouponsCtrl as couponsCtrl'
}

});

})();