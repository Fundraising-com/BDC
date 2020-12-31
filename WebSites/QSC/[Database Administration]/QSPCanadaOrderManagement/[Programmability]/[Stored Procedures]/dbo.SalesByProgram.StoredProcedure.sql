USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[SalesByProgram]    Script Date: 06/07/2017 09:20:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SalesByProgram] --@pFromAccountingPeriod int, @pToAccountingPeriod int 

AS
Set FMTONLY Off
Declare @pStartDate datetime, @pEndDate datetime, @pFMID varchar(10) 

--Select @pStartDate='7/1/13'
--Select @pEndDate='06/30/14 23:59:59'

Declare @LastYear		   Int,  
	@CurrYear              Int,
	@LastSeason 		   Char(1), 
	--@LastSeasonStartDate 	   Datetime , 
	--@LastSeasonEndDate 	   Datetime, 
	--@CurrentSeasonStartDate Datetime , 
	--@CurrentSeasonEndDate  Datetime, 
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

Set @CurrentFiscalYear 	=  'FY2017'
Set @LastFiscalYear 	=  'FY2016'

--Use when in Spring
SELECT @CurrYear = DATEPART(YYYY,GETDATE())
SELECT @LastYear = DATEPART(YYYY,GETDATE())-1
Set @FromAccountingPeriod = 7
Set @ToAccountingPeriod	= 12

--Use when in Fall
--SELECT @CurrYear = DATEPART(YYYY,GETDATE())+1
--SELECT @LastYear = DATEPART(YYYY,GETDATE())
--Set @FromAccountingPeriod = 1
--Set @ToAccountingPeriod	= 6



   --Set @CurrentSeasonStartDate =  @pStartDate
   --Set @CurrentSeasonEndDate  =  @pEndDate

   --Set @LastSeasonStartDate = DateAdd(yy,-1,@pStartDate ) 
   --Set @LastSeasonEndDate = DateAdd(yy,-1,@pEndDate ) 

Print @LastYear
PRINT @CurrYear
--PRINT @CurrentSeasonStartDate
--PRINT @CurrentSeasonEndDate
--PRINT @LastSeasonStartDate
--PRINT @LastSeasonEndDate

Create Table #S 	(
			 CampaignID  int,
			 FiscalYear 	Varchar(20),
			 Programs Varchar(100),
			 PrizePrograms Varchar(100),
			 OnlineMagUnits  int,
			 OnlineMagSales  Numeric(14,2),
			 LandedMagUnits int,
			 LandedMagSales Numeric(14,2),
			 LandedGiftUnits int,
			 LandedGiftSales Numeric(14,2),
			 LandedCDUnits int,			 
			 LandedCDSales Numeric(14,2),
			 LandedCandleUnits int,
			 LandedCandleSales Numeric(14,2),
			 OnlineTrtUnits int,
			 OnlineTrtSales Numeric(14,2),
			 LandedTrtUnits int,
			 LandedTrtSales Numeric(14,2),
			OnlineEntertainmentUnits int,
			OnlineEntertainmentSales Numeric(14,2),
			LandedEntertainmentUnits int,
			LandedEntertainmentSales Numeric(14,2),
			 LandedJewelryUnits int,
			 LandedJewelrySales Numeric(14,2),
			 StudentCount int)
/*
** Full Last Year
*/

