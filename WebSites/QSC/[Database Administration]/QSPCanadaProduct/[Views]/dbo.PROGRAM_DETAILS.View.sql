USE [QSPCanadaProduct]
GO
/****** Object:  View [dbo].[PROGRAM_DETAILS]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PROGRAM_DETAILS]
AS
SELECT	pd.MagProgram_Instance,
		pd.Offer_Code AS Offer_ID,
		p.Product_Year AS Program_Year,
		p.Product_Season AS Program_Season,
		p.Product_Code, 
		pd.Program_ID,
		pd.Program_Type,
		aps.Description AS AD_Size,
		pd.prdPremiumCode AS Premium_Code,
		pd.prdPremiumCopy AS Premium_Copy, 
		'' AS Tape_Position,
		'' AS Descr_Copy_1,
		pd.Logged_By,
		pd.Log_Dt,
		pd.ProgramSectionID,
		CASE COALESCE (pd.AdInCatalog, 0) WHEN 0 THEN 'N' WHEN 1 THEN 'Y' END AS IsAdIncluded,
		CONVERT(numeric(10, 2), pd.AdCost) AS AdCost,
		p.Currency AS CurrencyID,
		pd.prdPremiumInd AS PremiumInd,
		pd.QSPPremiumID, 
		pd.ListingLevelID AS LevelID,
		pd.ListingCopyText AS AdCopyText,
		pd.TaxRegionID
FROM		Pricing_Details pd
LEFT OUTER JOIN
		AdPageSize aps
			ON aps.ID = pd.AdPageSizeID,
		Product p
WHERE	p.Product_Instance = pd.Product_Instance
GO
