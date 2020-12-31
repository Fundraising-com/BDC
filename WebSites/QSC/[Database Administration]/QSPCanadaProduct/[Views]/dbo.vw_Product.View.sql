USE [QSPCanadaProduct]
GO
/****** Object:  View [dbo].[vw_Product]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Product] AS
SELECT		p.Product_Instance,
			p.Product_Code,
			p.Product_Year,
			p.Product_Season,
			p.Product_Sort_Name,
			p.Status AS Product_Status,
			p.Lang AS Product_Lang,
			p.ProductLine,
			p.VendorNumber,
			p.VendorSiteName,
			p.PayGroupLookUpCode,
			p.Currency,
			p.Type,
			p.Nbr_Of_Issues_Per_Year,
			p.RemitCode,

			pub.Pub_Nbr,
			pub.Pub_Status,
			pub.Pub_Name,
			
			fh.Ful_Nbr,
			fh.InterfaceMediaID,
			fh.InterfaceLayoutID,
			fh.QSPAgencyCode,
			fh.TransmissionMethodID,

			pd.Pricing_Year,
			pd.Pricing_Season,
			pd.Status AS PricingDetails_Status,
			pd.TaxRegionID,

			ps.ID AS ProgramSection_ID,

			pm.Program_ID AS ProgramMaster_ID,
			pm.Status AS Program_Master_Status,
			pm.Lang AS Program_Master_Lang,
			pm.DateCreated AS Program_Master_DateCreated

FROM		Product p
LEFT JOIN	Publishers pub
				ON	pub.Pub_Nbr = p.Pub_Nbr
LEFT JOIN	Fulfillment_House fh
				ON	fh.Ful_Nbr = p.Fulfill_House_Nbr
LEFT JOIN	Pricing_Details pd
				ON	pd.Product_instance = p.Product_Instance
LEFT JOIN	ProgramSection ps
				ON	ps.ID = pd.ProgramSectionID
LEFT JOIN	Program_Master pm
				ON	pm.Program_ID = ps.Program_ID
GO
