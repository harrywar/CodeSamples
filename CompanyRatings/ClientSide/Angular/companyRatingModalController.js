// COMPANY RATING MODAL CONTROLLER
(function () {
    "use strict";

    angular.module(APPNAME)
    .controller('companyRatingModalController', CompanyRatingModalController);

    CompanyRatingModalController.$inject = ['$scope', '$baseController', "$companyRatingService", '$uibModalInstance'];

    function CompanyRatingModalController(
        $scope
        , $baseController
        , $companyRatingService
        , $uibModalInstance) {

        var vm = this;

        vm.ratingOptionsNumbers = [
            1,
            2,
            3,
            4,
             5
        ];

        vm.ratingSettings9 = {
            theme: 'fontawesome-stars',
            readonly: false
        };

        vm.ratingSettings11 = {
            theme: 'fontawesome-stars-o',
            readonly: true

        };


        $baseController.merge(vm, $baseController);

        vm.$companyRatingService = $companyRatingService;
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;
        vm.currentUserId = $('#PAGEUSER').val();
        vm.companyId = $('#BUYERID').val();
        vm.item = null;

        vm.notify = vm.$companyRatingService.getNotifier($scope);

        function _insertRatingSuccess(data) {

            vm.notify(function () {
                console.log("POSTING Rating Success", data)
            });

        }

        function _insertRatingError(jqXhr, data) {

            console.error(error);

        }

        vm.ok = function () {

            var ratingObject = {

                "companyId": vm.companyId,
                "rating": vm.rating,
                "ratingComment": vm.ratingComment,
                "raterId": vm.currentUserId

            };

            console.log('ratingObject', ratingObject);

            vm.$companyRatingService.insert(ratingObject, _insertRatingSuccess, _insertRatingError);

            vm.$uibModalInstance.close();

        };

        vm.cancel = function () {

            vm.$uibModalInstance.dismiss('cancel');

        };
    }

})();