USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[CompanyRating_Insert]    Script Date: 3/21/2017 11:08:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[CompanyRating_Insert]
	@id int OUTPUT
	,@companyId int
	,@rating decimal(18, 0)
	,@ratingComment nvarchar(MAX)
	,@raterId nvarchar(128)

AS

BEGIN

INSERT INTO [dbo].[CompanyRatings]
	([companyId]
	,[rating]
	,[ratingComment]
	,[raterId]
	,[createdDate])

VALUES
	(@companyId
	,@rating
	,@ratingComment
	,@raterId
	,GETDATE()
	)

SET @id = SCOPE_IDENTITY()

END

/*-------------TEST CODE -------------------


DECLARE @id int
	   ,@companyId int = 2
	   ,@rating decimal = 4
	   ,@ratingComment nvarchar(MAX) = 'this is a test rating'
	   ,@raterId nvarchar(128) = 'cde7eeeb-a281-4c26-972c-add5a13017b6'
	   

EXECUTE [dbo].[Rating_Insert]
		 @id
		,@companyId
        ,@rating
		,@ratingComment
		,@raterId 
		                                       	
SELECT 
		 [id]
        ,[companyId]
        ,[rating]
		,[ratingComment]
		,[raterId]
		,[createdDate]
FROM [dbo].[CompanyRatings]

*/