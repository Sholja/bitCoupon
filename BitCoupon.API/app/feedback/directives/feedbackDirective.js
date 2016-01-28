(function () {

    angular.module("feedbackModule")
    .directive("feedbackDirective", function () {
        return {
            templateUrl: '/app/feedback/templates/feedback.html',
            controller: 'FeedbackCtrl as feedCtrl'
        }
    });

})();