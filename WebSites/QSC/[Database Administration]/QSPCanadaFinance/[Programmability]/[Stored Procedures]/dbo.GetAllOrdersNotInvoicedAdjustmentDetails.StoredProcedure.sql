USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrdersNotInvoicedAdjustmentDetails]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllOrdersNotInvoicedAdjustmentDetails]
	@OrderID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 11/9/2004 
--   Get Adjustment Details List For Orders Not Yet Invoiced
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON


SELECT CD.Description as AdjustmentType, 
	CD2.Description as AccountType,
	A.ADJUSTMENT_ID,
	B.ACCOUNTID as ACCOUNT_ID,  
	A.ADJUSTMENT_TYPE_ID, 
	Adjustment_Effective_Date,                         
	ABS(ADJUSTMENT_AMOUNT) as ADJUSTMENT_AMOUNT,
	A.NOTE_TO_PRINT ,                                                                                      
	INTERNAL_COMMENT,                                                                                 
	B.ORDERID as ORDER_ID,  
	ISNULL(B.CAMPAIGNID,0) as CAMPAIGN_ID,
	GL_Entry_ID
FROM QSPCanadaOrderManagement..Batch B 
	LEFT JOIN ADJUSTMENT A on B.OrderID = A.Order_ID
	LEFT JOIN GL_Entry GL on GL.Adjustment_ID = A.Adjustment_ID
	LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = A.Adjustment_Type_ID
	LEFT JOIN QSPCanadaCommon..CodeDetail CD2 on CD2.Instance = A.Account_Type_ID
WHERE B.OrderID = @OrderID
ORDER BY Adjustment_Effective_Date DESC

SET NOCOUNT OFF
GO
