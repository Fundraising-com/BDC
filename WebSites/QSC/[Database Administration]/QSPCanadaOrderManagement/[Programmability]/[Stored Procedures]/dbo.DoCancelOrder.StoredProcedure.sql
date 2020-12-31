USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[DoCancelOrder]    Script Date: 06/07/2017 09:19:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DoCancelOrder]

	@OrderID int

AS

DECLARE @IsFieldSupplyOrder	BIT

SELECT	@IsFieldSupplyOrder = CASE OrderQualifierID WHEN 39007 THEN 1 ELSE 0 END
FROM	Batch
WHERE	OrderID = @OrderID

UPDATE	batch
SET		batch.StatusInstance = 40005
FROM	Batch b
WHERE	orderID = @OrderID

UPDATE	cod
SET		cod.DelFlag = 1
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
WHERE	b.OrderID = @OrderID

UPDATE	QSPCanadaCommon..SystemErrorLog
SET		IsReviewed = 1,
		IsFixed = 1
WHERE	OrderID = @OrderID

IF @IsFieldSupplyOrder = 1
BEGIN

	UPDATE	camp
	SET		FSOrderRecCreated = 0
	FROM	QSPCanadaCommon..Campaign camp
	JOIN	QSPCanadaOrderManagement..Batch batch
				ON	batch.CampaignID = camp.ID
	WHERE	batch.OrderID = @OrderID

END

/*Declare @Status int
Declare @paydate datetime
Declare @payid int
Declare @CohInstance int
Declare @UnReservedNotOk int
Declare @UnreservedErrorMessage	Varchar(100)
	
Select @Status = StatusInstance
From Batch
where	OrderID = @OrderID

	IF  @Status  in ( 40013,40014,40005 )	--Fulfilled, Partially Fulfilled or Cancelled
	Begin
		Select  'Order fulfilled or Cancelled already.'
	End
	Else
	Begin

		Exec QSPCanadaOrderManagement.dbo.Pr_FSUnReserveOrderItemsQty 	@OrderId ,  @UnReservedNotOk  OutPut, @UnreservedErrorMessage  OutPut
		
		If @UnReservedNotOk = 0
		Begin

		Select  @paydate = PaymentBatchDate, @payid = PaymentBatchID from Batch where OrderID = @OrderID

		--Payment Batch Cancelled
		Update QSPCanadaOrderManagement..PaymentBatch 
		Set StatusInstance=40005
		Where PaymentDate = @paydate and PaymentID = @payid


		--Delete (logical) Order Item
		Update QSPCanadaOrderManagement..CustomerOrderdetail
		Set Delflag=1, Comment = 'Item Quantity UnReserved and Cancelled By CancelOrder Proc '
		Where customerorderheaderInstance in (   Select Distinct h.instance
							From QSPCanadaOrderManagement..batch b, QSPCanadaOrderManagement..Customerorderdetail d, QSPCanadaOrderManagement..Customerorderheader h
							Where b.id=h.orderbatchid
							And b.date=h.orderbatchdate
							And h.instance=d.customerorderheaderinstance
							And orderid in(@OrderID )
							)

		--Delete (logical) Order Header
		Update QSPCanadaOrderManagement..CustomerOrderheader
		Set Delflag=1
		Where Instance in( Select Distinct h.instance
				    From QSPCanadaOrderManagement..batch b, QSPCanadaOrderManagement..Customerorderdetail d, QSPCanadaOrderManagement..Customerorderheader h
		    		    Where b.id=h.orderbatchid
				    And b.date=h.orderbatchdate
				    And h.instance=d.customerorderheaderinstance
				    And orderid in(@OrderID )
				 )


		--Delete (logical) Batch
		Update QSPCAnadaOrderManagement..Batch
		Set StatusInstance=40005, Comment = 'Order Cancelled and Item UnReserved By CancelOrder Proc'
		Where  orderid in(@OrderID )

		Select  'Batch Cancelled/Unreserved Successfully.'

		End
		Else
			Select 	IsNull(@UnreservedErrorMessage, 'Order not Cancelled')	
		
	End
*/
GO
