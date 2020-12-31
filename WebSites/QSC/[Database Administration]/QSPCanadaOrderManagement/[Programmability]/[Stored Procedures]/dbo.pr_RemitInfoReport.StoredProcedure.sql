USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitInfoReport]    Script Date: 06/07/2017 09:20:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jeff Miles>
-- Create date: <10/28/2008>
-- Description:	<For Remit Info Report>
-- =============================================
CREATE PROCEDURE [dbo].[pr_RemitInfoReport]

	@RemitBatchID	int

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT		codrh.TitleCode,
			codrh.RemitCode,
			codrh.MagazineTitle,
			COUNT(codrh.TitleCode) AS TotalSubs,
			ROUND(SUM(codrh.BasePrice), 2) AS GrossSale,
			SUM(ISNULL(cod.Tax, 0)) AS FederalTax,
			SUM(ISNULL(cod.Tax2, 0)) AS ProvincialTax,
			ROUND(SUM((ISNULL(pd.BasePriceSansPostage, 0) * ISNULL(pd.BaseRemitRate, 0)) + (ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0))), 6) AS NetRemitAmount,
			SUM(ISNULL(codrh.Tax, 0)) AS FederalTaxRemitAmount,
			SUM(ISNULL(codrh.Tax2, 0)) AS ProvincialTaxRemitAmount,
			CASE codrh.CurrencyID WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' END AS Currency,
			ROUND(SUM(ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0)), 2) AS PostageAmount
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
				AND	cod.DelFlag = 0
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.[Date] = coh.OrderBatchDate
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
WHERE		batch.StatusInstance <> 40005
AND			codrh.Status IN (42000, 42001)
AND			rb.RunID = @RemitBatchID
GROUP BY	codrh.RemitCode,
			codrh.TitleCode,
			codrh.MagazineTitle,
			codrh.CurrencyID
ORDER BY	CASE codrh.currencyid
				WHEN 801 THEN 'CAD'
				WHEN 802 THEN 'USD'
			END,
			codrh.TitleCode
END
GO
