(function () {

    angular.module("couponModule")
        .controller("EditController", ["CouponService", "FileUploader", "$state", "growl", function (CouponService, FileUploader, $state, growl) {

            var edit = this;
            edit.service = CouponService;
            edit.editCoupon = editCoupon;



            edit.uploader = edit.uploader = new FileUploader({
                method:"put"
            });

           
            edit.uploader.onBeforeUploadItem = function (item) {
                item.url = "/api/ImagesUploadApi/UploadFile/?id=" + edit.service.editCouponSelected.CouponId;
              
            };
         
            function editCoupon() {
                 
                 edit.service.editCouponSelected.$update({ id: edit.service.editCouponSelected.CouponId }, function (response) {
                  
                    edit.uploader.uploadAll();

                    
                    $state.go('editAlbum', { id: edit.service.editCouponSelected.CouponId });
                }, function (err) {
                    console.log(err);
                    if (err.status == 406)
                    {
                        growl.warning("Your total number of coupons is not divisible with your required number of coupons.");
                    }
                    if (err.status == 401)
                    {
                        growl.warning("This is not your coupon, you can't edit it.");
                    }


                });

            };

         
            var count = 0;

            edit.uploader.onAfterAddingFile = function (fileItem) {
                if (count > 0) {
                    edit.uploader.queue.shift();

                }
                count++;
                growl.info("Excelent choice");
            };
            

    }]);


})();