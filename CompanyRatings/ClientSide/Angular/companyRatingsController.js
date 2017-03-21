/// <reference path="../Templates/companyRatingModal.html" />
/// <reference path="../Templates/companyRatingModal.html" />
// COMPANY RATING CONTROLLER
(function () {
    "use strict";

    angular.module(APPNAME)
    .controller('companyRatingController', CompanyRatingController);

    CompanyRatingController.$inject = ['$scope', '$baseController', "$companyRatingService", "$uibModal", '$companyProfileService'];

    function CompanyRatingController(
        $scope
        , $baseController
        , $companyRatingService
        , $uibModal
        , $companyProfileService) {

        var vm = this;
        vm.items = null;

        vm.$companyRatingService = $companyRatingService;
        vm.$scope = $scope;
        vm.$uibModal = $uibModal;
        vm.$companyProfileService = $companyProfileService;

        vm.receiveCompanyRatings = _receiveCompanyRatings;
        vm.receiveCompanyRatingsError = _receiveCompanyRatingsError;

        vm.receiveCompanyAvgRating = _receiveCompanyAvgRating;
        vm.receiveCompanyAvgRatingError = _receiveCompanyAvgRatingError;

        vm.currentUserId = $('#PAGEUSER').val();
        // console.log('vm.currentUserId: ', vm.currentUserId);
        vm.companyId = $('#BUYERID').val();
        vm.rating = null;
        vm.ratingComment = null;
        vm.companyAvgRating = null;
        vm.openModal = _openModal;
        vm.buyerInfo = null;
        vm.numberOfReviews = null;

        vm.ratingOptionsNumbers = [
                1,
                2,
                3,
                4,
                5
        ];

        vm.ratingSettings9 = {
            theme: 'fontawesome-stars',
            readonly: true,
        };

        vm.ratingSettings13 = {
            theme: 'fontawesome-stars-o',
            readonly: true
        };

        $baseController.merge(vm, $baseController);

        vm.notify = vm.$companyRatingService.getNotifier($scope);

        render();

        function render() {

            // console.log('render() running');

            vm.$companyProfileService.getById(vm.companyId, _getBuyerSuccess, _getBuyerError);
            vm.$companyRatingService.getByCompanyId(vm.companyId, _receiveCompanyRatings, _receiveCompanyRatingsError);
            vm.$companyRatingService.getAverageByCompanyId(vm.companyId, _receiveCompanyAvgRating, _receiveCompanyAvgRatingError);

        }

        function _getBuyerSuccess(data) {
            vm.notify(function () {
                //console.log('GET Buyer Success', data);
                vm.buyerInfo = data.item;
                //console.log('vm.buyerInfo.name:', vm.buyerInfo.name);
            });
        }

        function _getBuyerError(data) {
            console.log("There was an error.", data);
        }


        function _receiveCompanyAvgRating(data) {
            vm.notify(function () {
                //console.log("GETTING AVG Rating Success", data);
                vm.companyAvgRating = data.item.ratingAverage;
                _initializeRatingsForAvgRating(data);
            });
        }

        function _receiveCompanyAvgRatingError(jqXhr, data) {
            console.error(error);
        }

        function _receiveCompanyRatings(data) {

            vm.notify(function () {
                // console.log("GETTING All Ratings Success", data)
                vm.items = data.items;

                if (data.items !== null) {
                    vm.numberOfReviews = data.items.length;
                }
            });

        }

        function _receiveCompanyRatingsError(jqXhr, error) {

            console.error(error);

        }

        function _initializeRatingsForAvgRating(data) {

            $('#company_rating').barrating({
                theme: 'fontawesome-stars-o',
                initialRating: data.item.ratingAverage,
                readonly: true,
            });

        }

        // OPEN MODAL
        function _openModal() {
            var companyRatingModal = vm.$uibModal.open({
                animation: true,
                templateUrl: '/Scripts/app/CompanyRatings/Templates/companyRatingModal.html',
                controller: 'companyRatingModalController as crmc',
                size: 'md',
                resolve: {
                    items: function () {
                    }
                }

            });

            _initializeRatingsForModal();

            companyRatingModal.result.then(function () {
                render();

            }, function () {
                // console.log('Modal closed at ' + new Date());
            });

        }

        function _initializeRatingsForModal() {
            //console.log('initializeRatingsForModal running');
            $('#modal_rating').barrating({
                theme: 'fontawesome-stars',
            });

        };
    }

})();