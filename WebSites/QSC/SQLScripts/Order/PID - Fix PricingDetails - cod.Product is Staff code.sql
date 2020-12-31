-- flipped to delflag =1
-- in 1227 cancelled
-- interet
-- staff sub but regular order
drop table #SubsToChange
create table #SubsToChange
(
                coh int,
                transid int,
                pc varchar(20),
                pname varchar(200),
                quantity int,
                oldprice numeric(9,2),
                lang varchar(4),
                taxregionid int,
                pidmatch int,
                pcmatch varchar(20),
                newprice numeric(9,2),
				newprogramsectionID int
)
insert into #SubsToChange
SELECT	cod.customerorderheaderinstance,
		cod.transid,
		cod.productcode,
		product_sort_name,
		cod.quantity,
		cod.price,
		ca.lang,
		pd.TaxRegionID,
		0,
		'',
		0,
		0
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderDetailAudit coda
			ON cod.CustomerOrderHeaderInstance = coda.CustomerOrderHeaderInstance
			AND cod.TransID = coda.TransID
--LEFT JOIN CustomerOrderDetailRemitHistory codrh
--			ON codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
--			AND codrh.TransID = cod.TransID
JOIN	CustomerOrderHeader coh
			ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	InternetOrderID ioi
			ON ioi.CustomerOrderHeaderInstance = coh.Instance
JOIN	Batch b
			ON coh.OrderBatchDate = b.[date] 
			AND coh.OrderBatchID = b.ID
JOIN	QSPCanadaCommon..Campaign ca
			on b.campaignid = ca.id
LEFT JOIN QSPCanadaProduct..Pricing_Details pd
			ON pd.MagPrice_Instance = cod.PricingDetailsID
Left JOIN QSPCanadaProduct..product p
			ON p.product_instance = pd.product_instance
WHERE	cod.DelFlag = 1
AND		coda.DelFlag = 0
AND		cod.StatusInstance = 506
AND		coda.AuditDate >= '2007-10-17 17:20:00'
AND		coda.AuditDate < '2007-10-17 17:22'
AND		cod.ProductType IN (46001)
order by cod.CustomerOrderHeaderInstance ,cod.transid 

update	#SubsToChange 
set		pidmatch = magprice_instance,
		pcmatch=p.product_code,
		newprice=qsp_price,
		newprogramsectionID = pd.programsectionID
from	qspcanadaproduct..product p,
		qspcanadaproduct..pricing_details pd,
		#SubsToChange 
where	product_sort_name like pname
and		quantity = nbr_of_issues
and		p.product_instance=pd.product_instance
and		p.product_year=2008
and		#SubsToChange.Lang = 'EN'
and		programsectionid in (398)
and		pd.taxregionid= #SubsToChange.taxregionid

update	#SubsToChange
set		pidmatch = magprice_instance,
		pcmatch = p.product_code,
		newprice=qsp_price,
		newprogramsectionID = pd.programsectionID
from	qspcanadaproduct..product p,
		qspcanadaproduct..pricing_details pd,
		#SubsToChange 
where	product_sort_name like pname
and		quantity = nbr_of_issues
and		p.product_instance=pd.product_instance
and		p.product_year=2008
and		#SubsToChange.Lang = 'FR'
and		programsectionid in (428)
and		pd.taxregionid= #SubsToChange.taxregionid

update	#SubsToChange
set		pidmatch = 52427,
		pcmatch = 'F742',
		newprice = 22.00,
		newprogramsectionID = 398
where	coh	= 8733912
AND		TransID = 3

update	#SubsToChange
set		pidmatch = 52077,
		pcmatch = 'F764',
		newprice = 33.00,
		newprogramsectionID = 398
where	coh	= 8734358
AND		TransID = 4

update	#SubsToChange
set		pidmatch = 52117,
		pcmatch = 'F749',
		newprice = 40.00,
		newprogramsectionID = 398
where	coh	= 8734436
AND		TransID = 4

update	#SubsToChange
set		pidmatch = 51549,
		pcmatch = '2332',
		newprice = 38.00,
		newprogramsectionID = 398
where	coh	= 8735148
AND		TransID = 3

