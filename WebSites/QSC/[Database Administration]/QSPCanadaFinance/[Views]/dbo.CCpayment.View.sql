USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[CCpayment]    Script Date: 06/07/2017 09:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  View [dbo].[CCpayment] As
Select   ccb.ID CCBatch, 
	 b.OrderID, 
	 b.CampaignID, 
	 coh.AccountID, 
	 cph.TotalAmount,
	 (Case b.IsStaffOrder 
	 When 0	 Then Round(TotalAmount,2)
	 When 1	 Then Round(TotalAmount/2,2)
	 End) AmountCharged,		
	 b.IsStaffOrder, 
         ccp.AuthorizationCode, 
	 Convert(Varchar(10),ccp.AuthorizationDate,101) AuthorizationDAte, 
	 coh.PaymentMethodInstance, 
	 c.LastName, c.FirstName, 
	 c.Type AS CustomerType, 
	 Substring(ccp.CreditCardNumber,1,4)+'********'+Substring(ccp.CreditCardNumber,13,4) as CCNumber, 
	 ccp.ReasonCode
from  QSPCanadaOrderManagement.dbo.creditcardbatch ccb,
      QSPCanadaOrderManagement.dbo.creditcardpayment ccp,
      QSPCanadaOrderManagement.dbo.customerpaymentheader cph,
      QSPCanadaOrderManagement.dbo.customerorderheader coh,
      QSPCanadaOrderManagement.dbo.batch b,
      QSPCanadaOrderManagement.dbo.customer c 
where --( isGLDone = 1 or isgldone is null)and
    ccb.ID = Batchid
and ccp.customerpaymentheaderinstance= cph.instance
and coh.instance = cph.customerorderheaderinstance
and cph.iscreditcard=1
and coh.orderbatchdate=date
and coh.orderbatchid=b.id
and c.instance = customerbilltoinstance
and ccp.statusinstance=19000 -- good
GO
