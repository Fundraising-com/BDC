SELECT	*
FROM	QSPCanadaProduct..Pricing_Details
WHERE	Pricing_Year = 2009
AND		Pricing_Season = 'F'
AND		Product_Code = '4288'

DECLARE @iRunID INT
DECLARE @OriginalRemitCode VARCHAR(20)
DECLARE @NewRemitCode VARCHAR(20)
DECLARE @OriginalPricingDetailsID INT

SET @iRunID = 1279
SET @OriginalRemitCode = '4288'
SET @NewRemitCode = '11UQ'


/*********************** get current season ******************************/
DECLARE 	@zProductSeason 	char(1)
DECLARE		@iProductYear		int

EXEC		pr_RemitTest_GetCurrentSeason @zProductSeason output, @iProductYear output
/*************************************************************************/

DECLARE	@iMainENProgramSectionID	INT,
		@iMainFRProgramSectionID	INT,
		@iStaffProgramSectionID		INT

-- Get catalog sections
SELECT	@iMainENProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaProduct..Pricing_Details pdFS,
		QSPCanadaProduct..ProgramSection psFS,
		QSPCanadaProduct..Program_Master pmFS,
		QSPCanadaProduct..ProgFSSectionMap pfssmFS
WHERE	pm.Program_ID = ps.Program_ID
AND		pdFS.FSContent_Catalog_Code = pm.Code
AND		psFS.ID = pdFS.ProgramSectionID
AND		pmFS.Program_ID = psFS.Program_ID
AND		pfssmFS.Catalog_Section_ID = psFS.ID
AND		psFS.Type = 3
AND		pfssmFS.Program_ID = 1
AND		pdFS.FSIsBrochure = 1
AND		pdFS.TaxRegionID = 1
AND		COALESCE(pdFS.FSProvinceCode, '') = ''
AND		pm.SubType <> 30305
AND		COALESCE(ps.Name, '') NOT LIKE 'Book%'
AND		pdFS.Language_Code = 'EN'
AND		pdFS.Product_Code LIKE '%GST%'
AND		pdFS.Pricing_Year = @iProductYear
AND		pdFS.Pricing_Season = @zProductSeason

SELECT	@iMainFRProgramSectionID =  ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaProduct..Pricing_Details pdFS,
		QSPCanadaProduct..ProgramSection psFS,
		QSPCanadaProduct..Program_Master pmFS,
		QSPCanadaProduct..ProgFSSectionMap pfssmFS
WHERE	pm.Program_ID = ps.Program_ID
AND		pdFS.FSContent_Catalog_Code = pm.Code
AND		psFS.ID = pdFS.ProgramSectionID
AND		pmFS.Program_ID = psFS.Program_ID
AND		pfssmFS.Catalog_Section_ID = psFS.ID
AND		psFS.Type = 3
AND		pfssmFS.Program_ID = 1
AND		pdFS.FSIsBrochure = 1
AND		pdFS.TaxRegionID = 1
AND		COALESCE(pdFS.FSProvinceCode, '') = ''
AND		pm.SubType <> 30305
AND		COALESCE(ps.Name, '') NOT LIKE 'Book%'
AND		pdFS.Language_Code = 'FR'
AND		pdFS.Product_Code LIKE '%GST%'
AND		pdFS.Pricing_Year = @iProductYear
AND		pdFS.Pricing_Season = @zProductSeason

SELECT	@iStaffProgramSectionID = ps.ID
FROM	QSPCanadaProduct..ProgramSection ps,
		QSPCanadaProduct..Program_Master pm,
		QSPCanadaCommon..Season s
WHERE	pm.Program_ID = ps.Program_ID
AND		s.ID = pm.Season
AND		pm.Program_Type like 'Staff%'
AND		s.FiscalYear = @iProductYear
AND		s.Season = @zProductSeason

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
AND				pd.ProgramSectionID IN (@iMainENProgramSectionID, @iMainFRProgramSectionID, @iStaffProgramSectionID)
AND				p.Type = 46001
GROUP BY		p.RemitCode,
				pd.TaxRegionID,
				CASE WHEN pd.ProgramSectionID in (@iStaffProgramSectionID) THEN 1 ELSE 0 END

CREATE TABLE	[#SubsToUpdate](
                [CustomerOrderHeaderInstance] [int] NOT NULL,
                [TransID] [int] NOT NULL,
                [Lang] [varchar](10) NULL,
                [OriginalRemitCode] [varchar](20) NULL,
                [NewRemitCode] [varchar](20) NULL,
                [IsStaffSub] [bit] NOT NULL,
                [Quantity] [int] NOT NULL,
                [OriginalIssues] [int] NOT NULL,
                [TaxRegionID] [int] NOT NULL,
                [PreviousPricingDetailsID] [int] NULL,
                [NewPricingDetailsID] [int] NOT NULL) 

