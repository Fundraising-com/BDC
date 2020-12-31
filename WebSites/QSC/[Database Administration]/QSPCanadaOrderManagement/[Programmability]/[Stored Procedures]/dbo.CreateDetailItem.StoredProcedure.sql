USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateDetailItem]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE         procedure [dbo].[CreateDetailItem]
	@orderdate smalldatetime,
	@coh int,
	@productcode varchar(50),
	@firstname varchar(50),
	@lastname varchar(50),
	@quantity int,
	@price numeric(10,2),
	@programsectionid int,
	@catalogprice numeric(10,2),
	@quantityreserved int,
	@producttype int,
	@pricingdetailsid int,
	@status int,
	@customershiptoinstance int,
	@priceoverrideID int = 45004,
	@renewal varchar(1) = null,
	@isVoucherRedemption bit = null,
	@GiftCard varchar(1) = null,
	@SupporterName varchar(81) = null,
	@newTransID int = null OUTPUT,
	@IsShippedToAccount bit = null
	
as
	declare @nexttransid int
	declare @tax1 numeric(10,2)
	declare @tax2 numeric(10,2)		
	declare @net numeric(10,2)
	declare @gross numeric(10,2)		
	declare @campaign int
	declare @orderdatestr varchar(10)
	select @orderdatestr = convert(varchar(20),@orderdate,101)	
	declare @pr varchar(2)
	declare @isShippedToAccountValue bit
	declare @GroupProfitAmount numeric(10,2)
	
	IF @IsShippedToAccount IS NULL
	BEGIN
		SET @isShippedToAccountValue = 0
	END
	ELSE
	BEGIN
		SET @isShippedToAccountValue = @IsShippedToAccount
	END

	If (ISNULL(@producttype, 0) = 0)
	BEGIN
		SELECT	@producttype = p.Type
		FROM	QSPCanadaProduct..PRICING_DETAILS pd
		JOIN	QSPCanadaProduct..Product p
					ON	p.Product_Instance = pd.Product_Instance
		WHERE	pd.MagPrice_Instance = @pricingdetailsid

		If (ISNULL(@producttype, 0) = 0)
		BEGIN
			SET @producttype = 46001
		END
	END

	select @firstname = rtrim(ltrim(@firstname))
	select @lastname = rtrim(ltrim(ISNULL(@lastname,'')))

	DECLARE @IsGift BIT
	IF (@GiftCard = 'R' OR @GiftCard = 'X')
	BEGIN
		SET @IsGift = 1
	END
	ELSE
	BEGIN
		SET @IsGift = 0
		SET @GiftCard = NULL
	END

	---- Build the temp table
	CREATE TABLE #temp2
		(
			tax1 money,
			tax2 money,
			gross money,
			net money,
			groupprofitamount money
		)
--sp_columns 'Account'
	select @nexttransid =NextDetailTransID, @campaign=campaignid from  customerorderheader where instance=@coh
	set @newTransID = @nexttransid
	
	/*select	@pr = addr.StateProvince
	from	CustomerOrderHeader coh,
		QSPCanadaCommon..CAccount a,
		QSPCanadaCommon..Address addr
	where	a.ID = coh.AccountID
	and	addr.AddressListID = a.AddressListID
	and	addr.Address_Type = 54002
	and	coh.Instance = @coh*/
	
	IF (@customershiptoinstance > 0)
	BEGIN
		SELECT	@pr = State
		FROM	Customer
		WHERE	Instance = @customershiptoinstance
	END
	
	IF (ISNULL(@pr, '') = '')
	BEGIN
		SELECT	@pr = State
		FROM	CustomerOrderHeader coh
		JOIN	Customer cust
					ON	cust.Instance = coh.CustomerBillToInstance
		WHERE	coh.Instance = @coh
	END

	IF (ISNULL(@pr, '') = '' OR @IsShippedToAccount = 1)
	BEGIN
		select	@pr = addr.StateProvince
		from	CustomerOrderHeader coh,
				QSPCanadaCommon..CAccount a,
				QSPCanadaCommon..Address addr
		where	a.ID = coh.AccountID
		and	addr.AddressListID = a.AddressListID
		and	addr.Address_Type = 54002
		and	coh.Instance = @coh
		
		IF (@CustomerShipToInstance > 0)
		BEGIN
			UPDATE	Customer
			SET		[State] = @pr
			WHERE	Instance = @CustomerShipToInstance
			AND		ISNULL([State], '') = ''
		END
		ELSE
		BEGIN
			UPDATE	cust
			SET		[State] = @pr
			FROM	CustomerOrderHeader coh
			JOIN	Customer cust
						ON	cust.Instance = coh.CustomerBillToInstance
			WHERE	coh.Instance = @coh
			AND		ISNULL([State], '') = ''
		END
	END


