/// <reference path="app.js" />
(function () {

    ///Main module of our app

    var app = angular.module("bitCouponApp", ['angularFileUpload',
        'mgcrea.ngStrap', 'ui.router', 'ngCart', 'angular-growl',
        "smart-table", 'angularSpinner', 'ngAnimate',
        'jcs-autoValidate', 'ngResource', 'couponModule',
        'accountModule', 'commentModule', 'feedbackModule',
        "ngTouch",
        "angular-carousel", "mapModule", "mgcrea.ngStrap", "mgcrea.ngStrap.aside", "ui.tinymce"

    ]);

    app.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

        $urlRouterProvider.otherwise("/");

        //Cofiguration of site routes using ui-router
        $stateProvider
            .state('index',
            {
                url: '/',
                templateUrl: '/app/coupon/couponList/templates/index.cshtml',
                controller: 'SearchCtrl',
                controllerAs: 'search'
            })
           
            .state('createCoupon', {
                url: '/coupon/create',
                templateUrl: '/app/coupon/create/templates/create.cshtml',
                controller: 'CreateCouponCtrl',
                controllerAs: 'coupCreate'

            })
             .state('createAlbum', {
                 url: '/coupon/createAlbum/:id',
                 templateUrl: '/app/coupon/create/templates/album.cshtml',
                 controller: 'AlbumController',
                 controllerAs: 'albumCtrl',
                 params: {
                     "id": "id"
                 }

             })
            .state('editCoupon', {
                url: '/coupon/edit/:id',
                templateUrl: '/app/coupon/edit/templates/edit.cshtml',
                controller: 'EditController',
                controllerAs: 'editCtrl',
                resolve: {
                    editCoupon: function (CouponService, $stateParams) {
                        CouponService.editCoupon($stateParams.id)
                    }
                }
            })
                .state('editAlbum', {
                    url: '/coupon/editAlbum/:id',
                    templateUrl: '/app/coupon/edit/templates/editAlbum.html',
                    controller: 'EditAlbumController',
                    controllerAs: 'editAlbumCtrl',
                    resolve: {
                        editCoupon: function (CouponService, $stateParams) {
                            CouponService.editCoupon($stateParams.id)
                        }
                    }

                })
            .state('createCategory', {
                url: '/category/create'
            })
            .state('details', {
                url: '/coupon/details/:id',
                templateUrl: '/app/coupon/details/templates/details.html',
                controller: 'DetailsController',
                controllerAs: 'details',
                resolve: {
                    details: function (CouponService, $stateParams) {
                        CouponService.detailsCoupon($stateParams.id)
                    }
                }

            })
            .state('register', {
                url: '/register',
                templateUrl: '/app/account/register/templates/register.html',
                controller: 'RegisterController',
                controllerAs: 'registerCtrl'
            })
            .state('login', {
                url: '/account/login',
                templateUrl: '/app/account/login/templates/login.html',
                controller: 'LoginController',
                controllerAs: 'loginCtrl'
            })

          .state('profile',
           {
               url: '/profile',
               templateUrl: '/app/account/profile/templates/personalInfo.html',
               controller: 'BuyerInfoCtrl',
               controllerAs: 'buyerInfoCtrl',
               resolve: {
                   user: function (AccountService) {
                       return AccountService.getUser();
                   }
               }
           })
            
        .state('unregistered', {
            url: '/unregistered',
            templateUrl: '/app/account/login/templates/unregistered.html'
        })
        .state('categories',
        {
            url: '/category',
            templateUrl: '/app/coupon/categories/templates/categories.html',
            controller: 'CategoriesController',
            controllerAs: 'catgCtrl'
        })
        .state('cart', {
            url: '/checkout',
            templateUrl: '/template/ngCart/shoopingCart.html'
        })
        .state('changePassword', {
            url: '/changepassword',
            templateUrl: '/app/account/login/templates/changePassword.html',
            controller: 'LoginController',
            controllerAs: 'loginCtrl'
        })
        .state('expiredcoupons', {
            url: '/coupons/expired',
            templateUrl: '/app/coupon/couponList/templates/expired.html',
            controller: 'SearchCtrl',
            controllerAs: 'search',
            resolve: {
                expired: function (CouponService) {
                    CouponService.getExpiredCoupons()
                }
            }
        })

        .state('expiredDetails', {
             url: '/coupons/expired/details/:id',
                templateUrl: '/app/coupon/details/templates/detailsExpired.html',
                controller: 'DetailsController',
                controllerAs: 'details',
                resolve: {
                    details: function (CouponService, $stateParams) {
                        CouponService.detailsExpiredCoupon($stateParams.id)
                    }
                }
        })
        .state('resetPassword', {
            url: '/account/resetpassword/?resetToken={Token}',
            templateUrl: '/app/account/login/templates/resetPassword.html',
            controller: 'LoginController',
            controllerAs: 'ctrl'

        })
        .state('seller', {
            url: '/seller/:id',
            templateUrl: '/app/account/profile/templates/sellerPage.html',
            controller: 'BuyerInfoCtrl',
            controllerAs: 'ctrl'
        })
        .state('aboutUs', {
            url: '/aboutUs',
            templateUrl: '/app/coupon/couponList/templates/aboutUs.html',
        })
        
    });

    app.filter('unsafe', function ($sce) {
         return function (val) {
            return $sce.trustAsHtml(val);
         };
        
    });

        
    app.run([
'defaultErrorMessageResolver',
function (defaultErrorMessageResolver) {
    // passing a culture into getErrorMessages('fr-fr') will get the culture specific messages
    // otherwise the current default culture is returned.
    defaultErrorMessageResolver.getErrorMessages().then(function (errorMessages) {
        errorMessages['invalidPassword'] = 'Your password must contain at least one big letter, number and special character.';
        errorMessages['nameNoNumbers'] = "Name or last name cannot contain numbers or special characters.";
        errorMessages['required'] = "This field is required.";
        errorMessages['phoneNumber'] = "Example: xxx-xxx-xxx";
    });
}
    ]);




    ///Setting request interceptor to intercept every request and put header on them (token for authorization)
    app.factory('httpRequestInterceptor', function ($cookies) {
        return {
            request: function (config) {
                var user = $cookies.get("user");
                if (user) {
                    user = JSON.parse(user);
                    config.headers['Authorization'] = 'Bearer ' + user.access_token;
                }

                return config;
            }
        };
    });

    app.config(['usSpinnerConfigProvider', function (usSpinnerConfigProvider) {
        usSpinnerConfigProvider.setTheme('bigBlue', { color: 'blue', radius: 20 });
    }]);

        

        app.config(function ($httpProvider, $cookiesProvider) {
            $httpProvider.interceptors.push('httpRequestInterceptor');
            $cookiesProvider.defaults.path = "/";
        });
        
        ///Config for growl notofier used in project
         app.config(['growlProvider', function (growlProvider) {
             growlProvider.globalPosition('top-center');
             growlProvider.globalTimeToLive(5000);
        }]);


    // directive for display images 
         app.directive('ngThumb', ['$window', function ($window) {
             var helper = {
                 support: !!($window.FileReader && $window.CanvasRenderingContext2D),
                 isFile: function (item) {
                     return angular.isObject(item) && item instanceof $window.File;
                 },
                 isImage: function (file) {
                     var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                     return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
                 }
             };

             return {
                 restrict: 'A',
                 template: '<canvas id="canvas"/>',
                 link: function (scope, element, attributes) {
                     if (!helper.support) return;

                     var params = scope.$eval(attributes.ngThumb);

                     if (!helper.isFile(params.file)) return;
                     if (!helper.isImage(params.file)) return;

                     var canvas = element.find('#canvas');
                     var reader = new FileReader();

                     reader.onload = onLoadFile;
                     reader.readAsDataURL(params.file);

                     function onLoadFile(event) {
                         var img = new Image();
                         img.onload = onLoadImage;
                         img.src = event.target.result;
                     }

                     function onLoadImage() {
                         var width = params.width || this.width / this.height * params.height;
                         var height = params.height || this.height / this.width * params.width;
                         canvas.attr({ width: width, height: height });
                         canvas[0].getContext('2d').drawImage(this, 0, 0, width, height);
                     }
                 }
             };
         }]);
    
         app.directive('myRepeatDirective', function() {
             return function(scope) {
                 if (scope.$last){
                     scope.$emit('LastElem');
                 }
             };
         })
  .directive('myMainDirective', function() {
      return function (scope) {
          
          scope.$on('LastElem', function (event) {
              setTimeout(function () {  
              $('#grid-slider').flexslider({

        slideshowSpeed: 100000,
        animation: "fade",
        smoothHeight: true,
        easing: "linear",
        controlNav: false,
        nextText: '<i class="ti-angle-right"></i>',
        prevText: '<i class="ti-angle-left"></i>'
              });
            
    // The slider being synced must be initialized first
    $('#carousel').flexslider({
        animation: "slide",
        controlNav: false,
        animationLoop: false,
        slideshow: false,
        itemWidth: 150,
        itemMargin: 5,
        asNavFor: '#slider'
    });

    $('#slider').flexslider({
        animation: "slide",
        controlNav: false,
        animationLoop: false,
        slideshow: false,
        sync: "#carousel"
    });


    // Isotope init
    $('.row-isotope').isotope({
        itemSelector: '.item',
        layoutMode: 'masonry',
        masonry: {
            columnWidth: '.grid-sizer'
        }
    });

    $('.truncate').succinct({
        size: 80

    });
              }, 1500);

          });
      };
     
  });



    })();