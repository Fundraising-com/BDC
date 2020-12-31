USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddInvoiceAdjustment]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[AddInvoiceAdjustment]
	@AccountID		int,
	@OrderID		int,
	@InternalComment	varchar(100),
	@Amount		numeric(10,2),
	@CampaignID		int,
	@AdjustmentType	int,
	@ChangedBy		int,
	@RefundID		int = null,
	@Value			int output 
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/7/2004 
--   Add Invoice Adjustment
--   MTC 7/29/2004
--   Added code for Magnet GL and AP adjustments
--   MTC 8/25/2004
--   Added Code to calculate the Account Type
--   MTC 8/30/2004
--   Automate the GL entries for the Adjustment
--   MTC 9/27/2004
--   Added code to call Write_APInterface_For_Adjustment or Write_APInterface_For_MagnetProfit
--   MTC 1/5/2005 
--   Added Code to handle Online Profit (49028)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

DECLARE @AdjustmentId int

DECLARE @Now datetime
DECLARE @AdjustmentAmount numeric(10,2)
SET @Now = GetDate()

DECLARE @AccountType INT
EXEC GetAccountType @AccountID, @AccountType OUTPUT

DECLARE @AdjustmentTypeDebit	BIT
SELECT	@AdjustmentTypeDebit = CASE Debit_Credit WHEN 'D' THEN 1 ELSE 0 END
FROM	Adjustment_Type
WHERE	Adjustment_Type_ID = @AdjustmentType

IF @AdjustmentTypeDebit = 1
	BEGIN
		SET @AdjustmentAmount = @Amount * -1
	END
ELSE
	BEGIN
		SET @AdjustmentAmount = @Amount
	END

INSERT Adjustment
(
	Account_ID,
	Account_Type_ID,
	Adjustment_Type_ID,
	Adjustment_Effective_Date,
	Adjustment_Amount,
	Note_To_Print,
	Date_Created,
	DateTime_Modified,
	Last_Updated_By,
	Country_Code,
	Internal_Comment,
	Order_ID,
	Campaign_ID,
	Adjustment_Batch_ID,
	Refund_ID
)
SELECT	@AccountID, 
		ISNULL(@AccountType, 0),
		@AdjustmentType,
		GETDATE(), 
		@AdjustmentAmount, 
		NULL,
		GETDATE(), 
		NULL,
		@ChangedBy,
		'CA',
		@InternalComment,
		@OrderID, 
		@CampaignID,
		NULL,
		NULL

--Get the AdjustmentID
SET @AdjustmentId = Scope_Identity()

-- No GL entry for Online Profit (already booked with payment)
IF @AdjustmentType <> 49028  and @AdjustmentType <> 49012
BEGIN
	EXEC dbo.AddGLEntriesForAdjustment @AccountID, @OrderID, @AdjustmentId, @AdjustmentType, @Amount, @ChangedBy, @RefundID
END

--Return Value
SELECT @Value = @AdjustmentId

SET NOCOUNT OFF
GO
