USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[CompanyRating_GetAvgByCompanyId]    Script Date: 3/21/2017 11:10:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[CompanyRating_GetAvgByCompanyId]
     @companyId int
	 
AS

BEGIN

SELECT 
	AVG(rating) AS rating_average

		
FROM [dbo].[CompanyRatings]

  WHERE ([companyId] = @companyId)

    
END

/*-------------TEST CODE -------------------

DECLARE @companyId int = 3

EXECUTE [dbo].[CompanyRating_GetAvgByCompanyId]
		 @companyId
		 	                  
*/
