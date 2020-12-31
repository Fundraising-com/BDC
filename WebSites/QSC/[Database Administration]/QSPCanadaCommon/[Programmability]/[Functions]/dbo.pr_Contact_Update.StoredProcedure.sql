USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Contact_Update]    Script Date: 06/07/2017 09:33:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'Contact'
-- Gets: @iId int
-- Gets: @iContactListID int
-- Gets: @iCAccountID int
-- Gets: @sTitle varchar(10)
-- Gets: @sFirstName varchar(20)
-- Gets: @sLastName varchar(30)
-- Gets: @sMiddleInitial varchar(10)
-- Gets: @iTypeId int
-- Gets: @sFunction varchar(50)
-- Gets: @sEmail varchar(60)
-- Gets: @iAddressID int
-- Gets: @iPhoneListID int
-- Gets: @bDeletedTF bit
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Contact_Update]
	@iId int,
	@iContactListID int,
	@iCAccountID int,
	@sTitle varchar(10),
	@sFirstName varchar(20),
	@sLastName varchar(30),
	@sMiddleInitial varchar(10),
	@iTypeId int,
	@sFunction varchar(50),
	@sEmail varchar(60),
	@iAddressID int,
	@iPhoneListID int,
	@bDeletedTF bit,
	@dDateChanged datetime
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[Contact]
SET 
	[ContactListID] = @iContactListID,
	[CAccountID] = @iCAccountID,
	[Title] = @sTitle,
	[FirstName] = @sFirstName,
	[LastName] = @sLastName,
	[MiddleInitial] = @sMiddleInitial,
	[TypeId] = @iTypeId,
	[Function] = @sFunction,
	[Email] = @sEmail,
	[AddressID] = @iAddressID,
	[PhoneListID] = @iPhoneListID,
	[DeletedTF] = @bDeletedTF,
	[DateChanged] = @dDateChanged
WHERE
	[Id] = @iId
GO
