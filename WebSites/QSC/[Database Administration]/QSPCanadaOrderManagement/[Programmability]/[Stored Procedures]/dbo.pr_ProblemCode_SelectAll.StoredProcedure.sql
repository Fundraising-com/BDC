USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProblemCode_SelectAll]    Script Date: 06/07/2017 09:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'ProblemCode'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ProblemCode_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[Description]
FROM [dbo].[ProblemCode]
ORDER BY 
	[Instance] ASC
GO
