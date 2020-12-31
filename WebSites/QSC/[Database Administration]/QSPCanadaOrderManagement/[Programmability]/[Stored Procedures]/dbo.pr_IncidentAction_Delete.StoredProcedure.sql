USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_Delete]    Script Date: 06/07/2017 09:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete an existing row from the table 'IncidentAction'
-- using the Primary Key. 
-- Gets: @iIncidentInstance int
-- Gets: @iActionInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_IncidentAction_Delete]
	@iInstance int
AS
SET NOCOUNT ON
-- DELETE an existing row from the table.
DELETE FROM [dbo].[IncidentAction]
WHERE
	[Instance] = @iInstance
GO
