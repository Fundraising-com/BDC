USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Contact_SelectOneLastByAccountID]    Script Date: 06/07/2017 09:33:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select one or more existing rows from the table 'Contact'
-- based on a foreign key field.
-- Gets: @iPhoneListID int
-- 03/09/2006 - Ben : CHANGED TO GET PRIMARY CONTACT INSTEAD OF LAST
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Contact_SelectOneLastByAccountID]
	@iCAccountID int

AS
SET NOCOUNT ON

DECLARE	@iPrimaryCount	int

SELECT	@iPrimaryCount = COUNT(*)
FROM		Contact c
WHERE	c.TypeId = 10
AND		c.DeletedTF = 0
AND		c.CAccountID = @iCAccountID

IF(@iPrimaryCount > 0)
BEGIN
	SELECT TOP 1
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
		[TypeId] = 10
	AND	[CAccountID] = @iCAccountID
	AND	[DeletedTF] = 0
	ORDER BY
		[DateChanged]	DESC
END
ELSE
BEGIN
	SELECT TOP 1
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
	ORDER BY
		[DateChanged]	DESC
END
GO
