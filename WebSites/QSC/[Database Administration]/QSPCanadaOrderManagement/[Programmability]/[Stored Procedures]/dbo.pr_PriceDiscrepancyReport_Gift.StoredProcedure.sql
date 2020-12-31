USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_PriceDiscrepancyReport_Gift]    Script Date: 06/07/2017 09:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_PriceDiscrepancyReport_Gift]
@AccountID int, @CampaignID int, @OrderID int,@FMID int, @InvoiceID int
AS
SET NOCOUNT ON

-- SSS, Sep, 2004
-- used in .net Price Discrepancy Report

 Select tch.Classroom , 	tch.Instance as TeacherID, 
	tch.LastName as TeacherName,
	IsNull(stu.FirstName,'')+' '+stu.LastName as ParticipantName,
	cod.ProductCode,cod.ProductName,  
	cod.Quantity,	cod.CatalogPrice,
	cod.Price,
	Case cod.PriceOverrideID When 45001 then cod.Price
				 When 45003 then cod.Price
				 Else cod.CatalogPrice
				 End as InvoiceAmount,

 	(cod.Price -
	Case cod.PriceOverrideID When 45001 then cod.Price
				 When 45003 then cod.Price
				 Else cod.CatalogPrice
				 End) as VarianceAmount,
	QSPCanadaOrderManagement.DBO.UDF_GetCodeDetail( cod.PriceOverrideID) as Reason, 
	ca.Lang
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
   and cod.CatalogPrice 	  <> cod.Price
    and env.isincentive 	  = 'N'   
   and cod.PriceOverrideID 	  <> 45001 -- should not be a Coupen  
   and cod.ProductType 		  in ( 46002,46003,46005) -- should be  gift items
  and coh.AccountId 		= isnull(@AccountID,  coh.AccountId )
  and coh.CampaignId 		= isnull(@CampaignID,coh.CampaignId )
  and batch.OrderID 		= isnull(@OrderID,batch.OrderID)
  and ca.FMID 			= isnull(@FMID,ca.FMID )
  and isnull(inv.Invoice_ID,'')	= isnull(@InvoiceID, isnull(inv.Invoice_ID,''))
 Order By  tch.Classroom, tch.LastName,stu.FirstName
GO
