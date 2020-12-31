USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_SelectByIncidentID]    Script Date: 06/07/2017 09:20:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_IncidentAction_SelectByIncidentID]
	@iIncidentInstance int
	
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	IncidentAction.[instance],
	[IncidentInstance],
	action.Description as ActionDescription,
	[ActionInstance],
	[Comments],
	[UserIDCreated],
	[DateCreated]
	
FROM [dbo].[IncidentAction], [Action]
WHERE
	[IncidentInstance] = @iIncidentInstance and 
	Action.Instance = IncidentAction.ActionInstance
GO
