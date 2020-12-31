USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[RedoRemit]    Script Date: 06/07/2017 09:20:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[RedoRemit] 

@OrderId int

AS


/*
2004-09-10 00:00:00.000	103
2004-11-02 00:00:00.000	138
2004-11-02 00:00:00.000	277
2004-11-04 00:00:00.000	256
2004-11-05 00:00:00.000	133
2004-11-10 00:00:00.000	165
2004-11-15 00:00:00.000	101
2004-11-15 00:00:00.000	122
2004-11-22 00:00:00.000	146



*/



declare	@batchDate datetime,
	@batchID int,
	@remitstatus int

set @batchDate = '2004-09-10'
set @batchid = '103'

	declare @status int
	declare @coh int
	declare @tid int
	declare @rbatch int
	declare @recipient varchar(81)
	declare @first varchar(40)
	declare @last varchar(40)
	declare @address1 varchar(50)
	declare @address2 varchar(50)
	declare @city varchar(50)
	declare @state varchar(50)
	declare @zip varchar(20)
	declare @ZipPlusFour varchar(4)

	-- defensive programming - if this batch isn't approved err out - or already remitted
	select @status = StatusInstance from Batch where date=@batchDate and id = @batchID


	declare  @fiscal  int
	select @fiscal = long1value from qspcanadacommon..systemoptions where keyvalue='FiscalYear'

	declare @season varchar(1)
	select @season = season,@fiscal=FiscalYear from qspcanadacommon..season where fiscalyear is not null
		and name not like 'Fiscal%'
		and @batchDate  between StartDate and Enddate

	-- 1. get a list of all the fulfillment house ids from COD and customer 
	-- that aren't already in RemitBatch that are in good state
	-- create those w/Sent to remit as 1/1/95 and default status
