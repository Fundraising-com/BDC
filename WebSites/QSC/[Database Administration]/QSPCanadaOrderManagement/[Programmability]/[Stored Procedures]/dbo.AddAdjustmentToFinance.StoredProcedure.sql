USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[AddAdjustmentToFinance]    Script Date: 06/07/2017 09:19:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddAdjustmentToFinance]
	@OrderID		int
AS

SET NOCOUNT ON
Declare	@AccountID		int,
	@InternalComment	varchar(100),
	@Amount		numeric(10,2),
	@CampaignID		int,
	@AdjustmentType	int,
	@AdjustmentAccountType int,
	@ChangedBy		int,
	@OrderType		int

Select @AdjustmentType = 49016
Select @AdjustmentAccountType = 0
Select @ChangedBy = 0

Select	@AccountID = AccountID, 
	@CampaignID = CampaignID,
	@Amount = MagnetPostage,
	@OrderType = OrderTypeCode,
	@InternalComment = Comment
--	@ChangedBy = ChangeUserID
From Batch
Where OrderID = @OrderID

-- Only write when it's Magnet Order
IF (@OrderType <> 41009)
Begin
	return
End

Exec QSPCanada..AddInvoiceAdjustment 
	@AccountID,
	@OrderID,
	@InternalComment,
	@Amount,
	@CampaignID,
	@AdjustmentType,
	@AdjustmentAccountType,
	@ChangedBy		

SET NOCOUNT OFF
GO
