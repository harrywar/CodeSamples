using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class MessageService : BaseService
    {
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static int InsertMessage(MessageInsertRequest model)
        {
            int id = 0;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Message_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@sender_id", model.SenderId);
                    paramCollection.AddWithValue("@receiver_id", model.ReceiverId);
                    paramCollection.AddWithValue("@content", model.Content);
                    paramCollection.AddWithValue("@conversation_id", model.ConversationId);

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

        public static List<MessageDomain> GetMessageByConversationId(int id)
        {
            List<MessageDomain> messageList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Message_GetByConversationId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@conversation_id", id);
              },
                map: delegate (IDataReader reader, short set)
                {
                    MessageDomain SingleMessage = new MessageDomain();
                    int startingIndex = 0; //startingOrdinal

                    SingleMessage.ConversationId = reader.GetSafeInt32(startingIndex++);
                    SingleMessage.MessageId = reader.GetSafeInt32(startingIndex++);
                    SingleMessage.SenderId = reader.GetSafeString(startingIndex++);
                    SingleMessage.ReceiverId = reader.GetSafeString(startingIndex++);
                    SingleMessage.Content = reader.GetSafeString(startingIndex++);
                    SingleMessage.CreateDate = reader.GetSafeDateTime(startingIndex++);
                    SingleMessage.SenderFullName = reader.GetSafeString(startingIndex++);
                    SingleMessage.ReceiverFullName = reader.GetSafeString(startingIndex++);
                    SingleMessage.SenderUrl = reader.GetSafeString(startingIndex++);
                    SingleMessage.ReceiverUrl = reader.GetSafeString(startingIndex++);
                    SingleMessage.IsRead = reader.GetSafeBool(startingIndex++);

                    if (messageList == null)
                    {
                        messageList = new List<MessageDomain>();
                    }

                    messageList.Add(SingleMessage);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return messageList;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static bool UpdateMessageAsRead(int messageId)
        {
            bool success = false;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Messages_Update_MarkAsRead"
                      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                      {
                          paramCollection.AddWithValue("@messageId", messageId);

                          success = true;
                      });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return success;

        }
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static bool MarkMessagesAsReadById(ListOfIntsRequest model)
        {
            bool isSuccess = false;

            try
            {
                foreach (int messageId in model.Items)
                {
                    UpdateMessageAsRead(messageId);
                }

                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSuccess;
        }

    }
}