// COMPANY RATING SERVICE
(function () {
    "use strict";

    angular.module(APPNAME)
    .factory('$companyRatingService', companyRatingServiceFactory);

    companyRatingServiceFactory.$inject = ['$baseService', '$sabio'];

    function companyRatingServiceFactory($baseService, $sabio) {

        var aSabioServiceObject = sabio.services.companyratings;

        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);

        return newService;
    }


})();