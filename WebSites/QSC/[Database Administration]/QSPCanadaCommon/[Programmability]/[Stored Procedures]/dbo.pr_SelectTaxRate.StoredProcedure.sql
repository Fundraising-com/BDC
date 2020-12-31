USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectTaxRate]    Script Date: 06/07/2017 09:33:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectTaxRate]

	@iTaxRegionID	int

AS

	SELECT	top 1
			ConsolidatedRate / 100
	FROM		QSPCanadaCommon..TaxRegion
	WHERE	ID = @iTaxRegionID
	AND		EffectiveDate =
		(SELECT	max(EffectiveDate)
		FROM 		QSPCanadaCommon..TaxRegion
		WHERE	ID = @iTaxRegionID
		AND		EffectiveDate <= getdate())
GO
