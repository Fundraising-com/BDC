USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_ProductSeason]    Script Date: 06/07/2017 09:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[pr_RemitTest_ProductSeason]

@iRunID		int

AS

/*********************** get current season ******************************/
DECLARE 	@ProductSeason 	char(1)
DECLARE		@ProductYear	int

EXEC		pr_RemitTest_GetCurrentSeason @ProductSeason output, @ProductYear output
/*************************************************************************/

IF EXISTS
(
	SELECT		cd.Description,
			p.Product_Sort_Name,
			pd.*,
			cod.CreationDate,
			codrh.Status,
			codrh.Customerorderheaderinstance,
			p.Pub_Nbr,
			pub.Pub_Name,
			codrh.MagazineTitle,
			codrh.TitleCode, 			
			codrh.CurrencyID AS CurrencyID,
			pd.Pricing_Year,
			pd.Pricing_Season
	FROM		CustomerOrderDetailRemitHistory codrh,
			RemitBatch rb,
			CustomerOrderHeader coh,
			Batch b,
			QSPCanadaCommon..CodeDetail cd,
			CustomerOrderDetail cod,
			CustomerRemitHistory crh,
			QSPCanadaProduct..Pricing_Details pd,
			QSPCanadaProduct..Product p,
			QSPCanadaProduct..Fulfillment_House fh,
			QSPCanadaProduct..Publishers pub
	WHERE		codrh.RemitBatchID = rb.id
	AND		cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
	AND		cod.TransID = codrh.TransID
	AND		coh.Instance = cod.CustomerOrderHeaderInstance
	AND		b.ID = coh.OrderBatchID
	AND		b.Date = coh.OrderBatchDate
	AND		cd.Instance = b.OrderQualifierID
	AND		codrh.CustomerRemitHistoryInstance = crh.Instance
	AND		cod.PricingDetailsID = pd.MagPrice_Instance
	AND		pd.Product_Instance = p.Product_Instance
	AND		p.Fulfill_House_Nbr = fh.Ful_Nbr   
	AND		p.Pub_Nbr = pub.Pub_Nbr
	AND		(pd.Pricing_Year <> @ProductYear
	OR		pd.Pricing_Season <> @ProductSeason)
	AND		codrh.Status IN (42000, 42001)
	AND		rb.runid = @iRunID
)
	SELECT 1
ELSE
	SELECT 0
GO
