using Microsoft.Practices.Unity;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/conversations")]
    [Authorize]
    public class ConversationApiController : ApiController
    {
        [Dependency]
        public IConversationService _ConversationService { get; set; }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("insert"), HttpPost]

        public HttpResponseMessage InsertConversation(ConversationInsertRequest model)

        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int conversationId = _ConversationService.InsertConversation(model);

            ItemResponse<int> response = new ItemResponse<int>(); // { Item = conversationId };

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("get/{senderId}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetConversationsBySenderId(string senderId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ConversationListDomain conversationList = _ConversationService.GetConversationsBySenderId(senderId);

            var response = new ItemResponse<ConversationListDomain> { Item = conversationList};

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("check"), HttpPost]
        [Authorize]
        public HttpResponseMessage CheckConversationExists(ConversationInsertRequest model)

        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //- Check for existing conversations.


           ConversationDomain conversationDomain = _ConversationService.CheckConversationExists(model);

            //- If no conversation found, create new conversation.

            if (conversationDomain == null)
            {
                int conversationId = _ConversationService.InsertConversation(model);

                conversationDomain = _ConversationService.GetConversationById(conversationId);
            }

            var response = new ItemResponse<ConversationDomain> { Item = conversationDomain };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("{id:int}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetConversationById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ConversationDomain conversation = _ConversationService.GetConversationById(id);

            var response = new ItemResponse<ConversationDomain> { Item = conversation };

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }


        [Route("unread")]
        public HttpResponseMessage GetUnreadConversationsByUserId(string userId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<int> conversationList = _ConversationService.GetUnreadConversationIdByUserId(userId);

            var response = new ItemsResponse<int> { Items = conversationList };

            return Request.CreateResponse(HttpStatusCode.OK, conversationList);
        }

    }
}
