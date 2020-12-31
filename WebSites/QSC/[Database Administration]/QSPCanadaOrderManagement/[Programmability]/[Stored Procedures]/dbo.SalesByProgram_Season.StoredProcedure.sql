USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[SalesByProgram_Season]    Script Date: 06/07/2017 09:20:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SalesByProgram_Season] @pFromAccountingPeriod int, @pToAccountingPeriod int 

AS
Set FMTONLY Off
Declare @pStartDate datetime, @pEndDate datetime, @pFMID varchar(10) 

Select @pStartDate='7/1/13'
Select @pEndDate='06/30/14 23:59:59'

Declare @LastYear		   Int,  
	@CurrYear              Int,
	@LastSeason 		   Char(1), 
	@LastSeasonStartDate 	   Datetime , 
	@LastSeasonEndDate 	   Datetime, 
	@CurrentSeasonStartDate Datetime , 
	@CurrentSeasonEndDate  Datetime, 
    	@CurrentFiscalYear 	   Varchar(4), 
	@LastFiscalYear 	   Varchar(10),
	@PointInTime           datetime,
	@FromAccountingPeriod	int,
	@ToAccountingPeriod	int

Declare  @CurSum 		Numeric(14,2), 
	 @LastSum  		Numeric(14,2), 
	 @RenewedDiff  	Numeric(14,2), 
	 @DiffPercent 		Numeric(14,2)  ,
	 @LastWholeSum  	Numeric(14,2),
	 @ExpiredSum 		Numeric(14,2)
Declare  @NewSum 		Numeric(14,2)

Select @PointInTime = CONVERT(VARCHAR(10), DATEADD(dd, 1, GETDATE()), 120)
Select @PointInTime = dateadd( year, -1, @PointInTime)
print @PointInTime

Set @CurrentFiscalYear 	=  'FY2013'
Set @LastFiscalYear 	=  'FY2012'
SELECT @CurrYear = DATEPART(YYYY,GETDATE())
SELECT @LastYear = DATEPART(YYYY,GETDATE())-1
Set @FromAccountingPeriod = @pFromAccountingPeriod
Set @ToAccountingPeriod	= @pToAccountingPeriod


   Set @CurrentSeasonStartDate =  @pStartDate
   Set @CurrentSeasonEndDate  =  @pEndDate

   Set @LastSeasonStartDate = DateAdd(yy,-1,@pStartDate ) 
   Set @LastSeasonEndDate = DateAdd(yy,-1,@pEndDate ) 

Print @LastYear
PRINT @CurrYear
PRINT @CurrentSeasonStartDate
PRINT @CurrentSeasonEndDate
PRINT @LastSeasonStartDate
PRINT @LastSeasonEndDate

Create Table #S 	(
			 CampaignID  int,
			 FiscalYear 	Varchar(20),
			 Programs Varchar(100),
			 PrizePrograms Varchar(100),
			 OnlineUnits  int,
			 OnlineSales 	Numeric(14,2),
			 LandedMagUnits int,
			 LandedSales 	Numeric(14,2),
			 GiftUnits int,
			 LandedGift Numeric(14,2),
			 CDUnits int,			 
			 LandedCD Numeric(14,2),
			 ChocUnits int,
			 LandedChoc Numeric(14,2),
			 JewelryUnits int,
			 LandedJewelry Numeric(14,2),
			 StudentCount int)
/*
** Full Last Year
*/

insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	,
			 LandedMagUnits ,
			 LandedSales 	,
			 GiftUnits ,
			 LandedGift ,
			 CDUnits ,			 
			 LandedCD ,
			 ChocUnits ,
			 LandedChoc,
			 JewelryUnits ,
			 LandedJewelry ,
			 StudentCount
)			 
Select ca.id,
		@LastFiscalYear
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36001) 
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36003) 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale
		,Landed.Units as LandedMagUnits
		,Landed.NetSale as LandedMagNetSale
		,Gift.Units as GiftUnits
		,Gift.NetSale as GiftNetSale
		,CD.Units as CDUnits
		,CD.NetSale as CDNetSale
		,Choc.Units as ChocUnits
		,Choc.NetSale as ChocNetSale
		,Jewelry.Units as JewelryUnits
		,Jewelry.NetSale as JewelryNetSale
		,(
			Select isnull(count(distinct studentinstance),0) from batch 
				join customerorderheader coh on orderbatchid = id and orderbatchdate = date
				join qspcanadafinance.dbo.invoice i on i.order_id = orderid
				join qspcanadafinance.dbo.gl_entry ge on i.invoice_id = ge.invoice_id
				--JOIN QSpCanadaFinance.dbo.vw_GAO_Mapping_Account_Year vw2 
				--	ON vw2.Historical_Account_Year = ge.accounting_year AND vw2.ACCOUNTING_PERIOD = ge.ACCOUNTING_PERIOD
				where orderqualifierid in (39009, 39001, 39002, 39015, 39020) 
					and batch.campaignid = ca.id  and batch.statusinstance <> 40005
					and ge.ACCOUNTING_YEAR = @LastYear
					and ge.Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
					--and ge.GL_ENTRY_DATE between @LastSeasonStartDate and @LastSeasonEndDate
		) as StudentCount
		from qspcanadacommon.dbo.campaign as ca 
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and accounting_year = @LastYear
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)internet 	on ca.id = internet.campaignid
		left join
		(
			Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid in(39001, 39002, 39015, 39020) 
								 and section_type_id = 2
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Landed on ca.id = Landed.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting							
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 1
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Gift
		on ca.id = Gift.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 9
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)CD
		on ca.id = CD.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 10
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Choc
		on ca.id = Choc.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 11
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Jewelry
		on ca.id = Jewelry.campaignid	
			
	where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 0
		and ca.status <> 37005
		and ca.id in 
			( Select distinct campaignid 
			    from QSpCanadaFinance.dbo.vw_GetNetForReporting 
			   where accounting_year in (@LastYear) 								 
			     and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
			     and orderqualifierid in(39001, 39002, 39015, 39020, 39009) )
	order by ca.id
	
--Staff	full last year
	
insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	
		  )	
Select ca.id,
		@LastFiscalYear
		,'Staff' 
		,'NA' 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale	
	from qspcanadacommon.dbo.campaign as ca 
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)internet 	on ca.id = internet.campaignid		
where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 1
		and ca.status <> 37005
		and ca.id in 
		     ( Select distinct campaignid 
			 from QSpCanadaFinance.dbo.vw_GetNetForReporting 
			where accounting_year in (@LastYear ) 
			  and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
			  and orderqualifierid in(39001, 39002, 39015, 39020, 39009) )
	order by ca.id		
		
/**
** Point in time last year
*/


insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	,
			 LandedMagUnits ,
			 LandedSales 	,
			 GiftUnits ,
			 LandedGift ,
			 CDUnits ,			 
			 LandedCD ,
			 ChocUnits ,
			 LandedChoc,
			 JewelryUnits ,
			 LandedJewelry ,
			 StudentCount
)	
		 
Select ca.id,
		'PointInTime'
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36001) 
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36003) 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale
		,Landed.Units as LandedMagUnits
		,Landed.NetSale as LandedMagNetSale
		,Gift.Units as GiftUnits
		,Gift.NetSale as GiftNetSale
		,CD.Units as CDUnits
		,CD.NetSale as CDNetSale
		,Choc.Units as ChocUnits
		,Choc.NetSale as ChocNetSale
		,Jewelry.Units as JewelryUnits
		,Jewelry.NetSale as JewelryNetSale
		,(
			Select isnull(count(distinct studentinstance),0) from batch join customerorderheader coh on orderbatchid = id and orderbatchdate = date
				join qspcanadafinance.dbo.invoice i on i.order_id = orderid
				join qspcanadafinance.dbo.gl_entry ge on i.invoice_id = ge.invoice_id
				--JOIN QSpCanadaFinance.dbo.vw_GAO_Mapping_Account_Year vw2 
				--	ON vw2.Historical_Account_Year = ge.accounting_year AND vw2.ACCOUNTING_PERIOD = ge.ACCOUNTING_PERIOD
				where orderqualifierid in (39009, 39001, 39002, 39015, 39020) 
					and batch.campaignid = ca.id  and batch.statusinstance <> 40005
					and gl_entry_date <= @PointInTime	
					and ge.ACCOUNTING_YEAR = @LastYear	
			     		and ge.Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
		) as StudentCount
		from qspcanadacommon.dbo.campaign as ca 
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)internet 	on ca.id = internet.campaignid
		left join
		(
			Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020) 
								 and section_type_id = 2
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Landed on ca.id = Landed.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 1
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Gift
		on ca.id = Gift.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 9
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)CD
		on ca.id = CD.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 10
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Choc
		on ca.id = Choc.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 11
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Jewelry
		on ca.id = Jewelry.campaignid	
			
	where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		 isstafforder = 0
