USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccountClass_SelectAll]    Script Date: 06/07/2017 09:33:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CAccountClass'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CAccountClass_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Id],
	[AccountClass],
	[Name]
FROM [dbo].[CAccountClass]
ORDER BY 
	[Id] ASC
GO
