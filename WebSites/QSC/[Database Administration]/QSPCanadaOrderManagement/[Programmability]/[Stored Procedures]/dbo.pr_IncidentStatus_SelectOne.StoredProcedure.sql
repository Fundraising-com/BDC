USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentStatus_SelectOne]    Script Date: 06/07/2017 09:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_IncidentStatus_SelectOne]
	@iInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Instance],
	[Description]
FROM [dbo].[IncidentStatus]
WHERE
	[Instance] = @iInstance
GO
