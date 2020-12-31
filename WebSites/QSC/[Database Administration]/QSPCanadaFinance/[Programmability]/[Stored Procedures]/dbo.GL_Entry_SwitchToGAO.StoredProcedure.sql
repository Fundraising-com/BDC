USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Entry_SwitchToGAO]    Script Date: 06/07/2017 09:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Entry_SwitchToGAO]

	@AccountingYear INT,
	@AccountingPeriod	INT

AS

UPDATE	e
SET		BusinessUnitID = CASE WHEN e.BusinessUnitID = 1 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(inv.Order_ID), 0) = 0 THEN 3
							  WHEN e.BusinessUnitID = 2 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(inv.Order_ID), 0) = 0 THEN 4
							  ELSE e.BusinessUnitID
						 END
FROM	GL_Entry e
JOIN	Invoice inv
			ON	inv.Invoice_ID = e.Invoice_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(inv.Order_ID), 0) = 0
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE WHEN e.BusinessUnitID = 1 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(pmt.Order_ID), 0) = 0 THEN 3
							  WHEN e.BusinessUnitID = 2 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(pmt.Order_ID), 0) = 0 THEN 4
							  ELSE e.BusinessUnitID
						 END
FROM	GL_Entry e
JOIN	Payment pmt
			ON	pmt.Payment_ID = e.Payment_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(pmt.Order_ID), 0) = 0
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE WHEN e.BusinessUnitID = 1 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(adj.Order_ID), 0) = 0 THEN 3
							  WHEN e.BusinessUnitID = 2 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(adj.Order_ID), 0) = 0 THEN 4
							  ELSE e.BusinessUnitID
						 END
FROM	GL_Entry e
JOIN	Adjustment adj
			ON	adj.Adjustment_ID = e.Adjustment_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(adj.Order_ID), 0) = 0
AND		adj.Order_ID > 0
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE e.BusinessUnitID WHEN 1 THEN 3 ELSE 4 END
FROM	GL_Entry e
JOIN	Adjustment adj
			ON	adj.Adjustment_ID = e.Adjustment_ID
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = adj.Campaign_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
AND		camp.StartDate >= '2012-01-01'
AND		adj.Order_ID IS NULL
AND		adj.Adjustment_Type_ID NOT IN (49024, 49046)
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE e.BusinessUnitID WHEN 1 THEN 3 ELSE 4 END
FROM	GL_Entry e
JOIN	Adjustment adj
			ON	adj.Adjustment_ID = e.Adjustment_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
AND		adj.Date_Created > '2012-02-21'
AND		adj.Order_ID IS NULL
AND		adj.Adjustment_Type_ID IN (49024)
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE e.BusinessUnitID WHEN 1 THEN 3 ELSE 4 END
FROM	GL_Entry e
JOIN	Adjustment adj
			ON	adj.Adjustment_ID = e.Adjustment_ID
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = adj.Campaign_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
AND		camp.StartDate >= '2012-01-01'
AND		adj.Order_ID IS NULL
AND		adj.Adjustment_Type_ID IN (49046)
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE WHEN e.BusinessUnitID = 1 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 0 THEN 3
							  WHEN e.BusinessUnitID = 2 AND ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 0 THEN 4
							  ELSE e.BusinessUnitID
						 END
FROM	GL_Entry e
JOIN	Refund ref
			ON	ref.Refund_ID = e.Refund_ID
JOIN	QSPCanadaOrderManagement..CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
			AND	cod.TransID = ref.TransID
JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	QSPCanadaOrderManagement..Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 0
AND		e.BusinessUnitID IN (1, 2)

UPDATE	e
SET		BusinessUnitID = CASE e.BusinessUnitID WHEN 1 THEN 3 ELSE 4 END
FROM	GL_Entry e
JOIN	AP_Cheque_Remit apcr
			ON	apcr.AP_Cheque_Remit_ID = e.AP_Cheque_Remit_ID
WHERE	e.Accounting_Period = @AccountingPeriod
AND		e.Accounting_Year = @AccountingYear
AND		apcr.RemitBatchID > 1382
AND		e.BusinessUnitID IN (1, 2)
GO
