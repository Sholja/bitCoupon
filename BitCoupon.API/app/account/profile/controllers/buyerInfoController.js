
(function () {

    angular.module("couponModule")
    .controller("BuyerInfoCtrl", ["AccountService", "$http", "$state", function (AccountService, $http, $state, user) {

        var buyerInfo = this;
        buyerInfo.service = AccountService;
        buyerInfo.userInfo = user;

        buyerInfo.showDiv = showDiv;
        buyerInfo.hideDiv = hideDiv;
        buyerInfo.show = true;
        buyerInfo.callSellerTabel = callSellerTabel;
        buyerInfo.sellerActiveCoupons = [];
        buyerInfo.sellerExpiredCoupons = [];
        buyerInfo.callSellerTable = callSellerTable;

        function showDiv() {
            buyerInfo.show = true;
        };

        function hideDiv() {
            buyerInfo.show = false;
        };

       
        var lastStart = 0;
        var maxNodes = 40;

        ///Function for table for active coupons on seller page
        function callSellerTabel(tableState) {
            var params = {
                couponId: $state.params.id,
                page: tableState.pagination.start,
                search: typeof tableState.search.predicateObject == "undefined" || Object.keys(tableState.search.predicateObject).length == 0 ? "" : tableState.search.predicateObject.$,
                sort: typeof tableState.sort.predicate == "undefined" ? "" : tableState.sort.predicate,
                reverse: typeof tableState.sort.reverse == "undefined" ? false : tableState.sort.reverse,

            }
            if (params.couponId == undefined)
            {
                params.couponId = 0;
            }
      
            ///Gets bought items (coupons) and display them in table on profile 
            ///page of user (it uses pagination, search and sort while getting items)
           
                $http.get("/api/couponsapi/getactive?page=" + params.page + "&search=" + params.search
                    + "&sort=" + params.sort + "&reverse=" + params.reverse + "&couponId=" + params.couponId + "&method=" + true)
                    .then(function (response) {
                        if (tableState.pagination.start == 0) {
                            buyerInfo.sellerActiveCoupons = response.data;
                        }
                        else {
                            buyerInfo.sellerActiveCoupons = buyerInfo.sellerActiveCoupons.concat(response.data);

                            if (lastStart < tableState.pagination.start && buyerInfo.sellerActiveCoupons.length > maxNodes) {
                                buyerInfo.sellerActiveCoupons.splice(0, 8);
                            }
                        }

                        lastStart = tableState.pagination.start;

                    }, function (error) {


                    });
           

        };

        var newLastStart = 0;
        var newMaxNodes = 40;

        ///Function for table for expired coupons on seller page
        function callSellerTable(tableState) {
            var params = {
                couponId: $state.params.id,
                page: tableState.pagination.start,
                search: typeof tableState.search.predicateObject == "undefined" || Object.keys(tableState.search.predicateObject).length == 0 ? "" : tableState.search.predicateObject.$,
                sort: typeof tableState.sort.predicate == "undefined" ? "" : tableState.sort.predicate,
                reverse: typeof tableState.sort.reverse == "undefined" ? false : tableState.sort.reverse,
            }

            ///Gets bought items (coupons) and display them in table on profile 
            ///page of user (it uses pagination, search and sort while getting items)
            $http.get("/api/couponsapi/getactive?page=" + params.page + "&search=" + params.search
                + "&sort=" + params.sort + "&reverse=" + params.reverse + "&couponId=" + params.couponId + "&method=" + false)
                .then(function (response) {
                    if (tableState.pagination.start == 0) {
                        buyerInfo.sellerExpiredCoupons = response.data;
                    }
                    else {
                        buyerInfo.sellerExpiredCoupons = buyerInfo.sellerExpiredCoupons.concat(response.data);

                        if (newLastStart < tableState.pagination.start && buyerInfo.sellerExpiredCoupons.length > newMaxNodes) {
                            buyerInfo.sellerExpiredCoupons.splice(0, 8);
                        }
                    }

                    newLastStart = tableState.pagination.start;

                }, function (error) {


                });

        };

    }]);

})();