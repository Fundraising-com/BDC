drop table #TempPID
-- GET LIST
SELECT		cod.CustomerOrderHeaderInstance,
		cod.TransID,
		cod.statusinstance as CODStatus,
		c.State,
		ca.Lang,
		cod.ProductCode,
		cod.Price,
		b.OrderID,
		ca.IsStaffOrder
INTO		#TempPID
from 
customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join customer c on coh.customerbilltoinstance = c.instance
join batch b on coh.orderbatchdate = b.date and b.id = coh.orderbatchid
join qspcanadacommon..campaign ca on b.campaignid = ca.id
and isnull(pricingdetailsid,0)=0
where 
b.orderqualifierid = 39009
and cod.delflag = 0
and b.statusinstance in (40002,40003,40013) 
and b.date>'9/1/07'
and b.ordertypecode not in(41009)

and coh.instance not in (8775976,8782714,8807137)

order by orderid, coh.instance, transid


select * from #TempPID


begin tran t1
-- MATCH TO PRICING DETAILS
UPDATE		cod
SET		--cod.ProductName = p.Product_Sort_Name,
		cod.Quantity = pd.Nbr_Of_Issues,
		cod.Price = pd.QSP_Price,
		cod.PriceA = pd.QSP_Price,
		cod.ProgramSectionID = pd.ProgramSectionID,
		cod.PriceOverrideID = 45002,
		cod.PricingDetailsID = pd.MagPrice_Instance,
		cod.producttype= p.type
FROM		#TempPID t,
		CustomerOrderDetail cod,
		QSPCanadaCommon..TaxRegionProvince trp,
		QSPCanadaProduct..Pricing_Details pd,
		QSPCanadaProduct..Product p
WHERE		cod.CustomerOrderHeaderInstance = t.CustomerOrderHeaderInstance
AND		cod.TransID = t.TransID
AND		trp.Province = t.State
AND		pd.Product_Code = t.ProductCode
--AND		pd.QSP_Price = t.Price
AND		pd.ProgramSectionID = CASE t.IsStaffOrder WHEN 1 THEN 426 ELSE CASE t.Lang WHEN 'FR' THEN 428 ELSE 398 END END
AND		pd.TaxRegionID = trp.TaxRegionID
AND		p.Product_Instance = pd.Product_Instance
--rollback tran t1
commit tran t1

select * from customerorderdetail cod join #temppid t on t.customerorderheaderinstance = cod.customerorderheaderinstance and t.transid = cod.transid

--Update Batch
begin tran t2
UPDATE	b
SET		b.IsStaffOrder = ca.IsStaffOrder
FROM	#TempPID t
JOIN	Batch b
			ON	b.OrderID = t.OrderID
JOIN	QSPCanadaCommon..Campaign ca
			ON	b.CampaignID = ca.ID
--rollback tran t1
commit tran t1


DECLARE		@iCustomerOrderHeaderInstance	int,
		@iTransID			int,
		@i				int,
		@OrderDateStr			varchar(20),
		@Tax				money,
		@TaxA				money,
		@Net				money,
		@Gross				money,
		@fPrice				numeric(10, 2),
		@iProgramSectionID		int,
		@zProductCode			varchar(20),
		@iAccountID			int,
		@iCampaignID			int,
		@iPricingDetailsID		int,
		@zProvince			varchar(2),
		@stat                           int


DECLARE		c1 CURSOR FOR
		SELECT		 CustomerOrderHeaderInstance,
				TransID
		FROM		#TempPID


OPEN c1

FETCH NEXT FROM c1 INTO @iCustomerOrderHeaderInstance, @iTransID

WHILE(@@fetch_status = 0)
BEGIN
	---- Build the temp table
	CREATE TABLE #taxes
	(
		tax1 money,
		tax2 money,
		gross money,
		net money
	)

	SELECT		@OrderDateStr = CONVERT(varchar(20), coh.OrderBatchDate, 101),
			@fPrice = cod.Price,
			@iProgramSectionID = cod.ProgramSectionID,
			@zProductCode = cod.ProductCode,
			@iAccountID = coh.AccountID,
			@iCampaignID = coh.CampaignID,
			@iPricingDetailsID = cod.PricingDetailsID
	FROM		CustomerOrderHeader coh,
			CustomerOrderDetail cod
	WHERE		cod.CustomerOrderHeaderInstance = coh.Instance
	AND		coh.Instance = @iCustomerOrderHeaderInstance
	AND		cod.TransID = @iTransID

	SELECT		@zProvince = addr.StateProvince
	FROM		qspcanadacommon..CAccount a,
			qspcanadacommon..Address addr
	WHERE		addr.AddressListID = a.AddressListID
	AND		addr.Address_Type = 54002
	AND		a.ID = @iAccountID
		
	INSERT INTO #taxes EXEC QSPCanadaCommon..PR_CALC_ORDER_ITEM_AMOUNTS
	@OrderDateStr, @fPrice, @iProgramSectionID, 
	@zProductCode, 'N', @iCampaignID, @iPricingDetailsID, @zProvince

	select @Tax=tax1, @TaxA=tax2, @Net=net, @Gross=gross from #taxes
	
	DROP TABLE #taxes

	--if(@stat=0 or @stat=19000)
	--begin
		UPDATE		CustomerOrderDetail
		SET		Tax = @Tax,
				TaxA = @Tax,
				Tax2 = @TaxA,
				Tax2A = @TaxA,
				Net = @Net,
				Gross = @Gross,
				StatusInstance=502
		WHERE		CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
		AND		TransID = @iTransID

	
	FETCH NEXT FROM c1 INTO @iCustomerOrderHeaderInstance, @iTransID
END

CLOSE c1
DEALLOCATE c1

Declare @o int
Declare c2 cursor for select distinct  orderid from #tempPID where orderid <> 9516018
open c2
FETCH NEXT FROM c2 INTO @o
WHILE(@@fetch_status = 0)
BEGIN
	--update batch set isstafforder=1 where orderid = @o
	exec pr_CloseOrder @o

	insert into ReportRequestBatch (BatchOrderId, IsPrinted, IsQSPPrint) values(@o,1,1)
	FETCH NEXT FROM c2 INTO @o
END
CLOSE c2
DEALLOCATE c2