insert #S(CampaignID  ,
			 FiscalYear ,
			 Programs ,
			 PrizePrograms ,
			 OnlineMagUnits  ,
			 OnlineMagSales 	,
			 LandedMagUnits ,
			 LandedMagSales 	,
			 LandedGiftUnits ,
			 LandedGiftSales ,
			 LandedCDUnits ,			 
			 LandedCDSales ,
			 LandedCandleUnits ,
			 LandedCandleSales,
			 OnlineTRTUnits,
			 OnlineTRTSales,
			 LandedTrtUnits,
			 LandedTrtSales,
			 OnlineEntertainmentUnits,
			 OnlineEntertainmentSales,
			 LandedEntertainmentUnits,
			 LandedEntertainmentSales,
			 LandedJewelryUnits ,
			 LandedJewelrySales ,
			 StudentCount
)			 
Select ca.id,
		@LastFiscalYear
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36001) 
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36003) 
		,OnlineMag.Units as OnlineMagUnits
		,OnlineMag.NetSale as OnlineMagSales
		,LandedMag.Units as LandedMagUnits
		,LandedMag.NetSale as LandedMagNetSales
		,Gift.Units as LandedGiftUnits
		,Gift.NetSale as LandedGiftSales
		,CD.Units as LandedCDUnits
		,CD.NetSale as LandedCDSale
		,Candle.Units as LandedCandleUnits
		,Candle.NetSale as LandedCandleSales		
		,OnlineTRT.Units as OnlineTRTUnits
		,OnlineTRT.NetSale as OnlineTRTSales
		,LandedTrt.Units as LandedTrtUnits
		,LandedTrt.NetSale as LandedTrtSales
		,OnlineEntertainment.Units as OnlineEntertainmentUnits
		,OnlineEntertainment.NetSale as OnlineEntertainmentSales
		,LandedEntertainment.Units as LandedEntertainmentUnits
		,LandedEntertainment.NetSale as LandedEntertainmentSales
		,Jewelry.Units as LandedJewelryUnits
		,Jewelry.NetSale as LandedJewelrySales
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
		)OnlineMag 	on ca.id = onlineMag.campaignid
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
				
		)LandedMag on ca.id = LandedMag.campaignid
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
								 and section_type_id = 13
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Candle
		on ca.id = Candle.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 14
								 and accounting_year = @LastYear
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineTRT 	on ca.id = OnlineTRT.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 14
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)LandedTrt	on ca.id = LandedTrt.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 15
								 and accounting_year = @LastYear
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineEntertainment 	on ca.id = OnlineEntertainment.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 15
								 and accounting_year = @LastYear							 
								 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)LandedEntertainment	on ca.id = LandedEntertainment.campaignid
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
			 OnlineMagUnits  ,
			 OnlineMagSales ,
			 OnlineTrtUnits,
			 OnlineTrtSales,
			 OnlineEntertainmentUnits,
			 OnlineEntertainmentSales		
		  )	
Select ca.id,
		@LastFiscalYear
		,'Staff' 
		,'NA' 
		,OnlineMag.Units as OnlineMagUnits
		,OnlineMag.NetSale as OnlineMagSales
		,OnlineTrt.Units as OnlineTrtUnits
		,OnlineTrt.NetSale as OnlineTrtSales
		,OnlineEntertainment.Units as OnlineEntertainmentUnits
		,OnlineEntertainment.NetSale as OnlineEntertainmentSales
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
		)OnlineMag 	on ca.id = OnlineMag.campaignid		
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 14
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineTrt 	on ca.id = OnlineTrt.campaignid		
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 15
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineEntertainment 	on ca.id = OnlineEntertainment.campaignid		
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
			 OnlineMagUnits  ,
			 OnlineMagSales 	,
			 LandedMagUnits ,
			 LandedMagSales 	,
			 LandedGiftUnits ,
			 LandedGiftSales ,
			 LandedCDUnits ,			 
			 LandedCDSales ,
			 LandedCandleUnits ,
			 LandedCandleSales,
			 OnlineTrtUnits,
			 OnlineTrtSales,
			 LandedTrtUnits,
			 LandedTrtSales,
			 OnlineEntertainmentUnits,
			 OnlineEntertainmentSales,
			 LandedEntertainmentUnits,
			 LandedEntertainmentSales,
			 LandedJewelryUnits ,
			 LandedJewelrySales ,
			 StudentCount
)	
		 
