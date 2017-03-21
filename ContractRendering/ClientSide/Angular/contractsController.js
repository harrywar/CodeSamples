
(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('contractsButtonController', ContractsButtonController);

    ContractsButtonController.$inject = ['$scope', '$baseController', '$uibModal'];

    function ContractsButtonController(
        $scope,
        $baseController,
        $uibModal,
        $quoteService) {

        // Injection
        var vm = this;
        vm.$scope = $scope;
        vm.$uibModal = $uibModal;

        // Public Properties
        vm.items = null;

        // Public Methods
        vm.openContractsEditorModal = _openContractsEditorModal;

        $baseController.merge(vm, $baseController);

        function _openContractsEditorModal() {

            var contractsEditorModal = vm.$uibModal.open({
                animation: true,
                templateUrl: '/Scripts/app/Contracts/Templates/editorTemplate.html',
                controller: 'contractsEditorModalController as cemc',
                size: 'lg',
                resolve: {
                    items: function () {
                    }
                }
            });

            contractsEditorModal.result.then(function () {

            }, function (data) {
                console.log('data', data);

            });
        };

    }

})();