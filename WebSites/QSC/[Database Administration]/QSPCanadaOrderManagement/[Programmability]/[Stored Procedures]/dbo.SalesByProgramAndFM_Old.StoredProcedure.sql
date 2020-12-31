USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[SalesByProgramAndFM_Old]    Script Date: 06/07/2017 09:20:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
Create PROCEDURE [dbo].[SalesByProgramAndFM_Old] @pStartDate datetime, @pEndDate DATETIME--, @pFMID varchar(10) 

AS
Set FMTONLY Off
--Declare @pStartDate datetime, @pEndDate DATETIME
DECLARE @pFMID varchar(10) 

--Select @pStartDate='7/1/12'
--Select @pEndDate='12/31/12 23:59:59'

Declare @LastYear		   Int,  
	@CurrYear              Int,
	@LastSeason 		   Char(1), 
	@LastSeasonStartDate 	   Datetime , 
	@LastSeasonEndDate 	   Datetime, 
	@CurrentSeasonStartDate Datetime , 
	@CurrentSeasonEndDate  Datetime, 
    @CurrentFiscalYear 	   Varchar(4), 
	@LastFiscalYear 	   Varchar(10),
	@PointInTime           datetime

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

--SELECT CONVERT(VARCHAR(10), DATEADD(dd, 1, GETDATE()), 120)
If datepart(mm,@pStartDate) between 7 and 12
Begin
   Set @CurrentFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate)+1 as varchar),3,2)
   Set @LastFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate) as varchar),3,2)
   Set @CurrYear= Datepart(yy,@pStartDate)+1 
   Set @LastYear= @CurrYear-1
End 

If datepart(mm,@pStartDate) between 1 and 6
Begin
    Set @CurrentFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate) as varchar),3,2)
    Set @LastFiscalYear 	=  'FY'+ substring(cast(Datepart(yy,@pStartDate)-1 as varchar),3,2)
    Set @CurrYear= Datepart(yy,@pStartDate)+1 
    Set @LastYear= @CurrYear-1
End 

   Set @CurrentSeasonStartDate =  @pStartDate
   Set @CurrentSeasonEndDate  =  @pEndDate

   Set @LastSeasonStartDate = DateAdd(yy,-1,@pStartDate ) 
   Set @LastSeasonEndDate = DateAdd(yy,-1,@pEndDate ) 

Print @LastYear

Create Table #S 	(
			 CampaignID  int,
			 FiscalYear 	Varchar(20),
			 FMID		Varchar(4),
			 FMName		Varchar(30),
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
			 FMID,
			 FMName,
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
		,fm.fmid
		,fm.LastName
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
				where orderqualifierid in (39009, 39001, 39002, 39015, 39020) 
					and batch.campaignid = ca.id  and batch.statusinstance <> 40005
					and  accounting_year = @LastYear
		) as StudentCount
		from qspcanadacommon.dbo.campaign as ca 
		join qspcanadacommon.dbo.FieldManager fm ON fm.fmid = ca.FMID
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and accounting_year = @LastYear							 
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
								 group by campaignid
				
		)Jewelry
		on ca.id = Jewelry.campaignid	
			
	where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 0
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting where accounting_year in (@LastYear ) and orderqualifierid in(39001, 39002, 39015, 39020, 39009) )
	order by ca.id
	
--Staff	full last year
	
insert #S(CampaignID  ,
			 FiscalYear ,
			 FMID, 
			 FMName,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	
		  )	
Select ca.id,
		@LastFiscalYear
		,fm.fmid
		,fm.LastName
		,'Staff' 
		,'NA' 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale	
	from qspcanadacommon.dbo.campaign as ca 
	join qspcanadacommon.dbo.FieldManager fm ON fm.fmid = ca.FMID
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and accounting_year = @LastYear							 
								 group by campaignid
		)internet 	on ca.id = internet.campaignid		
where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 1
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting where accounting_year in (@LastYear ) and orderqualifierid in(39001, 39002, 39015, 39020, 39009) )
	order by ca.id		
		
/**
** Point in time last year
*/


insert #S(CampaignID  ,
			 FiscalYear ,
			 FMID,
			 FMName,
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
		,fm.fmid
		,fm.LastName
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
				where orderqualifierid in (39009, 39001, 39002, 39015, 39020) 
					and batch.campaignid = ca.id  and batch.statusinstance <> 40005
					and gl_entry_date <= @PointInTime	
					and accounting_year = @LastYear	
		) as StudentCount
		from qspcanadacommon.dbo.campaign as ca 
		join qspcanadacommon.dbo.FieldManager fm ON fm.fmid = ca.FMID
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
								 group by campaignid
				
		)Jewelry
		on ca.id = Jewelry.campaignid	
			
	where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		 isstafforder = 0
--		and ca.id = 79202
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting where accounting_year in (@LastYear ) and orderqualifierid in(39001, 39002, 39015, 39020, 39009)
							 and invoice_date <= @PointInTime )
	order by ca.id

--Staff	point in time
	
insert #S(CampaignID  ,
			 FiscalYear ,
			 FMID,
			 FMName,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	
		  )	
