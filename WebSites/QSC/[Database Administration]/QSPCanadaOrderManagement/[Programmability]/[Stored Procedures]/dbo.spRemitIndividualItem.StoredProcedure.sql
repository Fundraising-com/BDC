USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spRemitIndividualItem]    Script Date: 06/07/2017 09:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE                procedure [dbo].[spRemitIndividualItem]
	@customerorderheaderinstance int,
	@transid int,
	@remitstatus int OUTPUT	
as
--set nocount on
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
	declare @remitcode varchar(20)
	declare @newpricingdetailsid int
	declare @newprogramsectionid int
	declare @closest_nbr_issues int
	declare @taxregionid int
	declare @productcode varchar(20)

--Get actual season/year
SET @now = getDate()

SELECT 

	-- Ben - 2006-01-04 : Changed so that it keeps the products for one month after a season change
	@Year = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 THEN YEAR(CONVERT(smalldatetime,@now)) + 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 7 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE
			0
		END
	,@Season = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 OR MONTH(CONVERT(smalldatetime,@now)) = 1 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,@now)) BETWEEN 2 AND 7 THEN 'F' --All mag offers are Fall even though they span the entire year--'S'
		ELSE ''
		END

--Get the COD seaon, year, productcode, taxregion
SELECT		@cod_Season = p.Product_Season,
			@cod_Year = p.Product_Year,
			@remitcode = p.RemitCode,
			@taxregionid = pd.TaxRegionID,
			@productcode = p.Product_Code
FROM		QSPCanadaProduct..Product p,
			QSPCanadaProduct..Pricing_Details pd,
			CustomerOrderDetail cod
WHERE		pd.Product_Instance = p.Product_Instance
AND			cod.PricingDetailsID = pd.MagPrice_Instance
AND			cod.CustomerOrderHeaderInstance = @customerorderheaderinstance
AND			cod.TransID = @transid

--if cod is not current year/season then correct with current one
IF @cod_Season <> @Season OR @cod_Year <>  @Year or @cod_Year is null or @cod_Season is null
BEGIN

	set @taxregionid = coalesce(@taxregionid,1)
	--get all combinations for the magazine
	SELECT		p.RemitCode,
				pd.TaxRegionID,
				MIN(pd.Nbr_Of_Issues) AS low_issue,
				MAX(pd.Nbr_Of_Issues) AS high_issue,
				AVG(pd.Nbr_Of_Issues) AS median
	INTO		#NewOffers
	FROM		QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p
	WHERE		p.Product_Instance = pd.Product_Instance
	AND			p.Product_Year = @Year
	AND			p.Product_Season = @Season
	AND			p.RemitCode = @remitcode
	AND			(p.Product_Code NOT LIKE 'G%' OR @productcode LIKE 'G%')
	AND			(p.Product_Code NOT LIKE 'K%' OR @productcode LIKE 'K%')
	AND			(p.Product_Code NOT LIKE 'D%' OR @productcode LIKE 'D%')
	GROUP BY	p.RemitCode,
				pd.TaxRegionID
	

	--get the closet number of isses
	SELECT		@closest_nbr_issues = CASE --Nbr_of_Issues
					WHEN cod.Quantity < no.median THEN no.low_issue 
					ELSE no.high_issue END
	FROM 		CustomerOrderDetail cod,
				#NewOffers no
	WHERE 		no.RemitCode = @remitcode
	AND			cod.CustomerOrderHeaderInstance = @customerorderheaderinstance
	AND			cod.TransID = @transid

	--get the new pricingdetailsid
	SELECT		TOP 1
				@newpricingdetailsid = pd.MagPrice_Instance,
				@newprogramsectionid = pd.ProgramSectionID
	FROM		QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p
	WHERE		p.Product_Instance = pd.Product_Instance
	AND			p.Product_Year = @Year 
	AND			p.Product_Season = @Season 
	ANd			p.RemitCode = @remitcode
	AND			pd.TaxRegionID = @taxregionid
	AND			pd.Nbr_Of_Issues = @closest_nbr_issues

	print @season
	print @remitcode
	print @year

	print @closest_nbr_issues
	print @taxregionid

	--update the cod with current pricingdetailsid
	update customerorderdetail 
		set pricingdetailsid = @newpricingdetailsid,
			programsectionid = @newprogramsectionid
				where customerorderheaderinstance = @customerorderheaderinstance
				and TransID = @transid

	print 'pdid='
	print @newpricingdetailsid

END

	select max(id)as id ,FulfillmentHouseNbr, status into #RB from   RemitBatch where  status=42000
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
			prd.PremiumInd,
			isnull(prd.Premium_code,'') as Premium_code,
			isnull(prd.Premium_Copy,'') as Premium_Copy,
			PD.ABCCode as ABC, -- abc
			Renewal,
			ProductCode,
			P.RemitCode AS RemitCode,
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
			into #Item
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
			p.product_instance=pd.product_instance
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
			and customerorderdetail.statusinstance NOT IN (500, 507, 515, 516) -- 502 is paid
			and customerorderdetail.delflag=0
			and COALESCE(customerorderheader.paymentmethodinstance, 0) <> 50005 -- Payment error - not internet		-- Ben 2005/10/18
			and customerbilltoinstance=customer.instance
			--and customer.statusinstance = 300
			and customerorderdetail.producttype=46001    -- mags only
			and customershiptoinstance = 0
			order by customerorderheaderinstance, 
			transid

		/*
		**   Any ship to 
		*/
		insert #Item
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
			P.RemitCode as RemitCode,
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
			p.product_instance=pd.product_instance
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
			and customerorderdetail.statusinstance NOT IN (500, 507, 515, 516) -- 502 is paid
			and customerorderdetail.delflag=0
			and COALESCE(customerorderheader.paymentmethodinstance, 0) <> 50005 -- Payment error - not internet		-- Ben 2005/10/18
			and customershiptoinstance=customer.instance
			--and customer.statusinstance = 300
			and customerorderdetail.producttype=46001    -- mags only
			and customershiptoinstance <> 0
			order by customerorderheaderinstance, 
			transid

			
--select * from #temp
		-- should be quick and then fill in rbid where it's null
		declare a cursor for select distinct fulfill  from #Item where rbid is null
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


			
			update #Item set rbid = @maxid where fulfill = @fulfill
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
				address1, address2, city, state , zip,ZipPlusFour  from #Item
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

			select @recipient = ltrim(rtrim(recipient)) from #Item where 
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
			

			update #Item set CustomerRemitHistoryInstance = @maxcust where 
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
		update #Item set ProductCode = '2110',EffortKey='SKAWYW9' where ProductCode='8212'	

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
				from #Item --where not exists (select * from customerorderdetailremithistory x where x.customerorderheaderinstance=#Temp.customerorderheaderinstance and x.transid = #Temp.transid)
			order by customerorderheaderinstance,transid

		
			
		-- update customerordetaile status sent to remit
		update customerorderdetail set statusinstance=507 from
			customerorderdetail where customerorderdetail.customerorderheaderinstance =
				@coh
				and customerorderdetail.transid=@transid


	drop table #Item
	drop table #RB

--	commit tran t1		

	
	select @remitstatus =  @@error
GO
