USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[VinnieComparisonReport]    Script Date: 06/07/2017 09:20:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[VinnieComparisonReport] @pStartDate datetime, @pEndDate datetime, @IsInvoiced int, @pFMID varchar(10) 

--Sept 21, 2007 MS -  Added online and Laded Sales column, removed Insert into with Temp table Issue#3472
--Jan 15, 2008 MS - Added code to get New and Renewed code. Issue#3948
AS

Declare @LastYear		   Int,  
	@LastSeason 		   Char(1), 
	@LastSeasonStartDate 	   Datetime , 
	@LastSeasonEndDate 	   Datetime, 
	@CurrentSeasonStartDate Datetime , 
	@CurrentSeasonEndDate  Datetime, 
        	@CurrentFiscalYear 	   Varchar(4), 
	@LastFiscalYear 	   Varchar(4)

Declare  @CurSum 		Numeric(14,2), 
	 @LastSum  		Numeric(14,2), 
	 @RenewedDiff  	Numeric(14,2), 
	 @DiffPercent 		Numeric(14,2)  ,
	 @LastWholeSum  	Numeric(14,2),
	 @ExpiredSum 		Numeric(14,2)
Declare  @NewSum 		Numeric(14,2)

Create Table #OldAccounts (OldAccId Int)
Create Table #NewAccounts (NewAccId Int)
Create Table #CurAccounts (CurrentAccId Int)

Create Table #CurNew 	(Term 		Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)
	
Create Table #CurNewByQualifier
			 (Term 		Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)


Create Table #EstCurNew (Term 		Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)


Create Table #CurRenew (Term		Varchar(20),
			 FiscalYear 	Varchar(20),
			 Type		Varchar(10),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)
	
Create Table #CurRenewByQualifier
			 (Term		Varchar(20),
			 FiscalYear	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName	 Varchar(100),
			 FMID		 Varchar(4),
			 FMName	 Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)

Create Table #EstCurRenew(Term 	Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)

Create Table #EstLastRenewByQualifier
			(Term 		Varchar(20),
			 FiscalYear 	Varchar(20),
			 Type		Varchar(10),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)

Create Table #EstLastRenew(Term 	Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)

Create Table #EstLastExpiredByQualifier
			(Term 		Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)

Create Table #EstLastExpired(Term 	Varchar(20),
			 FiscalYear 	Varchar(10),
			 Type		Varchar(20),
			 AccountId 	Int,
			 AccountName 	Varchar(100),
			 FMID 		Varchar(4),
			 FMName 	Varchar(100),
			 OnlineSales 	Numeric(14,2),
			 LandedSales 	Numeric(14,2),
			 Sales		Numeric(14,2)	)

Create Table #Renew    (AccountID 	Int, 
			AccountName 	Varchar(100), 
			FMID 		Varchar(10), 
			FMName 	Varchar(50) , 
			LastOnlineSales 	Numeric (14,2) ,
			LastLandedSales Numeric(14,2) ,
			LastSales  	Numeric(14,2) ,
   			CurOnlineSales  	Numeric(14,2) ,
			CurLandedSales Numeric(14,2) ,
			CurSales  	Numeric(14,2) )  
--initialise variables

Set @CurSum = 0 
Set  @LastSum  = 0 
Set  @RenewedDiff   = 0 
Set  @DiffPercent   = 0 
Set  @LastWholeSum  = 0 
Set  @ExpiredSum  = 0 
Set  @NewSum = 0 


If datepart(mm,@pStartDate) between 7 and 12
Begin
   Set @CurrentFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate)+1 as varchar),3,2)
   Set @LastFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate) as varchar),3,2)
End 

If datepart(mm,@pStartDate) between 1 and 6
Begin
    Set @CurrentFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate) as varchar),3,2)
    Set @LastFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate)-1 as varchar),3,2)
End 

   Set @CurrentSeasonStartDate =  @pStartDate
   Set @CurrentSeasonEndDate  =  @pEndDate

   Set @LastSeasonStartDate = DateAdd(yy,-1,@pStartDate ) 
   Set @LastSeasonEndDate = DateAdd(yy,-1,@pEndDate ) 

--Select @CurrentSeasonStartDate,@CurrentSeasonEndDate,@LastSeasonStartDate,@LastSeasonEndDate 

