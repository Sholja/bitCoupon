(function () { 

///Directive for search expired coupons
angular.module("couponModule")
   .directive("coupExpiredSearch", function () {
       return {
           templateUrl: '/app/coupon/couponList/templates/searchExpired.html',
           controller: 'SearchCtrl as search'
       }
   });
})();