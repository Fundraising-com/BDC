USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvoiceAdjustmentDetails]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllInvoiceAdjustmentDetails]

	@InvoiceID int,
	@OrderID	int = null

AS

SET NOCOUNT ON
IF (@InvoiceId = 0)
BEGIN
	SELECT		ISNULL(inv.Invoice_ID,0) AS Invoice_ID, 
				ISNULL(adjType.Name, '') AS AdjustmentType, 
				cdAccType.Description AS AccountType,
				adj.Adjustment_ID,
				b.AccountId as Account_ID,
				adj.Adjustment_Type_ID,
				adj.Adjustment_Effective_Date,                         
				ABS(adj.Adjustment_Amount) AS Adjustment_Amount,
				adj.Note_To_Print,
				adj.Internal_Comment,
				b.OrderId as Order_ID,
				ISNULL(b.CampaignID, 0) AS Campaign_ID,
				GL_Entry_ID
	FROM		QSPCanadaOrderManagement..Batch b 
	LEFT JOIN	Adjustment adj ON adj.Order_ID = b.OrderID	AND	adj.Account_ID = b.AccountId
	LEFT JOIN	Invoice inv ON b.OrderID = inv.Order_ID 	
	LEFT JOIN	Adjustment_Type adjType	ON adjType.Adjustment_Type_ID = adj.Adjustment_Type_ID
	LEFT JOIN	GL_Entry gle ON	gle.Adjustment_ID = adj.Adjustment_ID
	LEFT JOIN	QSPCanadaCommon..CodeDetail cdAccType ON cdAccType.Instance = adj.Account_Type_ID
	WHERE		b.OrderID = @OrderID
	ORDER BY	adj.Adjustment_Effective_Date DESC
END
ELSE
BEGIN
	SELECT		inv.Invoice_ID, 
				ISNULL(adjType.Name, '') AS AdjustmentType, 
				cdAccType.Description AS AccountType,
				adj.Adjustment_ID,
				inv.Account_ID,
				adj.Adjustment_Type_ID,
				adj.Adjustment_Effective_Date,                         
				ABS(adj.Adjustment_Amount) AS Adjustment_Amount,
				adj.Note_To_Print,
				adj.Internal_Comment,
				inv.Order_ID,
				ISNULL(b.CampaignID, 0) AS Campaign_ID,
				GL_Entry_ID
	FROM		Invoice inv
	JOIN		QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.Order_ID
	LEFT JOIN	Adjustment adj ON adj.Order_ID = inv.Order_ID	AND	adj.Account_ID = inv.Account_ID
	LEFT JOIN	Adjustment_Type adjType	ON adjType.Adjustment_Type_ID = adj.Adjustment_Type_ID
	LEFT JOIN	GL_Entry gle ON	gle.Adjustment_ID = adj.Adjustment_ID
	LEFT JOIN	QSPCanadaCommon..CodeDetail cdAccType ON cdAccType.Instance = adj.Account_Type_ID
	WHERE		inv.Invoice_ID = @InvoiceID
	ORDER BY	adj.Adjustment_Effective_Date DESC
END
SET NOCOUNT OFF
GO
