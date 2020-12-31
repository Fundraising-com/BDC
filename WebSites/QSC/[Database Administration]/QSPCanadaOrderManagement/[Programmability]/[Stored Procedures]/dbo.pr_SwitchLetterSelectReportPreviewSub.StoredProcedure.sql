USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterSelectReportPreviewSub]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterSelectReportPreviewSub]
	
	@iCustomerOrderHeaderInstance int,
	@iTransID int 
 AS

select 
	newid() as UniqueID,
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
	xyz.Price as Amount,
	codrh.CustomerOrderHeaderInstance,
	codrh.TransID,
	codrh.remitbatchid,
	COALESCE(p.Lang, 'EN') as Language
from 
dbo.RemitBatch rb,
dbo.CustomerRemitHistory crh,
dbo.CustomerOrderHeader coh,
dbo.CustomerOrderDetail cod,
QSPCanadaProduct.dbo.Pricing_Details pd,
QSPCanadaProduct.dbo.Product p,
dbo.Batch b,
dbo.CustomerOrderDetailRemitHistory  codrh,
(select * from vw_GetSwitchLetterAmount ) xyz

where 

   crh.RemitBatchId = rb.Id
   and codrh.RemitBatchId = crh.RemitBatchId
   and codrh.CustomerRemitHistoryInstance = crh.Instance
   and crh.CustomerInstance = coh.CustomerBillToInstance
   and coh.OrderBatchDate = b.[Date]
   and coh.OrderBatchID = b.ID
   and crh.instance = (select max(instance) from customerremithistory x where x.customerinstance= coh.CustomerBillToinstance)
   and codrh.TransID = @iTransID
   and codrh.CustomerOrderHeaderInstance= @iCustomerOrderHeaderInstance
   and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
   and cod.TransID = codrh.TransID
   and pd.MagPrice_Instance = cod.PricingDetailsID
   and p.Product_Instance = pd.Product_Instance
   and xyz.CustomerOrderHeaderInstance=codrh.CustomerOrderHeaderInstance 
   and xyz.TransID= codrh.TransID
 and codrh.remitbatchid = (SELECT     MAX(RemitBatchID)
                            FROM          CustomerOrderDetailRemitHistory x
                            WHERE      x.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND x.TransID = codrh.TransID)
GO
