USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[AddPaymentToFinance]    Script Date: 06/07/2017 09:19:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPaymentToFinance] 
	@OrderID		int
AS

Declare	@AccountID		int,
	@CampaignID		int,
	@PaymentAccountType	int,
	@PaymentMethod	int,
	@CheckNumber		int,
	@CheckDate		datetime,
	@CheckPayer		varchar(50),
	@CreditCardOwner	varchar(50),
	@CreditCardAuthNumber varchar(50),
	@Amount		numeric(10,2),
	@ChangedBy		int

SET NOCOUNT ON

Select @PaymentAccountType = 50601
Select @CheckNumber = -1
Select @CheckPayer = ''
Select @CheckDate = '1995/1/1'
Select @CreditCardOwner = ''
Select @ChangedBy = 0

Select	@AccountID = AccountID, 
	@CampaignID = CampaignID,
	@Amount = PaymentSend
--	@ChangedBy = ChangeUserID
From Batch
Where OrderID = @OrderID

Select	@CreditCardAuthNumber = AuthorizationCode 
From	CreditCardPayment, CustomerPaymentHeader as CPH, Batch
Where	CustomerPaymentHeaderInstance = CPH.Instance
And	CPH.PaymentBatchDate = Batch.PaymentBatchDate 
And	CPH.PaymentBatchID = Batch.PaymentBatchID
And	Batch.OrderID = @OrderID

Select	@PaymentMethod = PaymentMethodInstance
From	CustomerOrderHeader as COH, Batch
Where	Batch.Date = COH.OrderBatchDate
And	Batch.ID = COH.OrderBatchID
And	Batch.OrderID = @OrderID

-- by check
IF @PaymentMethod = 50002
Begin
	Select @CheckDate = DateSent From Batch Where Batch.OrderID = @OrderID
End

Exec QSPCanadaFinance..AddInvoicePayment 
	@AccountID, 
	@OrderID, 
	@CampaignID,
	@PaymentAccountType, 
	@PaymentMethod,
	@CheckNumber,
	@CheckDate,
	@CheckPayer,
	@CreditCardOwner,
	@CreditCardAuthNumber,
	@Amount, 
	@ChangedBy

SET NOCOUNT OFF
GO
