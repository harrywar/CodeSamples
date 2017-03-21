(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('contractsEditorModalController', ContractsEditorModalController);

    ContractsEditorModalController.$inject = ['$scope', '$baseController', '$uibModalInstance', '$quoteService', '$pdfService', '$contractsService'];

    function ContractsEditorModalController(
        $scope,
        $baseController,
        $uibModalInstance,
        $quoteService,
        $pdfService,
        $contractsService
        ) {

        // Injection
        var vm = this;
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;
        vm.$quoteService = $quoteService;
        vm.$pdfService = $pdfService;
        vm.$contractsService = $contractsService;

        vm.insertCurrentQuoteInfo = _insertCurrentQuoteInfo;
        vm.publishContract = _publishContract;

        // Public Properties
        vm.items = null;
        vm.currentQuoteId = $("#QUOTEID").val();
        //vm.currentQRId = 36;

        vm.notify = vm.$quoteService.getNotifier($scope);

        // SUMMERNOTE OPTIONS
        //vm.options = {
        //    height: 300,
        //    focus: true,
        //    airMode: true,
        //    toolbar: [
        //            ['edit', ['undo', 'redo']],
        //            ['headline', ['style']],
        //            ['style', ['bold', 'italic', 'underline', 'superscript', 'subscript', 'strikethrough', 'clear']],
        //            ['fontface', ['fontname']],
        //            ['textsize', ['fontsize']],
        //            ['fontclr', ['color']],
        //            ['alignment', ['ul', 'ol', 'paragraph', 'lineheight']],
        //            ['height', ['height']],
        //            ['table', ['table']],
        //            ['insert', ['link', 'picture', 'video', 'hr']],
        //            ['view', ['fullscreen', 'codeview']],
        //            ['help', ['help']]
        //    ]
        //};


        // Public Methods

        $baseController.merge(vm, $baseController);

        vm.$quoteService.getQuoteInfoForQuoteReviewByQuoteId(vm.currentQuoteId, _getCurrentQRInfoSuccess, _commonError);

        function _getCurrentQRInfoSuccess(data) {

            vm.notify(function () {

                vm.currentQuoteRequestId = data.item.quoteRequestId;

                //console.log('current quote info:', data.item);

                _createNewContractEntry();

            });
        };

        function _commonError(data) {

            console.log("There was an error", data);
        };

        function _createNewContractEntry() {

            vm.DBpayload = {

                quoteRequestId: vm.currentQuoteRequestId,
                quoteId: vm.currentQuoteId

            };

            //console.log('vm.DBpayload', vm.DBpayload);

            vm.$contractsService.saveContract(vm.DBpayload, _insertContractSuccess, _commonError);

        };

        function _insertContractSuccess(data) {

            //console.log('saving contract to db success', data);

            vm.currentContractId = data.item;

        };

        function _insertCurrentQuoteInfo() {

            vm.$quoteService.getQuoteInfoForQuoteReviewByQuoteId(vm.currentQuoteId, _insertCurrentQuoteInfoSuccess, _commonError);

        };

        function _insertCurrentQuoteInfoSuccess(data) {

            vm.notify(function () {

                vm.currentQuoteInfo = data.item;

            });

        };

        function _publishContract() {

            //console.log('vm.currentContract:', vm.currentContract);

            vm.currentContract = $('.contract-text').val();

            vm.PDFpayload = {

                ContractTerms: vm.currentContract,
                QuoteRequestId: vm.currentQuoteRequestId,
                QuoteId: vm.currentQuoteId

            };

            //console.log('vm.PDFpayload', vm.PDFpayload);

            vm.$pdfService.createContract(vm.PDFpayload, _createContractSuccess, _commonError);

        };

        function _createContractSuccess(data) {

            //console.log('generate pdf to aws success', data);

            vm.contractPdfURL = data.item;

            //window.open(vm.contractPdfURL);

            vm.updateUrlPayload = {

                contractId: vm.currentContractId,
                url: vm.contractPdfURL
            };

            //console.log('vm.updateUrlPayload', vm.updateUrlPayload);

            vm.$contractsService.updateContractURL(vm.currentContractId, vm.updateUrlPayload, _updateContractURLSuccess, _commonError);

            vm.ok();
        };

        function _updateContractURLSuccess(data) {

            //console.log('success updating contracts table with aws url', data);
        };

        vm.ok = function () {

            vm.$uibModalInstance.close(vm.contractPdfURL);
        };

        vm.cancel = function () {

            vm.$uibModalInstance.dismiss('cancel');

        };


    }

})();