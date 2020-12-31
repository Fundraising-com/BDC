USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllOrdersNotInvoicedPaymentDetails]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllOrdersNotInvoicedPaymentDetails]
	@OrderID	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 11/9/2004 
--   Get Invoice Payment Details List For Orders Not Yet Invoiced
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT CD.Description as PaymentMethod, 
	CD2.Description as AccountType,
	PAYMENT_ID,
	B.ACCOUNTID as ACCOUNT_ID,  
	P.PAYMENT_METHOD_ID, 
	PAYMENT_EFFECTIVE_DATE, 
	CHEQUE_NUMBER,
	CHEQUE_DATE,
	CHEQUE_PAYER,
	CREDIT_CARD_OWNER,
	CREDIT_CARD_AUTHORIZATION,
	PAYMENT_AMOUNT,
	P.NOTE_TO_PRINT ,  --null for all   
	B.ORDERID as ORDER_ID,  
	ISNULL(B.CAMPAIGNID,0) as CAMPAIGN_ID
FROM QSPCanadaOrderManagement..Batch B
	LEFT JOIN Payment P on B.OrderID = P.Order_ID
	LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = P.Payment_Method_ID
	LEFT JOIN QSPCanadaCommon..CodeDetail CD2 on CD2.Instance = P.Account_Type_ID
WHERE B.OrderID = @OrderID
ORDER BY PAYMENT_EFFECTIVE_DATE DESC


SET NOCOUNT OFF
GO
