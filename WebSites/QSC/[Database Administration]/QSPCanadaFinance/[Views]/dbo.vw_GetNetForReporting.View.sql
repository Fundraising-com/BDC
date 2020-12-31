USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[vw_GetNetForReporting]    Script Date: 06/07/2017 09:16:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetNetForReporting]

AS

SELECT		b.CampaignID,
			b.OrderQualifierID,
			invs.section_type_id,
			ge.ACCOUNTING_YEAR,
			ge.ACCOUNTING_PERIOD,
			ge.GL_ENTRY_DATE Invoice_Date,
			invs.ProgramType,
			SUM((ISNULL(invs.Total_Tax_Excluded, 0) - ISNULL(invs.Group_Profit_Amount, 0) - ISNULL(invs.ThirdParty_Profit_Amount, 0) - ISNULL(invs.US_Postage_Amount, 0)) * ISNULL(gcpmt.GiftCardPercentage, 1.0) * CASE invs.ProgramType WHEN 30332 THEN 0.5 ELSE 1 END) NetSale,
			SUM(ISNULL(invs.Item_Count, 0) * CASE invs.ProgramType WHEN 30332 THEN 0.5 ELSE 1 END) Units,
			1 IsInvoiced
FROM		qspcanadaordermanagement.dbo.batch b
JOIN		qspcanadafinance.dbo.invoice i on i.order_id = orderid
JOIN		qspcanadafinance.dbo.invoice_section invs on i.invoice_id = invs.invoice_id
JOIN		qspcanadafinance.dbo.gl_entry ge on ge.invoice_id = i.invoice_id
--JOIN		QSPCanadaFinance.dbo.Campaign_Program_Multiplier_vw VW ON VW.CampaignId = b.CampaignID
LEFT JOIN	(SELECT		pmt.Order_ID, SUM(CASE pmt.Payment_Method_ID WHEN 50007 THEN 0 ELSE pmt.Payment_Amount END) / SUM(pmt.Payment_Amount) GiftCardPercentage --Exclude Gift Card Portion of Gift Card Redemption Orders
			FROM		QSPCanadaFinance..Payment pmt
			WHERE		pmt.PAYMENT_AMOUNT > 0.00
			GROUP BY	pmt.Order_ID) gcpmt
			ON	gcpmt.ORDER_ID = b.OrderID
GROUP BY	b.CampaignID,
			b.OrderQualifierID,
			invs.SECTION_TYPE_ID,
			ge.ACCOUNTING_YEAR,
			ge.ACCOUNTING_PERIOD,
			ge.GL_ENTRY_DATE,
			invs.ProgramType
			--, vw.ProgramMultiplier

UNION ALL

SELECT		b.CampaignID,
			b.OrderQualifierID,
			ps.Type Section_Type_ID,
			NULL Account_Year,
			NULL Accounting_Period,
			NULL invoice_date,
			pm.SubType ProgramType,
			SUM(cod.Net * ISNULL(gcpmt.GiftCardPercentage, 1.0) * (CASE pm.SubType WHEN 30332 THEN 0.5 ELSE 1 END)) NetSale,
			SUM((CASE WHEN ps.Type = 2 AND cod.ProductCode NOT LIKE 'DG%' AND cod.ProductCode NOT IN ('D130','D131','D132') THEN 1 ELSE cod.Quantity END) * (CASE pm.SubType WHEN 30332 THEN 0.5 ELSE 1 END)) Units,
			0 IsInvoiced
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaProduct..Pricing_Details pd ON pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..ProgramSection ps ON ps.ID = pd.ProgramSectionID
JOIN		QSPCanadaProduct..Program_Master pm ON pm.program_ID = ps.Program_ID
--JOIN		dbo.UDF_GetBillableOrdersFromBatch() o ON o.Instance = cod.CustomerOrderHeaderInstance AND o.TransId = cod.TransID --This returns only orders that will eventually be invoiced
LEFT JOIN	(SELECT		pmt.Order_ID, SUM(CASE pmt.Payment_Method_ID WHEN 50007 THEN 0 ELSE pmt.Payment_Amount END) / SUM(pmt.Payment_Amount) GiftCardPercentage --Exclude Gift Card Portion of Gift Card Redemption Orders
			FROM		QSPCanadaFinance..Payment pmt
			WHERE		pmt.PAYMENT_AMOUNT > 0.00
			GROUP BY	pmt.Order_ID) gcpmt
			ON	gcpmt.ORDER_ID = b.OrderID
WHERE		ISNULL(b.IsInvoiced, 0) = 0
AND			ps.Type IN (1,2,9,10,11,13,14,15,16)
AND			b.OrderQualifierID NOT IN (39007,39012,39011,39008,39018,39019)
AND			cod.StatusInstance NOT IN (500)
AND			b.StatusInstance IN (40010,40012,40013,40014)
AND			cod.DelFlag = 0
AND			cod.ProductCode <> 'NNNN'
AND			ISNULL(cod.PricingDetailsID, 0) <> 0
AND			b.Date >= DATEADD(yy, -1, GETDATE())
AND			cod.StatusInstance NOT IN (506)
AND			coh.PaymentMethodInstance NOT IN (50005)
GROUP BY	b.CampaignID,
			b.OrderQualifierID,
			ps.Type,
			pm.SubType

UNION ALL

SELECT		adj.Campaign_ID CampaignID,
			NULL,
			NULL,
			ge.ACCOUNTING_YEAR,
			ge.ACCOUNTING_PERIOD,
			adj.Adjustment_Effective_Date Invoice_Date,
			NULL,
			SUM(adj.Adjustment_Amount) NetSale,
			0 Units,
			1 IsInvoiced
FROM		Adjustment adj
JOIN		Adjustment_Type adjT ON adjT.Adjustment_Type_ID = adj.Adjustment_Type_ID
JOIN		QSPCanadaFinance..GL_Entry ge on ge.Adjustment_ID = adj.Adjustment_id
WHERE		adjT.ExcludeFromInvoicing = 1
GROUP BY	adj.Campaign_ID,
			ge.ACCOUNTING_YEAR,
			ge.ACCOUNTING_PERIOD,
			adj.Adjustment_Effective_Date

GO
