(function () {

    angular.module("couponModule")
    .factory("CategoriesFactory", ["$resource", function ($resource) {
        return $resource("/api/categoriesapi/:id");
    }]);

})();