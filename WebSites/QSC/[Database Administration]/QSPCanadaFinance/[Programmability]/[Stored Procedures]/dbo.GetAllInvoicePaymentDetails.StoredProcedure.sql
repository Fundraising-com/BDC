USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvoicePaymentDetails]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllInvoicePaymentDetails]
	@InvoiceId int,
	@OrderID	int = null
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Invoice Payment Details List For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON
IF (@InvoiceId = 0)
BEGIN
	SELECT isnull(Invoice_ID,0) as Invoice_ID,
			CD.Description as PaymentMethod, 
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
		LEFT JOIN Payment P on P.Order_ID = B.ORDERID AND P.Account_ID = B.AccountID  
		LEFT JOIN INVOICE I ON I.ORDER_ID = B.OrderID
		LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = P.Payment_Method_ID
		LEFT JOIN QSPCanadaCommon..CodeDetail CD2 on CD2.Instance = P.Account_Type_ID
	WHERE B.ORDERID = @OrderID
	ORDER BY PAYMENT_EFFECTIVE_DATE DESC
END	
ELSE
BEGIN
	SELECT Invoice_ID,
			CD.Description as PaymentMethod, 
			CD2.Description as AccountType,
			PAYMENT_ID,
			I.ACCOUNT_ID as ACCOUNT_ID,  
			P.PAYMENT_METHOD_ID, 
			PAYMENT_EFFECTIVE_DATE, 
			CHEQUE_NUMBER,
			CHEQUE_DATE,
			CHEQUE_PAYER,
			CREDIT_CARD_OWNER,
			CREDIT_CARD_AUTHORIZATION,
			PAYMENT_AMOUNT,
			P.NOTE_TO_PRINT ,  --null for all   
			I.ORDER_ID as ORDER_ID,  
			ISNULL(B.CAMPAIGNID,0) as CAMPAIGN_ID
		FROM INVOICE I 
		INNER JOIN QSPCanadaOrderManagement..Batch B on B.OrderID = I.Order_ID
		LEFT JOIN Payment P on P.Order_ID = I.Order_ID AND P.Account_ID = I.Account_ID  
		LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = P.Payment_Method_ID
		LEFT JOIN QSPCanadaCommon..CodeDetail CD2 on CD2.Instance = P.Account_Type_ID
	WHERE Invoice_ID = @InvoiceId
	ORDER BY PAYMENT_EFFECTIVE_DATE DESC
END
SET NOCOUNT OFF
GO
