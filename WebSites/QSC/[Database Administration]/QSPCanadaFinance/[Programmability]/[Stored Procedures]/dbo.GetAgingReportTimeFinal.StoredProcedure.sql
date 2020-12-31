USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAgingReportTimeFinal]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetAgingReportTimeFinal]
	@AsOfDate	datetime
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 10/21/2004 
--   Get Aging Report For Canada Finance System
--   MTC 11/4/2004
--   Include all payments(with or without an invoice created) and exclude internet activity
--   Re-Written  June 19, 2006 MS
--   Added Ading Bucket for 180, 360 and 720 and Current,Last  and Next Fiscal CAIds MS Aug 14, 2008

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Declare @CurrentDate Datetime

Declare @CurrentSeasonStart Datetime
Declare @CurrentSeasonEnd Datetime

Declare @LastSeasonStart Datetime
Declare @LastSeasonEnd Datetime

Declare @NextSeasonStart Datetime
Declare @NextSeasonEnd Datetime


Select @CurrentDate= Convert(Datetime,Convert(Varchar(10),GetDate(),101))

--Select @CurrentDate CurrentDate

--Current Season
Select @CurrentSeasonStart=s.StartDate,@CurrentSeasonEnd=s.EndDate
from qspcanadacommon..season S
where @CurrentDate between s.StartDate and s.Enddate and s.Season = 'Y'

--Select @CurrentSeasonStart CurrentSeasonStart,@CurrentSeasonEnd CurrentSeasonEnd

--Last Season
Select @LastSeasonStart=s.StartDate,@LastSeasonEnd=s.EndDate
from qspcanadacommon..season S
where @CurrentSeasonStart-31 between s.StartDate and s.Enddate and s.Season = 'Y'

--Select @LastSeasonStart LastSeasonStart,@LastSeasonEnd LastSeasonEnd

--Next Season
Select @NextSeasonStart=@CurrentSeasonStart+365,@NextSeasonEnd=@CurrentSeasonEnd+365


----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 10/21/2004 
--   Get Aging Report For Canada Finance System
--   MTC 11/4/2004
--   Include all payments(with or without an invoice created) and exclude internet activity
--   Re-Written  June 19, 2006 MS
--   Added Ading Bucket for 180, 360 and 720 and Current,Last  and Next Fiscal CAIds MS Aug 14, 2008

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
	StartDate       datetime,
	TransactionDate datetime,
	TransactionId		Int,
	InvoiceAmount  		Numeric(10,2),
	PaymentAmount 	Numeric(10,2),
	AdjustmentAmount 	Numeric(10,2),
	ProfitAmount 	Numeric(10,2),
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
	AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR 
   	         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006')) ---- CustServ Non CC Items by MS March 22, 2007 Issue#2047
	AND 	CONVERT(Datetime, CONVERT(Varchar,Invoice_Date,101)) <= @AsOfDate 

	--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1
	AND		gle.BusinessUnitID IN (1,2)

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
			,group_profit_amount
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

			--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1
			AND		gle.BusinessUnitID IN (1,2)

			group by c.FMID,LastName ,Firstname,b.orderqualifierid,c.id,b.accountid
			,Name
			,c.id
			,c.startdate
			,Invoice_Date
			,i.invoice_id
			,i.invoice_amount
			,group_profit_amount

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
     	QSPCanadaFinance..Invoice I  On p.order_id=i.order_id JOIN
     	QSPCanadaFinance..GL_Entry gle on gle.Invoice_ID = i.Invoice_ID
	WHERE B.OrderID = P.Order_ID
	AND 	B.AccountID = A.ID 
	AND	B.CampaignId= C.Id
	AND     C.FMID = F.FMID
	--AND 	p.Account_Id = IsNull(@AccountId,p.Account_Id)
	AND 	CONVERT(Datetime, CONVERT(Varchar,Payment_Effective_Date,101))<= @AsOfDate
	-->AND	B.OrderQualifierID  Not IN (39008,  39009)    --Exclude Cust Svc, internet
	AND 	B.OrderTypeCode    <> 41009		    --Magnet
	AND (B.OrderQualifierID   IN (39001,39002,39003,39005,39006,39020)   OR 
   	         (B.OrderQualifierID   IN (39013,39015 ) AND CONVERT(datetime, CONVERT(varchar,Invoice_Date,112)) <= '11/09/2006'))

	--AND		ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1
	AND		gle.BusinessUnitID IN (1,2)

	GROUP BY AccountID,Name,CampaignID,C.StartDate,Payment_Effective_Date,c.FMID,LastName ,Firstname,OrderTypeCode,OrderQualifierId,p.Payment_Id,payment_Amount

	--Adjustments
	INSERT #IPA
	SELECT C.FMID,
		LastName + ', ' + Firstname, 
		Null,
		Null,
		Adjustment_Type_ID,
		Account_ID,
		Name	,
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
		QSPCanadaFinance..GL_Entry gle 
	WHERE A.Account_Id = Acc.Id
	AND   C.BilltoAccountId= A.Account_Id
	AND   C.Id = A.Campaign_Id
	AND   C.FMID = F.FMID 
	AND   gle.Adjustment_ID = A.adjustment_ID
           --AND   A.Account_Id = IsNull(@AccountId,A.Account_Id)
         -->AND   A.Campaign_Id In (Select CampaignId from @CA)      MS Nov16, 2006 
	AND  CONVERT(Datetime, CONVERT(Varchar,Adjustment_Effective_Date,101)) <= @AsOfDate
