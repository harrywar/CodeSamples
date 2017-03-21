USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Message_Insert]    Script Date: 3/21/2017 11:37:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Message_Insert]
	@id int OUTPUT
	,@sender_id nvarchar(128)
	,@receiver_id nvarchar(128)
	,@content nvarchar(MAX)
	,@conversation_id int

AS

BEGIN

INSERT INTO [dbo].[Messages]
	([sender_id]
	,[receiver_id]
	,[content]
	,[createDate]
	,[conversation_id])

VALUES
	(@sender_id
	,@receiver_id
	,@content
	,GETDATE()
	,@conversation_id
	)

SET @id = SCOPE_IDENTITY()

END

/*-------------TEST CODE -------------------


DECLARE @id int
	   ,@sender_id nvarchar(128) = 3
	   ,@receiver_id nvarchar(128) = 14 
       ,@content nvarchar(MAX) = 'This is a reply to the test message'
	   ,@conversation_id int = 1


EXECUTE [dbo].[Message_Insert]
		 @id
		,@sender_id
        ,@receiver_id 
        ,@content
		,@conversation_id
		                                       	
SELECT 
		 [id]
        ,[sender_id]
        ,[receiver_id]
        ,[content]
		,[createDate]
		,[conversation_id]

FROM [dbo].[Messages]

*/