Select ca.id,
		'PointInTime'
		,fm.fmid
		,fm.LastName
		,'Staff' 
		,'NA' 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale	
	from qspcanadacommon.dbo.campaign as ca 
	join qspcanadacommon.dbo.FieldManager fm ON fm.fmid = ca.FMID
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
								 group by campaignid
		)internet 	on ca.id = internet.campaignid		
where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 1
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting where accounting_year in (@LastYear ) and orderqualifierid in(39001, 39002, 39015, 39020, 39009)
							 and invoice_date <= @PointInTime )
	order by ca.id		

/*
** Full Current Year
*/

insert #S(CampaignID  ,
			 FiscalYear ,
			 FMID,
			 FMName,
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
		,fm.fmid
		,fm.LastName
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
				where orderqualifierid in (39009, 39001, 39002, 39015, 39020) 
					and batch.campaignid = ca.id  and batch.statusinstance <> 40005
					and accounting_year = @CurrYear
		) as StudentCount
		from qspcanadacommon.dbo.campaign as ca 
		join qspcanadacommon.dbo.FieldManager fm ON fm.fmid = ca.FMID
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 2
								 and accounting_year = @CurrYear							 
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
								 group by campaignid
				
		)Jewelry
		on ca.id = Jewelry.campaignid		
	where --startdate between @CurrentSeasonStartDate and  @CurrentSeasonEndDate
		isstafforder = 0
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting where accounting_year in ( @CurrYear ) and orderqualifierid in(39001, 39002, 39015, 39020, 39009) )
		
	order by ca.id

--Current staff

insert #S(CampaignID  ,
			 FiscalYear ,
			 FMID,
			 FMName,
			 Programs ,
			 PrizePrograms ,
			 OnlineUnits  ,
			 OnlineSales 	
		  )	
Select ca.id,
		@CurrentFiscalYear
		,fm.fmid
		,fm.LastName
		,'Staff' 
		,'NA' 
		,Internet.Units as InternetUnits
		,Internet.NetSale as InternetNetSale	
	from qspcanadacommon.dbo.campaign as ca 
	join qspcanadacommon.dbo.FieldManager fm ON fm.fmid = ca.FMID
		left join
		(
				Select campaignid--, orderqualifierid 		
					,Sum(NetSale) as NetSale
					,sum(Units) as Units
					 from QSpCanadaFinance.dbo.vw_GetNetForReporting								
								 where orderqualifierid = 39009 
								 and section_type_id = 2								
								 and accounting_year = @CurrYear							 
								 group by campaignid
		)internet 	on ca.id = internet.campaignid		
where --startdate between @LastSeasonStartDate and  @LastSeasonEndDate
		isstafforder = 1
		and ca.status <> 37005
		and ca.id in ( Select distinct campaignid from QSpCanadaFinance.dbo.vw_GetNetForReporting where accounting_year in (@CurrYear ) and orderqualifierid in(39001, 39002, 39015, 39020, 39009)	)
	order by ca.id		


