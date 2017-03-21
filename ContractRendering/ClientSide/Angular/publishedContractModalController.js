(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('publishedContractModalController', PublishedContractModalController);

    PublishedContractModalController.$inject = ['$scope', '$baseController', '$uibModalInstance', 'publishedContract', '$notificationService', '$quoteStateMachineService'];

    function PublishedContractModalController(
        $scope,
        $baseController,
        $uibModalInstance,
        publishedContract,
        $notificationService,
        $quoteStateMachineService
        ) {

        // Injection
        var vm = this;
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;
        vm.$notificationService = $notificationService;
        vm.$quoteStateMachineService = $quoteStateMachineService;

        $baseController.merge(vm, $baseController);
        
        vm.quoteId = $("#QUOTEID").val();
        vm.userCompanyId = $("#PAGECOMPANY").val();

       
        vm.publishedContractURL = publishedContract.contractUrl;
        vm.quoteState = publishedContract.quoteState;

        vm.sendContractToSupplier = _sendContractToSupplier;

        // Public Methods

        console.log('vm.publishedContractURL', vm.publishedContractURL);
        console.log('vm.quoteState: ', vm.quoteState);

        function _sendContractToSupplier (EventId) {

            console.log('_sendContractToSupplier fired');

            _updateStatus(EventId);
        };

        vm.ok = function () {

            vm.$uibModalInstance.close();
        };

        vm.cancel = function () {

            vm.$uibModalInstance.dismiss('cancel');

        };

        
        function _updateStatus(Eventid) {

            console.log('Button Pressed');

            //vm.$quoterequestService.GetByQuoteId(vm.selectedQuoteRequestId, _captureProjectName, _onError);
            console.log('current quoteid: ', vm.quoteId);
            var stateData = {

                "quoteId": vm.quoteId,
                "companyId": vm.userCompanyId,
                "quoteState": vm.quoteState,
                "EventId": Eventid
            };

            // We set the Eventid value so we can use it to properly redirect the user
            vm.thisEventId = Eventid;

            //console.log('stateData.EventId: ', stateData.EventId);

            //- update to the appropriate status
            vm.$quoteStateMachineService.updateStateforQuote(stateData.quoteId, stateData, _onSuccessUpdate, _onUserError);

        };


        //- Success handler for when a status updates
        function _onSuccessUpdate(data) {

            console.log("Status Updated!", data);

            vm.$alertService.success('Contract Submitted For Approval', 'Success!');

            
            //location.reload(true);
            //if (vm.thisEventId == 7) {
            //    vm.alertService.success('Quote Request: ' + vm.quoteName, 'Completed!');
            //}
            //redirect the view to the updated url for Ng Route


            vm.$notificationService.notifySellerCompanyOfSubmittedContract(vm.quoteId, _notificationSuccess, _onUserError);


            _closeModal();
            
        };

        function _notificationSuccess(data) {
            console.log('notification success: ', data);
        };

        function _onUserError(jqXhr, error) {
            console.error(jqXhr);
        };

        function _closeModal () {
            console.log('running closeModal');
            vm.$uibModalInstance.close();
        };


    }

})();