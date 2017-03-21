USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Contract_GetByQuoteId]    Script Date: 3/21/2017 11:21:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[Contract_GetByQuoteId]
     @quoteId int
	 
AS

BEGIN

SELECT 
		[id]
		,[contractTerms]
        ,[quoteRequestId]
        ,[quoteId]
		,[stateId]
		,[createDate]
		,[url]
		
FROM [dbo].[Contracts]

  WHERE ([quoteId] = @quoteId)

  ORDER BY [createDate] DESC
    
END

/*-------------TEST CODE -------------------

DECLARE @quoteId int = 25

EXECUTE [dbo].[Contract_GetByQuoteId]
		 @quoteId
		 	                  
*/
