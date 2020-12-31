USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddGroupRefundRecord]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddGroupRefundRecord]

	@AP_Cheque_ID		INT,
	@Refund_Type_ID		INT,
	@Amount  			NUMERIC(10, 2),
	@Address1			VARCHAR(50),
	@Address2			VARCHAR(50),
	@City				VARCHAR(50),
	@Province			VARCHAR(10),
	@PostalCode			VARCHAR(10),
	@Country			VARCHAR(10),
	@CreateDate			DATETIME,
	@CreateUserID		VARCHAR(30),
	@UpdateDate			DATETIME,
	@UpdateUserID		VARCHAR(30),
	@CampaignID			INT,
	@InvoiceAmount		NUMERIC(10, 2),
	@PaymentAmount		NUMERIC(10, 2),
	@AdjustmentAmount	NUMERIC(10, 2),
	@RefundID			INT OUTPUT

AS

BEGIN TRANSACTION

INSERT Refund
(
	AP_Cheque_ID,
	Refund_Type_ID,
	Amount,
	Address1,
	Address2,
	City,
	Province,
	PostalCode,
	Country,
	Campaign_ID,
	Invoice_Amount,
	Payment_Amount,
	Adjustment_Amount,
	CreateDate,
	CreateUserID,
	UpdateDate,
	UpdateUserID
)
VALUES
(
	@AP_Cheque_ID,
	@Refund_Type_ID,
	@Amount,
	@Address1,
	@Address2,
	@City,
	@Province,
	@PostalCode,
	@Country,
	@CampaignID,
	@InvoiceAmount,
	@PaymentAmount,
	@AdjustmentAmount,
	@CreateDate,
	@CreateUserID,
	@UpdateDate,
	@UpdateUserID
)

SET @RefundID = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@RefundID, 0) = 0
BEGIN
	ROLLBACK
	SET @RefundID = 0
	RETURN
END

COMMIT
GO
