USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[CompanyRating_GetByCompanyId]    Script Date: 3/21/2017 11:11:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[CompanyRating_GetByCompanyId]
     @companyId int
	 
AS

BEGIN

SELECT 
		[id]
		,[companyId]
		,[rating]
		,[ratingComment]
		,[raterId]
        ,[createdDate]
		,(SELECT firstName + ' ' + lastName FROM [UserProfile] WHERE [UserProfile].userId = [CompanyRatings].raterId) AS reviewerFullName
		
FROM [dbo].[CompanyRatings]

  WHERE ([companyId] = @companyId)

  ORDER BY [createdDate] DESC
    
END

/*-------------TEST CODE -------------------

DECLARE @companyId int = 3

EXECUTE [dbo].[CompanyRating_GetByCompanyId]
		 @companyId
		 	                  
*/
