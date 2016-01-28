(function () {

    ///Service for account
    angular.module("accountModule")
        .service("AccountService", ["$http", "AccountFactory", "$httpParamSerializer", "$state", "$cookies", "growl", "$modal", function ($http, AccountFactory, $httpParamSerializer, $state, $cookies, growl, $modal) {
        
            var service = this;
            
            service.errorMessages = [];
            service.register = register;
            service.login = login;
            service.username = "";
            service.access_token = "";
            service.isLoggedIn = false;
            service.logout = logout;
            service.user = new AccountFactory();
            
            service.getUser = getUser;
            
            service.changePasswordData = {};
            service.goHome = goHome;
            service.changePassword = changePassword;
            service.showLogin = showLogin;
            service.showRegister = showRegister;
            service.refund = refund;
            service.refundForm = {};
            service.requestRefund = requestRefund;
            service.allPurchasedItems = [];
            service.isLoading = false;
            service.callServer = callServer;
            service.beginChangePassword = beginChangePassword;
            service.forgottenPassword = forgottenPassword;
            service.forgottenPasswordModel = {};
            service.forgottenPasswordSubmit = forgottenPasswordSubmit;
            service.resetPassword = resetPassword;
            service.resetPasswordModel = {};
            service.resetToken = "";
            service.editProfile = editProfile;
            service.editUser = {};
            service.editProfileModal = editProfileModal;
            service.listAvatars = [];
            service.getAvatars = getAvatars;

            var loginModal = $modal({ keyboard: true, templateUrl: "/app/account/login/templates/login.html", show: false });
            var registerModal = $modal({ keyboard: true, templateUrl: "/app/account/register/templates/register.html", show: false });
            var modal = $modal({ keyboard: true, templateUrl: "/app/account/login/templates/changePassword.html", show: false });
            var refundForm = $modal({ keyboard: true, templateUrl: "/app/account/profile/templates/refundFrom.html", show: false });
            var forgottenPasswordForm = $modal({ keyboard: true, templateUrl: "/app/account/login/templates/forgottenPasswordForm.html", show: false });
            var editProfileForm = $modal({ keyboard: true, templateUrl: "/app/account/profile/templates/editProfile.html", show: false });
            var loading = $modal({ keyboard: false, backdrop: false, templateUrl: "/app/common/templates/loading.html", show: false, placement: "center" });

            getUser();
            getAvatars();

            var lastStart = 0;
            var maxNodes = 40;


            ///Gets from selected seller active coupons
            function getSellerActiveCoupons(id) {
                $http.get('/api/couponsapi/getactive?userId=' + id)
                    .then(function (response) {
                        service.sellerActiveCoupons = response;
                    })
            };


            ///Gets from selected seller expired coupons
            function getSellerExpiredCoupons(id) {
                $http.get('/api/couponsapi/getexpired?userId=' + id)
                    .then(function (response) {
                        service.sellerExpiredCoupons = response;
                    })
            };

           

            ///Displays modal with form for edit user profile
            function editProfileModal() {
                service.editUser = service.user;
                editProfileForm.$promise.then(editProfileForm.show);
            };

            ///Edit user profile, sends request on back end with new informations about user
            function editProfile() {
                $http.post("/api/account/editprofile", service.editUser)
                    .then(function (response) {
                        editProfileForm.hide();
                        $state.reload();
                        growl.info("You have successfully edited your profile.");
                    }, function (error) {
                        var modelState = error.data.ModelState;
                        angular.forEach(modelState, function (errorMessages, attribute) {

                            if (attribute != "$id") {
                                angular.forEach(errorMessages, function (value, key) {
                                    growl.error(value, { ttl: 10000 });  //display errors sent from back end and display them on notifier
                                });
                            }
                        });
                    });
            };

            ///Function which will send request for password reset after user submits form
            function resetPassword() {
                service.resetPasswordModel.ResetToken = $state.params.resetToken;
                $http.post("/api/account/resetpassword", service.resetPasswordModel)
                    .then(function (response) {
                        $state.go('index');
                        growl.info("You have reseted your password. Please login with your new password.");
                    }, function (error) {
                        if (error.status == 404)
                        {
                            growl.error("There was a problem with server, please try again later.");
                        }
                        else
                        {
                            var modelState = error.data.ModelState;
                            angular.forEach(modelState, function (errorMessages, attribute) {

                                if (attribute != "$id") {
                                    angular.forEach(errorMessages, function (value, key) {
                                        growl.error(value, { ttl: 10000 });  //display errors sent from back end and display them on notifier
                                    });
                                }
                            });
                        }
                    });
            }

            ///Displays modal with forgotten password form
            function forgottenPassword() {
                loginModal.hide();
                forgottenPasswordForm.$promise.then(forgottenPasswordForm.show);
            };

            ///Activates after forgotten form is submited, sends request for forgotten password action
            function forgottenPasswordSubmit() {
                $http.post("/api/account/forgottenpassword", service.forgottenPasswordModel)
                    .then(function (response) {
                        forgottenPasswordForm.hide();
                        growl.info("Please check your email to reset your password.")
                    }, function (error) {
                        forgottenPasswordForm.hide();
                        growl.info("Please check your email to reset your password.")
                    });
            };

            ///It will display form for change password
            function beginChangePassword(){
                modal.$promise.then(modal.show);
            }

              ///Table on user profile triggers this function
              function callServer(tableState) {
                  var params = {
                      page:  tableState.pagination.start,
                      search: typeof tableState.search.predicateObject == "undefined" || Object.keys(tableState.search.predicateObject).length == 0 ? "" : tableState.search.predicateObject.$,
                      sort: typeof tableState.sort.predicate == "undefined" ? "" : tableState.sort.predicate,
                      reverse: typeof tableState.sort.reverse == "undefined" ? false : tableState.sort.reverse,
                  }
                  
                  ///Gets bought items (coupons) and display them in table on profile 
                  ///page of user (it uses pagination, search and sort while getting items)
                  $http.get("/api/couponsapi/getitems", {
                      params: params
                  })
                      .then(function (response) {
                    
                    if (tableState.pagination.start == 0) {
                        service.allPurchasedItems = response.data;
                    }
                    else {
                        service.allPurchasedItems = service.allPurchasedItems.concat(response.data);

                        if (lastStart < tableState.pagination.start && service.allPurchasedItems.length > maxNodes) {
                            service.allPurchasedItems.splice(0, 8);
                            }
                    }

                    lastStart = tableState.pagination.start;

                }, function (error) {


                });

            };

            ///Gets avatars from back end
            function getAvatars()
            {
                $http.get("/api/couponsapi/getavatars").then(function (response) {
                    service.listAvatars = response.data;
                });
            }

            ///User sends refund form to admin for approval
            function requestRefund()
            {
                $http.post("/api/paypal/createrefund", service.refundForm).then(
                    function (response) {
                        loading.hide();
                        refundForm.hide();
                        growl.info("Your request has been sent. Our support team will answer you as soon as possible.");
                    }, function (error) {
                        var modelState = error.data.ModelState;
                        angular.forEach(modelState, function (errorMessages, attribute) {

                            if (attribute != "$id") {
                                angular.forEach(errorMessages, function (value, key) {
                                    growl.error(value, { ttl: 10000 });  //display errors sent from back end and display them on notifier
                                });
                            }
                        });
                    });
            }

            ///Sends request for refund to paypal
            function refund(cartId)
            {
                loading.$promise.then(loading.show);
                $http.get("/api/paypal/refundpayment?cartId=" + cartId).then(

                    function (response) {
                        loading.hide();
                        growl.info("You have refunded this purchase. Check your paypal account for more informations.");
                        $state.reload();
                    }, function (error) {
                        
                        if (error.status == 406 || error.status == 404)
                        {
                            loading.hide();
                            growl.error("There was a problem with paypal. Please contact their support for more informations.");
                        }
                        else if (error.status == 400)  //if error is 400 that means it has passed more then 
                        {                               //7 days since purchase so user has to send request to                               admin for refund approval
                            loading.hide();
                            var array = [];
                            var cartId = error.data.Message;
                            array = cartId.split(',');
                            service.refundForm.PaymentId = array[1];
                            service.refundForm.CartId = array[0];
                            refundForm.$promise.then(refundForm.show);
                        }

                    });
            }



            ///Function whitch gets info about user
            function getUser() {

                $http.get("/api/account/UserInfo").then(
                    function (response) {
                        service.user = response.data;
                        
                    },
                    function (err) {
                        
                    }
                    );
            };



            ///When called, goes to state index
            function goHome() {
                $state.go('index');
            };

            ///Shows login form in modal
            function showLogin() {               
                loginModal.$promise.then(loginModal.show);
            };

            ///Shows register from in modal
            function showRegister() {
                $state.go('register');
            };

            ///Sends request for change password
            function changePassword()
            {
                $http.post('/api/Account/ChangePassword', service.changePasswordData).then(
                    function (response) {
                        modal.hide();
                        logout();
                        growl.info("You have changed your password, please login with your new password.", {ttl: 10000});
                        $state.go('index');
                    },
                    function (error) {
                        var modelState = error.data.ModelState;
                        angular.forEach(modelState, function (errorMessages, attribute) {

                            if (attribute != "$id") {
                                angular.forEach(errorMessages, function (value, key) {
                                    growl.error(value, { ttl: 10000 });  //display errors sent from back end and display them on notifier
                                });
                            }
                        });
                    });
            }

            ///Logs out user from appliaction
            function logout() {
                $http.post("/api/account/logout").then(function () {  //send request to logout user on back end of application
                    $cookies.remove("user");  //remove object user from cookie (this object is stored in cookie during login)
                    service.username = "";
                    service.access_token = "";
                    service.isLoggedIn = false;
                    service.user = {};  //remove any user info from object on logout
                    $state.reload();
                });
            };

            initUser();

            ///Initialize user on startup
            function initUser() {
                var user = $cookies.get("user");
                if (user) {
                    user = JSON.parse(user);
                    service.username = user.username;
                    service.isLoggedIn = true;
                    service.access_token = user.access_token;
                } else {
                    service.isLoggedIn = false;
                }

            }

            ///Logs in user
            function login(user) {
                user.grant_type = "password";
                $http.post("/Token", $httpParamSerializer(user), {  //send request on back end to log in user
                    'Content-Type': 'application/x-www-form-urlencoded'
                }).then(function (response) {
                    var loggedInUser = {
                        username: user.username,
                        isLoggedIn: true,
                        access_token: response.data.access_token,
                    };

                    $cookies.putObject("user", loggedInUser);  //store info abou user in cookie (token will be used later for authentication)
                    initUser();
                    getUser();

                    loginModal.hide();

                    var data = { username: user.username};
                    $http.post('/home/firstsignin', data).then(
                        function (response) {
                            if (response.status == 200)
                            {
                                modal.$promise.then(modal.show);
                            }
                        },
                        function (error) {
                            growl.success("You are now logged in.")
                            $state.go("index");
                        })                    
                },
                function (error) {
                    growl.error(error.data.error_description, {ttl: 10000})
                })
            }

            ///Registers user
            function register(params) {
                service.errorMessages = [];
                $http.post("/api/account/register", params)  //send request for registration
                    .then(function (response) {
                        registerModal.hide();
                        service.login({ username: params.Email, password: params.Password });  //if registration is success auto login user
                    }, function (error) {
                        var modelState = error.data.ModelState;
                        angular.forEach(modelState, function (errorMessages, attribute) {

                            if (attribute != "$id") {
                                angular.forEach(errorMessages, function (value, key) {
                                    growl.error(value, { ttl: 10000 });  //display errors sent from back end and display them on notifier
                                });
                            }
                        });
                    });
            };

            return service;

        }]);

})();