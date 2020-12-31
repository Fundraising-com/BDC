USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_Vendor]    Script Date: 06/07/2017 09:20:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_RemitTest_Vendor]

@iRunID		int = 0

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

CREATE TABLE	[#VendorInfo](
				[remitcode] [varchar](20) NULL,
                [TermsName] [varchar](50) NULL,
                [VendorNumber] [varchar](30) NULL,
                [VendorSiteName] [varchar](15) NULL,
                [Fulfill_House_Nbr] [varchar](3) NULL,
                [Pub_Nbr] int NULL,
				[Currency] int)

INSERT INTO	[#VendorInfo](
			[remitcode],
			[TermsName],
			[VendorNumber],
			[VendorSiteName],
			[Fulfill_House_Nbr],
			[Pub_Nbr],
			[Currency])
SELECT DISTINCT
			p.RemitCode,
			p.TermsName,
			p.VendorNumber,
			p.VendorSiteName,
			p.Fulfill_House_Nbr,
			p.Pub_Nbr,
			p.Currency
FROM		QSPCanadaProduct..Product p
WHERE		p.product_year = @ProductYear
AND			p.product_season = @ProductSeason
AND			p.Status NOT IN (30601, 30603) --Unremittable mags are not included in AP
AND			p.Type = 46001 --Magazine
ORDER BY	remitcode

IF EXISTS
(
	SELECT		RemitCode
	FROM		#VendorInfo
	GROUP BY	RemitCode
	HAVING COUNT(*) > 1
)
	SELECT 1
ELSE
	SELECT 0

DROP TABLE #VendorInfo
GO
