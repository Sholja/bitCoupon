(function () { 

///Search coupon directive, with search form in his template
angular.module("couponModule")
   .directive("coupCouponsSearch", function () {
       return {
           templateUrl: '/app/coupon/couponList/templates/searchCoupon.cshtml',
           controller: 'SearchCtrl as search'
       }
   });
})();