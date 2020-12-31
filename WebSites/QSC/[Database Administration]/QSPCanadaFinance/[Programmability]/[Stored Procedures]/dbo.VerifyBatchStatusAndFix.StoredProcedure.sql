USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[VerifyBatchStatusAndFix]    Script Date: 06/07/2017 09:17:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[VerifyBatchStatusAndFix]  @OrderId Int , @Output	Int outPut , @OutPutMessage Varchar(200) OutPut

AS

	-- Check item status if it is 510 pickable, 511 picked then BHE not shipped
	-- if 502 paid item cannot update batch status

	Select   d.statusInstance   Into #A 
	From      QSPCanadaOrderManagement..Batch b,
		QSPCanadaOrderManagement..CustomerOrderHeader h,
		QSPCanadaOrderManagement..CustomerOrderDetail d,
 		QSPCanadaOrderManagement..CustomerPaymentHeader cph
	 	left outer join QSPCanadaOrderManagement..CreditCardPayment ccp on 
	 	cph.Instance=ccp.CustomerPaymentHeaderInstance
	Where   b.id=h.orderbatchid
		And b.date=h.orderbatchdate
		And h.Instance = d.CustomerOrderHeaderInstance
		And h.instance=cph.CustomerOrderHeaderInstance
		And orderid= @OrderId
		And d.producttype not in (46013,46014,46015)	
		And d.statusInstance not in (507,508,512,513) 	--Remit, ship,un-remittable,un-shipable
		And ( (h.paymentMethodInstance <> 50002 And ccp.StatusInstance = 19000) Or paymentMethodInstance=50002) 
		--And d.statusInstance  in (502,510,511)		--Paid,Pickable, Picked
		And d.delflag<> 1				--Not deleted
		And b.OrderQualifierId <> 39014			--CC re-process courtesy (Not billable)

	Select *  From #A Where statusInstance  in (502,510,511)

	If @@Rowcount > 0
	Begin
		Set @Output = 1
		Set @OutPutMessage = 'Order has either paid item or BHE/Gift item not shipped '
			
	End
	Else
	Begin
		Update QSPCanadaOrderManagement..Batch Set StatusInstance = 40013 Where OrderId=@OrderId
		Set @Output = 0
		Set @OutPutMessage = 'Order BatchStatus updated '
	End

	Drop Table #A
GO
