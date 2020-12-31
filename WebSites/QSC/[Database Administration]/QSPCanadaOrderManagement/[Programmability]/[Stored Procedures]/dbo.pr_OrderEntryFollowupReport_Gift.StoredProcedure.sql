USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_OrderEntryFollowupReport_Gift]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_OrderEntryFollowupReport_Gift]   
@OrderID int

--@AccountID int, @CampaignID int, 


AS
SET NOCOUNT ON

-- SSS, Sep, 2004
-- used in .net Order Entry Followup Report

 Select 	tch.Classroom ,
	case ca.Lang when 'FR' then case tch.LastName when 'UNKNOWN' then 'INCONNU' else  tch.LastName end 
	else  tch.LastName end as TeacherName,
	isnull(stu.FirstName,'')+' '+stu.LastName as ParticipantName,
        	coh.CustomerBillToInstance,
  	cod.Recipient 			as PurchaserName,
	CustBill.Phone 			as CustomerPhone,
	cod.Catalogprice,cod.Quantity,cod.Price,
        	batch.OrderID,
	ca.Lang
 From 	QSPCanadaOrderManagement..CustomerOrderHeader 	as coh,
	QSPCanadaCommon..Campaign 			as ca,
	QSPCanadaOrderManagement..CustomerOrderDetail 	as cod ,
	QSPCanadaOrderManagement..Student 		as stu,
	QSPCanadaOrderManagement..Teacher 		as tch,
	QSPCanadaOrderManagement..Batch 		as batch,
	QSPCanadaOrderManagement..Customer		as CustBill
 Where coh.Instance  		= cod.CustomerOrderHeaderInstance
   and coh.StudentInstance 	= stu.instance
   and stu.teacherInstance 	= tch.Instance
   and coh.CampaignID 		= ca.ID
   and coh.OrderBatchID    	= Batch.id
   and coh.OrderBatchDate  	= Batch.Date
   and coh.CustomerBillToInstance = CustBill.Instance
   and (cod.ProductCode = 'NNNN' OR cod.StatusInstance = 501) -- Illegible Item Code
   and ISNULL(cod.ProductType, 0) IN (0, 46002, 46018, 46020, 46022)
   and cod.delflag <>1
   and batch.orderqualifierid not in (39014) -- should not be CC-Reprocess
   and batch.OrderID 		= @OrderID

   AND NOT EXISTS	(SELECT	1
					FROM	incident inc
					JOIN	incidentAction incA
								ON	inc.IncidentInstance = incA.IncidentInstance
					WHERE	inc.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
					AND		inc.TransID = cod.TransID
					AND		incA.ActionInstance IN (18, 22, 150, 151) --CC Update, Remove From OEFU, New Sub to Invoice, New Item to Invoice
					)

  Order by  tch.LastName,tch.Classroom, ParticipantName
GO
