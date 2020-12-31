USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spRePrepareRemitBatches]    Script Date: 06/07/2017 09:20:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- 
-- There will be one FulfillmentBatch record per Fulfillment house run
-- we are then able to track the file that gets generated as well as 
-- number of units, $'s remitted, # CHADDS and cancellations.
--
-- This stored procedure will prep remit batches and put orders and order 
-- batches in the pre-remit state.  Usually done upon close of an order batch
--
CREATE               procedure [dbo].[spRePrepareRemitBatches]
	@batchDate datetime,
	@batchID int,
	@remitstatus int OUTPUT	
as
set nocount on
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
	if( @status <> 40004)
	begin
		RAISERROR('Batch not in approved state',1,1)
		set nocount off

		return @@Error
	end

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
		--and name not like 'Fiscal%'
		--and @batchDate  between StartDate and Enddate

	select max(id)as id ,FulfillmentHouseNbr, status into #RB from   RemitBatch where  status=42000
		group by  FulfillmentHouseNbr, status
--	begin tran t1

		select  
			customerorderheaderinstance, 
			transid,
			rb.id as rbid, 
			p.countrycode,
			0 as CustomerRemitHistoryInstance,
			case p.status when '30601' then 42010 else 42000 end as status,
			recipient,
			Quantity,
			Remit_rate,
			Basic_Price_Yr,
			Currency,
			lang,
			prd.PremiumInd,
			isnull(prd.Premium_code,'') as Premium_code,
			isnull(prd.Premium_Copy,'') as Premium_Copy,
			PD.ABCCode as ABC, -- abc
			Renewal,
			ProductCode,
			P.RemitCode as RemitCode,		--RemitCode
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
			into #Items
			from 
			customerorderdetail,
			customerorderheader,
			customer,
			qspcanadaproduct..pricing_details pd,
			qspcanadaproduct..program_details prd,
			qspcanadaproduct..product p
			left join #RB rb on rb.FulfillmentHouseNbr= p.Fulfill_House_Nbr
				and rb.status=42000
			where
			p.product_instance = pd.product_instance
			and customerorderdetail.pricingdetailsid = pd.MagPrice_Instance
			and customerorderheaderinstance=customerorderheader.instance
			and orderbatchdate=@batchDate and orderbatchid=@batchID
			and prd.programsectionid = pd.programsectionid		
			and prd.program_year = pd.pricing_year 
			and prd.program_season = pd.pricing_season
			and prd.product_code = pd.product_code		
			and prd.offer_id = pd.offer_code
			and prd.taxregionid = pd.taxregionid
			and customerorderdetail.statusinstance = 507 -- 502 is paid
			and customerorderdetail.delflag=0
			and COALESCE(customerorderheader.paymentmethodinstance, 0) <> 50005 -- Payment error - not internet		-- Ben 2005/10/18
			and customerbilltoinstance=customer.instance
			and customer.statusinstance = 300
			and customerorderdetail.producttype=46001    -- mags only
			and customershiptoinstance = 0
			order by customerorderheaderinstance, 
			transid

		/*
		**   Any ship to 
		*/
		insert #Items
		select  
			customerorderheaderinstance, 
			transid,
			rb.id as rbid, 
			p.countrycode,
			0 as CustomerRemitHistoryInstance,
			case p.status when '30601' then 42010 else 42000 end as status,
			recipient,
			Quantity,
			Remit_rate,
			Basic_Price_Yr,
			Currency,
			lang,
			prd.PremiumInd,
			isnull(prd.Premium_code,'') as Premium_code,
			isnull(prd.Premium_Copy,'') as Premium_Copy,
			PD.ABCCode as ABC, -- abc
			Renewal,
			ProductCode,
			P.RemitCode as RemitCode,		--RemitCode
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
			qspcanadaproduct..product p
			left join #RB rb on rb.FulfillmentHouseNbr= p.Fulfill_House_Nbr
				and rb.status=42000
			where
			p.product_instance = pd.product_instance
			and customerorderdetail.pricingdetailsid = pd.MagPrice_Instance
			and customerorderheaderinstance=customerorderheader.instance
			and orderbatchdate=@batchDate and orderbatchid=@batchID
			and prd.programsectionid = pd.programsectionid		
			and prd.program_year = pd.pricing_year 
			and prd.program_season = pd.pricing_season
			and prd.product_code = pd.product_code		
			and prd.offer_id = pd.offer_code
			and prd.taxregionid = pd.taxregionid
			and customerorderdetail.statusinstance = 507 -- 502 is paid
			and customerorderdetail.delflag=0
			and COALESCE(customerorderheader.paymentmethodinstance, 0) <> 50005 -- Payment error - not internet		-- Ben 2005/10/18
			and customershiptoinstance=customer.instance
			and customer.statusinstance = 300
			and customerorderdetail.producttype=46001    -- mags only
			and customershiptoinstance <> 0
			order by customerorderheaderinstance, 
			transid

			
