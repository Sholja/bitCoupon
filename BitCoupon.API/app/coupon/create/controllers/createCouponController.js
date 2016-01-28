(function () {
   ///Controller for create coupons
   angular.module("couponModule")
          .controller("CreateCouponCtrl", ["CouponService", "$http", 'FileUploader', '$state', 'growl', "CouponFactory", function (CouponService, $http, FileUploader, $state, growl, CouponFactory) {

              
              var coupon = this;
              coupon.service = CouponService;
              coupon.uploader = new FileUploader({});
              coupon.url = "";
              coupon.id = "";
                         
              getCategories();
              coupon.service.coupon.CategoryId = 2;
             //create coupon
              coupon.create = createCoupon;

             function getCategories() {
                 $http.get("/api/categoriesapi/getcategoriesforcreate").then(function (response) {
                     coupon.service.createCategoryList = response.data;
                     
                  }, function (error) {
                      console.log(error);
                      var modelState = error.data.ModelState;
                      angular.forEach(modelState, function (errorMessages, attribute) {

                          if (attribute != "$id") {
                              angular.forEach(errorMessages, function (value, key) {
                                  growl.error(value, { ttl: 10000 });
                              });
                          }
                      });

                  });
              }
           
             
              ///Function for create coupon
              function createCoupon() {
                  var text = tinyMCE.activeEditor.getContent({ format: 'raw' });
                
                  coupon.service.coupon.DescriptionOnSellerPage = text;
                  coupon.service.coupon.$save(function (response) {
                  coupon.uploader.url = '/api/ImagesUploadApi/UploadFile/?id=' + response.CouponId + '&edit=false';
                  coupon.url = coupon.uploader.url;
                  coupon.service.id = response.CouponId;
                     
                      coupon.uploader.uploadAll();
                     
                      $state.go('createAlbum', { id: coupon.service.id });
                  }, function (error) {
                      console.log(error);
                      var modelState = error.data.ModelState;
                      angular.forEach(modelState, function (errorMessages, attribute) {

                          if (attribute != "$id") {
                              angular.forEach(errorMessages, function (value, key) {
                                  growl.error(value, { ttl: 10000 });
                      });
                          }
                      });

                  });


                   };

              coupon.uploader.filters.push({
                  name: 'extensionFilter',
                  fn: function (item, options) {
                      var filename = item.name;
                      var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                      if (extension == "jpg" || extension == "jpeg"
                          )
                          return true;
                      else {
                          growl.warning("Invalid file format. Please select a file with pdf/doc/docs or rtf format and try again.");
                          return false;
                      }
                  }
              });

              coupon.uploader.onBeforeUploadItem = function (item) {
                  item.url = coupon.uploader.url;
              };

             
              coupon.uploader.onCompleteItem = function (fileItem, response, status, headers) {
                  console.log("moj status", status)
                  if (status == 417) {
                      growl.info("Coupon Created, wait for admin aprooval. We had a problem with upload service, please contact our team.", { ttl: 10000 });

                  }
                  else { 
                  growl.info("Coupon Created, wait for admin aprooval. Add gallery of pictures for your coupon if you want", { ttl: 10000 });
                  }
                  coupon.service.coupon = new CouponFactory();
                  coupon.service.createCategoryList = [];
              };
             
             
              var count = 0;
              coupon.uploader.onAfterAddingFile = function (fileItem) {
                  if (count > 0)
                  {
                  coupon.uploader.queue.shift();
                 }
                  count++;
              };
          }]);
})();
             