update	#SubsToChange
set		pidmatch = 52431,
		pcmatch = '1730',
		newprice = 52.00,
		newprogramsectionID = 398
where	coh	= 8735148
AND		TransID = 5

update	#SubsToChange
set		pidmatch = 52117,
		pcmatch = 'F749',
		newprice = 40.00,
		newprogramsectionID = 398
where	coh	= 8735505
AND		TransID = 1

update	#SubsToChange
set		pidmatch = 52751,
		pcmatch = 'F721',
		newprice = 39.00,
		newprogramsectionID = 398
where	coh	= 8736100
AND		TransID = 9

update	#SubsToChange
set		pidmatch = 52117,
		pcmatch = 'F749',
		newprice = 40.00,
		newprogramsectionID = 398
where	coh	= 8736502
AND		TransID = 1

update	#SubsToChange
set		pidmatch = 52165,
		pcmatch = 'F734',
		newprice = 39.00,
		newprogramsectionID = 398
where	coh	= 8736851
AND		TransID = 7

update	#SubsToChange
set		pidmatch = 52431,
		pcmatch = '1730',
		newprice = 52.00,
		newprogramsectionID = 398
where	coh	= 8736997
AND		TransID = 1

select * from #substochange

--Delete old crh's and codrh's
begin tran t1

delete	crh
FROM	CustomerRemitHistory crh
JOIN	CustomerOrderDetailRemitHistory codrh
			ON	codrh.CustomerRemitHistoryInstance = crh.Instance
JOIN	#SubsToChange stc
			ON	stc.coh = codrh.CustomerOrderHeaderInstance
			AND	stc.TransID = codrh.TransID

delete	codrh
FROM	CustomerOrderDetailRemitHistory codrh
JOIN	#SubsToChange stc
			ON	stc.coh = codrh.CustomerOrderHeaderInstance
			AND	stc.TransID = codrh.TransID

commit tran t1


--Update COD
BEGIN TRAN t2
UPDATE		cod
SET			cod.ProductCode = stc.pcmatch,
			cod.Price = stc.NewPrice,
			cod.PricingDetailsID = stc.pidMatch,
			cod.ProgramSectionID = stc.NewProgramSectionID,
			cod.PriceOverrideID = 45004,
			cod.StatusInstance = 502,
			cod.DelFlag = 0
FROM		CustomerOrderDetail cod
JOIN		#substochange stc
				ON	stc.COH = cod.CustomerOrderHeaderInstance
				AND	stc.TransID = cod.TransID
COMMIT TRAN t2

/*
CustomerOrderHeaderInstance	TransID
8733912	3
8734358	4
8734436	4
8735148	3
8735148	5
8735505	1
8736100	9
8736502	1
8736851	7
8736997	1
8747105	2
8747537	3
8780722	1
8780722	6
8780722	8
8780722	9
8780722	12
8780722	13
8780722	17
8782720	3
8782720	4
8782720	6
8782720	7
Have no codrh, must ensure they still don't after
*/

/*
Afterwards, will look at any chadds that took place
*/

-- REMIT
DECLARE	@iCustomerOrderHeaderInstance	int,
		@iTransID			int,
		@i					int,
		@OrderDateStr		varchar(20),
		@Tax				money,
		@TaxA				money,
		@Net				money,
		@Gross				money,
		@fPrice				numeric(10, 2),
		@iProgramSectionID	int,
		@zProductCode		varchar(20),
		@iAccountID			int,
		@iCampaignID		int,
		@iPricingDetailsID	int,
		@zProvince			varchar(2),
		@stat               int


DECLARE	c1 CURSOR FOR
		SELECT	CustomerOrderHeaderInstance,
				TransID,
				isnull(StatusInstance,0)
		FROM	#SubsToChange

OPEN c1

FETCH NEXT FROM c1 INTO @iCustomerOrderHeaderInstance, @iTransID,@stat

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

	
		EXEC spRemitIndividualItem @iCustomerOrderHeaderInstance, @iTransID, @i OUTPUT
		PRINT @i

	FETCH NEXT FROM c1 INTO @iCustomerOrderHeaderInstance, @iTransID,@stat
END

CLOSE c1
DEALLOCATE c1