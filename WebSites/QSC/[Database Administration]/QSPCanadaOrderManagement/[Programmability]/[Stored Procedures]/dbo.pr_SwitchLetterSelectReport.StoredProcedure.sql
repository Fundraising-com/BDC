USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SwitchLetterSelectReport]    Script Date: 06/07/2017 09:20:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SwitchLetterSelectReport] 

	@iSwitchLetterBatchID int

AS
select 	distinct
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
	slbcod.CustomerOrderHeaderInstance,
	slbcod.TransID,
	--codrh.remitbatchid,
	--convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END) as amount,
	convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END)  as amount, --MS March 25, 2007
	COALESCE(p.Lang, 'EN') as Language,
	slb.Reason AS Reason
	
from 
switchletterbatch slb,
SwitchLetterBatchCustomerOrderDetail slbcod,
dbo.CustomerRemitHistory crh,
dbo.CustomerOrderHeader coh,
dbo.customer c,
dbo.Batch b,
dbo.CustomerOrderDetailRemitHistory  codrh,
customerorderdetail cod,
QSPCanadaProduct.dbo.Pricing_Details pd,
QSPCanadaProduct.dbo.Product p,
QSPCanadaCommon..Campaign ca

where 
   coh.OrderBatchDate = b.[Date]
   and coh.OrderBatchID = b.ID
   and codrh.RemitBatchId = crh.RemitBatchId
   and codrh.CustomerRemitHistoryInstance = crh.Instance
   and codrh.CustomerOrderHeaderInstance = coh.Instance

   and crh.customerinstance = c.instance

   and slb.instance =  @iSwitchLetterBatchID
   and slb.instance = slbcod.switchletterbatchinstance
   and codrh.TransID=slbcod.TransID
   and codrh.CustomerOrderHeaderInstance = slbcod.CustomerOrderHeaderInstance
   and ca.ID = coh.CampaignID
   --and crh.instance = (select max(instance) from customerremithistory x where x.customerinstance= c.instance)
   and codrh.remitbatchid = (SELECT     MAX(RemitBatchID)
                            FROM          CustomerOrderDetailRemitHistory x
                            WHERE      x.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND x.TransID = codrh.TransID)
   and cod.CustomerOrderHeaderInstance=slbcod.CustomerOrderHeaderInstance 
   and cod.TransID= slbcod.TransID
   and pd.MagPrice_Instance = cod.PricingDetailsID
   and p.Product_Instance = pd.Product_Instance
order by slbcod.CustomerOrderHeaderInstance, slbcod.transid
GO
