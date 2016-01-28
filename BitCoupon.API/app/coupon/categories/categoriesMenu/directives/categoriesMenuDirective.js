(function () {

    angular.module("couponModule")
        .directive("coupCategoriesMenuDirective", function () {

            return {

                templateUrl: "/app/coupon/categories/categoriesMenu/templates/categoriesMenu.html",
                controller: "CategoriesMenuController as ctrl"

            }

        })

})();