(function () {
    angular.module("feedbackModule")
        .controller("FeedbackCtrl", ["FeedbackService", function (FeedbackService) {
            var self = this;
            self.service = FeedbackService;

        }])
})();