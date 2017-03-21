// CONTACTS BUTTON MODAL CONTROLLER

(function () {
    "use strict";

    angular.module(APPNAME)
    .controller('messagingContactsModalController', MessagingContactsModalController);

    MessagingContactsModalController.$inject = ['$scope', '$baseController', '$messagingService', '$uibModalInstance'];

    function MessagingContactsModalController(
        $scope,
        $baseController,
        $messagingService,
        $uibModalInstance) {

        var vm = this;
        vm.currentUserId = $('#PAGEUSER').val();
        vm.item = null;
        vm.items = null;
        vm.checkIfConversationExists = _checkIfConversationExists;

        vm.$messagingService = $messagingService;
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;

        $baseController.merge(vm, $baseController);

        vm.notify = vm.$messagingService.getNotifier($scope);

        _renderUserProfiles();

        function _renderUserProfiles() {

            //console.log('renderUserProfiles running');
            vm.$messagingService.getAllUserProfiles(_getAllUserProfilesSuccess, _getAllUserProfilesError);

        }

        function _getAllUserProfilesSuccess(data) {

            vm.notify(function () {
                vm.items = data.items;
                //console.log('GETTING All User Profiles Success', vm.items);

            })
        }

        function _getAllUserProfilesError(jqXhr, error) {

            console.error(error);
        }

        function _checkIfConversationExists(item) {

            var checkObject = {

                "senderId": vm.currentUserId,
                "receiverId": item.userId

            };

            //console.log('checkObject ', checkObject);

            vm.$messagingService.checkConversationExists(checkObject, _checkConversationExistsSuccess, _checkConversationExistsError);
        }

        function _checkConversationExistsSuccess(data) {

            vm.notify(function () {
                vm.item = data.item;
                //console.log('CHECKING Conversation Exists Success', data)

            })

            vm.ok();
        }

        function _checkConversationExistsError(jqXhr, error) {

            console.error(error);
        }

        vm.ok = function () {

            vm.$uibModalInstance.close();
        };

        vm.cancel = function () {

            vm.$uibModalInstance.dismiss('cancel');

        };

    }

})();