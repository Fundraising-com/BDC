USE [QSPCanadaFinance]
GO

/*
select * from adjustment_type
select * from qspcanadacommon..campaign where id = 85412
select top 1 * from adjustment order by adjustment_id desc
*/

DECLARE
	@AccountID		int,
	@OrderID		int,
	@InternalComment	varchar(100),
	@Amount		numeric(10,2),
	@CampaignID		int,
	@AdjustmentType	int,
	@ChangedBy		int,
	@RefundID		int

SET @AccountID = 30068
SET @OrderID = null
SET @InternalComment = 'Prize Allowance Credit'
SET @Amount = '10.80'
SET @CampaignID = 85412
SET @AdjustmentType = 49033
SET @ChangedBy = 612
SET @RefundID = null


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