--select * from #temp
		-- should be quick and then fill in rbid where it's null
		declare a cursor for select distinct fulfill  from #Items where rbid is null
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


			
			update #Items set rbid = @maxid where fulfill = @fulfill
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

		declare b cursor for select customerorderheaderinstance,transid, rbid,customerbilltoinstance,
				address1, address2, city, state , zip,ZipPlusFour  from #Items
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
			create table #crhtempinst
			(
				 NextInstance int
			)
			insert into #crhtempinst exec qspcanadaordermanagement..InsertNextInstance 18

			select @maxcust = NextInstance from #crhtempinst
			drop table #crhtempinst
			-- Break apart recipient name

			select @recipient = recipient from #Items where 
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
			

			update #Items set CustomerRemitHistoryInstance = @maxcust where 
				rbid = @rbatch and customerorderheaderinstance=@coh and transid=@tid

			fetch next from b into	 @coh, @tid, @rbatch, @bill, @address1, @address2, @city, @state , @zip,@ZipPlusFour 

		end
		close b
		deallocate b
		-- create entries in CODRemitHistory and CustomerRemit history pointing
		-- to the corresponding FulfillmentBatch record and customerremit
--select * from #temp

		/* special processing per Roxanad
		For title 8212 (Sports Illustrated for Kids Teen Edition), when the file is send to remit, the file for TIME should go with code 2110 (Sports Illustrated for Kids) but with the appropriate effort key.
		*/

		--update #Items set EffortKey='SKAVNX2' where ProductCode='8212'	
		update #Items set ProductCode = '2110',EffortKey='SKAVRL7' where ProductCode='8212'	

		insert CustomerOrderDetailRemitHistory 
			(CustomerOrderHeaderInstance,
			TransID,
			RemitBatchID,
			CustomerRemitHistoryInstance,
			CountryCode,
			Status,
			Quantity,
			RemitRate,
			BasePrice,
			CurrencyID,
			Lang,
			PremiumIndicator,
			PremiumCode,
			PremiumDescription,
			ABCCode,
			Renewal,
			TitleCode,
			MagazineTitle,
			CatalogPrice,
			ItemPriceTotal,
			NumberOfIssues,
			DefaultGrossValue,
			Comment,
			SwitchLetterBatchID,
			GiftOrderType,
			GiftOrderStatus,
			GiftCardDateGenerated,
			SupporterName,
			DateChanged,
			UserIDChanged,
			EffortKey,
			RemitCode)
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
			EffortKey,
			RemitCode
				from #Items
			order by customerorderheaderinstance,transid

		
			
		-- update customerordetaile status sent to remit
		update customerorderdetail set statusinstance=507 from
			customerorderdetail,#Items where customerorderdetail.customerorderheaderinstance =
				#Items.customerorderheaderinstance
				and customerorderdetail.transid=#Items.transid


	drop table #Items
	drop table #RB

--	commit tran t1		

	set nocount off
	select @remitstatus =  @@error
GO
