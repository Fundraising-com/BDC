USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[PaymentDistributionReport]    Script Date: 06/07/2017 09:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[PaymentDistributionReport]    (@AccountType 		Int,
							@AccountId		Int,
							@PaymentDateFrom  	DateTime,
							@PaymentDateTo     	DateTime,
							@OrderId	       	Int,
							@PaymentMethodId	Int)
AS
BEGIN

SELECT CONVERT(Varchar(10),P.PAYMENT_EFFECTIVE_DATE,101) PAYMENT_EFFECTIVE_DATE, 
	P.PAYMENT_ID, 
	P.ACCOUNT_ID, 
	A.NAME, 
	P.PAYMENT_METHOD_ID, 
	P.PAYMENT_AMOUNT, 
	P.ORDER_ID, 
    P.CHEQUE_NUMBER, 
	P.ACCOUNT_TYPE_ID,
	(CASE ISNULL(P.PAYMENT_METHOD_ID,0)
		WHEN 50002 THEN  P.CHEQUE_PAYER
	 	WHEN 2 THEN  P.CHEQUE_PAYER
		WHEN 50003 THEN  P.CREDIT_CARD_OWNER
		WHEN 3 THEN  P.CREDIT_CARD_OWNER
		WHEN 50004 THEN  P.CREDIT_CARD_OWNER
		 WHEN 4 THEN  P.CREDIT_CARD_OWNER
	 	ELSE NULL
	END) Payer,
	(CASE ISNULL(P.PAYMENT_METHOD_ID,0)
	 	--WHEN 50002 THEN 'Cheques / Cash'
	 	--WHEN 2 THEN 'Cheques /  Cash'	--Will be removed
	 	WHEN 50002 THEN CASE p.CHEQUE_NUMBER WHEN 'CASH' THEN 'Cash' ELSE 'Cheque' END
	 	WHEN 2 THEN CASE p.CHEQUE_NUMBER WHEN 'CASH' THEN 'Cash' ELSE 'Cheque' END
	 	WHEN 50003 THEN 'Visa'
	 	WHEN 3 THEN 'Visa'
	 	WHEN 50004 THEN 'Master'
		WHEN 4 THEN 'Master'
		ELSE 'Unknown'
	END) PaymentMethod,
	(CASE ISNULL(P.ACCOUNT_TYPE_ID,0)
		WHEN 50601 THEN 'Group'			--Regular & Account are Group
		WHEN 50603 THEN 'Group'
		WHEN 50602 THEN 'FM'
		WHEN 50604 THEN 'Employee'
		WHEN 1 THEN 'Group'
		ELSE 'UnKnown'
	END) AccountType,
	(CASE ISNULL(DateTime_Created , '01/01/1990')		-- When payment Record is Created It is Approved
		WHEN '01/01/1990' THEN 'Un-Approved'
		ELSE 'Approved'
	END) PaymentStatus , COUNT(*) cnt
FROM    PAYMENT p 
JOIN	QSPCanadaCommon..CAccount a ON P.ACCOUNT_ID = a.Id
JOIN	QSPCanadaOrderManagement..Batch b ON b.OrderID = p.ORDER_ID
WHERE	P.Account_Type_Id = ISNULL(@AccountType, p.Account_Type_Id)
AND		P.Account_Id 	  = ISNULL(@AccountId,P.Account_Id)
AND		P.Order_Id 	  = ISNULL(@OrderId, p.Order_Id)
AND		P.Payment_Method_Id = ISNULL(@PaymentMethodId, P.Payment_Method_Id)
AND		CONVERT(VARCHAR(10),P.Payment_Effective_date,101) >= @PaymentDateFrom
AND		CONVERT(VARCHAR(10),P.Payment_Effective_date,101) <= @PaymentDateTo
AND		b.OrderQualifierID IN (39001, 39002, 39020)
GROUP BY PAYMENT_EFFECTIVE_DATE, 
	      P.PAYMENT_ID, 
	      P.ACCOUNT_ID, 
	      A.NAME, 
	      P.PAYMENT_METHOD_ID, 
	      P.PAYMENT_AMOUNT, 
	      P.ORDER_ID,  
	      P.CHEQUE_PAYER, 
	      P.CREDIT_CARD_OWNER, 
	      P.CHEQUE_NUMBER, 
	      P.ACCOUNT_TYPE_ID,
	      DATETIME_CREATED

END
GO
