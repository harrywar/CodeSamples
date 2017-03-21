using Sabio.Web.Domain;
using Sabio.Web.Hubs;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/messages")]

    public class MessageApiController : ApiController
    {
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route(), HttpPost]
        [Authorize]
        public HttpResponseMessage InsertMessage(MessageInsertRequest model)

        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        
            int messageId = MessageService.InsertMessage(model);

            ItemResponse<int> response = new ItemResponse<int> { Item = messageId};

            SignalRHub.SendMessage(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("{id}"), HttpGet]
        [Authorize]
        public HttpResponseMessage GetMessageByConversationId(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<MessageDomain> MessageList = MessageService.GetMessageByConversationId(id);

            var response = new ItemsResponse<MessageDomain> { Items = MessageList };


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [Route("markRead"), HttpPut]
        [Authorize]
        public HttpResponseMessage MarkMessageAsRead(ListOfIntsRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<bool> response;

            switch (model == null)
            {
                case true:
                    response = new ItemResponse<bool> { Item = false };
                    break;

                default:
                    bool isSuccess = MessageService.MarkMessagesAsReadById(model);

                    response = new ItemResponse<bool> { Item = isSuccess };
                    break;
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
