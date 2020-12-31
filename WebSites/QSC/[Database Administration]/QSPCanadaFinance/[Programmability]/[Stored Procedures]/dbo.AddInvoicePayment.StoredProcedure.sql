USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddInvoicePayment]    Script Date: 06/07/2017 09:17:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddInvoicePayment] 
	@AccountID		int,
	@OrderID		int,
	@CampaignID		int,
	@PaymentMethod	int,
	@CheckNumber		varchar(50) = null,
	@CheckDate		datetime = null,
	@CheckPayer		varchar(50) = null,
	@CreditCardOwner	varchar(50) = null,
	@CreditCardAuthNumber varchar(50) = null,
	@Amount		numeric(10,2),
	@ChangedBy		int,
	@Value			int output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Add Invoice Payment
--   MTC 8/25/2004
--   Added Code to calculate the Account Type
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

DECLARE @Description varchar(100)
DECLARE @PaymentId int

IF @CheckNumber = '-1'
BEGIN
	SET @CheckNumber =  NULL
END
IF @CheckDate = '1/1/1995'
BEGIN
	SET @CheckDate = NULL
END

DECLARE @AccountType INT
EXEC GetAccountType @AccountID, @AccountType OUTPUT


INSERT Payment 
SELECT @AccountID, 
	ISNULL(@AccountType,0),
	@PaymentMethod,
	GetDate(), --effective date
	@CheckNumber,
	@CheckDate,
	@CheckPayer,
	@CreditCardOwner,
	@CreditCardAuthNumber,
	@Amount, 
	null, --NoteToPrint, 
	GetDate(),  --date created
	null, -- date modified
	@ChangedBy,
	@OrderID, 
	'CA',  --Country Code
	@CampaignID

SET @PaymentId = Scope_Identity()

EXEC dbo.GL_Entry_InsertPayment @PaymentID

SELECT @Value = @PaymentId

SET NOCOUNT OFF
GO
