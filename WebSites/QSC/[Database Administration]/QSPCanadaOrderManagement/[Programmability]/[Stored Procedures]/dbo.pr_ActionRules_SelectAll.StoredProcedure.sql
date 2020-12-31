USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ActionRules_SelectAll]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'ActionRules'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ActionRules_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[ActionInstance],
	[Unique],
	[ErrorMessage]
FROM [dbo].[ActionRules]
ORDER BY 
	[Instance] ASC
GO