--		and ca.id = 79202
		and ca.status <> 37005
		and ca.id in 
			( Select distinct campaignid 
			    from QSpCanadaFinance.dbo.vw_GetNetForReporting 
			   where accounting_year in (@LastYear ) 
			     and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
			     and orderqualifierid in(39001, 39002, 39015, 39020, 39009)
			     and invoice_date <= @PointInTime )
	order by ca.id

--Staff	point in time
	
insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	
		  )	
Select ca.id,
		'PointInTime'
		,'Staff' 
		,'NA' 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale	
	from qspcanadacommon.dbo.campaign as ca 
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting	
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)internet 	on ca.id = internet.campaignid		
where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 1
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting
				where accounting_year in (@LastYear ) 
			     	and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
				and orderqualifierid in(39001, 39002, 39015, 39020, 39009)
							 and invoice_date <= @PointInTime )
	order by ca.id		

/*
** Full Current Year
*/

insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	,
			 LandedMagUnits ,
			 LandedSales 	,
			 GiftUnits ,
			 LandedGift ,
			 CDUnits ,			 
			 LandedCD ,
			 ChocUnits ,
			 LandedChoc,
			 JewelryUnits ,
			 LandedJewelry ,
			 StudentCount
)			 
Select ca.id,
		@CurrentFiscalYear
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36001) 
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36003) 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale
		,Landed.Units as LandedMagUnits
		,Landed.NetSale as LandedMagNetSale
		,Gift.Units as GiftUnits
		,Gift.NetSale as GiftNetSale
		,CD.Units as CDUnits
		,CD.NetSale as CDNetSale
		,Choc.Units as ChocUnits
		,Choc.NetSale as ChocNetSale
		,Jewelry.Units as JewelryUnits
		,Jewelry.NetSale as JewelryNetSale
		,(
			Select isnull(count(distinct studentinstance),0) from batch join customerorderheader coh on orderbatchid = id and orderbatchdate = date
				join qspcanadafinance.dbo.invoice i on i.order_id = orderid
				join qspcanadafinance.dbo.gl_entry ge on i.invoice_id = ge.invoice_id		
				--JOIN QSpCanadaFinance.dbo.vw_GAO_Mapping_Account_Year vw2 
				--	ON vw2.Historical_Account_Year = ge.accounting_year AND vw2.ACCOUNTING_PERIOD = ge.ACCOUNTING_PERIOD
				where orderqualifierid in (39009, 39001, 39002, 39015, 39020) 
					and batch.campaignid = ca.id  and batch.statusinstance <> 40005
					and ge.ACCOUNTING_YEAR = @CurrYear
			     		and ge.Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
		) as StudentCount
		from qspcanadacommon.dbo.campaign as ca 
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)internet 	on ca.id = internet.campaignid
		left join
		(
			Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020) 
								 and section_type_id = 2
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Landed on ca.id = Landed.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 1
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Gift
		on ca.id = Gift.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 9
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)CD
		on ca.id = CD.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 10
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Choc
		on ca.id = Choc.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 11
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Jewelry
		on ca.id = Jewelry.campaignid		
	where --startdate between @CurrentSeasonStartDate and  @CurrentSeasonEndDate
		isstafforder = 0
		and ca.status <> 37005
		and ca.id in 
			( Select distinct campaignid 
			from QSpCanadaFinance.dbo.vw_GetNetForReporting 
			where accounting_year in ( @CurrYear ) 
			and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
			and orderqualifierid in(39001, 39002, 39015, 39020, 39009) )
		
	order by ca.id

