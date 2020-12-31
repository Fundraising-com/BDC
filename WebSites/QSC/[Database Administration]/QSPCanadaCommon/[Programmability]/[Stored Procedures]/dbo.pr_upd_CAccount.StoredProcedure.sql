USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_CAccount]    Script Date: 06/07/2017 09:33:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_upd_CAccount]
	@Id int,
	@Name varchar(50),
	@Lang varchar(10),
	@CAccountCodeClass varchar(10),
	@CAccountCodeGroup varchar(50),
	@StatusID int,
	@Enrollment int,
	@Comment varchar(1000),
	@EMail varchar(75),
	@IsPrivateOrg bit,
	@IsAdultGroup bit,
	@Sponsor varchar(50),
	@ParentID int,
	@UserIDModified UserID_UDDT
AS

/*
 *
 * Please note :  PhoneListID and AddressListID are not included in this update procedure on purpose
 * Theoretically, once an account is created, while the contents of it's phone list and address list may change,
 * the PhoneListID and AddressListID should NEVER change. If for some reason they do need to be switched,
 * a developer should make the change manually. Same goes for Country.
 ********************************************************************************************************************************************/

UPDATE 
	CAccount 
SET 
	[Name]			= @Name,
	Lang			= @Lang,
	CAccountCodeClass	= @CAccountCodeClass,
	CAccountCodeGroup	= @CAccountCodeGroup,
	StatusID		= @StatusID,
	Enrollment		= @Enrollment,
	Comment		= @Comment,
	EMail			= @EMail,
	IsPrivateOrg		= @IsPrivateOrg,
	IsAdultGroup		= @IsAdultGroup,
	Sponsor			= @Sponsor,
	ParentID		= @ParentID,
	UserIDModified		= @UserIDModified,
	DateUpdated		= getdate()
WHERE 
	[Id] = @Id
GO
