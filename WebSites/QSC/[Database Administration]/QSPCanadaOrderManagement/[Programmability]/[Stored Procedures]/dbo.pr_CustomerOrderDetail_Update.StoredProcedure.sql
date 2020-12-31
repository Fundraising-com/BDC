USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetail_Update]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetail_Update]
	@coh					int,
	@transid				int,
	@iShipToInstance		int,
	@recipient				varchar(81),
	@quantity				int,
	@price					numeric(10,2),
	@pricingdetailsid		int,
	@overridecode			int,
	@status					int,
	@delflag				int

AS

declare @nexttransid		int
declare @tax1				numeric(10,2)
declare @tax2				numeric(10,2)		
declare @net				numeric(10,2)
declare @gross				numeric(10,2)		
declare @campaign			int
declare @orderdatestr		varchar(10)
declare @pr					varchar(2)
declare @programsectionid	int
declare @catalogprice		numeric(10,2)
declare @producttype		int
declare @productcode		varchar(50)
DECLARE	@name				varchar(50)
DECLARE @fTotalPrice		float

IF @delflag = 1 --mark record as deleted
BEGIN
	update  CustomerOrderDetail	
	set		DelFlag = @delflag	
	where	CustomerOrderHeaderInstance = @coh
	and		TransID = @transid
END
ELSE
BEGIN
	---- Build the temp table
	CREATE TABLE #temp2
		(
			tax1 money,
			tax2 money,
			gross money,
			net money
		)

	SELECT		@name = Product_Sort_Name,
				@programsectionid = programsectionid,
				@catalogprice = QSP_Price,
				@producttype = type,
				@productcode = p.product_code
	FROM		QSPCanadaProduct..Pricing_Details pd,
				QSPCanadaProduct..Product p 
	WHERE		pd.MagPrice_Instance = @pricingdetailsid
	AND			pd.product_instance = p.product_instance

	/*SELECT		@pr = addr.StateProvince
	FROM		CustomerOrderHeader coh,
				QSPCanadaCommon..CAccount a,
				QSPCanadaCommon..Address addr
	WHERE		a.ID = coh.AccountID
	AND			addr.AddressListID = a.AddressListID
	AND			addr.Address_Type = 54002
	AND			coh.Instance = @coh*/
	
	IF (@iShipToInstance > 0)
	BEGIN
		SELECT	@pr = State
		FROM	Customer
		WHERE	Instance = @iShipToInstance
	END
	
	IF (ISNULL(@pr, '') = '')
	BEGIN
		SELECT	@pr = State
		FROM	CustomerOrderHeader coh
		JOIN	Customer cust
					ON	cust.Instance = coh.CustomerBillToInstance
		WHERE	coh.Instance = @coh
	END

	insert into #temp2 exec qspcanadacommon..PR_CALC_ORDER_ITEM_AMOUNTS
	@orderdatestr, @price, @programsectionid, 
			@productcode, 'N', @campaign, @pricingdetailsid,@pr

	select @tax1=tax1, @tax2=tax2, @net=net, @gross=gross from #temp2

	SET		@fTotalPrice = @price * @quantity
	
	update  CustomerOrderDetail	
	set		Recipient= @recipient,
			CustomerShipToInstance = @iShipToInstance,
			ProductCode=@productcode,
			ProductName=@name,
			Quantity=@quantity,
			Price=@fTotalPrice,
			PriceA=@fTotalPrice,
			Tax=@tax1,
			TaxA=@tax1,
			ProgramSectionID=@programsectionid,
			CatalogPrice=@catalogprice,
			PriceOverrideID=@overridecode,
			ProductType=@producttype,
			PricingDetailsID=@pricingdetailsid,
			Tax2=@tax2,
			Tax2A=@tax2,
			Net=@net,
			Gross=@gross,
			DelFlag=@delflag	
			where CustomerOrderHeaderInstance= @coh
				and TransID=@transid
	
	DROP TABLE #temp2
END
GO
