USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CodeDetail_SelectOne]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CodeDetail'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CodeDetail_SelectOne]
	@iInstance	int
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
	[Instance] = @iInstance
ORDER BY 
	[Instance] ASC
GO
