USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[Commission_FALL_2012_NetBalance_vw]    Script Date: 06/07/2017 09:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  VIEW [dbo].[Commission_FALL_2012_NetBalance_vw] AS 
SELECT 	ID, Name, CampaignID, FMID, SUM(Payments) AS Balance
FROM 
(SELECT i.ID, i.Name, i.CampaignID, i.FMID, SUM(nsv.PaymentAmount) AS Payments
FROM Commission_FALL_2012_NetCommisionableSales_vw nsv
JOIN Commission_FALL_2012_InvoicesByCampaign_vw i ON i.invoice_id = nsv.invoice_id
GROUP BY i.ID, i.Name, i.CampaignID, i.FMID
UNION
SELECT p.ID, p.Name, p.CampaignID, p.FMID, SUM(nsv.PaymentAmount) AS Payments
FROM Commission_FALL_2012_NetCommisionableSales_vw nsv
JOIN Commission_FALL_2012_PaymentsByCampaign_vw p on p.payment_id = nsv.payment_id
GROUP BY p.ID, p.Name, p.CampaignID, p.FMID) data
GROUP BY ID, Name, CampaignID, FMID
GO
