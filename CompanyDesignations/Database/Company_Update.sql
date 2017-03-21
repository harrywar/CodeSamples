USE [C27]
GO
/****** Object:  StoredProcedure [dbo].[Company_Update]    Script Date: 3/21/2017 9:58:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER Proc [dbo].[Company_Update]

			@id int
           ,@CName nvarchar(200)
           ,@TypeId int 
		   ,@Phone nvarchar(20)
		   ,@faxNumber nvarchar(20)
		   ,@email nvarchar(50)
		   ,@url nvarchar(150) = null
		   ,@designations int
		   	
AS


BEGIN

UPDATE [dbo].[Company]
SET   
[name] = @CName
,[typeId] = @TypeId
,[phoneNumber] = @Phone 
,[faxNumber] = @faxNumber 
,[email] = @email 
,[url] = @url
,[designations] = @designations

WHERE [id] =  @id

END


/*

Declare		@id int = 52
           ,@CName nvarchar(200)= 'Steel Corp'
           ,@TypeId int = 2
		   ,@Phone nvarchar(20) = '800-123-4567'
		   ,@faxNumber nvarchar(20) = '800-987-6543'
		   ,@email nvarchar(50) = 'info@steelcorp.com'
		   ,@url nvarchar(150) = 'steelcorp.com'
		   ,@designations int = 0

Execute [dbo].[Company_Update] 

			@id 
           ,@CName 
           ,@TypeId  
		   ,@Phone 
		   ,@faxNumber 
		   ,@email  
		   ,@url
		   ,@designations

Select *
From dbo.Company
*/