(function () {

    angular.module("couponModule")
        .controller("AlbumController", ["CouponService", "FileUploader", "$state", "growl", function (CouponService, FileUploader, $state, growl) {

            var album = this;
            album.service = CouponService;
            album.uploader = new FileUploader();
            album.finish = finishBtn;



            album.uploader.filters.push({
                name: 'extensionFilter',
                fn: function (item, options) {
                    var filename = item.name;
                    var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                    if (extension == "jpg" || extension == "jpeg")
                        return true;
                    else {
                        growl.warning("Invalid file format. Please select a file with jpg/jpeg format and try again.")
                        
                        return false;
                    }
                }
            });

           
          

            album.uploader.filters.push({
                name: 'sizeFilter',
                fn: function (item, options) {
                    var fileSize = item.size;
                    fileSize = parseInt(fileSize) / (1024 * 1024);
                    if (fileSize <= 2)
                        return true;
                    else {

                        growl.warning("Selected file exceeds the 2MB file size limit.Please choose a new file and try again.");
                        return false;
                    }
                }
            });

            album.uploader.filters.push({
                name: 'itemResetFilter',
                fn: function (item, options) {
                    if (this.queue.length < 6)
                        return true;
                    else {
                        growl.warning("You have exceeded the limit of uploading files.");
                        return false;
                    }
                }
            });

            album.uploader.onBeforeUploadItem = function (item) {
                item.url = "/api/ImagesUploadApi/UploadFile/?id=" + $state.params.id+"&edit=false"
            };

            

            function finishBtn() {
                growl.info("Gallery added. Enjoy!", { ttl: 10000 });
                $state.go("index");
            };
            album.uploader.onCompleteItem = function (fileItem, response, status, headers) {
                
                
                if (status == 400)
                {
                    growl.warning("You have exceeded the limit of uploading pictures.");
                }
                if (status == 417)
                {
                    growl.warning("Problem with upload service, please try again later.");
                }
                
            };
           



    }]);

})();