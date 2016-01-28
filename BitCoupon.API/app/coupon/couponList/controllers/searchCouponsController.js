(function () {

    'use strict'

    ///Controller used for coupons search
    angular.module("couponModule")
    .controller("SearchCtrl", ["CouponService", "$http", "AccountService", function (CouponService, $http, AccountService) {
    var search = this;
    
    search.serviceSearch = CouponService;
    search.accountService = AccountService;
    search.tabPanels = true;
    search.showLatest = showLatest;
    search.showBestDeals = showBestDeals;

    function showLatest(){
        search.tabPanels = true;
    }

    function showBestDeals(){
        search.tabPanels = false;
    }


    search.searchBy = function() {
        var params = {
            name: search.serviceSearch.search,
            CategoryId: search.serviceSearch.dropdown
        }
        $http.get("/api/couponsapi/search/?name=" + params.name + "&CategoryId=" + params.CategoryId + "&method=" + true).then(function (response) {
            
            search.serviceSearch.couponsList = response.data;
        });
    };

    search.searchByExpired = function(){
     var params = {
            name: search.serviceSearch.searchExpired,
            CategoryId: search.serviceSearch.dropdownExpired
        }
     $http.get("/api/couponsapi/search/?name=" + params.name + "&CategoryId=" + params.CategoryId + "&method=" + false).then(function (response) {

         search.serviceSearch.couponsExpiredList = response.data;
     });
    };

    
    

    }]);

})();