/// <reference path="../Templates/botConversationModalTemplate.html" />
/// <reference path="../../QuoteRequests/Template/QuoteRequestAddNew.html" />
/// <reference path="../../QuoteRequests/Template/QuoteRequestAddNew.html" />
// AUTHENTICATED LAYOUT MESSAGING CONTROLLER FACTORY

(function () {
	"use strict";
	
	//console.log("conversation controller fired!");
	angular.module(APPNAME)
		.controller('authenticatedLayoutMessagingController', AuthenticatedLayoutMessagingController);

	AuthenticatedLayoutMessagingController.$inject = ['$scope', '$baseController', "$messagingService", "$uibModal", '$quoterequestService', '$signalRService', '$alertService'];

	function AuthenticatedLayoutMessagingController(
		$scope,
		$baseController,
		$messagingService,
		$uibModal,
		$quoterequestService,
		$signalRService,
		$alertService)
		{


		var vm = this;
		vm.items = null;      
		vm.currentUserId = $('#PAGEUSER').val();
		vm.conversationItems = null;

		vm.$messagingService = $messagingService;
		vm.$signalRService = $signalRService;
		vm.$scope = $scope;
		vm.$uibModal = $uibModal;
		vm.$quoterequestService = $quoterequestService;
		vm.$alertService = $alertService;

		$baseController.merge(vm, $baseController);

		vm.notify = vm.$messagingService.getNotifier($scope);


		
		// Public Methods
		vm.openBotConversationModal = _openBotConversationModal;

		// Startup functions
		_renderConversations();





        //....// - Callback function that provides toastr alert when new message is received
        //....// - and connected user is not on the messages page
        vm.$signalRService.CallbackRegistration('addNewMessageToPage', function (message)
        {
            vm.$alertService.success("New message recieved");

			console.log("AuthenticatedLayoutMessagingController signalR registered.");

		})


		function _renderConversations()
		{

			//console.log('vm.currentUserId: ', vm.currentUserId);
			vm.$messagingService.getBySenderId(vm.currentUserId, _getConversationsSuccess, _getConversationsError);
		}



		// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		function _getConversationsSuccess(data) {

			//console.log("Get conversationsSuccess: ", data);

			if (!(data.item.conversationObjects != null && typeof data.item.conversationObjects === "object" && data.item.conversationObjects.length > 0)) {
				console.log('item.conversationObjects, ', data.item.conversationObjects);
				data.item.conversationObjects = null;
			}


			vm.notify(function () {
				vm.conversationItems = data.item.conversationObjects;
				//console.log('GETTING Conversations Success for Message Page: ', vm.conversationItems);

				if (data.item.unreadConversations[0] != 0) {
					vm.unreadConversations = data.item.unreadConversations;
				}
				//console.log('GETTING Conversations Success for Message Page (part deux): ', vm.unreadConversations);
			});
		}



		// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		function _getConversationsError(jqXhr, error) {

			console.error(error);
		}

		

		// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		function _openBotConversationModal() {
			console.log("open bot modal fired.");
			var modalInstance = vm.$uibModal.open({
				templateUrl: '/Scripts/app/Messages/Templates/botConversationModalTemplate.html',

				controller: 'botConversationModalController as BotCC',
				size: 'md',
				resolve: {
					// This is where data is passed to the opening modal
					conversation: function inputToModal (){}
				}
			});

			//  when the modal closes it returns a promise
			modalInstance.result.then(function (quoteRequest) {

				//  if the user closed the modal by clicking Save
				console.log(" returned from modal: ", quoteRequest);
				vm.createNewQuoteRequest(quoteRequest);

			}, function () {
				console.log('Modal dismissed at: ' + new Date());   //  if the user closed the modal by clicking cancel
			});
		}


	}

})();