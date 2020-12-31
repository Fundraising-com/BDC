USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetQSPExclusiveTotalsForInvoice]    Script Date: 06/07/2017 09:17:20 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetQSPExclusiveTotalsForInvoice] @InvoiceID  INT
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MS 4/25/2006
--   Get Totals For QSP Exclusive titles for Invoice
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT Isnull( CONVERT(NUMERIC(10,2),(SUM(CASE c.IsStaffOrder
	WHEN 1 THEN (Price/2) 
	ELSE Price 
	END ))) ,0)QSPExclusiveTotal
FROM 	QSPCanadaOrderManagement..Batch b,
     	QSPCanadaOrderManagement..CustomerorderHeader h,
     	QSPCanadaOrderManagement..CustomeroRderDetail d,
	QSPCanadaCommon..Campaign c,
     	QSPcanadaproduct..pricing_Details pd,
     	QSPcanadaproduct..Product p,
     	QSPCanadaFinance..Invoice i	
WHERE b.id=OrderBatchId
AND	b.date=orderBatchdate
AND	b.CampaignId=c.id
AND	h.instance=d.customerorderheaderinstance
AND 	pd.magprice_Instance=d.pricingDetailsid
AND 	p.product_Instance=pd.product_instance
AND 	d.statusinstance  IN (507,508,512,513,514)
AND 	d.producttype 	 IN (46001,46006)
AND 	d.Delflag =0
AND 	p.IsQSPExclusive=1
AND 	I.order_id =b.OrderId
AND 	i.Invoice_id=@InvoiceID
GO
