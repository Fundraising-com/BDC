USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Contact]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[pr_upd_Contact]
	@ContactID int,
	@ContactListID int,
	@CAccountID int,
	@Title varchar(10),
	@FirstName varchar(20),
	@LastName varchar(30),
	@MiddleInitial varchar(10),
	--@TypeId int,
	@Function varchar(50),
	@Email varchar(60)
AS

UPDATE 
	dbo.Contact 
SET 
	  ContactListID 	= @ContactListID
	, CAccountID 		= @CAccountID
	, Title 		= @Title
	, FirstName 		= @FirstName
	, LastName 		= @LastName
	, MiddleInitial 	= @MiddleInitial
	--, TypeId 		= @TypeId
	, [Function] 		= @Function
	, Email 		= @Email
WHERE 
	Contact.[Id] 		= @ContactID
GO
