USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_DeleteWActionInstanceLogic]    Script Date: 06/07/2017 09:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete one or more  existing rows from the table 'IncidentAction'
-- using the Primary Key field [ActionInstance]. 
-- Gets: @iActionInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_IncidentAction_DeleteWActionInstanceLogic]
	@iActionInstance int

AS
SET NOCOUNT ON
-- DELETE one or more existing rows from the table.
DELETE FROM [dbo].[IncidentAction]
WHERE
	[ActionInstance] = @iActionInstance
GO
