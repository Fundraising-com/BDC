USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[Commission_FALL_2012_InvoicesByCampaign_vw]    Script Date: 06/07/2017 09:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Commission_FALL_2012_InvoicesByCampaign_vw] AS 
SELECT a.ID, a.Name, i.invoice_id, b.OrderQualifierID, MIN(c.id) AS CampaignID, MIN(c.FMID) AS FMID
FROM Invoice i
JOIN Qspcanadacommon..caccount a  WITH (NOLOCK) ON a.ID = i.Account_ID
JOIN QSPCanadaCommon.dbo.Campaign c  WITH (NOLOCK) ON c.BillToAccountID = a.ID
JOIN QSPCanadaOrderManagement.dbo.Batch b  WITH (NOLOCK) ON i.ORDER_ID = b.OrderID
JOIN QSPCanadaCommon.dbo.CodeDetail cd  WITH (NOLOCK) ON cd.Instance = b.OrderQualifierID
WHERE (c.StartDate >= '11-JUN-2011')-- AND c.EndDate < '01-JAN-2012')
  AND c.status = 37002 -- Campaign is Approved
GROUP BY a.ID, a.Name, i.invoice_id, b.OrderQualifierID
GO
