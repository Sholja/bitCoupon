(function () {

    angular.module("couponModule")
        .controller("EditAlbumController", ["CouponService", "FileUploader", "$state", "growl", "$http", function (CouponService, FileUploader, $state, growl, $http) {

            var editAlbum = this;
            editAlbum.service = CouponService;
             editAlbum.uploader = editAlbum.uploader = new FileUploader({
             });

             editAlbum.deleteImg = deleteImg;
             editAlbum.finishBtn = finishBtn;
            


             editAlbum.uploader.onBeforeUploadItem = function (item) {
                 item.url = "/api/ImagesUploadApi/UploadFile/?id=" + $state.params.id;
             };

             function finishBtn() {
                 growl.info("Gallery succesfuly edited. Enjoy!", { ttl: 10000 });
                 
                 var list = editAlbum.service.couponsList;

                 for (var i = 0; i < list.length; i++) {   // removes from list edited item because edit needs admin aproval
                    if (list[i].CouponId == editAlbum.service.editCouponSelected.CouponId)
                     {
                         editAlbum.service.couponsList.splice(i, 1);
                     }
                 }
               $state.go("index");
                 
             };

             function deleteImg(item) {
                $http.delete("/api/ImagesUploadApi/" + item.Id).then(function (response) {
                     growl.info("Picture succesfuly deleted.", { ttl: 10000 });
                     var index = editAlbum.service.editCouponSelected.Images.indexOf(item);
                     editAlbum.service.editCouponSelected.Images.splice(index, 1);

                 }, function (err) {
                     console.log(err);
                 });
             }


            //filters for upload

             editAlbum.uploader.filters.push({
                 name: 'extensionFilter',
                 fn: function (item, options) {
                     var filename = item.name;
                     var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                     if (extension == "jpg" || extension == "jpeg")
                         return true;
                     else {
                         growl.error("Invalid file format. Please select a file with jpg/jpeg format and try again.", { ttl: 10000 });
                         return false;
                     }
                 }
             });

            


             editAlbum.uploader.filters.push({
                 name: 'sizeFilter',
                 fn: function (item, options) {
                     var fileSize = item.size;
                     fileSize = parseInt(fileSize) / (1024 * 1024);
                     if (fileSize <= 2)
                         return true;
                     else {
                         growl.error("Selected file exceeds the 2MB file size limit.Please choose a new file and try again.", { ttl: 10000 });
                         return false;
                     }
                 }
             });

             editAlbum.uploader.filters.push({
                 name: 'itemResetFilter',
                 fn: function (item, options) {
                     if (editAlbum.service.editCouponSelected.Images.length < 6 )
                         return true;
                     else {
                         growl.error("Please delete some pictures to upload new ones", { ttl: 10000 })
                         return false;
                     }
                 }
             });

             editAlbum.uploader.onCompleteItem = function (fileItem, response, status, headers) {
               
                 if (status == 200)
                 {
                 editAlbum.service.editCouponSelected.Images.push(response);
                 }
                 else if (status == 400)
                 {
                     growl.error("Please delete some pictures to upload new ones", { ttl: 10000 });
                 }
             };

    }]);

})();