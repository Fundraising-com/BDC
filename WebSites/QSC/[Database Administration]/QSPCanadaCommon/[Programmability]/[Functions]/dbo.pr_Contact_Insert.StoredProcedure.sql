USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Contact_Insert]    Script Date: 06/07/2017 09:33:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'Contact'
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
-- Returns: @iId int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Contact_Insert]
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
	@dDateChanged datetime,
	@iId int OUTPUT
AS
-- INSERT a new row in the table.
INSERT [dbo].[Contact]
(
	[ContactListID],
	[CAccountID],
	[Title],
	[FirstName],
	[LastName],
	[MiddleInitial],
	[TypeId],
	[Function],
	[Email],
	[AddressID],
	[PhoneListID],
	[DeletedTF],
	[DateChanged]
)
VALUES
(
	@iContactListID,
	@iCAccountID,
	@sTitle,
	@sFirstName,
	@sLastName,
	@sMiddleInitial,
	@iTypeId,
	@sFunction,
	@sEmail,
	@iAddressID,
	@iPhoneListID,
	ISNULL(@bDeletedTF, (0)),
	@dDateChanged
)
-- Get the IDENTITY value for the row just inserted.
SELECT @iId=SCOPE_IDENTITY()
GO
