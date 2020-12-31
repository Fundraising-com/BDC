USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Contact_SelectAllByAccountID]    Script Date: 06/07/2017 09:33:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select one or more existing rows from the table 'Contact'
-- based on a foreign key field.
-- Gets: @iPhoneListID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Contact_SelectAllByAccountID]
	@iCAccountID int

AS
SET NOCOUNT ON
-- SELECT one or more existing rows from the table.
SELECT
	[Id],
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
FROM [dbo].[Contact]
WHERE
	[CAccountID] = @iCAccountID
AND	[DeletedTF] = 0
GO
