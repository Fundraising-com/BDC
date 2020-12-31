USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spRemitIndividualItem_MS]    Script Date: 06/07/2017 09:20:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   procedure [dbo].[spRemitIndividualItem_MS]
		@customerorderheaderinstance int,
		@transid int,
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
	declare @now smalldatetime
	declare @cod_Season varchar(1)
	declare @cod_Year int
	declare @Season varchar(1)
	declare @Year int
	declare @productcode varchar(10)
	declare @newpricingdetailsid int 
	declare @closest_nbr_issues int
	declare @taxregionid int
	declare @Cnt	int
	declare @newProgramSection int


	--Get actual season/year
	SET @now = getDate()

	SELECT 

	-- Ben - 2006-01-31 : Changed so that it keeps the products for one month after a season change
	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 THEN YEAR(CONVERT(smalldatetime,@now)) + 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 7 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 OR MONTH(CONVERT(smalldatetime,@now)) = 1 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,@now)) BETWEEN 2 AND 7 THEN 'S'
		ELSE ''
		END

	--Get the COD seaon, year, productcode, taxregion
	select @cod_Season = pricing_season,@cod_Year = pricing_year,@productcode=product_code, @taxregionid=taxregionid 
	from qspcanadaproduct..pricing_details, customerorderdetail 
	where pricingdetailsid=magprice_instance
	and customerorderheaderinstance =@customerorderheaderinstance and transid=@transid

	--if cod is not current year/season then correct with current one
	IF @cod_Season <> @Season OR @cod_Year <>  @Year or @cod_Year is null or @cod_Season is null
	BEGIN

		select @productcode = productcode from customerorderdetail where customerorderheaderinstance =@customerorderheaderinstance and transid=@transid

		set @taxregionid = coalesce(@taxregionid,1)

		--get all combinaison for the magazine
		select product_code, taxregionid, min(nbr_of_issues) as low_issue, max(nbr_of_issues) as high_issue, avg(nbr_of_issues) as median 
		into #SpringOffers
		from qspcanadaproduct..pricing_details 
		where pricing_year=@Year and pricing_season=@Season and product_code=@productcode 
		group by product_code, taxregionid
	
	
		--get the closet number of isses
		select	@closest_nbr_issues = 	case --Nbr_of_Issues
						when Quantity < #SpringOffers.median then #SpringOffers.low_issue 
						else #SpringOffers.high_issue 
						end
		from customerorderdetail, #SpringOffers
		where 	customerorderheaderinstance = @customerorderheaderinstance
		and TransID = @transid
		and productcode=product_code

		--get the new pricingdetailsid
		select  top 1 @newpricingdetailsid =  magprice_instance,
			       @newProgramSection= ProgramSectionId
		from	qspcanadaproduct..pricing_details
		where 	pricing_year=@Year 
		and pricing_season=@Season 
		and product_code=@productcode
		and taxregionid = @taxregionid
		and nbr_of_issues = @closest_nbr_issues

		/*print @season
		print @productcode
		print @year
		print @closest_nbr_issues
		print @taxregionid*/

		--update the cod with current pricingdetailsid
		update customerorderdetail 
		set pricingdetailsid = @newpricingdetailsid, programSectionid=@newProgramSection
		where customerorderheaderinstance = @customerorderheaderinstance
		and TransID = @transid

	END
	
	--Last remit batch ids for all fulfillment houses
	select max(id)as id ,FulfillmentHouseNbr, status into #RB from   RemitBatch 
	where  status=42000
	group by FulfillmentHouseNbr, status

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
	and customerorderheader.instance=@customerorderheaderinstance
	and transid = @transid
	and prd.programsectionid = pd.programsectionid		
	and prd.program_year = pd.pricing_year 
	and prd.program_season = pd.pricing_season
	and prd.product_code = pd.product_code		
	and prd.offer_id = pd.offer_code
	and prd.taxregionid = pd.taxregionid
	and customerorderdetail.statusinstance <> 507 -- 502 is paid
	and customerorderdetail.delflag=0
	and customerbilltoinstance=customer.instance
	--and customer.statusinstance = 300
	and customerorderdetail.producttype=46001    -- mags only
	and customershiptoinstance = 0
	order by customerorderheaderinstance, transid

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
		case p.status when '30601' then 42010 else 42000 end as status,
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
		isnull(ZipPlusFour,'') as ZipPlusFour,		isnull(pd.Effort_Key,'') as EffortKey
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
		productcode=p.product_code 
		and p.product_season=pd.pricing_season
		and p.Product_year=pd.pricing_year
		and p.product_code=pd.product_code			
		and customerorderdetail.pricingdetailsid = pd.MagPrice_Instance
		and customerorderheaderinstance=customerorderheader.instance
		and customerorderheader.instance=@customerorderheaderinstance
		and transid = @transid
		and prd.programsectionid = pd.programsectionid		
		and prd.program_year = pd.pricing_year 
		and prd.program_season = pd.pricing_season
		and prd.product_code = pd.product_code		
		and prd.offer_id = pd.offer_code
		and prd.taxregionid = pd.taxregionid
		and customerorderdetail.statusinstance  <> 507 -- 502 is paid
		and customerorderdetail.delflag=0
		and customershiptoinstance=customer.instance
		--and customer.statusinstance = 300
		and customerorderdetail.producttype=46001    -- mags only
		and customershiptoinstance <> 0
	order by customerorderheaderinstance, transid

			
	Select @Cnt = Count(*)  from #temp
	
	If @Cnt > 0	--Only do Item exist for remit
	Begin
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
			create table #crhtempinst
			(
				 NextInstance int
			)
			insert into #crhtempinst exec qspcanadaordermanagement..InsertNextInstance 18

			select @maxcust = NextInstance from #crhtempinst
			drop table #crhtempinst
			-- Break apart recipient name

			select @recipient = ltrim(rtrim(recipient)) from #temp where 
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
		-- create entries in CODRemitHistory and CustomerRemit history pointing
		-- to the corresponding FulfillmentBatch record and customerremit
		--select * from #temp

		/* special processing per Roxanad
		For title 8212 (Sports Illustrated for Kids Teen Edition), when the file is send to remit, the file for TIME should go with code 2110 (Sports Illustrated for Kids) but with the appropriate effort key.
		*/

--		update #temp set EffortKey='SKAVNX2' where ProductCode='8212'	
		update #temp set ProductCode = '2110',EffortKey='SKAW1A7' where ProductCode='8212'	

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
			EffortKey)
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
			customerorderdetail where customerorderdetail.customerorderheaderinstance =
				@coh
				and customerorderdetail.transid=@transid

		select @remitstatus =  @@error
		drop table #temp
		drop table #RB

--	commit tran t1		
	End
	Else
	Begin
		Set @remitstatus = 1
	End
	set nocount off
GO
