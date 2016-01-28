(function(){

    ///Facktory for feedback module
    angular.module("feedbackModule")
        .factory("FeedbackFactory", ["$resource", function ($resource) {
            return $resource("/api/feedbacksapi/:id");
        }]);

})();