USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAgingReport]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetAgingReport]
	
	@AsOfDate				datetime,
	@FMID					varchar(4) = NULL,
	@GAOSalesOnly			bit = 0,
	@FromCampaignStartDate	datetime = NULL

AS

SET NOCOUNT ON

	IF @FromCampaignStartDate IS NULL
		SET @FromCampaignStartDate = '2012-01-01'

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
	StartDate       datetime,
	TransactionDate datetime,
	TransactionId		Int,
	InvoiceAmount  		Numeric(10,2) Not Null,
	PaymentAmount 	Numeric(10,2) Not Null,
	AdjustmentAmount 	Numeric(10,2) Not Null,
	ProfitAmount 	Numeric(10,2) Not Null,
	) 

	CREATE  TABLE #IPAAggregate
	(
	FMID			Varchar(4),
	FMName		Varchar(50),
	AccountID		Int,
	AccountName		Varchar(50), 
	AgingDays		Int,
	Amount  		Numeric(10,2)
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
		MAX(DATEDIFF(dd, ISNULL(Invoice_Date,@AsOfDate), @AsOfDate)) ,
		CampaignID,
		C.StartDate,
		Invoice_Date,
		i.invoice_id,
		Invoice_Amount,
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Numeric(10,2),0.0) 
	FROM 	QSPCanadaFinance..Invoice I, 
		QSPCanadaOrderManagement..Batch B  ,
		QSPCanadaCommon..CAccount A,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F,
		QSPCanadaFinance..GL_Entry gle 
	WHERE B.OrderID = I.Order_ID
	AND  	B.AccountID=A.ID 
	AND	B.CampaignId = C.Id
	AND 	C.FMID=F.FMID
	AND		gle.Invoice_ID = i.Invoice_ID
	AND 	B.OrderTypeCode    <> 41009 --Magnet
	AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39016,39017,39018,39019,39020,39021,39022,39023) OR 
   	         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006')) ---- CustServ Non CC Items by MS March 22, 2007 Issue#2047
	AND 	CONVERT(Datetime, CONVERT(Varchar,Invoice_Date,101)) <= @AsOfDate 

	AND		F.FMID = ISNULL(@FMID, F.FMID)
	
	AND		(ISNULL(@GAOSalesOnly, 0) = 0 OR gle.BusinessUnitID IN (3,4))

	AND		C.StartDate >= ISNULL(@FromCampaignStartDate, C.StartDate) 
	
	AND		A.BusinessUnitID NOT IN (2) --EFR
	
	--AND		A.CAccountCodeGroup <> 'Comm'
	
	GROUP BY AccountID,Name,CampaignID,C.StartDate,Invoice_Date,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,i.invoice_id,Invoice_Amount

	-- Internet & Customer Service Profit
	INSERT #IPA
			select 
			C.FMID,
		LastName + ', ' + Firstname
			, b.orderqualifierid
			,0
			,0
			,b.accountid
			,Name
			,0
			,c.id
			,c.startdate
			,i.Invoice_Date
			,i.invoice_id
			,0.00--i.invoice_amount
			,0.00
			,0.00
			,SUM(ISNULL(group_profit_amount, 0.00))
			from qspcanadafinance.dbo.invoice  i
			join qspcanadafinance.dbo.invoice_section iss on i.invoice_id = iss.invoice_id
			join qspcanadaordermanagement.dbo.batch b on  order_id = b.orderid
			Join qspcanadacommon.dbo.campaign c on c.id = b.campaignid
			Join QSPCanadaCommon..CAccount A on A.ID = b.AccountID
			JOIN QSPCanadaCommon..FieldManager F on 	 	C.FMID=F.FMID
			JOIN QSPCanadaFinance..GL_Entry gle on gle.Invoice_ID = i.Invoice_ID

			where 
			orderqualifierid in ( 39009, 39015)
				AND 	CONVERT(Datetime, CONVERT(Varchar,Invoice_Date,101)) <= @AsOfDate 

			AND		F.FMID = ISNULL(@FMID, F.FMID)

			AND		(ISNULL(@GAOSalesOnly, 0) = 0 OR gle.BusinessUnitID IN (3,4))

			AND		C.StartDate >= ISNULL(@FromCampaignStartDate, C.StartDate) 

			AND		A.BusinessUnitID NOT IN (2) --EFR
	
			--AND		A.CAccountCodeGroup <> 'Comm'

			group by c.FMID,LastName ,Firstname,b.orderqualifierid,c.id,b.accountid
			,Name
			,c.id
			,c.startdate
			,Invoice_Date
			,i.invoice_id
			,i.invoice_amount
			--,group_profit_amount

	--Payments	
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname,  
		OrderQualifierId,
		OrderTypeCode,
		Null,
		AccountID,
		Name,
		MAX(DATEDIFF(dd, ISNULL(Payment_Effective_Date, @AsOfDate), @AsOfDate)) ,
		CampaignID,
		C.StartDate,
		Payment_Effective_Date,
		p.Payment_Id,
		CONVERT(Numeric(10,2),0.0) ,
		ISNULL(Payment_Amount,0),
		CONVERT(Numeric(10,2),0.0) ,
		CONVERT(Numeric(10,2),0.0) 
	FROM	QSPCanadaOrderManagement..Batch B , 
	             QSPCanadaCommon..CAccount A ,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F ,
		QSPCanadaFinance..Payment P LEFT OUTER JOIN 
     	QSPCanadaFinance..Invoice I  On p.order_id=i.order_id LEFT OUTER JOIN
     	QSPCanadaFinance..GL_Entry gle on gle.Invoice_ID = i.Invoice_ID
	WHERE B.OrderID = P.Order_ID
	AND 	B.AccountID = A.ID 
	AND	B.CampaignId= C.Id
	AND     C.FMID = F.FMID
	--AND 	p.Account_Id = IsNull(@AccountId,p.Account_Id)
	AND 	CONVERT(Datetime, CONVERT(Varchar,Payment_Effective_Date,101))<= @AsOfDate
	-->AND	B.OrderQualifierID  Not IN (39008,  39009)    --Exclude Cust Svc, internet
	AND 	B.OrderTypeCode    <> 41009		    --Magnet
	AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39016,39017,39018,39019,39020,39021,39022,39023)   OR
   	         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006'))

	AND		F.FMID = ISNULL(@FMID, F.FMID)

	AND		(ISNULL(@GAOSalesOnly, 0) = 0 OR gle.BusinessUnitID IN (3,4))

	AND		C.StartDate >= ISNULL(@FromCampaignStartDate, C.StartDate) 
	
	AND		A.BusinessUnitID NOT IN (2) --EFR

	--AND		A.CAccountCodeGroup <> 'Comm'

	GROUP BY AccountID,Name,CampaignID,C.StartDate,Payment_Effective_Date,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,p.Payment_Id,payment_Amount

	--Adjustments
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname, 
		Null,
		Null,
		A.Adjustment_Type_ID,
		Account_ID,
		acc.Name	,
		MAX(DATEDIFF(dd, ISNULL(Adjustment_Effective_Date, @AsOfDate), @AsOfDate)) ,
		Campaign_ID,
		C.StartDate,
		Adjustment_Effective_Date,
		a.Adjustment_ID,
		CONVERT(numeric(10,2),0.0),
		CONVERT(numeric(10,2),0.0) , 
		ISNULL(Adjustment_Amount, 0.0),
		CONVERT(Numeric(10,2),0.0) 
	FROM  	QSPCanadaFinance..Adjustment A , 
		QSPCanadaCommon..CAccount Acc,
		QSPCanadaCommon..Campaign C ,
		QSPCanadaCommon..FieldManager F,
		QSPCanadaFinance..GL_Entry gle,
		QSPCanadaFinance..ADJUSTMENT_TYPE adjT
	WHERE A.Account_Id = Acc.Id
	AND   C.BilltoAccountId= A.Account_Id
	AND   C.Id = A.Campaign_Id
	AND   C.FMID = F.FMID 
	AND   gle.Adjustment_ID = A.adjustment_ID
	AND   adjT.Adjustment_Type_ID = A.Adjustment_Type_ID
           --AND   A.Account_Id = IsNull(@AccountId,A.Account_Id)
         -->AND   A.Campaign_Id In (Select CampaignId from @CA)      MS Nov16, 2006 
	AND  CONVERT(Datetime, CONVERT(Varchar,Adjustment_Effective_Date,101)) <= @AsOfDate
	and  A.adjustment_type_id not in ( 49028,49030)--49012,49016)

	AND		F.FMID = ISNULL(@FMID, F.FMID)

	AND		(ISNULL(@GAOSalesOnly, 0) = 0 OR gle.BusinessUnitID IN (3,4))

	AND		C.StartDate >= ISNULL(@FromCampaignStartDate, C.StartDate) 
	
	AND		Acc.BusinessUnitID NOT IN (2) --EFR
	AND		AdjT.ExcludeFromInvoicing = 0
	--AND		Acc.CAccountCodeGroup <> 'Comm'

	GROUP BY A.Account_Id,A.Campaign_ID,C.StartDate,Adjustment_Effective_Date,acc.Name,c.FMID,LastName ,Firstname,A.Adjustment_Type_ID,a.adjustment_Id,Adjustment_Amount


