USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_SelectOneByYearSeason]    Script Date: 06/07/2017 09:33:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Season_SelectOneByYearSeason]

	@iID		int = 0
	,@iFiscalYear	int
	,@sSeason	char(1)

AS

SELECT		COUNT(*)
FROM		[Season]
WHERE		[FiscalYear] 	= @iFiscalYear
AND		[Season] 	= @sSeason
AND 		[ID] 		<> @iID
GO
