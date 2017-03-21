(function () {
    "use strict";

    angular.module(APPNAME)
        .factory('$companyDesignationsService', CompanyDesignationsServiceFactory);

    CompanyDesignationsServiceFactory.$inject = ['$baseService', '$sabio'];

    function CompanyDesignationsServiceFactory($baseService, $sabio) {

        var serviceCopy = sabio.services.companyDesignations;

        //  merge the jQuery object with the angular base service to simulate inheritance
        var newService = $baseService.merge(true, {}, serviceCopy, $baseService);

        return newService;
    }
})();