Select distinct s1.FMID, s1.FMName, s1.Programs, s1.PrizePrograms
			--,PriorYearPointInTime.CampaignCount as PointInTimeCampaignCount
			--,PriorYearPointInTime.studentcount as PointInTimeStudentcount
			--,PriorYearPointInTime.OnlineUnits as PointInTimeOnlineUnits
			,PriorYearPointInTime.OnlineSales 
			+PriorYearPointInTime.LandedSales as PointInTimeMagNetSales
			,PriorYearPointInTime.LandedJewelry as PointInTimeJewelryNetSales
			,PriorYearPointInTime.LandedGift as PointInTimeGiftNetSales
			,PriorYearPointInTime.LandedChoc as PointInTimeChocolateNetSales
			,PriorYearPointInTime.LandedCD as PointInTimeCookieDoughNetSales
			,PriorYearPointInTime.OnlineSales 
			+PriorYearPointInTime.LandedSales 
			+PriorYearPointInTime.LandedJewelry 
			+PriorYearPointInTime.LandedGift 
			+PriorYearPointInTime.LandedChoc 
			+PriorYearPointInTime.LandedCD as PointInTimeTotalNetSales
			--,PriorYearPointInTime.CDUnits as PointInTimeTotalCDUnits
			--,PriorYearPointInTime.LandedMagUnits as PointInTimeTotalLandedMagUnits
			--,PriorYearPointInTime.GiftUnits as PointInTimeTotalGiftUnits
		--	,PriorYearPointInTime.LandedCD as PointInTimeTotalLandedCD
			--,PriorYearPointInTime.CDUnits as PointInTimeTotalCDUnits
		--	,PriorYearPointInTime.LandedChoc as PointInTimeTotalLandedChoc
			--,PriorYearPointInTime.ChocUnits as PointInTimeTotalChocUnits
			--,PriorYearPointInTime.JewelryUnits		 as PointInTimeTotalJewelryUnits			
			--,PriorYearPointInTime.TotalUnits as PointInTimeTotalUnits
			--,PriorYearPointInTime.Total as PointInTimeTotal
			--,CurrentYearTotal.CampaignCount as CurrentYearTotalCampaignCount
			--,CurrentYearTotal.studentcount as CurrentYearTotalStudentcount
			--,CurrentYearTotal.OnlineUnits as CurrentYearTotalOnlineUnits
			--,CurrentYearTotal.LandedMagUnits as CurrentYearTotalLandedMagUnits
			,CurrentYearTotal.OnlineSales 
			+CurrentYearTotal.LandedSales as MagNetSales
			,CurrentYearTotal.LandedJewelry as JewelryNetSales
			,CurrentYearTotal.LandedGift as GiftNetSales
			,CurrentYearTotal.LandedChoc as ChocolateNetSales
			,CurrentYearTotal.LandedCD as CookieDoughNetSales
			,CurrentYearTotal.OnlineSales 
			+CurrentYearTotal.LandedSales 
			+CurrentYearTotal.LandedJewelry 
			+CurrentYearTotal.LandedGift 
			+CurrentYearTotal.LandedChoc 
			+CurrentYearTotal.LandedCD as TotalNetSales
			--,CurrentYearTotal.GiftUnits as CurrentYearTotalGiftUnits
			--,CurrentYearTotal.CDUnits as CurrentYearTotalCDUnits
			--,CurrentYearTotal.ChocUnits as CurrentYearTotalChocUnits
			--,CurrentYearTotal.JewelryUnits		 as CurrentYearTotalJewelryUnits			
			--,CurrentYearTotal.TotalUnits as CurrentYearTotalUnits
			--,CurrentYearTotal.Total as CurrentYearTotal
			--,PriorYearTotal.CampaignCount as PriorYearTotalCampaignCount
			--,PriorYearTotal.studentcount as PriorYearTotalStudentcount
			--,PriorYearTotal.OnlineUnits as PriorYearTotalOnlineUnits
			--,PriorYearTotal.LandedMagUnits as PriorYearTotalLandedMagUnits
			--,PriorYearTotal.OnlineSales 
			--+PriorYearTotal.LandedSales as PriorYearTotalMagSales
			--,PriorYearTotal.LandedGift as PriorYearTotalLandedGift
			--,PriorYearTotal.GiftUnits as PriorYearTotalGiftUnits
			--,PriorYearTotal.LandedCD as PriorYearTotalLandedCD
			--,PriorYearTotal.CDUnits as PriorYearTotalCDUnits
			--,PriorYearTotal.LandedChoc as PriorYearTotalLandedChoc
			--,PriorYearTotal.ChocUnits as PriorYearTotalChocUnits
			--,PriorYearTotal.LandedJewelry as PriorYearTotalLandedJewelry
			--,PriorYearTotal.JewelryUnits		 as PriorYearJewelryUnits			
			--,PriorYearTotal.TotalUnits as PriorYearTotalUnits
			--,PriorYearTotal.Total as PriorYearTotal
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
				,isnull((isnull(sum(onlineunits),0)+ isnull(sum(landedmagunits),0)+ isnull(sum(giftunits),0)+isnull(sum(chocunits),0)+ isnull(sum(CDUnits),0)+ isnull(sum(jewelryunits),0)),0) as TotalUnits
				,isnull((isnull(sum(onlinesales),0)+ isnull(sum(landedsales),0)+ isnull(sum(landedgift),0)+isnull(sum(landedchoc),0)+ isnull(sum(landedcd),0)+ isnull(sum(landedjewelry),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear=@LastFiscalYear  and s1.fmid = s.fmid 
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
				,isnull((isnull(sum(onlineunits),0)+ isnull(sum(landedmagunits),0)+ isnull(sum(giftunits),0)+isnull(sum(chocunits),0)+ isnull(sum(CDUnits),0)+ isnull(sum(jewelryunits),0)),0) as TotalUnits
				,isnull((isnull(sum(onlinesales),0)+ isnull(sum(landedsales),0)+ isnull(sum(landedgift),0)+isnull(sum(landedchoc),0)+ isnull(sum(landedcd),0)+ isnull(sum(landedjewelry),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear=@CurrentFiscalYear  and s1.fmid  = s.fmid 
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
				,isnull((isnull(sum(onlineunits),0)+ isnull(sum(landedmagunits),0)+ isnull(sum(giftunits),0)+isnull(sum(chocunits),0)+ isnull(sum(CDUnits),0)+ isnull(sum(jewelryunits),0)),0) as TotalUnits
				,isnull((isnull(sum(onlinesales),0)+ isnull(sum(landedsales),0)+ isnull(sum(landedgift),0)+isnull(sum(landedchoc),0)+ isnull(sum(landedcd),0)+ isnull(sum(landedjewelry),0)),0) as Total
							from #s s where s1.programs=s.programs and s.prizeprograms=s1.prizeprograms and fiscalyear='PointInTime' and s1.fmid  = s.fmid 
		)PriorYearPointInTime
	order by s1.FMID, s1.FMName, s1.Programs, s1.PrizePrograms


--select * from #s --where programs=' Mag Exp'  and fiscalyear='FY13' and landedsales is not null order by programs, prizeprograms

--drop Table #S
GO