---Get old new and current account based on camign dates
 Insert #OldAccounts
 Select distinct billtoaccountid 
 From qspcanadacommon.dbo.campaign as ca
 Where ca.startdate  <  @CurrentSeasonStartDate
 --And ca.isstafforder <> 1 
	and ca.status <> 37005
 And ca.fmid  = isnull(@pFMID,ca.fmid)  
---OLD ACCT only when ran in last fiscal	Jan15th 2008 MS
 And  Exists ( Select 1
		 From qspcanadacommon..campaign c
		 Where c.id=ca.id
		 And  ca.startdate >= (Select startdate 
				       From qspcanadacommon.dbo.Season 
				       Where FiscalYear in (Select FiscalYear-1 from qspcanadacommon.dbo.Season S1 
							  Where startdate = @CurrentSeasonStartDate and enddate = @CurrentSeasonEndDate )	 
					and season='Y')
		 And  ca.Enddate <=    (Select Enddate 
				           From qspcanadacommon.dbo.Season 
				           Where FiscalYear in (Select FiscalYear-1 from qspcanadacommon.dbo.Season S1 
							     Where startdate = @CurrentSeasonStartDate And enddate = @CurrentSeasonEndDate )	 
				           And season='Y')
		)


--Select * from  #OldAccounts

 Insert #NewAccounts
 Select distinct billtoaccountid --as id
 From qspcanadacommon..campaign as ca
 Where ca.startdate >= @CurrentSeasonStartDate and  ca.EndDate <=  @CurrentSeasonEndDate
-- And ca.isstafforder <> 1 
and ca.status <> 37005 
 And ca.fmid  = isnull(@pFMID,ca.fmid)  
---An account will be new when it did not run in the preceeding season
And not exists ( Select 1
	            From qspcanadacommon..campaign c
	            Where c.id=ca.id
                         And  ca.startdate >= (Select startdate 
				      From qspcanadacommon.dbo.Season 
				      Where FiscalYear in (Select FiscalYear-1 From qspcanadacommon.dbo.Season S1 
							Where startdate = @CurrentSeasonStartDate And enddate = @CurrentSeasonEndDate )	 
					And season='Y')
		 And  ca.Enddate <=    (  Select Enddate 
					From qspcanadacommon.dbo.Season 
					Where FiscalYear in (Select FiscalYear-1 From qspcanadacommon.dbo.Season S1 
							     Where startdate = @CurrentSeasonStartDate And enddate = @CurrentSeasonEndDate )	 
					And season='Y')
		)


--Select * from #NewAccounts

 Insert #CurAccounts
 Select distinct billtoaccountid --as id 
 From QspCanadaCommon.dbo.campaign as ca,
      QspCanadaOrdermanagement.dbo.batch as batch ,
      QSPCanadaOrderManagement.dbo.CustomerOrderHeader coh
 Where ca.id  = batch.campaignid
 And ca.startdate >= @CurrentSeasonStartDate
 And ca.EndDate <=  @CurrentSeasonEndDate
 --And ca.isstafforder <> 1 
 And ca.status <> 37005 
 And batch.id = coh.orderbatchid
 And batch.date = coh.orderbatchdate
 And batch.statusinstance <> 40005 
 And batch.OrderQualifierID IN (39001,39002,39009)    
 And ca.fmid  = isnull(@pFMID,ca.fmid) 

--Select * from #CurAccounts

-- Sales sales from current CAs
Insert #CurNewByQualifier
Select   'Current' as Term,
	@LastFiscalYear as FiscalYear,
	 '2New' as Type,  
	ac.id as AccountID,
	 ac.Name as AccountName,
	 fm.fmid as FMID,
	 isnull(fm.lastname,'') +', '+isnull(fm.firstname,'') as fmName,
	 Case OrderQualifierid When 39009 then IsNull(sum(cod.price),0) 
		Else 0
	 End OnlineSales,
	 Case OrderQualifierid When 39009 then 0
			       Else IsNull(sum(cod.price),0) 
	 End LandedSales,
	 Convert(Numeric(14,2),0) TotalSales
From   QSPCANADACOMMON.dbo.CAMPAIGN as ca,
 	QspcanadaOrderManagement.dbo.batch as batch,
	QSPCANADACOMMON.dbo.Fieldmanager as fm,
 	QSPCanadaOrderManagement.dbo.CustomerOrderHeader coh,
 	QSPCanadaOrderManagement.dbo.CustomerOrderDetail cod,
	QspCanadaCommon..cAccount as ac
