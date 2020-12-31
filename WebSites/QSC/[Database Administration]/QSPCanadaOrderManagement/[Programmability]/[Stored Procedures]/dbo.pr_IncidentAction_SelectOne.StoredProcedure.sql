USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_SelectOne]    Script Date: 06/07/2017 09:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_IncidentAction_SelectOne]
	@iInstance int
	
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Instance],
	[IncidentInstance],
	[ActionInstance],
	[Comments],
	[UserIDCreated],
	[DateCreated]
FROM [dbo].[IncidentAction]
WHERE
	[Instance] = @iInstance
GO
