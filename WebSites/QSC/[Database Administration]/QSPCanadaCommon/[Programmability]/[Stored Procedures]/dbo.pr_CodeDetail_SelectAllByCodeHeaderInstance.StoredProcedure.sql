USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CodeDetail_SelectAllByCodeHeaderInstance]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CodeDetail'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CodeDetail_SelectAllByCodeHeaderInstance]
	@iCodeHeaderInstance	int
AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[CodeHeaderInstance],
	[Description],
	[Gross],
	[ADPCode]
FROM [dbo].[CodeDetail]
WHERE
	[CodeHeaderInstance] = @iCodeHeaderInstance
ORDER BY 
	[Instance] ASC
GO
