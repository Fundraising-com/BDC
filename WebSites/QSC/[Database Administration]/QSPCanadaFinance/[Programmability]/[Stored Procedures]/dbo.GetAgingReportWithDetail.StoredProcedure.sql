USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAgingReportWithDetail]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[GetAgingReportWithDetail]
(	 @FromDate	Datetime
	,@AsOfDate	Datetime
	,@AccountID 	Int
)

AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MS Jul 10, 2008
--   Get Aging Report with transaction detail For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SET @FromDate= ISNULL(@FromDate,'07/01/2002')
--SET @AsOfDate='07/03/2008'
--SET @AccountID=6559

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
	AccountContact		Varchar(100),
	AccountPhone		Varchar(30),
	AgingDays		Int,
	CampaignID		Int,
	TransactionId		Int,
	InvoiceAmount  		Numeric(10,2),
	InvoiceDate		DateTime,
	PaymentAmount 		Numeric(10,2),
	PaymentDate		DateTime,
	AdjustmentAmount 	Numeric(10,2),
	AdjustmentDate		DateTime
	) 

	Create Table #Out (ID Int Identity,
		FMID Varchar(4),
		FMName Varchar(100),
		AccountID  Int, 
		AccountName Varchar(100),
		AccountContact Varchar(100),
		AccountPhone Varchar(30),
		CampaignID Int,
		TransactionID Int,
		InvoiceAmount Numeric(14,2),
		InvoiceDate Datetime,
		PaymentAmount Numeric(14,2),
		Paymentdate Datetime,
		AdjustmentAmount Numeric(14,2),
		AdjustmentDate Datetime,
		[Current] Numeric(14,2),
		Over30 Numeric(14,2),
		Over60 Numeric(14,2),
		Over90 Numeric(14,2),
		Over120To180 Numeric(14,2),
		Over180To360 Numeric(14,2),
		Over360To720 Numeric(14,2),
		Over720 Numeric(14,2)
		)


	--Invoices
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname,  
		OrderQualifierId,
		OrderTypeCode,
		Null,
		AccountID,
		Name,
		Null,
		null,
		MAX(DATEDIFF(dd, ISNULL(Invoice_Date, GETDATE()), GETDATE())) ,
		CampaignID,
		i.invoice_id,
		Invoice_Amount,
		i.INVOICE_DATE,
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Datetime,CONVERT(Varchar(10),'01/01/1995',101)) ,
		CONVERT(Numeric(10,2),0.0),
		CONVERT(Datetime,CONVERT(Varchar(10),'01/01/1995',101)) 
	FROM 	QSPCanadaFinance..Invoice I, 
		QSPCanadaOrderManagement..Batch B  ,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F,
		QSPCanadaCommon..CAccount A
	WHERE B.OrderID = I.Order_ID
	AND  	B.AccountID=A.ID 
	AND	B.CampaignId = C.Id
	AND 	C.FMID=F.FMID
	AND 	I.Account_Id = IsNull(@AccountId,I.Account_Id)
	AND 	B.OrderTypeCode    <> 41009 --Magnet
	AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR 
   	         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006')) ---- CustServ Non CC Items by MS March 22, 2007 Issue#2047
	AND 	CONVERT(Datetime, CONVERT(Varchar,Invoice_Date,101)) BETWEEN @FromDate AND @AsOfDate
	GROUP BY AccountID,Name,CampaignID,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,i.invoice_id,Invoice_Amount,i.INVOICE_DATE
	
	--Payments	
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname,  
		OrderQualifierId,
		OrderTypeCode,
		Null,
		AccountID,
		Name,
		Null,
		null,
		MAX(DATEDIFF(dd, ISNULL(Payment_Effective_Date, GETDATE()), GETDATE())) ,
		CampaignID,
		p.Payment_Id,
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Datetime,CONVERT(Varchar(10),'01/01/1995',101)),
		ISNULL(Payment_Amount,0),
		CONVERT(Datetime,CONVERT(Varchar(10),p.PAYMENT_EFFECTIVE_DATE,101)),
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Datetime,CONVERT(Varchar(10),'01/01/1995',101))
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
	AND 	p.Account_Id = IsNull(@AccountId,p.Account_Id)
	AND 	CONVERT(Datetime, CONVERT(Varchar,Payment_Effective_Date,101))BETWEEN @FromDate AND @AsOfDate
	AND 	B.OrderTypeCode    <> 41009		    --Magnet
	AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR 
   	         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006'))
	GROUP BY AccountID,Name,CampaignID,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,p.Payment_Id,payment_Amount,p.PAYMENT_EFFECTIVE_DATE


	--Adjustments
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname, 
		Null,
		Null,
		Adjustment_Type_ID,
		Account_ID,
		Name	,
		null,
		null,
		MAX(DATEDIFF(dd, ISNULL(Adjustment_Effective_Date, GETDATE()), GETDATE())) ,
		Campaign_ID,
		a.Adjustment_ID,
		CONVERT(numeric(10,2),0.0),
		CONVERT(Datetime,CONVERT(Varchar(10),'01/01/1995',101)) ,
		CONVERT(numeric(10,2),0.0) , 
		CONVERT(Datetime,CONVERT(Varchar(10),'01/01/1995',101)) ,
		ISNULL(Adjustment_Amount, 0.0),
		CONVERT(Datetime,CONVERT(Varchar(10),a.ADJUSTMENT_EFFECTIVE_DATE,101)) 
	FROM  	QSPCanadaFinance..Adjustment A , 
		QSPCanadaCommon..CAccount Acc,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F 
	WHERE A.Account_Id = Acc.Id
	AND   C.BilltoAccountId= A.Account_Id
	AND   C.Id = A.Campaign_Id
	AND   C.FMID = F.FMID 
	AND   A.Account_Id = IsNull(@AccountId,A.Account_Id)
        AND  CONVERT(Datetime, CONVERT(Varchar,Adjustment_Effective_Date,101)) BETWEEN @FromDate AND @AsOfDate
	AND  A.Adjustment_Type_ID <> 49016
	GROUP BY A.Account_Id,A.Campaign_ID,Name,c.FMID,LastName ,Firstname,Adjustment_Type_ID,a.adjustment_Id,Adjustment_Amount,a.ADJUSTMENT_EFFECTIVE_DATE

	--Update contact
		UPDATE a
		SET AccountContact=c.FirstName+' '+c.LastName
		FROM QSPCanadacommon.dbo.Contact c, #IPA a
		WHERE 	a.AccountID = c.CAccountID
		AND	c.DeletedTF = 0
		AND c.DateChanged IN (SELECT MAX(c1.DateChanged) FROM QSPCanadacommon.dbo.Contact C1
				      WHERE c1.CAccountID=c.CAccountID and c1.DeletedTF = 0  )

		
		UPDATE a1
		SET AccountPhone=p.PhoneNumber
		FROM  #IPA a1, QSPCanadaCommon.dbo.Caccount a
		LEFT JOIN QSPCanadaCommon.dbo.Phone p ON a.phonelistId = p.PhoneListid and p.type=30505
		WHERE a1.AccountID = A.ID
		

	--Output
	INSERT #Out
	SELECT  FMID,
		FMName,
		AccountID, 
		AccountName,
		AccountContact,
		AccountPhone,
		CampaignID,
		TransactionID,
		InvoiceAmount,
		InvoiceDate,
		PaymentAmount,
		Paymentdate,
		AdjustmentAmount,
		AdjustmentDate,
		SUM(CASE WHEN AgingDays BETWEEN 0   AND 30 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)   AS [Current],
		SUM(CASE WHEN AgingDays BETWEEN 31 AND 60 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)   AS Over30,
		SUM(CASE WHEN AgingDays BETWEEN 61 AND 90 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)   AS Over60,
		SUM(CASE WHEN AgingDays BETWEEN 91 AND 120 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END) AS Over90,
		SUM(CASE WHEN AgingDays BETWEEN 121 AND 180 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)  AS Over120To180,
		SUM(CASE WHEN AgingDays BETWEEN 181 AND 360 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)  AS Over180To360,
		SUM(CASE WHEN AgingDays BETWEEN 361 AND 720 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)  AS Over360To720,
		SUM(CASE WHEN AgingDays > 720 THEN (InvoiceAmount-PaymentAmount-AdjustmentAmount) ELSE 0.0 END)             AS Over720
	FROM #IPA 
	GROUP BY FMID, FMName, AccountID, AccountName,CampaignID,TransactionID,InvoiceAmount,InvoiceDate,
		PaymentAmount,Paymentdate,
		AdjustmentAmount,AdjustmentDate,AccountContact,AccountPhone
	HAVING SUM (InvoiceAmount-PaymentAmount-AdjustmentAmount) <> 0
	ORDER BY FMName, AccountName, AccountID 


	--Find Account Where there is a Balance
	SELECT * FROM #out O
	WHERE EXISTS (SELECT AccountId FROM #out O1
			WHERE o.AccountId=o1.AccountId
			GROUP BY AccountId,AccountName
			HAVING SUM([Current])+sUM(oVER30) +Sum(Over60) +Sum(Over90) +Sum(Over120To180) +Sum(Over180To360) +Sum(Over360to720)+ sum(over720) <>0
		      )


	
	DROP TABLE #IPA
	DROP TABLE #OUT
	
SET NOCOUNT OFF
GO
