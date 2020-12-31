USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceAdjustmentTotals]    Script Date: 06/07/2017 09:17:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInvoiceAdjustmentTotals]
	@InvoiceID 	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/7/2004 
--   Get Invoice Adjustment Totals For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT CD.Description as AdjustmentType,  SUM(Adjustment_Amount) as AdjustmentAmount
FROM INVOICE I 
	INNER JOIN Adjustment A on A.Order_ID = I.Order_ID AND A.Account_ID = I.Account_ID  
	INNER JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = A.Adjustment_Type_ID
	INNER JOIN Adjustment_Type adjT on adjT.Adjustment_Type_ID = A.Adjustment_Type_ID
WHERE	Invoice_ID = @InvoiceID
AND		adjT.ExcludeFromInvoicing = 0
GROUP BY CD.Description 

SET NOCOUNT OFF
GO
