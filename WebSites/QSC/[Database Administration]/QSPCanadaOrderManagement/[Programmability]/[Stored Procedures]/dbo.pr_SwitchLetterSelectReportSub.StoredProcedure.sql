USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterSelectReportSub]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterSelectReportSub] 

	@iCustomerOrderHeaderInstance	int,
	@iTransID			int
AS



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
	@iCustomerOrderHeaderInstance as CustomerOrderHeaderInstance,
	@iTransID as TransID,
	codrh.remitbatchid,
	--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as amount,
	convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END)  as amount, --MS March 25, 2007
	COALESCE(p.Lang, 'EN') as Language
	
from 
dbo.CustomerRemitHistory crh,
dbo.CustomerOrderHeader coh,
dbo.Batch b,
dbo.CustomerOrderDetailRemitHistory  codrh,
customerorderdetail cod,
QSPCanadaProduct..Pricing_Details pd,
QSPCanadaProduct..Product p,
QSPCanadaCommon..Campaign ca

where 
   coh.OrderBatchDate = b.[Date]
   and coh.OrderBatchID = b.ID
   and codrh.RemitBatchId = crh.RemitBatchId
   and codrh.CustomerRemitHistoryInstance = crh.Instance
   and codrh.CustomerOrderHeaderInstance = coh.Instance

   and codrh.TransID= @iTransID
   and codrh.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
   and ca.ID = coh.CampaignID
   ---and crh.instance = (select max(instance) from customerremithistory x where x.customerinstance= c.instance)
 --  and codrh.remitbatchid = (SELECT     MAX(RemitBatchID)
  --                          FROM          CustomerOrderDetailRemitHistory x
    --                        WHERE      x.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND x.TransID = codrh.TransID)
   and cod.CustomerOrderHeaderInstance= @iCustomerOrderHeaderInstance
   and cod.TransID= @iTransID
   and pd.MagPrice_Instance = cod.PricingDetailsID
   and p.Product_Instance = pd.Product_Instance
GO
