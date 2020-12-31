select	cod.CustomerOrderHeaderInstance, cod.TransID, b.CampaignID, cod.PricingDetailsID, cod.Renewal, CASE ISNULL(cod.Price, 0.00) WHEN 0.00 THEN cod.CatalogPrice ELSE cod.Price END Price,
		cod.PriceOverrideID, cust.Instance CustomerInstance, cod.ProductType, cust.FirstName, cust.LastName, cust.Address1, cust.Address2, cust.City, cust.State, cust.Zip
into #substofix
from Incident i
join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = i.CustomerOrderHeaderInstance and cod.TransID = i.TransID
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
JOIN	Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
where ia.ActionInstance = 25
and b.Date >= '2014-07-01'
and isnull(cod.InvoiceNumber,0) = 0
and cod.DelFlag = 0
and cod.StatusInstance not in (501, 500, 508)
and b.IsInvoiced = 1
and coh.Instance not in
(
	SELECT	CustomerOrderHeaderInstance
	FROM	Incident i
	JOIN	IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
	WHERE	ia.ActionInstance IN (1, 14, 18, 150, 151) --1 cancel, 14 cancel before remit, 18 CC update, 150 new sub to invoice, 151 new item to invoice
)

select * from #substofix

BEGIN TRAN

--Cursor
DECLARE @CustomerOrderHeaderInstance int,
		@TransID int,
		@CampaignID int,
		@PricingDetailsID int,
		@Renewal nvarchar(1),
		@Price float,
		@PriceOverrideID int, 
		@CustomerInstance int,
		@ProductType int,
		@FirstName nvarchar(50),
		@LastName nvarchar(50),
		@Address1 nvarchar(50),
		@Address2 nvarchar(50),
		@City nvarchar(50),
		@State nvarchar(5),
		@Zip nvarchar(20),
		@OrderQualifierID int

SET @OrderQualifierID = 39020

DECLARE	info CURSOR FOR
SELECT		CustomerOrderHeaderInstance, TransID, CampaignID, PricingDetailsID, Renewal, Price, PriceOverrideID, CustomerInstance,
			ProductType, FirstName, LastName, Address1, Address2, City, State, Zip
FROM		#substofix
order by	CustomerOrderHeaderInstance

OPEN info
FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance, @TransID, @CampaignID, @PricingDetailsID, @Renewal, @Price, @PriceOverrideID, @CustomerInstance,
							@ProductType, @FirstName, @LastName, @Address1, @Address2, @City, @State, @Zip

WHILE(@@fetch_status = 0)
BEGIN
	DECLARE @NewCustomerOrderHeaderInstance INT,
			@IncidentInstance INT,
			@IncidentActionInstance INT,
			@CancelIncidentInstance INT,
			@CancelIncidentActionInstance INT
	
	EXEC pr_AddNewItemForCustomerService @CampaignID, @PricingDetailsID, @Renewal, @Price, @PriceOverrideID, @CustomerInstance, @ProductType, 612, @FirstName, @Lastname,
											@Address1, @Address2, @City, @State, @Zip, @OrderQualifierID, @CustomerOrderHeaderInstance, @TransID
	
	SELECT	@NewCustomerOrderHeaderInstance = MAX(Instance)
	FROM	CustomerOrderHeader
	
	EXEC pr_Incident_Insert @IncidentInstance out, 239, @CustomerOrderHeaderInstance, @TransID, 5, 3, 1, null, 'Product Update Not Invoiced Issue', '612'

	EXEC pr_IncidentAction_Insert 	@IncidentActionInstance out, @IncidentInstance, 151, 'Product Update Not Invoiced Issue', '612'

	EXEC pr_CancelSubPriorToRemit @NewCustomerOrderHeaderInstance, 1, '612', ''

	EXEC pr_Incident_Insert @CancelIncidentInstance out, 239, @NewCustomerOrderHeaderInstance, 1, 5, 3, 1, null, 'Product Update Not Invoiced Issue', '612'

	EXEC pr_IncidentAction_Insert 	@CancelIncidentActionInstance out, @CancelIncidentInstance, 8, 'Product Update Not Invoiced Issue', '612'

	FETCH NEXT FROM info  INTO  @CustomerOrderHeaderInstance, @TransID, @CampaignID, @PricingDetailsID, @Renewal, @Price, @PriceOverrideID, @CustomerInstance,
								@ProductType, @FirstName, @LastName, @Address1, @Address2, @City, @State, @Zip

END
CLOSE info
DEALLOCATE info

--COMMIT TRAN

DROP TABLE #substofix

/*select *
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
where isnull(cod.InvoiceNumber,0) = 0
and b.Date >= '2014-07-01'
and cod.DelFlag = 0
and cod.StatusInstance in (507, 512)
and b.IsInvoiced = 1
--and coh.PaymentMethodInstance = 50005
order by cod.CustomerOrderHeaderInstance*/