USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentStatus_SelectAll]    Script Date: 06/07/2017 09:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_IncidentStatus_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[Description]
FROM [dbo].[IncidentStatus]
ORDER BY 
	[Instance] ASC
GO