CREATE  TABLE #Output
	(
	FMID		Varchar(4),
	FMName		Varchar(50),
	AccountID	Int,
	AccountName	Varchar(50), 
	CampaignID  int,
	[Current]	Numeric(14,2),
	Over30		Numeric(14,2),
	Over60		Numeric(14,2),
	Over90		Numeric(14,2),
	Over120		Numeric(14,2),
	Over180		Numeric(14,2),
	Over360		Numeric(14,2),
	Over720		Numeric(14,2)
	) 

INSERT INTO #Output
SELECT		FMID,
			FMName,
			AccountID,
			AccountName,
			CampaignID,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) < 30 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS [Current],
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) BETWEEN 30 AND 60 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over30,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) BETWEEN 61 AND 90 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over60,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) BETWEEN 91 AND 120 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over90,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) BETWEEN 121 AND 180 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over120,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) BETWEEN 181 AND 360 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over180,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) BETWEEN 361 AND 720 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over360,
			SUM(CASE WHEN (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) > 720 THEN (InvoiceAmount) - (PaymentAmount) - (AdjustmentAmount) - (ProfitAmount) ELSE 0.00 END) AS Over720
FROM		#IPA I
GROUP BY	FMID,
			FMName,
			AccountID,
			AccountName,
			CampaignID
