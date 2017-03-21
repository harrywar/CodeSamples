USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Contract_UpdateURL]    Script Date: 3/21/2017 11:21:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Contract_UpdateURL]
	@id int 
	,@url nvarchar(MAX)

AS

BEGIN

UPDATE [dbo].[Contracts]

SET 
	[url] = @url

WHERE id = @id

END

/*-------------TEST CODE -------------------

DECLARE @id int = 18
	   ,@url nvarchar(MAX) = 'www.testing.com'


EXECUTE [dbo].[Contract_UpdateURL]
		@id
		,@url

		                                       	
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