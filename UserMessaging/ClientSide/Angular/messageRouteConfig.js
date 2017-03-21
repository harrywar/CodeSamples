// Developed by James
(function () {
    "use strict";

    angular.module(APPNAME)
        .config(["$routeProvider", "$locationProvider",
            function ($routeProvider, $locationProvider) {

                $routeProvider.when('/0/:botId', {
                    // When conversation with systemBot is opened
                    templateUrl: '/Scripts/app/Messages/Templates/botMessageTemplate.html',
                    controller: 'messageController',
                    controllerAs: 'mc'
                }).when('/:conversationId/:receiverId', {
                    // When conversation with human user is opened
                    templateUrl: '/Scripts/app/Messages/Templates/messageTemplate.html',
                    controller: 'messageController',
                    controllerAs: 'mc'
                })

                    .otherwise('');

                $locationProvider.html5Mode(false).hashPrefix('');

            }]);

})();