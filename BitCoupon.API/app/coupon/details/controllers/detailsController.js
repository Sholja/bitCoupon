(function () {
    ///Controller for details of coupon
    angular.module("couponModule")
        .controller("DetailsController", ["CouponService", "AccountService", 'usSpinnerService', function (CouponService, AccountService, usSpinnerService) {

            var ctrl = this;
            ctrl.service = CouponService;
            ctrl.accountService = AccountService;
            ctrl.getCoupon = getCoupon;
            ctrl.showDiv = showDiv;
            ctrl.hideDiv = hideDiv;
            ctrl.show = true;
            ctrl.pics = function () {
                imageSlider.reload();
            }
            ctrl.showPanels = '1';

            // ctrl.service.selectedCoupon.CouponRate = 1;

            //ctrl.isReadonly = ctrl.service.rated;
            ///Calls method from coupon service to get coupon details
            function getCoupon(index) {
                CouponService.detailsCoupon(index);
            }

            function showDiv() {
                usSpinnerService.spin('spinner-1');
                ctrl.show = true;
            };

            function hideDiv() {
                ctrl.show = false;
            };


            


        }]);

})();