--select cast(convert(datetime,getdate(),1) as varchar(10)	)
/*
	PRINT '@orderdate: ' +  Convert(varchar, @orderdate)
	PRINT '@price: ' + Convert(varchar, @price)
	PRINT '@programsectionid: ' + Convert(varchar, @programsectionid)
	PRINT '@pricingdetailsid: ' + Convert(varchar, @pricingdetailsid)
	PRINT '@productcode: ' + Convert(varchar, @productcode)
	PRINT '@campaign: ' + Convert(varchar, @campaign)
	PRINT '@province: ' + @pr
*/
--cast(convert(datetime,getdate(),1) as varchar(10)	)
--print @orderdatestr
	insert into #temp2 exec qspcanadacommon..PR_CALC_ORDER_ITEM_AMOUNTS
	@orderdatestr, @price, @programsectionid, 
			@productcode, 'N', @campaign, @pricingdetailsid,@pr, @coh

	select @tax1=tax1, @tax2=tax2, @net=net, @gross=gross, @groupprofitamount=groupprofitamount from #temp2

	declare @name varchar(50)
	select @name = LEFT(Product_Sort_Name, 50) from
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..Product p where MagPrice_Instance = @pricingdetailsid
			and pd.Product_Instance = p.Product_Instance
			/*and pd.product_code= p.product_code
			and pd.pricing_year = p.product_year
			and pd.pricing_season = p.product_season
			and p.type= @producttype*/
		
--sp_columns 'CustomerOrderDetail'
	insert into CustomerOrderDetail	
		(CustomerOrderHeaderInstance,
		TransID,
		CustomerShipToInstance,
		Recipient, 
		ProductCode,
		ProductName,
		Quantity,
		Price,
		PriceA,
		Tax,
		TaxA,
		StatusInstance,
		Renewal,
		CreationDate,
		ChangeUserID,
		ChangeDate,
		GiftCD,
		IsGift,
		ProgramSectionID,
		CatalogPrice,
		QuantityReserved,
		PriceOverrideID,
		ProductType,
		PricingDetailsID,
		Tax2,
		Tax2A,
		Net,
		Gross,
		SupporterName,
		DelFlag,
		IsVoucherRedemption,
		IsShippedToAccount,
		GroupProfitAmount)
		values(
			@coh,
			@nexttransid,
			@customershiptoinstance,--0,  -- ship to
			@firstname + ' ' + ISNULL(@lastname,''),
			@productcode,
			@name,
			@quantity,
			@price,
			@price,
			@tax1,
			@tax1,
			@status,
			@renewal,
			GetDate(),
			'ADMI',
			GetDate(),
			@GiftCard,
			@IsGift,
			@programsectionid,
			@catalogprice,
			@QuantityReserved,
			@PriceOverrideID,
			@producttype,
			@pricingdetailsid,
			@tax2,
			@tax2,
			@net,
			@gross,
			@SupporterName,
			0,
			@isVoucherRedemption,
			@isShippedToAccountValue,
			@GroupProfitAmount
		)

	update 	customerorderheader set NextDetailTransID=	@nexttransid+1 where instance=@coh

	DROP TABLE #temp2
GO
