/// <reference path="../Templates/contactsModal.html" />
/// <reference path="../Templates/contactsModal.html" />
// MAIN MESSAGING CONTROLLER

(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('conversationController', ConversationController);

    ConversationController.$inject = ['$scope', '$baseController', "$messagingService", '$uibModal'];

    function ConversationController(
        $scope,
        $baseController,
        $messagingService,
        $uibModal) {

        // Injection
        var vm = this;

        vm.$messagingService = $messagingService;
        vm.$scope = $scope;
        vm.$uibModal = $uibModal;

        $baseController.merge(vm, $baseController);

        vm.notify = vm.$messagingService.getNotifier($scope);

        // Properties
        vm.items = null;

        vm.currentUserId = $('#PAGEUSER').val();
        vm.messageItems = null;
        vm.messageContent = null;
        vm.conversationItems = null;
        vm.botConversation = null;

        // Methods
        vm.openModal = _openModal;
        vm.getMessages = _getMessages;
        vm.sendMessage = _sendMessage;
        vm.setActiveTab = _setActiveTab;
        vm.submit = _submit;



        // Submit message on enter key



        // Scroll down to most recent message
        $scope.$watch(function () {
            setTimeout(function () {
                var testingLocation = $('#mCSB_5_container');
                // console.log('testingLocation', testingLocation);
                var scrollToPosition = testingLocation.height() - $("#mCSB_5").height();
                // console.log('scrollToPosition: ', scrollToPosition);
                $("#mCSB_5").scrollTop(scrollToPosition);
            }, 300);
        });



        // Startup
        _renderConversations();


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _submit() {
            if (vm.messageContent) {
                vm.messageContent = this.messageContent;
                //console.log('thisMessage:', vm.messageContent);
                _sendMessage();
                vm.messageContent = '';
            }
        };



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _renderConversations() {

            //console.log('vm.currentUserId: ', vm.currentUserId);
            vm.$messagingService.getBySenderId(vm.currentUserId, _getConversationsSuccess, _getConversationsError);
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getConversationsSuccess(data) {

            //console.log("Get conversationsSuccess: ", data);

            vm.notify(function () {
                vm.conversationItems = data.item.conversationObjects;
                //console.log('GETTING Conversations Success for Message Page: ', vm.conversationItems);

                // Bot Profile is created on compile and will not be dynamically changing.
                // Specifics can be found in the server's BotConversationService.
                if (vm.botConversation == null) {
                    vm.botConversation = data.item.botConversation;
                }
                
            });

            setTimeout(function () {
                $(window).trigger('resize');
                //console.log('trigger resize running');
            }, 200);

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getConversationsError(jqXhr, error) {

            console.error("Get conversations error: ", error);
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getMessages(item) {

            vm.$messagingService.getByConversationId(item.conversationId, _getMessagesSuccess, _getMessagesError);

            vm.lastClickedMessageConversationId = item.conversationId;
            vm.lastClickedMessageReceiverId = item.receiverId;

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getMessagesSuccess(data) {

            vm.notify(function () {
                vm.messageItems = data.items;
                //console.log('GETTING Messages Success', vm.messageItems);
            });

            setTimeout(function () {
                $(window).trigger('resize');
                //console.log('trigger resize running');
            }, 200);

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getMessagesError(jqXhr, error) {

            console.error(error);
        }



        function _logAjaxSuccess(data) {
            // Most basic Ajax success handler. Just logs data to the console.
            //console.log("Ajax success: ", data);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _sendMessage() {

            var messageObject = {

                "senderId": vm.currentUserId,
                "receiverId": vm.lastClickedMessageReceiverId,
                "content": vm.messageContent,
                "conversationId": vm.lastClickedMessageConversationId

            };

            //console.log('messageObject', messageObject);

            vm.$messagingService.insert(messageObject, _insertMessageSuccess, _insertMessageError);

            vm.messageContent = null;

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _insertMessageSuccess(data) {

            vm.notify(function () {
                vm.submittedMessageItem = data;
                //console.log('POSTING Messages Success', vm.submittedMessageItem);
                vm.$messagingService.getByConversationId(vm.lastClickedMessageConversationId, _getMessagesSuccess, _getMessagesError);

            });

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _insertMessageError(jqXhr, error) {

            console.error(error);
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ANGULAR OPEN CONTACTS MODAL
        function _openModal() {

            var messagingContactsModal = vm.$uibModal.open({
                animation: true,
                templateUrl: '/Scripts/App/Messages/Templates/contactsModal.html',
                controller: 'messagingContactsModalController as mcmc',
                size: 'sm',
                resolve: {
                    items: function () {
                        return vm.items;
                    }
                }
            });

            messagingContactsModal.result.then(function () {
                _renderConversations();

            }, function () {

                //console.log('Modal closed at ' + new Date());

            });
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _setActiveTab(tab) {
            for (var i = 0; i < vm.conversationItem.length; i++) {
                vm.conversationItem[i]["class"] = ""
            }

            conversationItem["class"] = "active";
        };


    }

})();