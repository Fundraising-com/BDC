USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[Commission_FALL_2012_InvoicedSales_vw]    Script Date: 06/07/2017 09:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Commission_FALL_2012_InvoicedSales_vw] AS 
SELECT i.ID, i.Name, i.CampaignID, i.FMID, SUM(nsv.PaymentAmount) AS InvoicedSales
FROM Commission_FALL_2012_NetCommisionableSales_vw nsv
join  Commission_FALL_2012_InvoicesByCampaign_vw i ON i.invoice_id = nsv.invoice_id
WHERE OrderQualifierID != 39009 -- Exclude Internet
GROUP BY i.ID, i.Name, i.CampaignID, i.FMID
GO