Select ca.id,
		'PointInTime'
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36001) 
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36003) 
		,OnlineMag.Units as OnlineMagUnits
		,OnlineMag.NetSale as OnlineMagSales
		,LandedMag.Units as LandedMagUnits
		,LandedMag.NetSale as LandedMagNetSale
		,Gift.Units as LandedGiftUnits
		,Gift.NetSale as LandedGiftSale
		,CD.Units as LandedCDUnits
		,CD.NetSale as LandedCDSales
		,Candle.Units as LandedCandleUnits
		,Candle.NetSale as LandedCandleSales
		,OnlineTRT.Units as OnlineTRTUnits
		,OnlineTRT.NetSale as OnlineTRTSales
		,LandedTrt.Units as LandedTrtUnits
		,LandedTrt.NetSale as LandedTrtSales
		,OnlineEntertainment.Units as OnlineEntertainmentUnits
		,OnlineEntertainment.NetSale as OnlineEntertainmentSales
		,LandedEntertainment.Units as LandedEntertainmentUnits
		,LandedEntertainment.NetSale as LandedEntertainmentSales
		,Jewelry.Units as LandedJewelryUnits
		,Jewelry.NetSale as LandedJewelryNetSale
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
		)OnlineMag 	on ca.id = OnlineMag.campaignid
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
				
		)LandedMag on ca.id = LandedMag.campaignid
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
								 and section_type_id = 13
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Candle
		on ca.id = Candle.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 14
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineTRT 	on ca.id = OnlineTRT.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 14
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)LandedTrt
		on ca.id = LandedTrt.campaignid		
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 15
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineEntertainment 	on ca.id = OnlineEntertainment.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 15
								 and invoice_date <= @PointInTime	
								 and accounting_year = @LastYear								 						 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)LandedEntertainment
		on ca.id = LandedEntertainment.campaignid		
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
			 OnlineMagUnits  ,
			 OnlineMagSales ,
			 OnlineTrtUnits,
			 OnlineTrtSales,	
			 LandedTrtUnits,
			 LandedTrtSales,
			 OnlineEntertainmentUnits,
			 OnlineEntertainmentSales,	
			 LandedEntertainmentUnits,
			 LandedEntertainmentSales		
		  )	
Select ca.id,
		'PointInTime'
		,'Staff' 
		,'NA' 
		,OnlineMag.Units as OnlineMagUnits
		,OnlineMag.NetSale as OnlineMagSales
		,OnlineTrt.Units as OnlineTrtUnits
		,OnlineTrt.NetSale as OnlineTRTSales	
		,LandedTrt.Units as LandedTrtUnits
		,LandedTrt.NetSale as LandedTRTSales
		,OnlineEntertainment.Units as OnlineEntertainmentUnits
		,OnlineEntertainment.NetSale as OnlineEntertainmentSales	
		,LandedEntertainment.Units as LandedEntertainmentUnits
		,LandedEntertainment.NetSale as LandedEntertainmentSales	
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
		)OnlineMag 	on ca.id = OnlineMag.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting	
								 where orderqualifierid = 39009 
								 and section_type_id = 14
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineTRT 	on ca.id = OnlineTRT.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting	
								 where orderqualifierid = 39009 
								 and section_type_id = 14
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)LandedTrt 	on ca.id = LandedTrt.campaignid		
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting	
								 where orderqualifierid = 39009 
								 and section_type_id = 15
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineEntertainment 	on ca.id = OnlineEntertainment.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting	
								 where orderqualifierid = 39009 
								 and section_type_id = 15
								 and invoice_date <= @PointInTime
								 and accounting_year = @LastYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)LandedEntertainment 	on ca.id = LandedEntertainment.campaignid		
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
			 OnlineMagUnits  ,
			 OnlineMagSales 	,
			 LandedMagUnits ,
			 LandedMagSales 	,
			 LandedGiftUnits ,
			 LandedGiftSales ,
			 LandedCDUnits ,			 
			 LandedCDSales ,
			 LandedCandleUnits,
			 LandedCandleSales,
			 OnlineTrtUnits,
			 OnlineTrtSales,
			 LandedTrtUnits,
			 LandedTrtSales,
			 OnlineEntertainmentUnits,
			 OnlineEntertainmentSales,
			 LandedEntertainmentUnits,
			 LandedEntertainmentSales,
			 LandedJewelryUnits ,
			 LandedJewelrySales ,
			 StudentCount
)			 
Select ca.id,
		@CurrentFiscalYear
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36001) 
		,QSPCanadaCommon.dbo.UDF_GetCampaignProgramsByType(ca.id, 36003) 
		,OnlineMag.Units as OnlineMagUnits
		,OnlineMag.NetSale as OnlineMagSale
		,LandedMag.Units as LandedMagUnits
		,LandedMag.NetSale as LandedMagNetSale
		,Gift.Units as LandedGiftUnits
		,Gift.NetSale as LandedGiftSales
		,CD.Units as CDUnits
		,CD.NetSale as CDSales
		,Candle.Units as CandleUnits
		,Candle.NetSale as CandleSales
		,OnlineTrt.Units as OnlineTrtUnits
		,OnlineTrt.NetSale as OnlineTrtSales
		,LandedTrt.Units as LandedTrtUnits
		,LandedTrt.NetSale as LandedTrtSales
		,OnlineEntertainment.Units as OnlineEntertainmentUnits
		,OnlineEntertainment.NetSale as OnlineEntertainmentSales
		,LandedEntertainment.Units as LandedEntertainmentUnits
		,LandedEntertainment.NetSale as LandedEntertainmentSales
		,Jewelry.Units as JewelryUnits
		,Jewelry.NetSale as JewelrySales
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
		)OnlineMag 	on ca.id = OnlineMag.campaignid
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
				
		)LandedMag on ca.id = LandedMag.campaignid
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
								 and section_type_id = 13
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)Candle
		on ca.id = Candle.campaignid
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 14
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineTRT 	on ca.id = OnlineTRT.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 14
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)LandedTrt
		on ca.id = LandedTrt.campaignid		
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid = 39009 
								 and section_type_id = 15
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineEntertainment 	on ca.id = OnlineEntertainment.campaignid
		left join
		(
			Select campaignid	
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting
								 where orderqualifierid in(39001, 39002, 39015, 39020)
								 and section_type_id = 15
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
				
		)LandedEntertainment
		on ca.id = LandedEntertainment.campaignid		
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
			 OnlineMagUnits  ,
			 OnlineMagSales ,
			 OnlineTrtUnits,
			 OnlineTrtSales,
			 OnlineEntertainmentUnits,
			 OnlineEntertainmentSales
		  )	
