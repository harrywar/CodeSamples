USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Message_DeleteBySenderId]    Script Date: 3/21/2017 11:37:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Message_DeleteBySenderId]
	@sender_id nvarchar(128)
AS

BEGIN

DELETE FROM [dbo].[Messages] 
 
WHERE sender_id = @sender_id

END



/*-------------TEST CODE -------------------


DECLARE @sender_id nvarchar(128) = 'James';

EXECUTE [dbo].[Message_DeleteBySenderId]
		@sender_id
		                                                	
SELECT 
		 [id]
        ,[sender_id]
		,[receiver_id]
        ,[content]
		,[createDate]
		,[conversation_id]
		,[firstName]

FROM [dbo].[Messages]

*/