--print @fiscal			
--print @season
--select * from qspcanadacommon..season where fiscalyear=@fiscal
		and name not like 'Fiscal%'
		and @batchDate  between StartDate and Enddate
	


	select max(id)as id ,FulfillmentHouseNbr, status into #RB from   RemitBatch where  status=42000
		group by  FulfillmentHouseNbr, status
	
		select 
			customerorderheaderinstance, 
			transid,
			rb.id as rbid, 
			p.countrycode,
			0 as CustomerRemitHistoryInstance,
			42000 as status,
			recipient,
			Quantity,
			Remit_rate,
			Basic_Price_Yr,
			Currency,
			lang,
			PremiumInd,
			isnull(Premium_code,'') as Premium_code,
			isnull(Premium_Copy,'') as Premium_Copy,
			PD.ABCCode as ABC, -- abc
			Renewal,
			ProductCode,
			P.Product_Sort_Name as ProductName,
			CatalogPrice,
			price,
			p.Fulfill_House_Nbr as fulfill,
			Nbr_of_issues as Issues, -- fill in number of issues
			DefaultGrossValue,
			' ' as Comment,
			0 as SwitchLetterBatchID,
			GiftCD, -- for gift order type
			0 as GiftOrderStatus,
			'1/1/95' as GiftCardDateGenerated,
			SupporterName,
			GetDate() as DateChanged,
			'ADMI' as UserIDChanged,
			CustomerBillToInstance,
			Address1,
			Address2,
			City,
			County,
			State,
			Zip,
			isnull(ZipPlusFour,'') as ZipPlusFour,
			isnull(pd.Effort_Key,'') as EffortKey
			into #Temp
			from 
			customerorderdetail,
			customerorderheader,
			customer,
			qspcanadaproduct..pricing_details pd,
			qspcanadaproduct..program_details prd,
			batch b,
			qspcanadaproduct..product p
			left join #RB rb on rb.FulfillmentHouseNbr= p.Fulfill_House_Nbr
				and rb.status=42000
			where
			productcode=p.product_code 
			and p.product_season=pd.pricing_season
			and p.Product_year=pd.pricing_year
			and p.product_code=pd.product_code			
			and customerorderdetail.pricingdetailsid = pd.MagPrice_Instance
			and customerorderheaderinstance=customerorderheader.instance
			and orderbatchdate=b.date and orderbatchid=b.id
			and prd.programsectionid = pd.programsectionid		
			and prd.program_year = pd.pricing_year 
			and prd.program_season = pd.pricing_season
			and prd.product_code = pd.product_code		
			and prd.offer_id = pd.offer_code
			and prd.taxregionid = pd.taxregionid
			and customerorderdetail.statusinstance =502--in (502,507) -- 502 is paid
			and customerorderdetail.delflag=0
			and customerbilltoinstance=customer.instance
			and customer.statusinstance = 300
			and customerorderdetail.producttype=46001    -- mags only
			and customershiptoinstance = 0  
			and b.orderid in (@OrderId)--and customerorderheaderinstance in(select customerorderheaderinstance from nick_bak)
			order by customerorderheaderinstance, 
			transid

		/*
		**   Any ship to 
		*/
		insert #temp
		select  
			customerorderheaderinstance, 
			transid,
			rb.id as rbid, 
			p.countrycode,
			0 as CustomerRemitHistoryInstance,
			42000 as status,
			recipient,
			Quantity,
			Remit_rate,
			Basic_Price_Yr,
			Currency,
			lang,
			PremiumInd,
			isnull(Premium_code,'') as Premium_code,
			isnull(Premium_Copy,'') as Premium_Copy,
			PD.ABCCode as ABC, -- abc
			Renewal,
			ProductCode,
			P.Product_Sort_Name as ProductName,
			CatalogPrice,
			price,
			p.Fulfill_House_Nbr as fulfill,
			Nbr_of_issues as Issues, -- fill in number of issues
			DefaultGrossValue,
			' ' as Comment,
			0 as SwitchLetterBatchID,
			GiftCD, -- for gift order type
			0 as GiftOrderStatus,
			'1/1/95' as GiftCardDateGenerated,
			SupporterName,
			GetDate() as DateChanged,
			'ADMI' as UserIDChanged,
			CustomerBillToInstance,
			Address1,
			Address2,
			City,
			County,
			State,
			Zip,
			isnull(ZipPlusFour,'') as ZipPlusFour,
			isnull(pd.Effort_Key,'') as EffortKey
			from 
			customerorderdetail,
			customerorderheader,
			customer,
			qspcanadaproduct..pricing_details pd,
			qspcanadaproduct..program_details prd,
			batch b,
			qspcanadaproduct..product p
			left join #RB rb on rb.FulfillmentHouseNbr= p.Fulfill_House_Nbr
				and rb.status=42000
			where
			productcode=p.product_code 
			and p.product_season=pd.pricing_season
			and p.Product_year=pd.pricing_year
			and p.product_code=pd.product_code			
			and customerorderdetail.pricingdetailsid = pd.MagPrice_Instance
			and customerorderheaderinstance=customerorderheader.instance
			and orderbatchdate=b.date and orderbatchid=b.id
			and prd.programsectionid = pd.programsectionid		
			and prd.program_year = pd.pricing_year 
			and prd.program_season = pd.pricing_season
			and prd.product_code = pd.product_code		
			and prd.offer_id = pd.offer_code
			and prd.taxregionid = pd.taxregionid
			and customerorderdetail.statusinstance  =502--in (502,507) -- 502 is paid
			and customerorderdetail.delflag=0
			and customershiptoinstance=customer.instance
			and customer.statusinstance = 300
			and customerorderdetail.producttype=46001    -- mags only
			and b.orderid in (@OrderId)
			and customershiptoinstance <> 0 --and customerorderheaderinstance in(select customerorderheaderinstance from nick_bak)
			order by customerorderheaderinstance, 
			transid

			
--select * from #temp
		-- should be quick and then fill in rbid where it's null
		declare a cursor for select distinct fulfill  from #temp where rbid is null
		open a
		declare @fulfill varchar(10)
		declare @maxid int
		fetch next from a into @fulfill
		while(@@fetch_status <> -1)
		begin

			create table #tempinst
			(
				 NextInstance int
			)
			insert into #tempinst exec qspcanadaordermanagement..InsertNextInstance 17

			select @maxid = NextInstance from #tempinst
			drop table #tempinst
			insert RemitBatch(ID, Date, Status, FulfillmentHouseNbr, UserIDChanged,DateChanged) 
				values(@maxid, '1/1/95', 42000,@fulfill,'ADMI',GetDate())


			
			update #temp set rbid = @maxid where fulfill = @fulfill
			-- insert a record for each one of these and then update the temp table
			fetch next from a into @fulfill

		end
		close a
		deallocate a


		-- Prepare CustomerRemitHistory
		declare @maxcust int
		declare @bill int
		declare @count int
		declare @index int
		declare @index2 int
		declare @i int
		set @i=0

		declare b cursor for select customerorderheaderinstance,transid, rbid,customerbilltoinstance,
				address1, address2, city, state , zip,ZipPlusFour  from #temp
		open b
		fetch next from b into	 @coh, @tid, @rbatch, @bill, @address1, @address2, @city, @state , @zip,@ZipPlusFour 		

		while(@@fetch_status <> -1)
		begin