INSERT INTO		[#SubsToUpdate]
				([CustomerOrderHeaderInstance]
			   ,[TransID]
			   ,[Lang]
			   ,[OriginalRemitCode]
			   ,[NewRemitCode]
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
				o.RemitCode,
				o.IsStaffOffer,
				CASE
					WHEN pd.Nbr_Of_Issues < o.Median THEN o.Low_Issue 
					ELSE o.High_Issue
				END AS Quantity,
				pd.Nbr_Of_Issues AS OriginalIssues,
				pd.TaxRegionID,
				cod.PricingDetailsID AS PreviousPricingDetailsID,
				0 AS NewPricingDetailsID
FROM			CustomerOrderDetailRemitHistory codrh, 
				RemitBatch rb,
				CustomerOrderDetail cod,
				QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p,
				QSPCanadaProduct..ProgramSection ps,
				QSPCanadaProduct..Program_Master pm,
				#Offers o
WHERE		 	rb.ID = codrh.RemitBatchID
AND				cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance 
AND 			cod.TransID = codrh.TransID 
AND				pd.MagPrice_Instance = cod.PricingDetailsID
AND				p.Product_Instance = pd.Product_Instance
AND				ps.ID = pd.ProgramSectionID
AND				pm.Program_ID = ps.Program_ID
AND				o.RemitCode = @NewRemitCode
AND				p.RemitCode = @OriginalRemitCode
AND 			o.TaxRegionID = pd.TaxRegionID
AND				o.IsStaffOffer = CASE WHEN p.Product_Code LIKE 'S%' THEN 1 ELSE 0 END
AND				(p.Product_Year <> @iProductYear
				OR	p.Product_Season <> @zProductSeason)
AND 			codrh.Status IN (42000, 42001, 42002, 42003, 42006, 42007) --12/04/07: JM: applies to Subs, Cancellations, and Chadds
AND				rb.RunID = @iRunID
ORDER BY 		codrh.CustomerOrderHeaderInstance,
				codrh.TransID

UPDATE 	stu
SET 	stu.NewPricingDetailsID = pd.MagPrice_Instance
FROM  	#SubsToUpdate stu,
		QSPCanadaProduct..Product p,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..ProgramSection ps, 
		QSPCanadaProduct..Program_Master pm
WHERE 	p.RemitCode = stu.NewRemitCode
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
AND		ps.ID IN (@iMainENProgramSectionID, @iMainFRProgramSectionID, @iStaffProgramSectionID)

SELECT	*
FROM	#SubsToUpdate

BEGIN TRAN t1

--If needed to requeue into next remit
EXEC pr_Remit_ReRemitSubsByPricingDetailsIDandRemitBatchID
	@PricingDetailsID = 291267,
	@RunIDFrom = 1279,
	@RunIDTo = 1279,
	@AlreadyRemittedToPublisher = 1,
	@ReRemitSubs = 1,
	@ReRemitCancels = 1,
	@ReRemitChadds = 1

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
	 	codrh.NumberOfIssues = pd.Nbr_Of_Issues,
		codrh.TitleCode = p.Product_Code,
		codrh.RemitCode = p.RemitCode
FROM 	CustomerOrderDetailRemitHistory codrh,
		#SubsToUpdate stu,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..Product p
WHERE	stu.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
AND		stu.TransID = codrh.Transid
AND		pd.MagPrice_Instance = stu.NewPricingDetailsID
AND		p.Product_Instance = pd.Product_Instance	
AND		stu.NewPricingDetailsID <> 0

UPDATE 	cod
SET		cod.PricingDetailsID = stu.NewPricingDetailsID,
		cod.ProductCode = p.Product_Code,
		cod.Quantity = pd.Nbr_Of_Issues,
		cod.Price = pd.Basic_Price_Yr,
		cod.CatalogPrice = pd.QSP_Price
FROM	#SubsToUpdate stu,
		CustomerOrderDetail cod,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..Product p
WHERE	stu.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
AND		stu.TransID = cod.TransID
AND		pd.MagPrice_Instance = stu.NewPricingDetailsID
AND		p.Product_Instance = pd.Product_Instance	
AND		stu.NewPricingDetailsID <> 0

COMMIT TRAN t1

DROP TABLE #SubsToUpdate
DROP TABLE #Offers