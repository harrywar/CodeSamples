// ANGULAR SERVICE 
(function () {
    "use strict";

    angular.module(APPNAME)
    .factory('$messagingService', messagingServiceFactory);

    messagingServiceFactory.$inject = ['$baseService', '$sabio'];

    function messagingServiceFactory($baseService, $sabio) {

        var aSabioServiceObject = sabio.services.conversations;
        var bSabioServiceObject = sabio.services.messages;

        var newService = $baseService.merge(true, {}, aSabioServiceObject, bSabioServiceObject, $baseService);

        return newService;
    }


})();