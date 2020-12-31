USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Phone_SelectAllByPhoneListID]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select one or more existing rows from the table 'Phone'
-- based on a foreign key field.
-- Gets: @iPhoneListID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Phone_SelectAllByPhoneListID]
	@iPhoneListID int

AS
SET NOCOUNT ON
-- SELECT one or more existing rows from the table.
SELECT
	[ID],
	[Type],
	[PhoneListID],
	coalesce([PhoneNumber], '') AS PhoneNumber,
	coalesce([BestTimeToCall], '') AS BestTimeToCall
FROM [dbo].[Phone]
WHERE
	[PhoneListID] = @iPhoneListID
GO
