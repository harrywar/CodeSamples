USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Conversation_CheckIfExists]    Script Date: 3/21/2017 11:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Conversation_CheckIfExists]

@sender_id nvarchar(128) 
,@receiver_id nvarchar(128)

AS

SELECT [id]
,[sender_id]
,[receiver_id]
,[createDate]

  FROM [C27].[dbo].[Conversations]

  WHERE ([sender_id] = @sender_id AND [receiver_id] = @receiver_id)
  
  OR

  ([sender_id] = @receiver_id AND [receiver_id] = @sender_id)



  /* Test Code

  DECLARE
  @sender_id nvarchar(128) = '296773ec-ca26-458b-8425-776eea1c1f13'
  ,@receiver_id nvarchar(128) ='10a19093-9f84-48e4-be81-d1f679e36a22'
  
  
EXECUTE [dbo].[Conversation_CheckIfExists]
  '296773ec-ca26-458b-8425-776eea1c1f13'
 ,'10a19093-9f84-48e4-be81-d1f679e36a22'

  */