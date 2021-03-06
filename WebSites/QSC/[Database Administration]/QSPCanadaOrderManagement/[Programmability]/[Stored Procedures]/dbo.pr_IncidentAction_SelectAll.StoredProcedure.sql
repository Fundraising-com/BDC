USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_SelectAll]    Script Date: 06/07/2017 09:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_IncidentAction_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[instance],
	[IncidentInstance],
	[ActionInstance],
	[Comments],
	[UserIDCreated],
	[DateCreated]
FROM [dbo].[IncidentAction]
ORDER BY 
	[IncidentInstance] ASC
	, [ActionInstance] ASC
GO
