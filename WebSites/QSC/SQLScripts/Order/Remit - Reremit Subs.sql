USE [QSPCanadaOrderManagement]
GO

DECLARE	@CustomerOrderHeaderInstance	int
DECLARE @TransID						int

DECLARE CustomerOrderDetails CURSOR FOR
select	distinct
		cod.CustomerOrderHeaderInstance,
		cod.TransID
from customerorderdetailremithistory h
join customerorderdetail cod on cod.customerorderheaderinstance = h.customerorderheaderinstance and cod.transid = h.transid
join qspcanadaproduct..pricing_details pd on cod.pricingdetailsid = pd.magprice_instance
join qspcanadaproduct..product p on p.product_instance = pd.product_instance
join remitbatch rb on rb.id = h.remitbatchid
where h.status = 42010
and pd.pricing_year = 2012 and pd.pricing_season = 'S'
and p.[status] <> 30601
and rb.runid = 1394

OPEN CustomerOrderDetails
FETCH NEXT FROM CustomerOrderDetails INTO  @CustomerOrderHeaderInstance, @TransID
							
WHILE(@@fetch_status = 0)
BEGIN
	exec pr_Remit_ReRemitSubsByCOD
			@CustomerOrderHeaderInstance, @TransID

FETCH NEXT FROM CustomerOrderDetails INTO  @CustomerOrderHeaderInstance, @TransID
							
END
CLOSE CustomerOrderDetails
DEALLOCATE CustomerOrderDetails

