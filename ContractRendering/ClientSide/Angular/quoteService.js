(function () {
    "use strict";

    //-----Service----------------------------------------------------------------------------
    angular.module(APPNAME).factory('$quoteService', QuoteServiceFactory);

    QuoteServiceFactory.$inject = ['$baseService', '$sabio'];

    function QuoteServiceFactory($baseService, $sabio) {

        var aSabioServiceObject = sabio.services.quotes;

        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);

        return newService;

    };
})();