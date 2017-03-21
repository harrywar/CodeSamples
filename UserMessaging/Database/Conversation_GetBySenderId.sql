USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Conversation_GetBySenderId]    Script Date: 3/21/2017 11:35:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Conversation_GetBySenderId]
     @sender_id nvarchar(128)
	 
AS

BEGIN

SELECT 
		[id]
		,[sender_id]
        ,[receiver_id]
        ,[createDate]
		,(SELECT firstName + ' ' + lastName FROM [UserProfile] WHERE [UserProfile].userId = [Conversations].sender_id) AS senderFullName
		,(SELECT firstName + ' ' + lastName FROM [UserProfile] WHERE [UserProfile].userId = [Conversations].receiver_id) AS receiverFullName
		,(SELECT url FROM [Media] JOIN [UserProfile] ON [UserProfile].userId = [Conversations].sender_id WHERE [Media].id = [UserProfile].mediaId) AS senderURL
        ,(SELECT url FROM [Media] JOIN [UserProfile] ON [UserProfile].userId = [Conversations].receiver_id WHERE [Media].id = [UserProfile].mediaId) AS receiverURL
		,(SELECT name FROM [Company] JOIN [UserProfile] ON [UserProfile].userId = [Conversations].receiver_id WHERE [UserProfile].companyId = [Company].id) AS receiverCompanyName
		,(SELECT name FROM [Company] JOIN [UserProfile] ON [UserProfile].userId = [Conversations].sender_id WHERE [UserProfile].companyId = [Company].id) AS senderCompanyName

FROM [dbo].[Conversations]

  WHERE ([sender_id] = @sender_id OR [receiver_id] = @sender_id)

  ORDER BY [createDate] DESC
    
END

/*-------------TEST CODE -------------------

DECLARE @sender_id nvarchar(128) = '296773ec-ca26-458b-8425-776eea1c1f13'

EXECUTE [dbo].[Conversation_GetBySenderId]
		 @sender_id
		 	                  
*/
