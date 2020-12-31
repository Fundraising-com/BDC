USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_ProductSeason_Fix]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      procedure [dbo].[pr_RemitTest_ProductSeason_Fix]

@iRunID		int

AS

/*********************** get current season ******************************/
DECLARE 	@zProductSeason 	char(1)
DECLARE		@iProductYear		int

EXEC		pr_RemitTest_GetCurrentSeason @zProductSeason output, @iProductYear output
/*************************************************************************/

DECLARE	@iMainENProgramSectionID		int,
		--@iMainFRProgramSectionID		int,
		@iStaffProgramSectionID			int

-- Get catalog sections
SELECT	@iMainENProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps
JOIN	QSPCanadaProduct..Program_Master pm on pm.Program_ID = ps.Program_ID
JOIN	QSPCanadaCommon..Season s on s.ID = pm.Season
WHERE	pm.Code LIKE 'MAG%'
AND		pm.Lang LIKE 'EN%'
AND		s.FiscalYear = @iProductYear
--AND		s.Season = @zProductSeason

/*SELECT	@iMainFRProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps
JOIN	QSPCanadaProduct..Program_Master pm on pm.Program_ID = ps.Program_ID
JOIN	QSPCanadaCommon..Season s on s.ID = pm.Season
WHERE	pm.Code LIKE 'MAG%'
AND		pm.Lang = 'FR'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason*/

SELECT	@iStaffProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaCommon..Season s
WHERE	pm.Program_ID = ps.Program_ID
AND		s.ID = pm.Season
AND		pm.Program_Type like 'Staff%'
AND		s.FiscalYear = @iProductYear
--AND		s.Season = @zProductSeason

--get all offers
CREATE TABLE	[#Offers](
				[RemitCode] [varchar](20) NULL,
				[TaxRegionID] [int] NOT NULL,
				[Low_Issue] [int] NULL,
				[High_Issue] [int] NULL,
				[Median] [int] NULL,
				[IsStaffOffer] [bit] NOT NULL)

INSERT INTO		[#Offers](
				[RemitCode],
				[TaxRegionID],
				[Low_Issue],
				[High_Issue],
				[Median],
				[IsStaffOffer])
SELECT			p.RemitCode,
				pd.TaxRegionID,
				MIN(pd.Nbr_Of_Issues) AS Low_Issue, 
				MAX(pd.Nbr_Of_Issues) AS High_Issue,
				AVG(pd.Nbr_Of_Issues) AS Median,
				CASE WHEN pd.ProgramSectionID in (@iStaffProgramSectionID) THEN 1 ELSE 0 END
FROM			QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p,
				QSPCanadaProduct..ProgramSection ps,
				QSPCanadaProduct..Program_Master pm
WHERE			p.Product_Instance = pd.Product_Instance
AND				ps.ID = pd.ProgramSectionID
AND				pm.Program_ID = ps.Program_ID
AND				pd.Pricing_Year = @iProductYear
AND				pd.Pricing_Season = @zProductSeason
AND				pd.ProgramSectionID IN (@iMainENProgramSectionID, @iStaffProgramSectionID)
AND				p.Type = 46001
AND				p.Status NOT IN (30601)
GROUP BY		p.RemitCode,
				pd.TaxRegionID,
				CASE WHEN pd.ProgramSectionID in (@iStaffProgramSectionID) THEN 1 ELSE 0 END


--Get Subs to Update

CREATE TABLE	[#SubsToUpdate](
                [CustomerOrderHeaderInstance] [int] NOT NULL,
                [TransID] [int] NOT NULL,
                [Lang] [varchar](10) NULL,
                [RemitCode] [varchar](20) NULL,
                [IsStaffSub] [bit] NULL,
                [Quantity] [int] NULL,
                [OriginalIssues] [int] NOT NULL,
                [TaxRegionID] [int] NOT NULL,
                [PreviousPricingDetailsID] [int] NULL,
                [NewPricingDetailsID] [int] NOT NULL) 

