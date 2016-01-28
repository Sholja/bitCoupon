(function () {

    angular.module('feedbackModule')
        .service('FeedbackService', ['FeedbackFactory', 'growl', '$state',  function (FeedbackFactory, growl, $state) {

            var service = this;

            service.feedback = new FeedbackFactory();
            service.createFeedback = createFeedback;
           
            function createFeedback() {

                service.feedback.$save(function (response) {
                    growl.success("Feedback created, Thank you.");
                    service.feedback = new FeedbackFactory();
                    $state.reload();
                }, function (error) {

                    growl.warning("Please fill all required inputs.", { ttl: 10000 });

                });
            };


        }]);

   


})();