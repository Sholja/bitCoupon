(function () {

    ///Coupon service, it is used to share informations between controller od coupon
    angular.module("couponModule")
    .service("CouponService", ["CategoriesFactory", "CouponFactory", "growl", "$state", "CommentFactory", "$http", "$modal", function (CategoriesFactory, CouponFactory, growl, $state, CommentFactory, $http, $modal) {
        var service = this;

        service.couponsList = [];
        service.categoryList = [];
        service.search = "";
        service.dropdown = 1;
        service.searchExpired = "";
        service.dropdownExpired = 1;
        service.selectedCoupon = new CouponFactory();
        service.detailsCoupon = detailsCoupon;
        service.detailsExpiredCoupon = detailsExpiredCoupon;
        service.createComment = createComment;
        service.deleteComment = deleteComment;
        service.reportComment = reportComment;
        service.content = "";
        service.searchByCategory = searchByCategory;
        service.deleteCoupon = deleteCoupon;
        service.deleteConfirmed = deleteConfirmed;
        service.deleteId = "";
        service.index = "";
        service.selectedExpired = {};
        var comment = new CommentFactory();
        service.rate = 0;
        service.avgRate = "";
        service.id;

        //create question
        service.showQuestionForm = showQuestionForm;
        var questionModal = $modal({ keyboard: "true", templateUrl: "/app/common/templates/question.html", show: false });
        service.createQuestion = createQuestion;
        service.question = {};
        service.selectedQuestionId = "";
        service.showAnswerFrom = showAnswerFrom;
        var answerModal = $modal({ keyboard: "true", templateUrl: "/app/common/templates/answer.html", show: false });
        service.createAnswer = createAnswer;
        service.answer = "";
        service.deleteQuestion = deleteQuestion;

        //edit album
        service.editAlbum = editAlbum;


        //edit coupon
        service.editCoupon = editCoupon;
        service.editCouponSelected = new CouponFactory();


        //create category
        service.category = new CategoriesFactory();
        service.createCategory = createCategory;

        //


        //create coupon
        service.coupon = new CouponFactory();
        service.coupon.CategoryId = 2;
        service.createCategoryList = [];


        //pagination

        service.couponsList = [];
        service.getCoupon = getCoupon;
        service.hasNext = true;
        service.nextPage = 0;
        service.loadCoupons = getCoupons;

        service.couponsExpiredList = [];
        service.hasNextExpired = true;
        service.nextPageExpired = 0;
        service.getExpiredCoupons = getExpiredCoupons;

        service.bestRated = [];
        service.latest = [];
        service.bestDeals = [];



        getCoupons();
        getCategories();
        getBestRated();

        service.showPanels = '1';

        service.showFirstPanel = showFirstPanel;
        service.showSecondPanel = showSecondPanel;
        service.showThirdPanel = showThirdPanel;
        service.showForthPanel = showForthPanel;

        function showFirstPanel() {

            service.showPanels = '1';
        };

        function showSecondPanel() {

            service.showPanels = '2';
        };

        function showThirdPanel() {
            service.showPanels = '3';
        };

        function showForthPanel() {
            service.showPanels = '4';

            setTimeout(function () {

                initialize()
            }, 1000)


        };





        service.myLocations = [];

        var myCenter = new google.maps.LatLng(43.83789239125797, 18.309952392578125);
        var geocoder;








        function initialize() {
            var myLocations = service.myLocations;

            var mapProp = {
                center: myCenter,
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            service.map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
            geocoder = new google.maps.Geocoder;

            if (jQuery.isEmptyObject(myLocations) == false) {
                for (var i = 0; i < myLocations.length; i++) {
                    var infowindow = new google.maps.InfoWindow; // to do problem with loading
                    var location = {
                        lat: parseFloat(myLocations[i].Lang.replace(',', '.')),
                        lng: parseFloat(myLocations[i].Long.replace(',', '.'))
                    }
                    geocodeLatLng(location, infowindow);
                }
            }

        }
        function geocodeLatLng(location, infowindow) {

            geocoder.geocode({ 'location': location }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        service.map.setZoom(9);
                        var marker = new google.maps.Marker({
                            position: location,
                            map: service.map
                        });
                        infowindow.setContent(results[1].formatted_address);
                        infowindow.open(service.map, marker);
                    } else {
                        window.alert('No results found');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }
            });
        }







        ///Gets best rated coupons
        function getBestRated() {
            $http.get("/api/couponsapi/bestrated").then(function (response) {
                service.bestRated = response.data.BestRated;
                service.latest = response.data.Latest;
                service.bestDeals = response.data.BestDeals;
                service.images = response.data.Images;
            }, function (error) { })
        }

        ///Deletes question by user Seller who created coupon
        function deleteQuestion(id) {
            $http.get("/api/questions/delete?questionId=" + id)
                .then(function (response) {
                    $.each(service.selectedCoupon.Questions, function (i) {
                        if (service.selectedCoupon.Questions[i].Id == response.data.Id) {
                            service.selectedCoupon.Questions.splice(i, 1);
                        }
                    });
                    growl.success("You have deleted selected question.");
                }, function (error) {
                    if (error.status == 401) {
                        growl.error("You don't have permission for this action");
                    }
                    else {
                        growl.error("There was a problem with server, please try again later.");
                    }
                });
        }


        ///Sends request on backend with answer to the selected question
        function createAnswer(name) {
            $http.get("/api/questions/answer?questionId=" + service.selectedQuestionId + "&answer=" + service.answer)
                .then(function (response) {

                    $.each(service.selectedCoupon.Questions, function (i) {
                        if (service.selectedCoupon.Questions[i].Id == response.data.Id) {
                            service.selectedCoupon.Questions[i].TimeAnswered = response.data.TimeAnswered;
                            service.selectedCoupon.Questions[i].AnswerContent = response.data.AnswerContent;
                            service.selectedCoupon.Questions[i].SellerName = response.data.SellerName;
                            service.selectedCoupon.Questions[i].SellerAvatar = response.data.SellerAvatar;
                        }
                    });
                    answerModal.hide();
                    service.answer = "";
                    growl.success("You have answered on this question.");
                }, function (error) {
                    if (error.status == 400) {
                        growl.error("Answer field is empty, please write your answer.");
                    }
                    else if (error.status == 401) {
                        growl.error("You don't have permission to answer question on this coupon.");
                    }
                    else {
                        growl.error("There was a problem with server, please try again later.");
                    }
                });
        }

        ///Creates new question
        function createQuestion(id) {
            service.question.CouponId = id;
            $http.post("/api/questions/createquestion", service.question)
                .then(function (response) {
                    service.question = {};
                    questionModal.hide();
                    service.selectedCoupon.Questions.push(response.data);
                    growl.success("You have added a new question. Please wait until seller of this coupon answers you.");
                }, function (error) {
                    var modelState = error.data.ModelState;
                    angular.forEach(modelState, function (errorMessages, attribute) {

                        if (attribute != "$id") {
                            angular.forEach(errorMessages, function (value, key) {
                                growl.error(value, { ttl: 10000 });  //display errors sent from back end and display them on notifier
                            });
                        }
                    });
                })
        }

        ///Displays modal with answer form
        function showAnswerFrom(id) {
            service.selectedQuestionId = id;
            answerModal.$promise.then(answerModal.show);
        }

        ///Displays modal with question form
        function showQuestionForm() {
            questionModal.$promise.then(questionModal.show);
        };

        ///Gets expired coupons from back end
        function getExpiredCoupons() {

            $http.get("/api/CouponsAPI/GetExpiredCoupons/" + service.nextPageExpired).then(function (result) {
                service.couponsExpiredList = service.couponsExpiredList.concat(result.data.Coupons);
                for (var i = 0; i < service.couponsExpiredList.length; i++) {
                    if (service.couponsExpiredList[i].Category.Name == null) {
                        var id = service.couponsExpiredList[i].CategoryId;
                        for (var j = 0; j < service.categoryList.length; j++) {
                            if (service.categoryList[j].Id == id) {
                                service.couponsExpiredList[i].Category = service.categoryList[j];
                            }
                        }
                    }
                }

                service.hasNextExpired = result.data.HasNext;
                service.nextPageExpired = result.data.NextPage;
            }, function (error) {
                service.hasNextExpired = false;
            });
        };

        ///Creates category by user seller, sends it for approval by admin
        function createCategory() {
            service.category.Approved = false;
            service.category.$save(function (response) {
                $state.go('index');
                growl.success("You have created a new category. It has been sent to admin for approval.");
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

        ///Delete coupon, loads confirmation window to modal
        function deleteCoupon(index) {
            service.index = index;
            service.deleteId = service.couponsList[index].CouponId;
            var modal = $modal({ keyboard: "true", templateUrl: "/app/coupon/delete/templates/deleteCoupon.html", show: "true" });
        }

        ///Confirm delete, deletes coupon
        function deleteConfirmed() {
            var id = service.deleteId;
            var index = service.index;
            service.selectedCoupon.$remove({ id: id }, function (response) {
                service.couponsList.splice(index, 1);
                $modal.hide;

            }, function (error) {
                $modal.hide;
                if (error.status == 401)  //depending of error status, display different message
                {
                    growl.warning("You don't have permission to delete this coupon");
                }
                else if (error.status == 400) {
                    growl.warning("This coupon has been bought, you cannot delete it.")
                }
                else {
                    growl.warning("There was a problem with server, please try again in few minutes.");
                }
            });
        }

        ///Search coupon by category
        function searchByCategory(name) {
            $http.get("/api/categoriesapi/?name=" + name).then(function (response) {
                service.couponsList = response.data;
            });
        }


        //Function which will get all categories and load them into dropdown
        function getCategories() {
            $http.get("/api/categoriesapi").then(function (response) {

                service.categoryList = response.data;
                for (var i = 0; i < service.couponsList.length; i++) {
                    if (service.couponsList[i].Category.Name == null) {
                        var id = service.couponsList[i].CategoryId;
                        for (var j = 0; j < service.categoryList.length; j++) {
                            if (service.categoryList[j].Id == id) {
                                service.couponsList[i].Category = service.categoryList[j];
                            }
                        }
                    }
                }

            }, function (err) {
                console.log(err);
            });
        }

        ///Send report about certen comment
        function reportComment(id) {
            $http.get("/api/reportcomments/" + id)
                .then(
                function (response) {
                    growl.success("Comment reported. Thank you for your support.");
                }, function (error) {
                    if (error.status == 400) {  //depending of response error, display different message
                        growl.warning("You have already reported this comment.");
                    }
                    else if (error.status == 404) {
                        growl.warning("You have to buy coupon to be able to report comment.");
                    }
                    else {
                        growl.warning("Either you do not have permission or this is your comment so you cannot report it.");
                    }
                })

        }

        ///Deletes coupon comment
        function deleteComment(index) {
            comment.$delete({ id: index },
                function (response) {

                    $.each(service.selectedCoupon.Comments, function (i) {
                        if (service.selectedCoupon.Comments[i].CommentId == response.CommentId) {
                            service.selectedCoupon.Comments.splice(i, 1);
                        }
                    });

                    growl.info("Comment deleted.");
                },
                function (error) {
                    if (error.status == 400)
                        growl.warning("You can only delete comments you have created.");
                    else if (error.status == 401) {
                        growl.warning("You do not have permission to delete this comment.");
                    }
                });
        }

        ///Creates comment about certen coupon
        function createComment(id) {
            comment.CouponId = id;
            comment.content = service.content;
            comment.CouponRate = service.rate;
            comment.$save(function (response) {

                service.selectedCoupon.Comments.push(response);

                growl.success("Comment created");
                service.content = "";
            }, function (error) {
                if (error.status == 400) {
                    growl.warning("You have to buy coupon to be able to comment it.", { ttl: 10000 });
                }
                else if (error.status == 401) {
                    growl.warning("You do not have permission to create comment.", { ttl: 10000 });
                }
            });
        }



        ///Gets coupons from back end
        function getCoupons() {
            CouponFactory.get({
                page: service.nextPage  //page number for pagination
            }, function (result) {
                service.couponsList = service.couponsList.concat(result.Coupons);
                for (var i = 0; i < service.couponsList.length; i++) {
                    if (service.couponsList[i].Category.Name == null) {
                        var id = service.couponsList[i].CategoryId;
                        for (var j = 0; j < service.categoryList.length; j++) {
                            if (service.categoryList[j].Id == id) {
                                service.couponsList[i].Category = service.categoryList[j];
                            }
                        }
                    }
                }
                service.hasNext = result.HasNext;
                service.nextPage = result.NextPage;
            }, function (error) {
                service.hasNext = false;
            });

        };

        function getCoupon(index) {
            var couponId = service.couponsList[index].Id;
            CouponFactory.get({
                Id: couponId
            }, function (response) {
                service.couponsList[index] = response;
            });

        };



        ///Function which will get info about coupon to disply on details page
        function detailsCoupon(id) {

            CouponFactory.get({
                id: id  //send request to get coupon details
            },
                function (response) {
                    service.selectedCoupon = response;  //put response into coupon object
                    $http.get("api/commentsapi/getrate?couponId=" + id).then(function (result) {  //calls method GetRate on back end   
                        service.avgRate = result.data;                                            //to get average rate for this coupon
                    }, function (error) {
                        console.log("");
                    });
                    service.myLocations = response.Locations
                },
                function (error) {
                    if (error.status == 400) {
                        growl.warning("You have to buy coupon to be able to comment it.", { ttl: 10000 });
                    }
                    else if (error.status == 401) {
                        growl.warning("You do not have permission to create comment.", { ttl: 10000 });
                    }

                });
        };

        ///Function which will get info about expired coupon to disply on details page
        function detailsExpiredCoupon(id) {

            $http.get("/api/couponsapi/getexpiredcoupon?couponId=" + id).then(function (response) {   //send request to get coupon details

                service.selectedExpired = response.data;  //put response into coupon object
            }
                , function (error) {
                });
        };

        ///Edit selected coupon
        function editCoupon(id) {

            CouponFactory.get({
                id: id
            },
                function (response) {
                    service.editCouponSelected = response;

                },
                function (error) {


                });
        };

        ///Edit pictures of selected coupon
        function editAlbum() {
            CouponFactory.get({
                id: id
            },
                function (response) {
                    service.editCouponSelected = response;
                },
                function (error) {


                });
        }

        return service;




    }]);

})();