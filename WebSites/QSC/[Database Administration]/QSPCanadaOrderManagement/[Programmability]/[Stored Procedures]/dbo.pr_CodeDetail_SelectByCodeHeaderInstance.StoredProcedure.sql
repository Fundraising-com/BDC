USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CodeDetail_SelectByCodeHeaderInstance]    Script Date: 06/07/2017 09:19:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CodeDetail_SelectByCodeHeaderInstance]
	@iCodeHeaderInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Instance],
	[CodeHeaderInstance],
	[Description],
	[Gross],
	[ADPCode]
FROM [dbo].[CodeDetail]
WHERE
	[CodeHeaderInstance] = @iCodeHeaderInstance
GO
