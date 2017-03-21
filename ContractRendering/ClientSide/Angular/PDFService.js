// ANGULAR SERVICE 
(function () {
    "use strict";

    angular.module(APPNAME)
    .factory('$pdfService', pdfServiceFactory);

    pdfServiceFactory.$inject = ['$baseService', '$sabio'];

    function pdfServiceFactory($baseService, $sabio) {

        var pdfService = sabio.services.pdf;

        var newService = $baseService.merge(true, {}, pdfService, $baseService);

        return newService;
    }


})();