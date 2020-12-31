USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CodeDetail_SelectOne]    Script Date: 06/07/2017 09:19:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CodeDetail_SelectOne]
	@iInstance int
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
	[Instance] = @iInstance
GO