/*
			select @count = count(*) from CustomerRemitHistory	
			if(@count > 0)
			begin
				select @maxcust= max(instance) +1 from CustomerRemitHistory	
			end
			else
			begin
				select @maxcust=1
			end
*/
			-------------------------------------------
			--- Safe way to get next instance
			-------------------------------------------
			set @i = @i + 1
			print @i
			create table #crhtempinst
			(
				 NextInstance int
			)
			insert into #crhtempinst exec qspcanadaordermanagement..InsertNextInstance 18

			select @maxcust = NextInstance from #crhtempinst
			drop table #crhtempinst
			-- Break apart recipient name

			select @recipient = recipient from #temp where 
				rbid = @rbatch and customerorderheaderinstance=@coh and transid=@tid

			select @index = charindex(' ', @recipient,1)
		
			-- see if theres another blank
			select @index2 = charindex( ' ', @recipient,@index+1)
			if(@index2 <> 0)
			begin
				select @first = left(@recipient, charindex(' ', @recipient,@index2)) 
				select @last = right(@recipient, datalength(@recipient) - @index2)	
			end
			else
			begin
				select @first = left(@recipient, charindex(' ', @recipient,@index)) 
				select @last = right(@recipient, datalength(@recipient) - @index)	
			end


			insert CustomerRemitHistory (
				remitbatchid,
				instance,
				customerinstance,
				Lastname, FirstName, StatusInstance,DateModified,UserIDModified,
				Address1,
				Address2,
				City,
				State,
				Zip,
				ZipPlusFour)
				 values( @rbatch,@maxcust,@bill, @last, @first, 42000, GetDate(),'KT', 
					@address1, @address2, @city, @state , @zip,@ZipPlusFour )
			

			update #temp set CustomerRemitHistoryInstance = @maxcust where 
				rbid = @rbatch and customerorderheaderinstance=@coh and transid=@tid

			fetch next from b into	 @coh, @tid, @rbatch, @bill, @address1, @address2, @city, @state , @zip,@ZipPlusFour 

		end
		close b
		deallocate b


		update #temp set EffortKey='SKAVNX2' where ProductCode='8212'	
		update #temp set ProductCode = '2110',EffortKey='SKAVRL7' where ProductCode='8212'	

		--select * from #temp

		insert CustomerOrderDetailRemitHistory 
			select 
			customerorderheaderinstance, 
			transid,
			rbid, 
			CustomerRemitHistoryInstance,
			countrycode,
			status,
			Quantity,
			Remit_rate,
			Basic_Price_Yr,
			Currency,
			lang,
			PremiumInd,
			Premium_code,
			Premium_Copy,
			abc, -- abc
			Renewal,
			ProductCode,
			ProductName,
			CatalogPrice,
			price,
			Issues, -- fill in number of issues
			 DefaultGrossValue,
			Comment,
			SwitchLetterBatchID,
			GiftCD, -- for gift order type
			GiftOrderStatus,
			 GiftCardDateGenerated,
			SupporterName,
			DateChanged,
			UserIDChanged,
			EffortKey
				from #Temp where not exists (select * from customerorderdetailremithistory x where x.customerorderheaderinstance=#Temp.customerorderheaderinstance and x.transid = #Temp.transid)
			order by customerorderheaderinstance,transid

		
			
		-- update customerordetaile status sent to remit
		update customerorderdetail set statusinstance=507 from
			customerorderdetail,#temp where customerorderdetail.customerorderheaderinstance =
				#temp.customerorderheaderinstance
				and customerorderdetail.transid=#temp.transid


	drop table #temp

	drop table #RB

	drop table #crhtempinst
GO
