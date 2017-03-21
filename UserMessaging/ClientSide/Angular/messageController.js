/// <reference path="../Templates/contactsModal.html" />
/// <reference path="../Templates/contactsModal.html" />
// MAIN MESSAGING CONTROLLER

(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('messageController', MessageController);

    MessageController.$inject = ['$scope', '$rootScope', '$baseController', '$messagingService', '$botConversationService', '$signalRService'];

    function MessageController(
        $scope,
        $rootScope,
        $baseController,
        $messagingService,
        $botConversationService,
        $signalRService)
        {


        // - Injection
        var vm = this;
        vm.$messagingService = $messagingService;
        vm.$signalRService = $signalRService;
        vm.$scope = $scope;
        vm.$botConversationService = $botConversationService;
        vm.$rootScope = $rootScope;

       

        // - Base controller merge
        $baseController.merge(vm, $baseController);


        // - Public Properties
        vm.items = null;
        vm.currentUserId = $('#PAGEUSER').val();
        vm.messageItems = null;
        vm.botMessages = null;
        vm.messageContent = null;
        vm.lastClickedMessageConversationId = null;
        vm.lastClickedMessageReceiverId = null;
        vm.notify = vm.$messagingService.getNotifier($scope);

        // - Public Methods
        vm.getMessages = _getMessages;
        vm.sendMessage = _sendMessage;
        vm.setActiveTab = _setActiveTab;


        // - Submit message on enter key
        vm.submit = function () {
            if (vm.messageContent) {
                vm.messageContent = this.messageContent;
                console.log('thisMessage:', vm.messageContent);
                _sendMessage();
                vm.messageContent = '';
            }
        };
        

        // - SignalR service callback function
        vm.$signalRService.CallbackRegistration('addNewMessageToPage', function (message) {
            _getMessages();

            console.log(message);

        }, true);

       

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


        // - Start-up functions
        _getMessages();





        //....// ============================================================================
        function _getMessages()
        {

            //console.log('signalRHub.client.addNewMessageToPage: ', signalR.client.addNewMessageToPage);

            // - $routeParams are specified in the ngRoute config.
            var conversationId = vm.$routeParams.conversationId;
            var receiverId = vm.$routeParams.receiverId;

            // - BotConversation
            if (!conversationId && vm.$routeParams.botId)
            {
                //console.log("Bot Conversation!!!"); // TODO DELETE

                vm.$botConversationService.getByUserId(vm.currentUserId, _getBotMessageSuccess, _getMessagesError);
            }

                // - Human-to-human conversation
                if (conversationId && receiverId)
                {

                vm.$messagingService.getByConversationId(conversationId, _getMessagesSuccess, _getMessagesError);

                vm.lastClickedMessageConversationId = conversationId;
                vm.lastClickedMessageReceiverId = receiverId;

                }

        }



        //....// ============================================================================
        function _getMessagesSuccess(data) {

            var incoming = data.items;

            vm.notify(function () {
                vm.messageItems = incoming;
                scrollSmoothToBottom("list_of_messages_profile_page");

                if (incoming != null && incoming.length > 0) {
                    var items = [];

                    for (var i = 0; i < incoming.length; i++) {
                        // - If an incoming message is NOT from the current user, and is NOT read, add to payload
                        if (incoming[i].receiverId == vm.currentUserId && !incoming[i].isRead) {
                            items.push(incoming[i].messageId);
                        }
                    }
                    if (items.length > 0) {

                        var payload = { items: items };

                        vm.$messagingService.markMessagesAsRead(payload, _logAjaxSuccess, _getMessagesError);
                    }
                }


            });

            scrollSmoothToBottom("list_of_messages_profile_page");

        }
        
        


        //....// ============================================================================
        function _getBotMessageSuccess(data) {
            var incoming = data.items;

            vm.notify(function () {
                vm.botMessages = incoming;
                console.log('GETTING BotMessages Success', vm.botMessages);
            });

            scrollSmoothToBottom("list_of_messages_profile_page");
            
        }
        





        //....// ============================================================================
        function _getMessagesError(jqXhr, error) {

            console.error(error);
        }
        





        //....// ============================================================================
        function _sendMessage()
        {

            var conversationId = vm.$routeParams.conversationId;
            //var messageObject;

            // Human-to-human conversation
            if (conversationId) {

            var messageObject =
                {

                    "senderId": vm.currentUserId,
                    "receiverId": vm.$routeParams.receiverId,
                    "content": vm.messageContent,
                    "conversationId": conversationId
                };

                //console.log('messageObject', messageObject); // TODO DELETE

                vm.$messagingService.insert(messageObject, _insertMessageSuccess, _insertMessageError);

                vm.messageContent = null;


                var message = messageObject.content;


            }


            // Bot Conversation
            if (!conversationId) {
                messageObject = {
                    "senderId": vm.currentUserId,
                    "content": vm.messageContent
                };

                vm.$botConversationService.insert(messageObject, _insertMessageSuccess, _insertMessageError);


                vm.messageContent = null;
            }

        }
        





        //....// ============================================================================
        function _insertMessageSuccess(data)
        {

            vm.notify(function () {
                vm.submittedMessageItem = data;
                console.log('POSTING Messages Success', vm.submittedMessageItem);
                //vm.$messagingService.getByConversationId(vm.$routeParams.conversationId, _getMessagesSuccess, _getMessagesError);
                _getMessages();
            });

        }





        //....// ============================================================================
        function _insertMessageError(jqXhr, error) {

            console.error(error);
        }





        //....// ============================================================================
        function _logAjaxSuccess(data) {
            // most basic Ajax success handler. Logs incoming json to console.
            console.log("Ajax Success: ", data);
        }
        




        //....// ============================================================================
        function _setActiveTab(tab)
        {
            for (var i = 0; i < vm.conversationItem.length; i++) {
                vm.conversationItem[i]["class"] = "";
            }

            conversationItem["class"] = "active";
        }





        //....// ============================================================================
        function scrollSmoothToBottom(id) {
            var div = document.getElementById(id);
            $('#' + id).animate({
                scrollTop: div.scrollHeight - div.clientHeight
            }, 500);
        }



                

    }

})();