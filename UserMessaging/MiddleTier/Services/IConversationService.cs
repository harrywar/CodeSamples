using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IConversationService
    {
        int InsertConversation(ConversationInsertRequest model);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        ConversationListDomain GetConversationsBySenderId(string sender_id);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        ConversationDomain CheckConversationExists(ConversationInsertRequest model);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        ConversationDomain GetConversationById(int id);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        List<int> GetUnreadConversationIdByUserId(string userId);

    }
}
