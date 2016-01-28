(function () {

    angular.module("mapModule").controller("mapEditController", ["$http", "$state", "growl", "CouponService", function ($http, $state, growl,CouponService) {
        var self = this;
        self.service = CouponService;
       
        self.clear = clear;
        self.saveMyCoord = saveMyCoord;
        self.isDisabledSave = true;
       
        setTimeout(function () { initialize(); }, 2000);

        var myCenter = new google.maps.LatLng(43.83789239125797, 18.309952392578125);
        var geocoder;
        var count;
        var myLoc = self.service.editCouponSelected.Locations;

        if (jQuery.isEmptyObject(myLoc) == false) {
            count = myLoc.length;
        }
        else {
            count = 0;
        }
           
        
        

       
        var markers = [];
      
        function initialize() {

            var mapProp = {
                center: myCenter,
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            self.map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
            geocoder = new google.maps.Geocoder;
           
            

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
                        myLoc[count] = GoogleApi;

                    }
                    count++;
               
            });
            
            if (jQuery.isEmptyObject(myLoc)==false) {
                
                
                for (var i = 0; i < myLoc.length; i++) {
                    
                    var location = {
                        lat: parseFloat(myLoc[i].Lang.replace(',', '.')),
                        lng: parseFloat(myLoc[i].Long.replace(',', '.'))
                    }
                   
                    var infowindow = new google.maps.InfoWindow;
                    geocodeLatLng(location, infowindow);
                }
               
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


            
        }

        

        function clear() {
            
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];


            self.isDisabledSave = false;
            count = 0;
            myLoc = []
          }
       

        function saveMyCoord() {
            var data = JSON.stringify(myLoc);
            $http.put(
                "/api/googleapi", data
              ).then(function (response) {
                growl.info("location/s saved", { ttl: 10000 });
                
            }, function (error) {
                if (error.status == 400) {
                    growl.info("please select location to save it")
                }
                else if (error.status == 403) {
                    growl.info("You allready created your locations. Please go to edit mode to edit your coupon")
               }
                console.log(error);
            });
        }

    }]);

})();