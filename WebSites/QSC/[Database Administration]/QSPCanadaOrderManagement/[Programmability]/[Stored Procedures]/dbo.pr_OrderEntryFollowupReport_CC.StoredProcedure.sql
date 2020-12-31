USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OrderEntryFollowupReport_CC]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_OrderEntryFollowupReport_CC]  
@OrderID int

--@AccountID int, @CampaignID int, 

AS
SET NOCOUNT ON

-- saqib shah, Sep, 2004
-- used in .net Order Entry Followup Report


Select tch.Classroom ,
	case ca.Lang when 'FR' then case tch.LastName when 'UNKNOWN' then 'INCONNU' else  tch.LastName end 
	else  tch.LastName end as TeacherName,
	isnull(stu.FirstName,'')+' '+isnull(stu.LastName,'') as ParticipantName,
        	coh.CustomerBillToInstance,
  	cod.Recipient		as SubscriberName,
             isnull(CustBill.FirstName,'')+' '+isnull(CustBill.LastName,'') as PurchaserName,
	CustBill.Phone 		as BillToCustomerPhone,
	CustBill.Phone 		as CustomerPhone,
	CustBill.Address1 	as CustomerAddress1,
	CustBill.Address2 	as CustomerAddress2,
	CustBill.City 		as CustomerCity,
	CustBill.State 		as CustomerState,
	CustBill.Zip 		as CustomerZip,
	cod.ProductCode 	as TitleCode,
	cod.productName 	as MagazineTitle,  
	cod.Quantity 		as Numberofissues,
	Case when ca.isstafforder = 0 then Convert(numeric(10,2),cod.Price) else Convert(numeric(10,2),cod.Price*.5) End as CatalogPrice, --(MS Nov 22, 06)
	--Case when ca.isstafforder = 0 then cod.Price else cod.Price*.5 End as CatalogPrice,
	ca.Lang,
	cp.StatusInstance
 From 	QSPCanadaOrderManagement..CreditCardPayment 	as cp,
      	QSPCanadaOrderManagement..CustomerPaymentHeader as ph,
	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 			as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement..Student 		as stu,
	QSPCanadaOrderManagement..Teacher 		as tch,
	QSPCanadaOrderManagement..Customer 		as CustBill,
	QSPCanadaOrderManagement..Batch 		as batch
 Where cp.CustomerPaymentHeaderInstance = ph.Instance
   and ph.CustomerOrderHeaderInstance  = coh.Instance	
   and coh.Instance  = cod.CustomerOrderHeaderInstance
   and coh.StudentInstance = stu.instance
   and stu.teacherInstance = tch.Instance
   and coh.CustomerBillToInstance = CustBill.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= batch.Date 
   and coh.PaymentMethodInstance in (50003,50004,50005) -- credit card payments
   and cp.StatusInstance in ( 19001,19002,19005) -- CC payment fail
        and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
   and cod.delflag <>1
   and cod.ProductType NOT IN (46017, 46021)
   
   AND NOT EXISTS	(SELECT	1
					FROM	incident inc
					JOIN	incidentAction incA
								ON	inc.IncidentInstance = incA.IncidentInstance
					WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
					AND		incA.ActionInstance IN (22) --Remove From OEFU Report
					)
					
   AND NOT EXISTS	(SELECT	1
					FROM	incident inc
					JOIN	incidentAction incA
								ON	inc.IncidentInstance = incA.IncidentInstance
					WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
					AND		inc.TransID = cod.TransID
					AND		incA.ActionInstance IN (18, 150, 151) --Update Credit Card Info, New Sub to Invoice, New Item to Invoice
					)

   and batch.OrderID = @OrderID
 Order by tch.LastName,tch.Classroom, ParticipantName,PurchaserName
option (maxdop 1)
GO