and adjustment_type_id not in ( 49028,49030)--49012,49016)

	AND		gle.BusinessUnitID IN (1,2)
--AND		((c.StartDate < '2012-01-01'and adjustment_type_id not in (49024)) OR a.Date_Created < '2012-02-21' and adjustment_type_id in (49024))

	GROUP BY A.Account_Id,A.Campaign_ID,C.StartDate,Adjustment_Effective_Date,Name,c.FMID,LastName ,Firstname,Adjustment_Type_ID,a.adjustment_Id,Adjustment_Amount


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
Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
 
	having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) < 30

	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 30 and 60
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 61 and 90
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 91 and 120
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over120
,0 as Over180
,0 as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 121 and 180
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05


Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over180
,0 as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 181 and 360
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05



Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over360
,0 as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) between 361 and 720
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05



Insert into
#Output
 Select
 I.FMID, FMName, AccountID, AccountName,CampaignID
,0 as [Current]
,0 as Over30
,0 as Over60
,0 as Over90
,0 as Over120
,0 as Over180
,0 as Over360
,Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) as Over720

from #IPA I
--inner join qspcanadacommon.dbo.campaign camp on camp.id = I.campaignid
--where camp.StartDate >= '2009-07-01'
group by I.FMID, FMName, AccountID, AccountName, CampaignID
having (DATEDIFF(dd, ISNULL(Max(TransactionDate),GetDate()), GetDate())) > 721
 	and Sum(InvoiceAmount)-Sum(PaymentAMount) -Sum(AdjustmentAmount)-Sum(ProfitAmount) not between -.05 and .05



Select o.*,Convert(Varchar(10),StartDate,101) as CampaignStartDate, camp.IsStaffOrder IsStaffCampaign, sdAcc.AccountBalance
from #Output o
inner join qspcanadacommon.dbo.campaign camp on camp.id = o.campaignid
inner join qspcanadacommon.dbo.caccount acc on acc.id = camp.Billtoaccountid
--left join qspcanadafinance.dbo.##AgingGAO20120531 gao on gao.campaignID = o.campaignid
left join (SELECT ISNULL(SUM(TransactionAmount), 0) AccountBalance, AccountID FROM dbo.UDF_Statement_GetDetails_WithBusLogic(@AsOfDate) GROUP BY AccountID) sdAcc on sdAcc.AccountID = o.accountid 

where acc.BusinessUnitID = 1
--and camp.StartDate >= '2009-07-01'
and acc.ID not in (30057, 30098, 30090, 30089, 30100, 30063, 30061, 30053, 30081, 30083, 30099, 30079, 30092, 30096, 30067, 30543, 30060, 32954, 32594)

/*and (o.[Current] <> ISNULL(gao.[Current] * -1, 0.00)
or o.[Over30] <> ISNULL(gao.[Over30] * -1, 0.00)
or o.[Over60] <> ISNULL(gao.[Over60] * -1, 0.00)
or o.[Over90] <> ISNULL(gao.[Over90] * -1, 0.00)
or o.[Over120] <> ISNULL(gao.[Over120] * -1, 0.00)
or o.[Over180] <> ISNULL(gao.[Over180] * -1, 0.00)
or o.[Over360] <> ISNULL(gao.[Over360] * -1, 0.00))*/

 order by  o.FMID, o.FMName, o.AccountID, o.AccountName,o.CampaignID

Drop table #Output
Drop table #ipa
Drop table #IPAAggregate
GO