Where  batch.campaignid = ca.id
And ca.BilltoAccountId = ac.id
And ca.fmid = fm.fmid
And batch.id = coh.orderbatchid
And batch.date = coh.orderbatchdate
And  coh.Instance = cod.CustomerOrderHeaderInstance
And cod.delflag <> 1 
And cod.statusinstance not in (501, 506) 
--and cod.productType in (46001, 46002, 46006,46007, 46012)
and cod.productType in (46001)
And ca.StartDate >= @CurrentSeasonStartDate 
And ca.EndDate   <= @CurrentSeasonEndDate 
And batch.statusinstance <> 40005 
And ca.status <> 37005
--And ca.isstafforder <> 1
And batch.OrderQualifierID IN (39001,39002,39009)
--and batch.IsInvoiced = @IsInvoiced
--and batch.IsInvoiced in (0, @IsInvoiced) 
And ca.fmid  = isnull(@pFMID,ca.fmid)  
And  ca.billtoaccountid  NOT IN ( Select OldAccid From #OldAccounts)    
Group by  ac.id,ac.Name,fm.fmid,fm.firstname,fm.lastname,batch.OrderQualifierID
Order by fm.fmid,fmName       

  
Insert #CurNew
Select Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME,Sum(OnlineSales),Sum(LandedSales),Sum(OnlineSales)+Sum(LandedSales)
From #CurNewByQualifier
Group By Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME

--Select * from #CurNew

------------------------- Estimated 
Insert #EstCurNew
Select   'Current' as Term,
		@CurrentFiscalYear as FiscalYear,
		 '2New' as Type, 
		ac.id as AccountID,
		 ac.Name as AccountName,
		 fm.fmid as FMID,
		 isnull(fm.lastname,'') +', '+isnull(fm.firstname,'') as fmName,
		Convert(Numeric(14,4),0) OnlineSales,
		Convert(Numeric(14,4),0) LandedSales,
		Case QspcanadaOrderManagement.dbo.UDF_IsCampaignCombo  (ca.id)  --if combo then fetch only 60%
			When 0 then Sum(ca.EstimatedGross)
			When 1 then Sum(ca.EstimatedGross*.6)
			End as Sales  
From      QSPCANADACOMMON.dbo.CAMPAIGN as ca,
	QSPCANADACOMMON.dbo.Fieldmanager as fm,
	QspCanadaCommon.dbo.cAccount as ac
Where   ca.fmid = fm.fmid
And ca.BilltoAccountId = ac.id
And ca.StartDate >= @CurrentSeasonStartDate
And ca.EndDate <=  @CurrentSeasonEndDate
And ca.status <> 37005
--And ca.isstafforder <> 1
And ca.fmid  = isnull(@pFMID,ca.fmid)  
And ca.id in ( Select distinct ca.id
	        From qspcanadacommon.dbo.campaign as ca,
		qspcanadacommon.dbo.campaignprogram cp
	       Where  ca.startdate >= @CurrentSeasonStartDate and  ca.EndDate <=  @CurrentSeasonEndDate
	       And ca.id  = cp.campaignid
	       And cp.programid in (1,2)
	       And cp.deletedtf <>1
 	       --And ca.isstafforder <> 1 
			and ca.status <> 37005  )
And  ca.billtoaccountid  NOT IN ( Select OldAccid From #OldAccounts )   
And  ca.billtoaccountid NOT IN  ( Select CurrentAccID From #CurAccounts)
Group by ac.id,ac.Name,fm.fmid,fm.firstname,fm.lastname,ca.id  
Order by fm.fmid,fm.firstname,fm.lastname   

--Select * from #EstCurNew

--------------------- Old account where there is an order in current season (Renew)---
Insert #CurRenewByQualifier
Select   'Current' as Term,
		@LastFiscalYear as FiscalYear,
		 '1Renewed' as Type,  
		ac.id as AccountID,
		 ac.Name as AccountName,
		 fm.fmid as FMID,
		 isnull(fm.lastname,'') +', '+isnull(fm.firstname,'') as fmName, 
		  Case OrderQualifierid When 39009 then IsNull(sum(cod.price),0) 
			Else 0
		 End OnlineSales,
		 Case OrderQualifierid When 39009 then 0
				       Else IsNull(sum(cod.price),0) 
		 End LandedSales,
		 Convert(Numeric(14,2),0) TotalSales 
From   QSPCANADACOMMON.dbo.CAMPAIGN as ca,
	QspcanadaOrderManagement.dbo.batch as batch,
	QSPCANADACOMMON.dbo.Fieldmanager as fm,
	QSPCanadaOrderManagement.dbo.CustomerOrderHeader coh,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail cod,
	QspCanadaCommon..cAccount as ac
Where    batch.campaignid = ca.id
And ca.BilltoAccountId = ac.id
And ca.fmid = fm.fmid
And batch.id = coh.orderbatchid
And batch.date = coh.orderbatchdate
And  coh.Instance = cod.CustomerOrderHeaderInstance
And cod.delflag <> 1 
And cod.statusinstance not in (501, 506) 
--and cod.productType in (46001, 46002, 46006,46007, 46012)
and cod.productType in (46001)
And ca.StartDate >= @CurrentSeasonStartDate 
And ca.EndDate   <= @CurrentSeasonEndDate 
And batch.statusinstance <> 40005 
And ca.status <> 37005
--And ca.isstafforder <> 1
And batch.OrderQualifierID IN (39001,39002,39009)
And ca.fmid  = isnull(@pFMID,ca.fmid)  
And  ca.billtoaccountid   IN ( select OldAccid from #OldAccounts)    
Group by  ac.id,ac.Name,fm.fmid,fm.firstname,fm.lastname,OrderQualifierid
Order by fm.fmid,fmName   

--Select * from #CurRenewByQualifier

Insert #CurRenew
Select Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME,Sum(OnlineSales),Sum(LandedSales),Sum(OnlineSales)+Sum(LandedSales)
From #CurRenewByQualifier
Group By Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME

--Select * from #CurRenew

---------------------------------------------------
Insert #EstCurRenew
Select  'Current' as Term,
		@CurrentFiscalYear as FiscalYear,
		 '1Renewed' as Type, 
		ac.id as AccountID,
		 ac.Name as AccountName,
		 fm.fmid as FMID,
		 isnull(fm.lastname,'') +', '+isnull(fm.firstname,'') as fmName, 
		Convert(Numeric(14,4),0) OnlineSales,
		Convert(Numeric(14,4),0) LandedSales,
		Case QspcanadaOrderManagement.dbo.UDF_IsCampaignCombo  (ca.id)  --if combo then fetch only 60%
			When 0 then Sum(ca.EstimatedGross)
			When 1 then Sum(ca.EstimatedGross*.6)
			End as Sales  
From     QSPCANADACOMMON.dbo.CAMPAIGN as ca,
	QSPCANADACOMMON.dbo.Fieldmanager as fm,
	QspCanadaCommon..cAccount as ac
Where   ca.fmid = fm.fmid
And ca.BilltoAccountId = ac.id
And ca.StartDate >= @CurrentSeasonStartDate
And ca.EndDate <=  @CurrentSeasonEndDate
And ca.status <> 37005
--And ca.isstafforder <> 1
And ca.fmid  = isnull(@pFMID,ca.fmid)  
And ca.id in ( Select Distinct ca.id
	        From qspcanadacommon..campaign as ca,
		  qspcanadacommon..campaignprogram cp
	        Where  ca.startdate >= @CurrentSeasonStartDate and  ca.EndDate <=  @CurrentSeasonEndDate
	        And ca.id  = cp.campaignid
	        And cp.programid in (1,2)
	        And cp.deletedtf <>1
	        --And ca.isstafforder <> 1 
			And ca.status <> 37005)
And  ca.billtoaccountid  IN ( Select OldAccid From #OldAccounts)   
And  ca.billtoaccountid NOT IN  ( Select CurrentAccID From #CurAccounts)
Group by ac.id,ac.Name,fm.fmid,fm.firstname,fm.lastname,ca.id  
Order by fm.fmid,fm.firstname,fm.lastname  

--Select * from #EstCurRenew


Insert #EstLastRenewByQualifier
Select   'Last' as Term,
	@LastFiscalYear as FiscalYear,
	 '1Renewed' as Type,  
	ac.id as AccountID,
	ac.Name as AccountName,
	 fm.fmid as FMID,
	 isnull(fm.lastname,'') +', '+isnull(fm.firstname,'') as fmName,
	 Case OrderQualifierid When 39009 then IsNull(sum(cod.price),0) 
	 Else 0
	 End OnlineSales,
	 Case OrderQualifierid When 39009 then 0
	 Else IsNull(sum(cod.price),0) 
	 End LandedSales,
	 Convert(Numeric(14,2),0) TotalSales  
From      QSPCANADACOMMON.dbo.CAMPAIGN as ca,
 	QspcanadaOrderManagement.dbo.batch as batch,
	QSPCANADACOMMON.dbo.Fieldmanager as fm,
 	QSPCanadaOrderManagement.dbo.CustomerOrderHeader coh,
 	QSPCanadaOrderManagement.dbo.CustomerOrderDetail cod,
	QspCanadaCommon..cAccount as ac
Where    batch.campaignid = ca.id
And ca.BilltoAccountId = ac.id
And ca.fmid = fm.fmid
And batch.id = coh.orderbatchid
And batch.date = coh.orderbatchdate
And  coh.Instance = cod.CustomerOrderHeaderInstance
And cod.delflag <> 1 
--and cod.productType in (46001, 46002, 46006,46007, 46012)
and cod.productType in (46001)
And ca.StartDate >= @LastSeasonStartDate
And ca.EndDate <=  @LastSeasonEndDate
And batch.statusinstance <> 40005 
And ca.status <> 37005
--And ca.isstafforder <> 1
And batch.OrderQualifierID IN (39001,39002,39009)
And ca.fmid  = isnull(@pFMID,ca.fmid)  
And  ca.billtoaccountid   IN ( Select NewAccid From #NewAccounts )   
Group by  ac.id,ac.Name,fm.fmid,fm.firstname,fm.lastname,OrderQualifierid
Order by fm.fmid,fmName

Insert #EstLastRenew
Select Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME,Sum(OnlineSales),Sum(LandedSales),Sum(OnlineSales)+Sum(LandedSales)
From #EstLastRenewByQualifier
Group By Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME

--Select * from #EstLastRenew

--last season resigned /expired  accounts   
Insert #EstLastExpiredByQualifier
Select  'Last' as Term,
	@LastFiscalYear as FiscalYear,
	 '3Not Renewed' as Type, 
	ac.id as AccountID,
	ac.Name as AccountName,
	 fm.fmid as FMID,
	Isnull(fm.lastname,'') +', '+Isnull(fm.firstname,'') as fmName, 
	Case OrderQualifierid When 39009 then IsNull(sum(cod.price),0) 
	Else 0
	End OnlineSales,
	Case OrderQualifierid When 39009 then 0
			       Else IsNull(sum(cod.price),0) 
	End LandedSales,
	Convert(Numeric(14,2),0) TotalSales
 From    QSPCANADACOMMON.dbo.CAMPAIGN as ca,
	QspcanadaOrderManagement.dbo.batch as batch,
	QSPCANADACOMMON.dbo.Fieldmanager as fm,
	QSPCanadaOrderManagement.dbo.CustomerOrderHeader coh,
	QSPCanadaOrderManagement.dbo.CustomerOrderDetail cod,
	QspCanadaCommon..cAccount as ac
Where    batch.campaignid = ca.id
And ca.BilltoAccountId = ac.id
And ca.fmid = fm.fmid
And batch.id = coh.orderbatchid
And batch.date = coh.orderbatchdate
And  coh.Instance = cod.CustomerOrderHeaderInstance
And cod.delflag <> 1 
--and cod.productType in (46001, 46002, 46006,46007, 46012)
and cod.productType in (46001)
And ca.StartDate >= @LastSeasonStartDate
And ca.EndDate <=  @LastSeasonEndDate
And batch.statusinstance <> 40005 
And ca.status <> 37005
--And ca.isstafforder <> 1
And batch.OrderQualifierID IN (39001,39002,39009)  
And ca.fmid  = isnull(@pFMID,ca.fmid)  
And  ca.billtoaccountid  NOT  IN ( Select NewAccid From #NewAccounts  )   
Group by  ac.id,ac.Name,fm.fmid,fm.firstname,fm.lastname,OrderQualifierid
Order by fm.fmid,fm.firstname,fm.lastname  

Insert #EstLastExpired
Select Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME,Sum(OnlineSales),Sum(LandedSales),Sum(OnlineSales)+Sum(LandedSales)
From #EstLastExpiredByQualifier
Group By Term,FiscalYear,Type,AccountId,AccountName,FMID,FMNAME

--Select * from #EstLastExpired

---------------------------------------report total---------------
--calculate report totals

Declare  @Renew1 Numeric(14,2) , @Renew2  Numeric(14,2)
Declare  @New1 Numeric(14,2) ,   @New2  Numeric(14,2) 

--initialise variables 
Set @Renew1 = 0 
Set @Renew2 = 0 
Set @New1 = 0 
Set @New2 = 0 

Select @Renew1  = IsNull(sum(Sales),0)   From #CurRenew  
Select @Renew2  = IsNull(sum(Sales),0)   From #EstCurRenew  
Set  @CurSum  = @Renew1 + @Renew2

Select @New1  = IsNull(sum(Sales),0)   From #CurNew  
Select @New2  = IsNull(sum(Sales),0)   From #EstCurNew  
Set      @NewSum  = @New1 + @New2


Select @LastSum = IsNull(sum(Sales),0)   From #EstLastRenew   

Set  @RenewedDiff =  @CurSum - @LastSum

IF @RenewedDiff > 0 and @LastSum > 0
Begin
	Set @DiffPercent = (@RenewedDiff / @LastSum) * 100 
End
Else
Begin
              Set @DiffPercent = 0 
End 

 Select @ExpiredSum = IsNull(Sum(Sales),0)   From #EstLastExpired   

Set @LastWholeSum = @LastSum + @ExpiredSum
	

---renewed accounts goes into one table so they can be showed parallel in report

 Declare @acid int, 
	  @acname varchar(100), 
	  @fmid varchar(10) , 
	  @fmname varchar(100), 
	  @sales Numeric(14,2), 
	  @Onlinesales Numeric(14,2),
	  @Landedsales Numeric(14,2),
	  @RecExist int
   
 Declare  c1 cursor for
     Select   AccountID, AccountName, FMID, FMName, IsNull(Sum(OnlineSales),0)OnlineSales,IsNull(Sum(LandedSales),0)LandedSales,IsNull(sum(Sales),0) as Sales
     From  #EstLastRenew
     Group By  AccountID, AccountName, FMID, FMName
     Order By FMID,AccountID 
  
  Declare  c2 cursor for
     Select   AccountID, AccountName, FMID, FMName, IsNull(Sum(OnlineSales),0)OnlineSales,IsNull(Sum(LandedSales),0)LandedSales,IsNull(sum(Sales),0) as Sales
     From  #EstCurRenew
     Group by  AccountID, AccountName, FMID, FMName
     Union All
     Select   AccountID, AccountName, FMID, FMName, IsNull(Sum(OnlineSales),0)OnlineSales,IsNull(Sum(LandedSales),0)LandedSales,IsNull(sum(Sales),0) as Sales
     From  #CurRenew
     Group by  AccountID, AccountName, FMID, FMName
     Order by FMID,AccountID 

  Open c1  
    Fetch Next From c1 Into @acid , @acname , @fmid  , @fmname ,@Onlinesales,@Landedsales, @sales 
    While @@fetch_status = 0
    Begin
      Insert into #Renew ( AccountID,AccountName,FMID,FMName,LastOnlineSales, LastLandedSales,LastSales) 
	          Values (@acid , @acname , @fmid  , @fmname ,@Onlinesales,@Landedsales, @sales ) 
      Fetch Next From c1 Into @acid , @acname , @fmid  , @fmname , @Onlinesales,@Landedsales,@sales  
    End 
  Close c1
  Deallocate c1

   Set @acid = null
   Set @acname  = null
   Set @fmid  = null
   Set @fmname = null
   Set @sales  = null 
   Set @RecExist = 0
   Set @Onlinesales=0
   Set @Landedsales=0

    Open c2  
    Fetch next from c2 into @acid , @acname , @fmid  , @fmname , @Onlinesales,@Landedsales, @sales 
    While @@fetch_status = 0
    Begin

    Select @RecExist = 1
    From #Renew
    Where AccountID = @acid
     And fmid = @fmid

     If @RecExist  = 1
     Begin
	Update #Renew 
	Set CurSales = @sales,
	      CurOnlineSales=@Onlinesales,
	      CurlandedSales=@Landedsales
    	Where AccountID = @acid
     	And fmid = @fmid
       End 
   Else
    Begin     
             Insert Into #Renew ( AccountID,AccountName,FMID,FMName,CurOnlineSales,CurlandedSales,CurSales) 
	Values (@acid , @acname , @fmid  , @fmname ,@Onlinesales, @Landedsales,@sales ) 
    End 

    Set @RecExist = 0 --initializing variable in loop
    Fetch Next From c2 Into @acid , @acname , @fmid  , @fmname , @Onlinesales,@Landedsales ,@sales  
  End 
  Close c2
  Deallocate c2

-----now return data to report
 	Select   @CurrentFiscalYear as CurrentFiscalYear, 
	 	@LastFiscalYear as LastFiscalYear, 
		'1Renewed' as Type,
		 AccountID,
		 AccountName,
		 FMID,
		 FMName,
		 IsNull(LastOnlineSales,0)as LastOnlineSales,
		 IsNull(LastLandedSales,0)as LastLandedSales,
		 IsNull(LastSales,0)  as LastSales,
		 IsNull(CurOnlineSales,0)as CurOnlineSales,
		 IsNull(CurLandedSales,0)as CurLandedSales,
		 IsNull(CurSales,0)   as CurSales, 
	 	@CurSum as CurSum,
		@LastSum as LastSum,
	 	@RenewedDiff as RenewedDiff,
	 	@DiffPercent as DiffPercent,
	 	@LastWholeSum as LastWholeSum,
	 	@NewSum as NewSum
  	From   #Renew

  	Union All
	Select 	@CurrentFiscalYear as CurrentFiscalYear, 
		@LastFiscalYear as LastFiscalYear, 
		 Type,
	 	AccountID,
		AccountName,
		FMID,
		FMName,
		--null as LastSales,
		0 as LastOnlineSales,
		0 as LastLandedSales,
		0 as LastSales,
		IsNull(sum(OnlineSales),0)CurOnlineSales,
		IsNull(sum(LandedSales),0)CurLandedSales,
		isNull(sum(Sales),0) as CurSales,
		@CurSum as CurSum,
		@LastSum as LastSum,
		@RenewedDiff as RenewedDiff,
		@DiffPercent as DiffPercent,
		@LastWholeSum as LastWholeSum,
		@NewSum as NewSum
	From #EstCurNew 
	Group by Type,AccountID,AccountName,	FMID,FMName

  	Union All
   	Select 	@CurrentFiscalYear as CurrentFiscalYear, 
		@LastFiscalYear as LastFiscalYear, 
		Type,
	 	AccountID,
		AccountName,
		FMID,
		FMName,
		--null as LastSales,
		0 as LastOnlineSales,
		0 as LastLandedSales,
		0 as LastSales,
		IsNull(sum(OnlineSales),0)CurOnlineSales,
		IsNull(sum(LandedSales),0)CurLandedSales,
		IsNull(sum(Sales),0) as CurSales,
		@CurSum as CurSum,
		@LastSum as LastSum,
		@RenewedDiff as RenewedDiff,
		@DiffPercent as DiffPercent,
		@LastWholeSum as LastWholeSum,
		@NewSum as NewSum
	From #CurNew 
	Group by Type,AccountID,AccountName,	FMID,FMName

	Union All
   	Select 	@CurrentFiscalYear as CurrentFiscalYear, 
		@LastFiscalYear as LastFiscalYear, 
		Type,
	 	AccountID,
		AccountName,
		FMID,
		FMName,
		IsNull(sum(OnlineSales),0)as LastOnlineSales,
		IsNull(sum(LandedSales),0)as LastLandedSales,
		IsNull(sum(Sales),0)  as LastSales,
		--null as CurSales,
		0 as CurOnlineSales,
		0 as CurlandedSales,
		0 as CurSales,
		@CurSum as CurSum,
		@LastSum as LastSum,
		@RenewedDiff as RenewedDiff,
		@DiffPercent as DiffPercent,
		@LastWholeSum as LastWholeSum,
		@NewSum as NewSum
	 From #EstLastExpired 
	Group by Type,AccountID,AccountName,	FMID,FMName

       Order by  FMName,Type, AccountName    

------------------------------------------------

--drop all temp tables Created
Drop table #OldAccounts
Drop table #NewAccounts
Drop table #CurAccounts
Drop Table #CurNewByQualifier
Drop Table #CurNew
Drop Table #EstCurNew
Drop Table #CurRenew
Drop Table #CurRenewByQualifier
Drop Table #EstCurRenew
Drop Table #EstLastExpiredByQualifier
Drop Table #EstLastExpired
Drop Table #EstLastRenewByQualifier
Drop Table #EstLastRenew
Drop Table #Renew
GO
