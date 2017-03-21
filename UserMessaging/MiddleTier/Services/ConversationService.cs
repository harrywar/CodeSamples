using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class ConversationService : BaseService, IConversationService
    {

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public int InsertConversation(ConversationInsertRequest model)
        {
            int id = 0;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Conversation_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@sender_id", model.SenderId);
                    paramCollection.AddWithValue("@receiver_id", model.ReceiverId);

                    SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<ConversationDomain> GetDBConversationsBySenderId(string sender_id)
        {

            List<ConversationDomain> conversationList = null;

            try
            {

                DataProvider.ExecuteCmd(GetConnection, "dbo.Conversation_GetBySenderId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@sender_id", sender_id);
              },
                map: delegate (IDataReader reader, short set)
                {
                    var singleConversation = new ConversationDomain();
                    int startingIndex = 0; //startingOrdinal

                    singleConversation.ConversationId = reader.GetSafeInt32(startingIndex++);
                    singleConversation.SenderId = reader.GetSafeString(startingIndex++);
                    singleConversation.ReceiverId = reader.GetSafeString(startingIndex++);
                    singleConversation.CreateDate = reader.GetSafeDateTime(startingIndex++);
                    singleConversation.SenderFullName = reader.GetSafeString(startingIndex++);
                    singleConversation.ReceiverFullName = reader.GetSafeString(startingIndex++);
                    singleConversation.SenderUrl = reader.GetSafeString(startingIndex++);
                    singleConversation.ReceiverUrl = reader.GetSafeString(startingIndex++);
                    singleConversation.ReceiverCompanyName = reader.GetSafeString(startingIndex++);
                    singleConversation.SenderCompanyName = reader.GetSafeString(startingIndex++);


                    if (conversationList == null)
                    {
                        conversationList = new List<ConversationDomain>();
                    }

                    conversationList.Add(singleConversation);

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return conversationList;
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public ConversationDomain CheckConversationExists(ConversationInsertRequest model)
        {
            ConversationDomain conversation = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Conversation_CheckIfExists"
             , inputParamMapper: delegate (SqlParameterCollection paramCollection)
             {
                 paramCollection.AddWithValue("@sender_id", model.SenderId);
                 paramCollection.AddWithValue("@receiver_id", model.ReceiverId);
             },
             map: delegate (IDataReader reader, short set)
             {
                 conversation = new ConversationDomain();
                 int startingIndex = 0; //startingOrdinal

                 conversation.ConversationId = reader.GetSafeInt32(startingIndex++);
                 conversation.SenderId = reader.GetSafeString(startingIndex++);
                 conversation.ReceiverId = reader.GetSafeString(startingIndex++);
                 conversation.CreateDate = reader.GetSafeDateTime(startingIndex++);
             });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return conversation;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public ConversationDomain GetConversationById(int id)
        {
            ConversationDomain singleConversation = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Conversation_GetById"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@id", id);
              },
              map: delegate (IDataReader reader, short set)
              {
                  singleConversation = new ConversationDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleConversation.ConversationId = reader.GetSafeInt32(startingIndex++);
                  singleConversation.SenderId = reader.GetSafeString(startingIndex++);
                  singleConversation.ReceiverId = reader.GetSafeString(startingIndex++);
                  singleConversation.CreateDate = reader.GetSafeDateTime(startingIndex++);
              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return singleConversation;
        }

        public List<int> GetUnreadConversationIdByUserId(string userId)
        {
            List<int> conversationList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Messages_Select_UnreadConversationIdByUserId"
                    , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                    {
                        paramCollection.AddWithValue("@userId", userId);
                    }, map: delegate (IDataReader reader, short set)
                    {
                        int id;

                        id = reader.GetSafeInt32(0);

                        if (conversationList == null)
                        {
                            conversationList = new List<int>();
                        }

                        conversationList.Add(id);
                    }
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (conversationList == null)
            {
                conversationList = new List<int> { 0 };
            }

            return conversationList;
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public ConversationListDomain GetConversationsBySenderId(string sender_id)
        {
            ConversationListDomain conversationList = null;

            try
            {
                conversationList = new ConversationListDomain();

                conversationList.BotConversation = BotConversationService.BuildBotProfile();

                conversationList.UnreadConversations = GetUnreadConversationIdByUserId(sender_id);

                conversationList.ConversationObjects = GetDBConversationsBySenderId(sender_id);

                if (conversationList.ConversationObjects != null && conversationList.ConversationObjects.Count > 0)
                {

                    foreach (ConversationDomain convo in conversationList.ConversationObjects)
                    {
                        convo.IsRead = true;

                        for (int i = 0; i < conversationList.UnreadConversations.Count; i++)
                        {
                            if (convo.ConversationId == conversationList.UnreadConversations[i])
                            {
                                convo.IsRead = false;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return conversationList;
        }
    }
}

