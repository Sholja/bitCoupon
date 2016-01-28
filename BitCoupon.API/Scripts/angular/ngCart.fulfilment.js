angular.module('ngCart.fulfilment', [])
    .service('fulfilmentProvider', ['$injector', function($injector){

        this._obj = {
            service : undefined,
            settings : undefined
        };

        this.setService = function(service){
            this._obj.service = service;
        };

        this.setSettings = function(settings){
            this._obj.settings = settings;
        };

        this.checkout = function(){
            var provider = $injector.get('ngCart.fulfilment.' + this._obj.service);
              return provider.checkout(this._obj.settings);

        }

    }])


.service('ngCart.fulfilment.log', ['$q', '$log', 'ngCart', function($q, $log, ngCart){

        this.checkout = function(){

            var deferred = $q.defer();

            $log.info(ngCart.toObject());
            deferred.resolve({
                cart:ngCart.toObject()
            });

            return deferred.promise;

        }

 }])

.service('ngCart.fulfilment.http', ['$http', 'ngCart', 'growl', '$modal', function($http, ngCart, growl, $modal){

    this.checkout = function (settings) {
        var modal = $modal({ keyboard: false, backdrop: false, templateUrl: "/app/common/templates/loading.html", show: "true", placement: "center" });
            return $http.post(settings.url,
                { data: ngCart.toObject(), options: settings.options }
                ).then(function (response) {
                    localStorage.clear(); 
                    window.location = response.data;
                }, function (error) {
                    modal.hide();
                    if (error.status == 404)
                    {
                        growl.error("Your shooping cart is empty. Pleas add coupons and then cehckout.", { ttl: 10000 });
                    }
                    else if (error.status == 400) {
                        growl.error("You have exceeded total number of coupons on one of the coupons you have tried to bught.", { ttl: 10000 });
                    }
                    else if (error.status == 401)
                    {
                        growl.error("You have to be logged in to buy coupons.", { ttl: 10000 });
                    }
                    else {
                        growl.error("There was a problem with PayPal. For any inconvenience please contact them.", { ttl: 10000 });
                    }
                });
        }
 }])


.service('ngCart.fulfilment.paypal', ['$http', 'ngCart', function($http, ngCart){


}]);

