USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProblemCode_SelectOne]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ProblemCode_SelectOne]
	@iInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Instance],
	[Description]
FROM [dbo].[ProblemCode]
WHERE
	[Instance] = @iInstance
GO
