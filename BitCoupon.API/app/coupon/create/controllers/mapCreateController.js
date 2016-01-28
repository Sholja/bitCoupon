(function () {

    
    angular.module("mapModule").controller("MapController", ["$http", "CouponService", "$state", "growl", function ($http, CouponService, $state, growl) {

        var self = this;

        self.service = CouponService;
        self.saveCoord = saveCoord;
        self.clear = clear;
        self.isDisabled = false;
        setTimeout(function () { initialize(); }, 1000); // sets timeout allowing maps to initialize 
       
        var myCenter = new google.maps.LatLng(43.808742, 18.400850);
        var myplaces = [];
        var count = 0;
        var markers = [];
        var geocoder = new google.maps.Geocoder;
        function initialize() {
            
            var mapProp = {
                center: myCenter,
                zoom: 9,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            self.map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
            google.maps.event.addListener(self.map, 'click', function (event) {
               
                // max selected locations is 3
                var infowindow = new google.maps.InfoWindow();
                if (count < 3) {
                    geocodeLatLng(event.latLng, infowindow);
                    var GoogleApi = {
                        CouponId: $state.params.id,
                        Long: event.latLng.lng(),
                        Lang: event.latLng.lat()
                    };
                    myplaces[count] = GoogleApi;
                  
                 }
                count++;
            });
          }

        function geocodeLatLng(location, infowindow) {

            geocoder.geocode({ 'location': location }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        self.map.setZoom(9);
                var marker = new google.maps.Marker({
                position: location,
                 map: self.map
            });
            markers.push(marker);
                        infowindow.setContent(results[1].formatted_address);
                        infowindow.open(self.map, marker);
                    } else {
                        window.alert('No results found');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }
            });
         }

       


        function saveCoord() {
            if (myplaces.count == 0)
            {
                var GoogleApi = {
                    CouponId: $state.params.id,
                    Long: 0,
                    Lang: 0
                };
                myplaces.push(GoogleApi);
               
            }

            data = JSON.stringify(myplaces);

           $http.post( url = "api/googleapi", data
            ).then(function (response) {
                growl.info("location/s saved", { ttl: 10000 });
                myplaces = []
                isDisabled();
               // $(".googleBtns").attr('disabled');
               // document.getElementsByClassName(".googleBtns").setAttribute("disabled");
            }, function (error) {
                console.log(error);
                myplaces = []
                if (error.status == 400)
                {
                    growl.info("please select location to save it")
                }
                else if (error.status==403)
                {
                    growl.info("You allready created your locations. Please go to edit mode to edit your coupon")

                }
            });
         }


        //clear map: set pins on null and reset markers array... then reset my places array and finaly reset count to zero :D
        function clear() {
          for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
            myplaces = [];
            count = 0;
        }

        function isDisabled()
        {
            self.isDisabled = true;
        }
       

    }]);

})();