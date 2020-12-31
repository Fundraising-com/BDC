USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_PriceDiscrepancyReport_Total]    Script Date: 06/07/2017 09:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PriceDiscrepancyReport_Total]
@AccountID int, @CampaignID int, @OrderID int,@FMID int, @InvoiceID int
AS
SET NOCOUNT ON

-- SSS, Sep, 2004
-- used in .net Price Discrepancy Report

Declare @Quantity_Gift int, @CatalogPrice_Gift Numeric(10,2), @ItemPriceTotal_Gift  Numeric(10,2),  @InvoiceAmount_Gift  Numeric(10,2), @Variance_Gift  Numeric(10,2)               
Declare @Quantity_Mag int, @CatalogPrice_Mag Numeric(10,2), @ItemPriceTotal_Mag  Numeric(10,2),  @InvoiceAmount_Mag Numeric(10,2), @Variance_Mag Numeric(10,2)        

 -- query to fetch Gift totals
 Select  @Quantity_Gift= IsNull(Sum(isNull(cod.Quantity,0)),0),
	@CatalogPrice_Gift = IsNull(Sum(IsNull(cod.CatalogPrice,0)),0),
	@ItemPriceTotal_Gift = IsNull(Sum(IsNull(cod.Price,0)),0),
	@InvoiceAmount_Gift = IsNull(Sum(Case cod.PriceOverrideID 	When 45001 then IsNull(cod.Price,0)
				 	When 45003 then IsNull(cod.Price,0)
				 	Else IsNull(cod.CatalogPrice,0)
				 	End),0),

 	@Variance_Gift = IsNull(Sum((IsNull(cod.Price,0) -
	Case cod.PriceOverrideID 	When 45001 then IsNull(cod.Price,0)
				 	When 45003 then IsNull(cod.Price,0)
				 	Else IsNull(cod.CatalogPrice,0)
				 	End)),0)

 From 	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 				as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod,
	QSPCanadaOrderManagement..Envelope 		as env,
	QSPCanadaOrderManagement..Student 			as stu,
	QSPCanadaOrderManagement..Teacher 			as tch,
	QSPCanadaOrderManagement..Customer 		as CustBill,
	QSPCanadaOrderManagement..Batch 			as batch LEFT OUTER JOIN qSPCanadaFinance..Invoice as inv
								 on batch.orderID = inv.Order_ID
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.StudentInstance 	  = stu.instance
   and stu.teacherInstance 	  = tch.Instance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		  = ca.ID
   and coh.OrderBatchID    	  = Batch.id
   and coh.OrderBatchDate  	  = batch.Date
   and coh.OrderBatchID    	  = env.OrderBatchID
   and coh.OrderBatchDate  	  = env.OrderBatchDate
   and tch.instance 		  = env.TeacherInstance
   and cod.CatalogPrice 	 	  <> cod.Price
   and env.isincentive 	  	  = 'N'   
   and cod.PriceOverrideID 	  <> 45001 -- should not be a Coupen  
   and cod.ProductType 		  <> 46001 -- should not be a magazine
   and coh.AccountId 		= isnull(@AccountID,  coh.AccountId )
   and coh.CampaignId 		= isnull(@CampaignID,coh.CampaignId )
   and batch.OrderID 		= isnull(@OrderID,batch.OrderID)
   and ca.FMID 			= isnull(@FMID,ca.FMID )
   and isnull(inv.Invoice_ID,'')	= isnull(@InvoiceID, isnull(inv.Invoice_ID,''))


--Magazine totals---

 Select   @Quantity_Mag  = IsNull(Sum(isNull(cod.Quantity,0)),0),
	@CatalogPrice_Mag  = IsNull(Sum(IsNull(cod.CatalogPrice,0)),0) ,
	@ItemPriceTotal_Mag  = IsNull(Sum(IsNull(cod.Price,0)),0),
	@InvoiceAmount_Mag  = IsNull(Sum(Case cod.PriceOverrideID 	When 45001 then IsNull(cod.Price,0)
				 	When 45003 then IsNull(cod.Price,0)
				 	Else IsNull(cod.CatalogPrice,0)
				 	End),0),

 	@Variance_Mag  = IsNull(Sum((IsNull(cod.Price,0) -
	Case cod.PriceOverrideID 	When 45001 then IsNull(cod.Price,0)
				 	When 45003 then IsNull(cod.Price,0)
				 	Else IsNull(cod.CatalogPrice,0)
				 	End)),0)
 From 	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 				as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod,
	QSPCanadaOrderManagement..Envelope 		as env,
	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory as codrh,
	QSPCanadaOrderManagement..Student 			as stu,
	QSPCanadaOrderManagement..Teacher 			as tch,
	QSPCanadaOrderManagement..Customer 		as CustBill,
	QSPCanadaOrderManagement..Batch 		as batch LEFT OUTER JOIN qSPCanadaFinance..Invoice as inv
								 on batch.orderID = inv.Order_ID
 Where coh.Instance  = cod.CustomerOrderHeaderInstance
   and cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
   and cod.TransID 		  = codrh.TransID
   and coh.StudentInstance 	  = stu.instance
   and stu.teacherInstance 	  = tch.Instance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		  = ca.ID
   and coh.OrderBatchID    	  = Batch.id
   and coh.OrderBatchDate  	  = batch.Date
   and coh.OrderBatchID    	  = env.OrderBatchID
   and coh.OrderBatchDate  	  = env.OrderBatchDate
   and tch.instance 		  = env.TeacherInstance
   and codrh.CatalogPrice 	  <> codrh.ItemPriceTotal
   and env.isincentive 		  = 'N'
   and cod.PriceOverrideID 	  <> 45001 -- should not be Coupen 
  and coh.AccountId 		= isnull(@AccountID,  coh.AccountId )
  and coh.CampaignId 		= isnull(@CampaignID,coh.CampaignId )
  and batch.OrderID 		= isnull(@OrderID,batch.OrderID)
  and ca.FMID 			= isnull(@FMID,ca.FMID )
  and isnull(inv.Invoice_ID,'')	= isnull(@InvoiceID, isnull(inv.Invoice_ID,''))



Select @Quantity_Gift + @Quantity_Mag as Quantity, @CatalogPrice_Gift + @CatalogPrice_Mag as CatalogPrice, @ItemPriceTotal_Gift + @ItemPriceTotal_Mag as ItemPriceTotal, @InvoiceAmount_Gift + @InvoiceAmount_Mag as InvoiceAmount, @Variance_Gift + @Variance_Mag as VarianceAmount
GO
