USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Contract_Insert]    Script Date: 3/21/2017 11:22:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Contract_Insert]
	@id int OUTPUT
	,@quoteRequestId int
	,@quoteId int

AS

BEGIN

INSERT INTO [dbo].[Contracts]
	([quoteRequestId]
	,[quoteId]
	,[createDate])

VALUES
	(@quoteRequestId
	,@quoteId
	,GETDATE()
	)

SET @id = SCOPE_IDENTITY()

END

/*-------------TEST CODE -------------------

DECLARE @id int 
	   ,@quoteRequestId int = 49
	   ,@quoteId int = 36

EXECUTE [dbo].[Contract_Insert]
		@id
		,@quoteRequestId
        ,@quoteId
		                                       	
SELECT 
		 [id]
        ,[contractTerms]
        ,[quoteRequestId]
		,[quoteId]
		,[stateId]
		,[createDate]
		,[url]

FROM [dbo].[Contracts]

*/