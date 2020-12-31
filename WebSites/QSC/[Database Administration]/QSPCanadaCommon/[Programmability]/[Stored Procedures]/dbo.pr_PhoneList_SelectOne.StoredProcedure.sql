USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_PhoneList_SelectOne]    Script Date: 06/07/2017 09:33:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'PhoneList'
-- based on the Primary Key.
-- Gets: @iID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_PhoneList_SelectOne]
	@iID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[ID],
	[CreateDate],
	[DeletedTF]
FROM [dbo].[PhoneList]
WHERE
	[ID] = @iID
GO