HAVING		SUM(InvoiceAmount) - SUM(PaymentAmount) - SUM(AdjustmentAmount) - SUM(ProfitAmount) NOT BETWEEN -.05 AND .05

/*Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) < 30
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) < 30
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) between 30 and 60
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 30 and 60
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) between 61 and 90
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 61 and 90
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) between 91 and 120
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 91 and 120
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) between 121 and 180
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 121 and 180
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over180
,0 as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) between 181 and 360
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 181 and 360
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05



Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over360
,0 as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) between 361 and 720
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 361 and 720
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05



Insert into
#Output
 Select
 FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over720

from #IPA I
where (DATEDIFF(dd, ISNULL(TransactionDate,GetDate()), GetDate())) > 721
group by FMID, FMName, AccountID, AccountName,CampaignID
--having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) > 721
--having Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05
*/


SELECT		o.*,
			Convert(Varchar(10),StartDate,101) as CampaignStartDate
FROM		#Output o
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = o.CampaignID
JOIN		QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
ORDER BY	CASE acc.CAccountCodeGroup WHEN 'Comm' THEN 1 ELSE 0 END, o.FMID, o.FMName, o.AccountID, o.AccountName, o.CampaignID

Drop table #Output
Drop table #ipa
Drop table #IPAAggregate

GO