INSERT INTO		[#SubsToUpdate]
			   ([CustomerOrderHeaderInstance]
			   ,[TransID]
			   ,[Lang]
			   ,[RemitCode]
			   ,[IsStaffSub]
			   ,[Quantity]
			   ,[OriginalIssues]
			   ,[TaxRegionID]
			   ,[PreviousPricingDetailsID]
			   ,[NewPricingDetailsID])
SELECT			cod.CustomerOrderHeaderInstance,
				cod.TransID,
				codrh.Lang,
				p.RemitCode,
				o.IsStaffOffer,
				CASE
					WHEN pd.Nbr_Of_Issues < o.Median THEN o.Low_Issue 
					ELSE o.High_Issue
				END AS Quantity,
				pd.Nbr_Of_Issues AS OriginalIssues,
				pd.TaxRegionID,
				cod.PricingDetailsID AS PreviousPricingDetailsID,
				0 AS NewPricingDetailsID
FROM			CustomerOrderDetailRemitHistory codrh
JOIN			RemitBatch rb
					ON	rb.ID = codrh.RemitBatchID
					AND	rb.RunID = @iRunID
JOIN			CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
					AND	cod.TransID = codrh.TransID 
JOIN			QSPCanadaProduct..Pricing_Details pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN			QSPCanadaProduct..Product p
					ON	p.Product_Instance = pd.Product_Instance
					AND	(p.Product_Year <> @iProductYear
					OR	p.Product_Season <> @zProductSeason)
JOIN			QSPCanadaProduct..ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
JOIN			QSPCanadaProduct..Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
LEFT JOIN		#Offers o
					ON	o.RemitCode = p.RemitCode
					AND	o.TaxRegionID = pd.TaxRegionID
					AND	o.IsStaffOffer = CASE WHEN p.Product_Code LIKE 'S%' THEN 1 ELSE 0 END
WHERE 			codrh.Status IN (42000, 42001)
AND				p.Status NOT IN (30601)
ORDER BY	 	codrh.CustomerOrderHeaderInstance,
				codrh.TransID


-- Update with the right Pricing_Details record

UPDATE 	stu
SET 	stu.NewPricingDetailsID = pd.MagPrice_Instance
FROM  	#SubsToUpdate stu,
		QSPCanadaProduct..Product p,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..ProgramSection ps, 
		QSPCanadaProduct..Program_Master pm
WHERE 	p.RemitCode = stu.RemitCode
AND 	pd.Product_Instance = p.Product_Instance
AND		ps.ID = pd.ProgramSectionID
AND		pm.Program_ID = ps.Program_ID
AND 	pd.TaxRegionID = stu.TaxRegionID
AND		pd.Nbr_Of_Issues = stu.Quantity
AND		(pm.Lang = stu.Lang OR pm.Lang = 'EN/FR')
AND		stu.IsStaffSub = CASE WHEN p.Product_Code LIKE 'S%' THEN 1 ELSE 0 END
AND		pd.Pricing_Year = @iProductYear
AND		pd.Pricing_Season = @zProductSeason
AND		p.Type = 46001
AND		p.Status NOT IN (30601)
AND		ps.ID IN (@iMainENProgramSectionID, @iStaffProgramSectionID)

UPDATE	codrh
SET 	codrh.RemitRate = pd.Remit_Rate,
		codrh.BasePrice = pd.Basic_Price_Yr,
	 	codrh.CurrencyID = p.Currency,
	 	codrh.Lang = p.Lang,
	 	codrh.PremiumIndicator = pd.prdPremiumInd,
		codrh.PremiumCode = ISNULL(pd.prdPremiumCode, ''),
	 	codrh.PremiumDescription = ISNULL(pd.prdPremiumCopy, ''),
	 	codrh.ABCCode = pd.ABCCode,
	 	codrh.CatalogPrice = pd.QSP_Price,
	 	codrh.MagazineTitle = p.Product_Sort_Name,
	 	codrh.DefaultGrossValue	= pd.DefaultGrossValue,
	 	codrh.EffortKey = ISNULL(pd.Effort_Key, ''),
	 	codrh.NumberOfIssues = pd.Nbr_Of_Issues
FROM 	CustomerOrderDetailRemitHistory codrh
JOIN	#SubsToUpdate stu
			ON	stu.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
			AND	stu.TransID = codrh.Transid
			AND	stu.NewPricingDetailsID <> 0
JOIN	QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = stu.NewPricingDetailsID
JOIN	QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
JOIN	RemitBatch rb
			ON	rb.ID = codrh.RemitBatchID
			AND	rb.RunID = @iRunID

UPDATE 	cod
SET		cod.PricingDetailsID = stu.NewPricingDetailsID
FROM	CustomerOrderDetail cod
JOIN	#SubsToUpdate stu
			ON	stu.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
			AND	stu.TransID = cod.TransID
			AND	stu.NewPricingDetailsID <> 0

--If no current offer found, then set sub to Magazine Inactive
UPDATE		codrh
SET			codrh.Status = 42010 -- Magazine Inactive
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		#SubsToUpdate stu
				ON	stu.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	stu.TransID = codrh.TransID
				AND	stu.NewPricingDetailsID = 0
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.RunID = @iRunID


DROP TABLE #SubsToUpdate
DROP TABLE #Offers
GO
