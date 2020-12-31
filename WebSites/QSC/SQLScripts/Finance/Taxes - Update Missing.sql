SELECT	cod.*
INTO	#SubsToFix
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		Customer cust
				ON	cust.Instance =CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
where isnull(cust.State, '') = ''
and b.Date >= '2014-07-01'
and cod.DelFlag <> 1
and cod.StatusInstance not in (501)
and ISNULL(cod.PricingDetailsID, 0) > 0
order by cod.CustomerOrderHeaderInstance

select * from #SubsToFix

begin tran

DECLARE @CustomerOrderHeaderInstance INT,
		@TransID INT

DECLARE	info CURSOR FOR
SELECT	CustomerOrderHeaderInstance, TransID
FROM	#SubsToFix
order by CustomerOrderHeaderInstance, TransID

OPEN info
FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance, @TransID

WHILE(@@fetch_status = 0)
BEGIN

	CREATE TABLE #temp2
		(
			tax1 money,
			tax2 money,
			gross money,
			net money
		)

	DECLARE	@OrderDateStr			VARCHAR(20),
			@Tax					NUMERIC(14, 6),
			@TaxA					NUMERIC(14, 6),
			@Net					NUMERIC(14, 6),
			@Gross					NUMERIC(14, 6),
			@CampaignID				INT,
			@Province				VARCHAR(2),
			@Tax1					money,
			@Tax2					money,
			@Price					NUMERIC(10, 2),
			@ProgramSectionID		INT,
			@PricingDetailsID		INT,
			@ProductCode			VARCHAR(20)

	SELECT	@OrderDateStr			= CONVERT(VARCHAR(20), coh.OrderBatchDate, 101),
			@CampaignID				= camp.ID,
			@Price					= cod.Price,
			@ProgramSectionID		= pd.ProgramSectionID,
			@PricingDetailsID		= pd.MagPrice_Instance,
			@ProductCode			= prod.Product_Code
	FROM	CustomerOrderDetail cod
	JOIN	CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
	JOIN	Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
	JOIN	QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
	JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN	QSPCanadaProduct..Product prod
			ON	prod.Product_Instance = pd.Product_Instance
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID
	
	select	@Province = addr.StateProvince
	from	CustomerOrderHeader coh,
			QSPCanadaCommon..CAccount a,
			QSPCanadaCommon..Address addr
	where	a.ID = coh.AccountID
	and		addr.AddressListID = a.AddressListID
	and		addr.Address_Type = 54002
	and		coh.Instance = @CustomerOrderHeaderInstance
	
	insert into #temp2 exec qspcanadacommon..PR_CALC_ORDER_ITEM_AMOUNTS
	@orderdatestr, @price, @programsectionid, 
			@productcode, 'N', @campaignID, @pricingdetailsid,@province

	select @tax1=tax1, @tax2=tax2, @net=net, @gross=gross from #temp2

	DROP TABLE #temp2

	update	cod
	SET		Tax = @Tax1,
			TaxA = @Tax1,
			Tax2 = @Tax2,
			Tax2A = @Tax2,
			Net = @Net,
			Gross = @Gross
	from	CustomerOrderDetail cod
	WHERE	cod.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
	AND		cod.TransID = @TransID

	DECLARE @CustInstance INT
	SELECT	@CustInstance = cust.Instance
	FROM	CustomerOrderHeader coh
	JOIN	Customer cust ON cust.Instance = coh.CustomerBillToInstance
	WHERE	coh.Instance = @CustomerOrderHeaderInstance
	
	update	Customer
	SET		State = @Province
	WHERE	Instance = @CustInstance
	AND		ISNULL(State,'') = ''

	--Get next record
	FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance, @TransID
END
CLOSE info
DEALLOCATE info

--commit tran	
	
drop table #SubsToFix
