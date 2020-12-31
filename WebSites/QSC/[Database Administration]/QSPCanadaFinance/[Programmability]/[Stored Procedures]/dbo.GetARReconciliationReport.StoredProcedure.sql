USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetARReconciliationReport]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetARReconciliationReport] 	@AsOfDate	datetime
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MS 04/25/2008
--   Include all including internet activity for Audit/AR Reconciliation
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

	--Transaction Level Entries
	CREATE  TABLE #IPA
	(
	FMID			Varchar(4),
	FMName		Varchar(50),
	OrderQualifier		Int,
	OrderType		Int,
	AdjustmentType		Int,
	AccountID		Int,
	AccountName		Varchar(50), 
	AgingDays		Int,
	CampaignID		Int,
	TransactionId		Int,
	InvoiceAmount  		Numeric(10,2),
	PaymentAmount 	Numeric(10,2),
	AdjustmentAmount 	Numeric(10,2)
	) 

	-- Individual CAs
	--DECLARE @CA TABLE( CampaignId Int) 

	--Invoices
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname,  
		OrderQualifierId,
		OrderTypeCode,
		Null,
		AccountID,
		Name,
		MAX(DATEDIFF(dd, ISNULL(Invoice_Date, GETDATE()), GETDATE())) ,
		CampaignID,
		i.invoice_id,
		Invoice_Amount,
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Numeric(10,2),0.0) 
	FROM 	QSPCanadaFinance..Invoice I, 
		QSPCanadaOrderManagement..Batch B  ,
		QSPCanadaCommon..CAccount A,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F 
	WHERE B.OrderID = I.Order_ID
	AND  	B.AccountID=A.ID 
	AND	B.CampaignId = C.Id
	AND 	C.FMID=F.FMID
	--And 	I.Account_Id = IsNull(@AccountId,I.Account_Id)
	-->AND	B.OrderQualifierID  Not IN (39008,  39009)    --Exclude Cust Svc, internet
	AND 	B.OrderTypeCode    <> 41009 --Magnet
	--AND 	B.OrderQualifierID    <> 39005 -- PSolver
	AND B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39009,39013,39015,39020)
	--AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR 
             --         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006')) ---- CustServ Non CC Items by MS March 22, 2007 Issue#2047
	AND 	CONVERT(Datetime, CONVERT(Varchar,Invoice_Date,101)) <= @AsOfDate
	GROUP BY AccountID,Name,CampaignID,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,i.invoice_id,Invoice_Amount
	
	--Payments	
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname,  
		OrderQualifierId,
		OrderTypeCode,
		Null,
		AccountID,
		Name,
		MAX(DATEDIFF(dd, ISNULL(Payment_Effective_Date, GETDATE()), GETDATE())) ,
		CampaignID,
		p.Payment_Id,
		CONVERT(Numeric(10,2),0.0) ,
		ISNULL(Payment_Amount,0),
		CONVERT(Numeric(10,2),0.0) 
	FROM	QSPCanadaOrderManagement..Batch B , 
	             QSPCanadaCommon..CAccount A ,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F ,
		QSPCanadaFinance..Payment P LEFT OUTER JOIN 
     		QSPCanadaFinance..Invoice I  On p.order_id=i.order_id
	WHERE B.OrderID = P.Order_ID
	AND 	B.AccountID = A.ID 
	AND	B.CampaignId= C.Id
	AND     C.FMID = F.FMID
	--AND 	p.Account_Id = IsNull(@AccountId,p.Account_Id)
	AND 	CONVERT(Datetime, CONVERT(Varchar,Payment_Effective_Date,101))<= @AsOfDate
	-->AND	B.OrderQualifierID  Not IN (39008,  39009)    --Exclude Cust Svc, internet
	AND 	B.OrderTypeCode    <> 41009		    --Magnet
	AND B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39009,39013,39015,39020)
	--AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR 
   	--         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006'))
	GROUP BY AccountID,Name,CampaignID,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,p.Payment_Id,payment_Amount


	--INSERT INTO @CA SELECT DISTINCT CampaignId FROM #IPA 

	--Adjustments
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname, 
		Null,
		Null,
		Adjustment_Type_ID,
		Account_ID,
		Name	,
		MAX(DATEDIFF(dd, ISNULL(Adjustment_Effective_Date, GETDATE()), GETDATE())) ,
		Campaign_ID,
		a.Adjustment_ID,
		CONVERT(numeric(10,2),0.0),
		CONVERT(numeric(10,2),0.0) , 
		ISNULL(Adjustment_Amount, 0.0)
	FROM  	QSPCanadaFinance..Adjustment A , 
		QSPCanadaCommon..CAccount Acc,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F 
	WHERE A.Account_Id = Acc.Id
	AND   C.BilltoAccountId= A.Account_Id
	AND   C.Id = A.Campaign_Id
	AND   C.FMID = F.FMID 
           --AND   A.Account_Id = IsNull(@AccountId,A.Account_Id)
         -->AND   A.Campaign_Id In (Select CampaignId from @CA)      MS Nov16, 2006 
	AND  CONVERT(Datetime, CONVERT(Varchar,Adjustment_Effective_Date,101)) <= @AsOfDate
	AND  A.Adjustment_Type_ID NOT IN( 49016,49028,49030) --Magnet, Cust Serv and Online
	GROUP BY A.Account_Id,A.Campaign_ID,Name,c.FMID,LastName ,Firstname,Adjustment_Type_ID,a.adjustment_Id,Adjustment_Amount

	--Output
	SELECT  FMID,
		FMName,
		AccountID, 
		AccountName,
		SUM(CASE WHEN AgingDays BETWEEN 0   AND 30 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)   AS [Current],
		SUM(CASE WHEN AgingDays BETWEEN 31 AND 60 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)   AS Over30,
		SUM(CASE WHEN AgingDays BETWEEN 61 AND 90 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)   AS Over60,
		SUM(CASE WHEN AgingDays BETWEEN 91 AND 120 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END) AS Over90,
		SUM(CASE WHEN AgingDays > 120 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)                               AS Over120
	FROM #IPA 
	GROUP BY FMID, FMName, AccountID, AccountName
	HAVING SUM (InvoiceAmount-PaymentAmount-AdjustmentAmount) <> 0
	ORDER BY FMName, AccountName, AccountID 


	DROP TABLE #IPA
GO
