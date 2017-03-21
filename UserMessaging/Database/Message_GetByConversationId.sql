USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Message_GetByConversationId]    Script Date: 3/21/2017 11:37:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Message_GetByConversationId]
     @conversation_id int

AS

BEGIN

SELECT ALL
		[conversation_id]
		,[Messages].id
		,[sender_id]
        ,[receiver_id]
        ,[content]
        ,[createDate]
		,(SELECT firstName + ' ' + lastName FROM [UserProfile] WHERE [UserProfile].userId = [Messages].sender_id) AS senderFullName
		,(SELECT firstName + ' ' + lastName FROM [UserProfile] WHERE [UserProfile].userId = [Messages].receiver_id) AS receiverFullName
		,(SELECT url FROM [Media] JOIN [UserProfile] ON [UserProfile].userId = [Messages].sender_id WHERE [Media].id = [UserProfile].mediaId) AS senderURL
        ,(SELECT url FROM [Media] JOIN [UserProfile] ON [UserProfile].userId = [Messages].receiver_id WHERE [Media].id = [UserProfile].mediaId) AS receiverURL
		, [is_read]

FROM [dbo].[Messages]

WHERE [conversation_id] = @conversation_id
	  
END

/*-------------TEST CODE -------------------

DECLARE @conversation_id int = 15

EXECUTE [dbo].[Message_GetByConversationId]
		 @conversation_id
		 	                  
*/
