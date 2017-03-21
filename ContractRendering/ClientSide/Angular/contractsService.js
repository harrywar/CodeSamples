// ANGULAR SERVICE 
(function () {
    "use strict";

    angular.module(APPNAME)
    .factory('$contractsService', ContractsService);

    ContractsService.$inject = ['$baseService', '$sabio'];

    function ContractsService($baseService, $sabio) {

       var ContractsService = sabio.services.contract;

        var newService = $baseService.merge(true, {}, ContractsService, $baseService);

        return newService;
    }


})();