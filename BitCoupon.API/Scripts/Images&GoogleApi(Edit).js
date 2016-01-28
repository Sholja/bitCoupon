
//api edit 



var map;
var myCenter = new google.maps.LatLng(43.808742, 18.400850);
var count = 0;
var myplaces = [];
var geocoder = new google.maps.Geocoder;
setTimeout(function () { initialize(); }, 1000);

function initialize() {
    console.log("init");
    var mapProp = {
        center: myCenter,
        zoom: 9,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("googleMap1"), mapProp);

    console.log(document.getElementById("googleMap1"));
    google.maps.event.addListener(map, 'click', function (event) {

        var CouponId = $("#couponId").val();
        console.log(CouponId);
        if (count < 3) {
            var infowindow = new google.maps.InfoWindow;
            geocodeLatLng(event.latLng,infowindow);
            var GoogleApi = {
                CouponId: CouponId,
                Long: event.latLng.lng(),
                Lang: event.latLng.lat()
            };
            myplaces[count] = GoogleApi;
            console.log(myplaces)

        }
        count++;
    });

}
function geocodeLatLng(location, infowindow) {

    geocoder.geocode({ 'location': location }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                map.setZoom(11);
                var marker = new google.maps.Marker({
                    position: location,
                    map: map
                });
                infowindow.setContent(results[1].formatted_address);
                infowindow.open(map, marker);
            } else {
                window.alert('No results found');
            }
        } else {
            window.alert('Geocoder failed due to: ' + status);
        }
    });
}


//function placeMarker(location) {


//    var marker = new google.maps.Marker({
//        position: location,
//        map: map,
//    });

//    var infowindow = new google.maps.InfoWindow({
//        content: 'Latitude: ' + location.lat() + '<br>Longitude: ' + location.lng()
//    });
//    infowindow.open(map, marker);


//}
google.maps.event.addDomListener(window, 'load', initialize);

$("#googleClk").click(function (event) {
    event.preventDefault();
    console.log("my places are" + myplaces);


    $.ajax({
        contentType: 'application/json',
        data: JSON.stringify(myplaces),
        method: "post",
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('tokenKey')
        },
        url: "/GoogleApis/Edit/"
    }).success(function (response) {
        bootbox.alert("new locations saved");
        $("#googleClk").attr('disabled','disabled');;
    }).error(function (response) {
        console.log("error");
    });

});

var id = $("#couponId").val();
console.log("coupon id " + couponId);







// images edit

//help functions !
var btn;
var finishBtn = $("#finishBtn")
var text = '<p style="padding-left: 15px"> No images</p>';

$('.UploadLoading').on('click', function () {
    btn = $(this).button('loading');
    finishBtn.attr('disabled','disabled');;
})

function EnableBtnS() {
    btn.button('reset');
    finishBtn.removeAttr('disabled', 'disabled');
}

function GETAllimages() {

    $.ajax({
        url: "/Images/GetImages/" + id,
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('tokenKey')
        }

    }).success(function (response) {
         GetImages(response);
         ErrorStatus()
    }).error(function (error) {
        console.log("moj error je " + error);

    });
};

function GetImages(response) {
    for (var i = 0; i < response.length; i++) {
        var temp = imgTemplate.replace("{IMGURL}", response[i].ImageUrl).replace("{IMAGEID}", response[i].Id).replace("{IMAGEID}", response[i].Id);
        imgDiv.append(temp);

    }

};
    
function finish() {
    bootbox.hideAll();
    bootbox.alert("succesfully edited")

    setTimeout(function () { window.location.href = "/Coupons/Index"; }, 3000);

};

function ErrorStatus() {
    var count = imgDiv.find('img').length;
    if (count === 0) {
        imgDiv.append(text);
    }
    else {
        imgDiv.find('p').remove();
    }
};

//code for edit

$("#uploadForm").submit(function (event) {

  
    $("[data-error-for]").html("");
    event.preventDefault();

    var data = new FormData(this);

    console.log(data);
    $.ajax({
        url: "/Images/EditImage/" + id,
        method: "post",
        data: data,
        processData: false,
        contentType: false,
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('tokenKey')
        }

    }).success(function (response) {
        EnableBtnS();
      
        if (response.length != 0) {
            GetImages(response)
        }
           ErrorStatus()

    }).error(function (error) {
        var response = JSON.parse(error.responseText);
        for (var i = 0; i < response.length; i++) {
            var error = response[i];
            var fieldKey = error.Key;
            var message = error.Message;

            $("[data-error-for='" + fieldKey + "']").html(message);
        }
        EnableBtnS();
    
    });


});

var imgTemplate = '<div id="picture{IMAGEID}" class="col-md-4 col-lg-4 col-sm-6" style="padding-top: 10px"> <img name"myimage" class="img-thumbnail" id="currentPicture" onclick="deleteFunc({IMAGEID})" src="{IMGURL}" /></div>'
var imgDiv = $("#imagesPlace")


setTimeout(function () { GETAllimages(); }, 500);

function deleteFunc(id) {
    var pictureToDelete = $("#picture" + id)

    var data = { id: id }
    $.ajax({
        url: "/Images/DeletePicture",
        method: "post",
        data: data,
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem('tokenKey')
        }
    }).success(function (response) {
        pictureToDelete.remove();
        ErrorStatus()
        // imgDiv.html(" ");
        $("[data-error-for]").html("");
        // GETAllimages();
    }).error(function (resposne) {

    });

};

$(document).ready(function () {

   

});

$('.pop').popover().click(function () {
    setTimeout(function () {
        $('.pop').popover('hide');
    },5000);
});


setTimeout(function () {
    $(".pop").click();
}, 1000);