Select ca.id,
		@CurrentFiscalYear
		,'Staff' 
		,'NA' 
		,OnlineMag.Units as OnlineMagUnits
		,OnlineMag.NetSale as OnlineMagSales
		,OnlineTrt.Units as OnlineTrtUnits
		,OnlineTrt.NetSale as OnlineTrtNetSale
		,OnlineEntertainment.Units as OnlineEntertainmentUnits
		,OnlineEntertainment.NetSale as OnlineEntertainmentNetSale
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
		)OnlineMag 	on ca.id = OnlineMag.campaignid	
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting							
								 where orderqualifierid = 39009 
								 and section_type_id = 14								
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineTRT 	on ca.id = OnlineTRT.campaignid	
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting							
								 where orderqualifierid = 39009 
								 and section_type_id = 15								
								 and accounting_year = @CurrYear							 
			     					 and Accounting_Period between @FromAccountingPeriod and @ToAccountingPeriod
								 group by campaignid
		)OnlineEntertainment 	on ca.id = OnlineEntertainment.campaignid	
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
			,PriorYearPointInTime.OnlineMagUnits as PointInTimeOnlineMagUnits
			,PriorYearPointInTime.OnlineMagSales as PointInTimeOnlineMagSales
			,PriorYearPointInTime.LandedMagSales as PointInTimeTotalLandedMagSales
			,PriorYearPointInTime.LandedMagUnits as PointInTimeTotalLandedMagUnits
			,PriorYearPointInTime.LandedGiftSales as PointInTimeTotalLandedGiftSales
			,PriorYearPointInTime.LandedGiftUnits as PointInTimeTotalLandedGiftUnits
			,PriorYearPointInTime.LandedCDSales as PointInTimeTotalLandedCDSales
			,PriorYearPointInTime.LandedCDUnits as PointInTimeTotalLandedCDUnits
			,PriorYearPointInTime.LandedCandleSales as PointInTimeTotalLandedCandleSales
			,PriorYearPointInTime.LandedCandleUnits as PointInTimeTotalLandedCandleUnits
			,PriorYearPointInTime.OnlineTrtSales as PointInTimeTotalOnlineTrtSales
			,PriorYearPointInTime.OnlineTrtUnits as PointInTimeTotalOnlineTrtUnits
			,PriorYearPointInTime.LandedTrtSales as PointInTimeTotalLandedTrtSales
			,PriorYearPointInTime.LandedTrtUnits as PointInTimeTotalLandedTrtUnits
			,PriorYearPointInTime.OnlineEntertainmentSales as PointInTimeTotalOnlineEntertainmentSales
			,PriorYearPointInTime.OnlineEntertainmentUnits as PointInTimeTotalOnlineEntertainmentUnits
			,PriorYearPointInTime.LandedEntertainmentSales as PointInTimeTotalLandedEntertainmentSales
			,PriorYearPointInTime.LandedEntertainmentUnits as PointInTimeTotalLandedEntertainmentUnits
			,PriorYearPointInTime.LandedJewelrySales as PointInTimeTotalLandedJewelrySales
			,PriorYearPointInTime.LandedJewelryUnits as PointInTimeTotalLandedJewelryUnits			
			,PriorYearPointInTime.Total as PointInTimeTotalSales
			,CurrentYearTotal.CampaignCount as CurrentYearTotalCampaignCount
			,CurrentYearTotal.studentcount as CurrentYearTotalStudentcount
			,CurrentYearTotal.OnlineMagUnits as CurrentYearTotalOnlineMagUnits
			,CurrentYearTotal.OnlineMagSales as CurrentYearTotalOnlineMagSales
			,CurrentYearTotal.LandedMagSales as CurrentYearTotalLandedMagSales
			,CurrentYearTotal.LandedMagUnits as CurrentYearTotalLandedMagUnits
			,CurrentYearTotal.LandedGiftSales as CurrentYearTotalLandedGiftSales
			,CurrentYearTotal.LandedGiftUnits as CurrentYearTotalLandedGiftUnits
			,CurrentYearTotal.LandedCDSales as CurrentYearTotalLandedCDSales
			,CurrentYearTotal.LandedCDUnits as CurrentYearTotalLandedCDUnits
			,CurrentYearTotal.LandedCandleSales as CurrentYearTotalLandedCandleSales
			,CurrentYearTotal.LandedCandleUnits as CurrentYearTotalLandedCandleUnits
			,CurrentYearTotal.OnlineTrtSales as CurrentYearTotalOnlineTrtSales
			,CurrentYearTotal.OnlineTrtUnits as CurrentYearTotalOnlineTrtUnits
			,CurrentYearTotal.LandedTrtSales as CurrentYearTotalLandedTrtSales
			,CurrentYearTotal.LandedTrtUnits as CurrentYearTotalLandedTrtUnits
			,CurrentYearTotal.OnlineEntertainmentSales as CurrentYearTotalOnlineEntertainmentSales
			,CurrentYearTotal.OnlineEntertainmentUnits as CurrentYearTotalOnlineEntertainmentUnits
			,CurrentYearTotal.LandedEntertainmentSales as CurrentYearTotalLandedEntertainmentSales
			,CurrentYearTotal.LandedEntertainmentUnits as CurrentYearTotalLandedEntertainmentUnits
			,CurrentYearTotal.LandedJewelrySales as CurrentYearTotalLandedJewelrySales
			,CurrentYearTotal.LandedJewelryUnits as CurrentYearTotalLandedJewelryUnits			
			,CurrentYearTotal.Total as CurrentYearTotalSales
			,PriorYearTotal.CampaignCount as PriorYearTotalCampaignCount
			,PriorYearTotal.studentcount as PriorYearTotalStudentcount
			,PriorYearTotal.OnlineMagUnits as PriorYearTotalOnlineMagUnits
			,PriorYearTotal.OnlineMagSales as PriorYearTotalOnlineMagSales
			,PriorYearTotal.LandedMagSales as PriorYearTotalLandedMagSales
			,PriorYearTotal.LandedMagUnits as PriorYearTotalLandedMagUnits
			,PriorYearTotal.LandedGiftSales as PriorYearTotalLandedGiftSales
			,PriorYearTotal.LandedGiftUnits as PriorYearTotalLandedGiftUnits
			,PriorYearTotal.LandedCDSales as PriorYearTotalLandedCDSales
			,PriorYearTotal.LandedCDUnits as PriorYearTotalLandedCDUnits
			,PriorYearTotal.LandedCandleSales as PriorYearTotalLandedCandleSales
			,PriorYearTotal.LandedCandleUnits as PriorYearTotalLandedCandleUnits
			,PriorYearTotal.OnlineTrtSales as PriorYearTotalOnlineTrtSales
			,PriorYearTotal.OnlineTrtUnits as PriorYearTotalOnlineTrtUnits
			,PriorYearTotal.LandedTrtSales as PriorYearTotalLandedTrtSales
			,PriorYearTotal.LandedTrtUnits as PriorYearTotalLandedTrtUnits
			,PriorYearTotal.OnlineEntertainmentSales as PriorYearTotalOnlineEntertainmentSales
			,PriorYearTotal.OnlineEntertainmentUnits as PriorYearTotalOnlineEntertainmentUnits
			,PriorYearTotal.LandedEntertainmentSales as PriorYearTotalLandedEntertainmentSales
			,PriorYearTotal.LandedEntertainmentUnits as PriorYearTotalLandedEntertainmentUnits
			,PriorYearTotal.LandedJewelrySales as PriorYearTotalLandedJewelry
			,PriorYearTotal.LandedJewelryUnits as PriorYearTotalLandedJewelryUnits			
			,PriorYearTotal.Total as PriorYearTotal
			from #s s1
		cross apply
		(
		select  count(campaignid) as CampaignCount
				,isnull(sum(studentcount),0) as StudentCount
				,isnull(sum(OnlineMagsales),0) as OnlineMagSales
				,isnull(sum(OnlineMagUnits),0) as OnlineMagUnits
				,isnull(sum(LandedMagSales),0) as LandedMagSales
				,isnull(sum(LandedMagUnits),0) as LandedMagUnits
				,isnull(sum(LandedGiftSales),0) as LandedGiftSales
				,isnull(sum(LandedGiftUnits),0) as LandedGiftUnits
				,isnull(sum(LandedCDSales),0) as LandedCDSales
				,isnull(sum(LandedCDUnits),0) as LandedCDUnits
				,isnull(sum(LandedCandleSales),0) as LandedCandleSales
				,isnull(sum(LandedCandleUnits),0) as LandedCandleUnits
				,isnull(sum(OnlineTrtSales),0) as OnlineTrtSales
				,isnull(sum(OnlineTrtUnits),0) as OnlineTrtUnits
				,isnull(sum(LandedTrtSales),0) as LandedTrtSales
				,isnull(sum(LandedTrtUnits),0) as LandedTrtUnits
				,isnull(sum(OnlineEntertainmentSales),0) as OnlineEntertainmentSales
				,isnull(sum(OnlineEntertainmentUnits),0) as OnlineEntertainmentUnits
				,isnull(sum(LandedEntertainmentSales),0) as LandedEntertainmentSales
				,isnull(sum(LandedEntertainmentUnits),0) as LandedEntertainmentUnits
				,isnull(sum(LandedJewelrySales),0) as LandedJewelrySales
				,isnull(sum(LandedJewelryUnits),0) as LandedJewelryUnits
				,isnull((isnull(sum(OnlineMagSales),0)+ isnull(sum(LandedMagSales),0)+ isnull(sum(LandedGiftSales),0)+isnull(sum(LandedCandleSales),0)+isnull(sum(OnlineTRTSales),0)+isnull(sum(LandedTRTSales),0)+isnull(sum(OnlineEntertainmentSales),0)+isnull(sum(LandedEntertainmentSales),0)+ isnull(sum(LandedCDSales),0)+ isnull(sum(LandedJewelrySales),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear=@LastFiscalYear
		)PriorYearTotal
		cross apply
		(
		select  count(campaignid) as CampaignCount
				,isnull(sum(studentcount),0) as StudentCount	
				,isnull(sum(OnlineMagsales),0) as OnlineMagSales
				,isnull(sum(OnlineMagUnits),0) as OnlineMagUnits
				,isnull(sum(LandedMagSales),0) as LandedMagSales
				,isnull(sum(LandedMagUnits),0) as LandedMagUnits
				,isnull(sum(LandedGiftSales),0) as LandedGiftSales
				,isnull(sum(LandedGiftUnits),0) as LandedGiftUnits
				,isnull(sum(LandedCDSales),0) as LandedCDSales
				,isnull(sum(LandedCDUnits),0) as LandedCDUnits
				,isnull(sum(LandedCandleSales),0) as LandedCandleSales
				,isnull(sum(LandedCandleUnits),0) as LandedCandleUnits
				,isnull(sum(OnlineTrtSales),0) as OnlineTrtSales
				,isnull(sum(OnlineTrtUnits),0) as OnlineTrtUnits
				,isnull(sum(LandedTrtSales),0) as LandedTrtSales
				,isnull(sum(LandedTrtUnits),0) as LandedTrtUnits
				,isnull(sum(OnlineEntertainmentSales),0) as OnlineEntertainmentSales
				,isnull(sum(OnlineEntertainmentUnits),0) as OnlineEntertainmentUnits
				,isnull(sum(LandedEntertainmentSales),0) as LandedEntertainmentSales
				,isnull(sum(LandedEntertainmentUnits),0) as LandedEntertainmentUnits
				,isnull(sum(LandedJewelrySales),0) as LandedJewelrySales
				,isnull(sum(LandedJewelryUnits),0) as LandedJewelryUnits
				,isnull((isnull(sum(OnlineMagSales),0)+ isnull(sum(LandedMagSales),0)+ isnull(sum(LandedGiftSales),0)+isnull(sum(LandedCandleSales),0)+isnull(sum(OnlineTRTSales),0)+isnull(sum(LandedTRTSales),0)+isnull(sum(OnlineEntertainmentSales),0)+isnull(sum(LandedEntertainmentSales),0)+ isnull(sum(LandedCDSales),0)+ isnull(sum(LandedJewelrySales),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear=@CurrentFiscalYear
		)CurrentYearTotal
				cross apply
		(
		select  count(campaignid) as CampaignCount
				,isnull(sum(studentcount),0) as StudentCount	
				,isnull(sum(OnlineMagsales),0) as OnlineMagSales
				,isnull(sum(OnlineMagUnits),0) as OnlineMagUnits
				,isnull(sum(LandedMagSales),0) as LandedMagSales
				,isnull(sum(LandedMagUnits),0) as LandedMagUnits
				,isnull(sum(LandedGiftSales),0) as LandedGiftSales
				,isnull(sum(LandedGiftUnits),0) as LandedGiftUnits
				,isnull(sum(LandedCDSales),0) as LandedCDSales
				,isnull(sum(LandedCDUnits),0) as LandedCDUnits
				,isnull(sum(LandedCandleSales),0) as LandedCandleSales
				,isnull(sum(LandedCandleUnits),0) as LandedCandleUnits
				,isnull(sum(OnlineTrtSales),0) as OnlineTrtSales
				,isnull(sum(OnlineTrtUnits),0) as OnlineTrtUnits
				,isnull(sum(LandedTrtSales),0) as LandedTrtSales
				,isnull(sum(LandedTrtUnits),0) as LandedTrtUnits
				,isnull(sum(OnlineEntertainmentSales),0) as OnlineEntertainmentSales
				,isnull(sum(OnlineEntertainmentUnits),0) as OnlineEntertainmentUnits
				,isnull(sum(LandedEntertainmentSales),0) as LandedEntertainmentSales
				,isnull(sum(LandedEntertainmentUnits),0) as LandedEntertainmentUnits
				,isnull(sum(LandedJewelrySales),0) as LandedJewelrySales
				,isnull(sum(LandedJewelryUnits),0) as LandedJewelryUnits
				,isnull((isnull(sum(OnlineMagSales),0)+ isnull(sum(LandedMagSales),0)+ isnull(sum(LandedGiftSales),0)+isnull(sum(LandedCandleSales),0)+isnull(sum(OnlineTRTSales),0)+isnull(sum(LandedTRTSales),0)+isnull(sum(OnlineEntertainmentSales),0)+isnull(sum(LandedEntertainmentSales),0)+ isnull(sum(LandedCDSales),0)+ isnull(sum(LandedJewelrySales),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear='PointInTime'
		)PriorYearPointInTime
	order by s1.Programs, s1.PrizePrograms
	
--select * from #s where programs=' Mag Exp'  and fiscalyear='FY13' and landedsales is not null order by programs, prizeprograms

--drop Table #S
GO
