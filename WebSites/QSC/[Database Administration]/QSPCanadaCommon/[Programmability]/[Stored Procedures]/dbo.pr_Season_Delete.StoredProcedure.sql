USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_Delete]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete an existing row from the table 'Season'
-- Gets: @iID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Season_Delete]
	@iID int
AS
SET NOCOUNT ON
-- DELETE an existing row from the table.
DELETE FROM 	[dbo].[Season]
WHERE		[ID] = @iID
GO
