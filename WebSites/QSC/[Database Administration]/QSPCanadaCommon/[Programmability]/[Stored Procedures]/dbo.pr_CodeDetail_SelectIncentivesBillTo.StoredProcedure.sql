USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CodeDetail_SelectIncentivesBillTo]    Script Date: 06/07/2017 09:33:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CodeDetail_SelectIncentivesBillTo]
	
	@IsCampaign2014OrLater	BIT
	
AS

SET NOCOUNT ON
-- SELECT all rows from the table.

IF (@IsCampaign2014OrLater = CONVERT(BIT, 1))
BEGIN
	SELECT		[Instance],
				[CodeHeaderInstance],
				[Description],
				[Gross],
				[ADPCode]
	FROM		[dbo].[CodeDetail]
	WHERE		[CodeHeaderInstance] = 51000
	AND			Instance IN (51002, 51003)
	ORDER BY	[Instance] ASC
END
ELSE
BEGIN
	SELECT		[Instance],
				[CodeHeaderInstance],
				[Description],
				[Gross],
				[ADPCode]
	FROM		[dbo].[CodeDetail]
	WHERE		[CodeHeaderInstance] = 51000
	ORDER BY	[Instance] ASC
END
GO
