USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CAccountCode_SelectAll]    Script Date: 06/07/2017 09:33:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CAccountCode'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CAccountCode_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Id],
	[Class],
	[AccountCode],
	[Name]
FROM [dbo].[CAccountCode]
ORDER BY 
	[Class] ASC
	, [AccountCode] ASC
GO
