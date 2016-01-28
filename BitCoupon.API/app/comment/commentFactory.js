(function(){

    ///Factory for comments (connects to api controller for comments)
    angular.module("commentModule")
        .factory("CommentFactory", ["$resource", function ($resource) {
            return $resource("/api/commentsapi/:id");
        }])

})();