--Current staff

insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	
		  )	
Select ca.id,
		@CurrentFiscalYear
		,'Staff' 
		,'NA' 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale	
	from qspcanadacommon.dbo.campaign as ca 
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting							
								 where orderqualifierid = 39009 
								 and section_type_id = 2								
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)internet 	on ca.id = internet.campaignid		
		where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 1
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting
				where accounting_year in (@CurrYear ) 
			     	and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
				and orderqualifierid in(39001, 39002, 39015, 39020, 39009)	)
	order by ca.id		


Select distinct s1.Programs, s1.PrizePrograms
			,PriorYearPointInTime.CampaignCount as PointInTimeCampaignCount
			,PriorYearPointInTime.studentcount as PointInTimeStudentcount
			,PriorYearPointInTime.OnlineUnits as PointInTimeOnlineUnits,
			PriorYearPointInTime.OnlineSales as PointInTimeOnlineSales
			,PriorYearPointInTime.LandedSales as PointInTimeTotalLandedSales
			,PriorYearPointInTime.LandedMagUnits as PointInTimeTotalLandedMagUnits
			,PriorYearPointInTime.LandedGift as PointInTimeTotalLandedGift
			,PriorYearPointInTime.GiftUnits as PointInTimeTotalGiftUnits
			,PriorYearPointInTime.LandedCD as PointInTimeTotalLandedCD
			,PriorYearPointInTime.CDUnits as PointInTimeTotalCDUnits
			,PriorYearPointInTime.LandedChoc as PointInTimeTotalLandedChoc
			,PriorYearPointInTime.ChocUnits as PointInTimeTotalChocUnits
			,PriorYearPointInTime.LandedJewelry as PointInTimeTotalLandedJewelry
			,PriorYearPointInTime.JewelryUnits		 as PointInTimeTotalJewelryUnits			
			,PriorYearPointInTime.Total as PointInTimeTotal
			,CurrentYearTotal.CampaignCount as CurrentYearTotalCampaignCount
			,CurrentYearTotal.studentcount as CurrentYearTotalStudentcount
			,CurrentYearTotal.OnlineUnits as CurrentYearTotalOnlineUnits,
			CurrentYearTotal.OnlineSales as CurrentYearTotalOnlineSales
			,CurrentYearTotal.LandedSales as CurrentYearTotalLandedSales
			,CurrentYearTotal.LandedMagUnits as CurrentYearTotalLandedMagUnits
			,CurrentYearTotal.LandedGift as CurrentYearTotalLandedGift
			,CurrentYearTotal.GiftUnits as CurrentYearTotalGiftUnits
			,CurrentYearTotal.LandedCD as CurrentYearTotalLandedCD
			,CurrentYearTotal.CDUnits as CurrentYearTotalCDUnits
			,CurrentYearTotal.LandedChoc as CurrentYearTotalLandedChoc
			,CurrentYearTotal.ChocUnits as CurrentYearTotalChocUnits
			,CurrentYearTotal.LandedJewelry as CurrentYearTotalLandedJewelry
			,CurrentYearTotal.JewelryUnits		 as CurrentYearTotalJewelryUnits			
			,CurrentYearTotal.Total as CurrentYearTotal
			,PriorYearTotal.CampaignCount as PriorYearTotalCampaignCount
			,PriorYearTotal.studentcount as PriorYearTotalStudentcount
			,PriorYearTotal.OnlineUnits as PriorYearTotalOnlineUnits,
			PriorYearTotal.OnlineSales as PriorYearTotalOnlineSales
			,PriorYearTotal.LandedSales as PriorYearTotalLandedSales
			,PriorYearTotal.LandedMagUnits as PriorYearTotalLandedMagUnits
			,PriorYearTotal.LandedGift as PriorYearTotalLandedGift
			,PriorYearTotal.GiftUnits as PriorYearTotalGiftUnits
			,PriorYearTotal.LandedCD as PriorYearTotalLandedCD
			,PriorYearTotal.CDUnits as PriorYearTotalCDUnits
			,PriorYearTotal.LandedChoc as PriorYearTotalLandedChoc
			,PriorYearTotal.ChocUnits as PriorYearTotalChocUnits
			,PriorYearTotal.LandedJewelry as PriorYearTotalLandedJewelry
			,PriorYearTotal.JewelryUnits		 as PriorYearJewelryUnits			
			,PriorYearTotal.Total as PriorYearTotal
			from #s s1
		cross apply
		(
		select  count(campaignid) as CampaignCount
				,isnull(sum(studentcount),0) as StudentCount
				,isnull(sum(onlinesales),0) as OnlineSales
				,isnull(sum(onlineunits),0) as OnlineUnits
				,isnull(sum(LandedSales),0) as LandedSales
				,isnull(sum(LandedMagUnits),0) as LandedMagUnits
				,isnull(sum(LandedGift),0) as LandedGift
				,isnull(sum(GiftUnits),0) as GiftUnits
				,isnull(sum(LandedCD),0) as LandedCD
				,isnull(sum(CDUnits),0) as CDUnits
				,isnull(sum(LandedChoc),0) as LandedChoc
				,isnull(sum(ChocUnits),0) as ChocUnits
				,isnull(sum(LandedJewelry),0) as LandedJewelry
				,isnull(sum(JewelryUnits),0) as JewelryUnits
				,isnull((isnull(sum(onlinesales),0)+ isnull(sum(landedsales),0)+ isnull(sum(landedgift),0)+isnull(sum(landedchoc),0)+ isnull(sum(landedcd),0)+ isnull(sum(landedjewelry),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear=@LastFiscalYear
		)PriorYearTotal
		cross apply
		(
		select  count(campaignid) as CampaignCount
				,isnull(sum(studentcount),0) as StudentCount	
				,isnull(sum(onlinesales),0) as OnlineSales
				,isnull(sum(onlineunits),0) as OnlineUnits
				,isnull(sum(LandedSales),0) as LandedSales
				,isnull(sum(LandedMagUnits),0) as LandedMagUnits
				,isnull(sum(LandedGift),0) as LandedGift
				,isnull(sum(GiftUnits),0) as GiftUnits
				,isnull(sum(LandedCD),0) as LandedCD
				,isnull(sum(CDUnits),0) as CDUnits
				,isnull(sum(LandedChoc),0) as LandedChoc
				,isnull(sum(ChocUnits),0) as ChocUnits
				,isnull(sum(LandedJewelry),0) as LandedJewelry
				,isnull(sum(JewelryUnits),0) as JewelryUnits
				,isnull((isnull(sum(onlinesales),0)+ isnull(sum(landedsales),0)+ isnull(sum(landedgift),0)+isnull(sum(landedchoc),0)+ isnull(sum(landedcd),0)+ isnull(sum(landedjewelry),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear=@CurrentFiscalYear
		)CurrentYearTotal
				cross apply
		(
		select  count(campaignid) as CampaignCount
				,isnull(sum(studentcount),0) as StudentCount	
				,isnull(sum(onlinesales),0) as OnlineSales
				,isnull(sum(onlineunits),0) as OnlineUnits
				,isnull(sum(LandedSales),0) as LandedSales
				,isnull(sum(LandedMagUnits),0) as LandedMagUnits
				,isnull(sum(LandedGift),0) as LandedGift
				,isnull(sum(GiftUnits),0) as GiftUnits
				,isnull(sum(LandedCD),0) as LandedCD
				,isnull(sum(CDUnits),0) as CDUnits
				,isnull(sum(LandedChoc),0) as LandedChoc
				,isnull(sum(ChocUnits),0) as ChocUnits
				,isnull(sum(LandedJewelry),0) as LandedJewelry
				,isnull(sum(JewelryUnits),0) as JewelryUnits
				,isnull((isnull(sum(onlinesales),0)+ isnull(sum(landedsales),0)+ isnull(sum(landedgift),0)+isnull(sum(landedchoc),0)+ isnull(sum(landedcd),0)+ isnull(sum(landedjewelry),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear='PointInTime'
		)PriorYearPointInTime
	order by s1.Programs, s1.PrizePrograms
	
--select * from #s where programs=' Mag Exp'  and fiscalyear='FY13' and landedsales is not null order by programs, prizeprograms

--drop Table #S
GO
