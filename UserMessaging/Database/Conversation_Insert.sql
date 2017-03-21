USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Conversation_Insert]    Script Date: 3/21/2017 11:36:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Conversation_Insert]
	@id int OUTPUT
	,@sender_id nvarchar(128)
	,@receiver_id nvarchar(128)

AS

BEGIN

INSERT INTO [dbo].[Conversations]
	([sender_id]
	,[receiver_id]
	,[createDate])

VALUES
	(@sender_id
	,@receiver_id
	,GETDATE()
	)

SET @id = SCOPE_IDENTITY()

END

/*-------------TEST CODE -------------------


DECLARE @id int
	   ,@sender_id nvarchar(128) = 'Josh'
	   ,@receiver_id nvarchar(128) = 'James'

EXECUTE [dbo].[Message_Insert]
		 @id
		,@sender_id
        ,@receiver_id 
		                                       	
SELECT 
		 [id]
        ,[sender_id]
        ,[receiver_id]
		,[createDate]
FROM [dbo].[Conversations]

*/