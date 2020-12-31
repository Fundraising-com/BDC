USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterSelectReportPreview]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterSelectReportPreview]
	@sTitleCode varchar(10),
	@iRemitBatchID  int =0,
	@dFrom datetime = '',
	@dTo datetime = ''
AS


if @iRemitBatchID <> 0 
begin

	select 
		crh.LastName,
	    	crh.FirstName,
	     	crh.Address1 as Street1,
		crh.Address2 as Street2,
		crh.City,
		crh.state as province,
		crh.zip as postalcode,
		'Canada' as Country,
		codrh.MagazineTitle,
		codrh.NumberOfissues	as NbIssues,	
		--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Amount,
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) as Amount, --MS march 25, 2007
		codrh.CustomerOrderHeaderInstance,
		codrh.TransID,
		codrh.remitbatchid,
		COALESCE(p.Lang, 'EN') as Language
	from 
	
	RemitBatch rb,
	CustomerRemitHistory crh,
	Customer c,
	CustomerOrderDetailRemitHistory  codrh,
	CustomerOrderDetail cod,
	CustomerOrderHeader coh,
	QSPCanadaProduct..Pricing_Details pd,
	QSPCanadaProduct..Product p,
	QSPCanadaCommon..Campaign ca
	
	where 
	
	   crh.RemitBatchId = rb.Id
	   and codrh.RemitBatchId = crh.RemitBatchId
	   and crh.CustomerInstance = c.Instance
	   and codrh.CustomerRemitHistoryInstance = crh.Instance
	   and codrh.customerremithistoryinstance = (select max(customerremithistoryinstance) from customerorderdetailremithistory x where x.CustomerOrderHeaderInstance=codrh.CustomerOrderHeaderInstance and x.transid=codrh.transid)
	   and codrh.RemitCode = @sTitleCode
	   and rb.RunID = @iRemitBatchID
	   and cod.CustomerOrderHeaderInstance=codrh.CustomerOrderHeaderInstance 
	   and cod.TransID= codrh.TransID
	   and cod.CustomerOrderHeaderInstance = coh.Instance
	   and pd.MagPrice_Instance = cod.PricingDetailsID
	   and p.Product_Instance = pd.Product_Instance
	   and ca.ID = coh.CampaignID
	   and not exists (select  CustomerOrderHeaderInstance, TransID from SwitchLetterBatchCustomerOrderDetail slbcod where slbcod.TransID=codrh.TransID and slbcod.CustomerOrderHeaderInstance= codrh.CustomerOrderHeaderInstance)
end
else
begin

 select 
	crh.LastName,
    	crh.FirstName,
     	crh.Address1 as Street1,
	crh.Address2 as Street2,
	crh.City,
	crh.state as province,
	crh.zip as postalcode,
	'Canada' as Country,
	codrh.MagazineTitle,
	codrh.NumberOfissues	as NbIssues,	
	--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as Amount,
	convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) as Amount,
	codrh.CustomerOrderHeaderInstance,
	codrh.TransID,
	codrh.remitbatchid,
	COALESCE(p.Lang, 'EN') as Language
	from 
	dbo.RemitBatch rb,
	dbo.CustomerRemitHistory crh,
	dbo.CustomerOrderHeader coh,
	dbo.Customer c,
	dbo.Batch b,
	dbo.CustomerOrderDetailRemitHistory  codrh,
	dbo.CustomerOrderDetail cod,
	QSPCanadaProduct.dbo.Pricing_Details pd,
	QSPCanadaProduct.dbo.Product p,
	QSPCanadaCommon..Campaign ca
	
	where 
	
	   crh.RemitBatchId = rb.Id
	   and codrh.RemitBatchId = crh.RemitBatchId
	   and codrh.CustomerRemitHistoryInstance = crh.Instance
	   and codrh.CustomerOrderHeaderInstance = coh.Instance
	   and coh.OrderBatchDate = b.[Date]
	   and coh.OrderBatchID = b.ID
	   and crh.CustomerInstance = c.Instance 
	   and codrh.customerremithistoryinstance = (select max(customerremithistoryinstance) from customerorderdetailremithistory x where x.CustomerOrderHeaderInstance=codrh.CustomerOrderHeaderInstance and x.transid=codrh.transid)
	   and codrh.RemitCode = @sTitleCode 
	   and cod.CreationDate  between  convert(nvarchar, @dFrom,101)  and convert(nvarchar, @dTo,101)
	   and cod.CustomerOrderheaderinstance = codrh.CustomerOrderheaderinstance
	   and cod.transid = codrh.transid
	   and cod.CustomerOrderHeaderInstance=codrh.CustomerOrderHeaderInstance 
	   and cod.TransID= codrh.TransID
	   and pd.MagPrice_Instance = cod.PricingDetailsID
	   and p.Product_Instance = pd.Product_Instance
	   and ca.ID = b.CampaignID
	   and not exists (select  CustomerOrderHeaderInstance, TransID from SwitchLetterBatchCustomerOrderDetail slbcod where slbcod.TransID=codrh.TransID and slbcod.CustomerOrderHeaderInstance= codrh.CustomerOrderHeaderInstance)

end
GO
