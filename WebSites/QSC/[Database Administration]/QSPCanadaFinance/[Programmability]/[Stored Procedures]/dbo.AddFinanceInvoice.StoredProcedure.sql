USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddFinanceInvoice]    Script Date: 06/07/2017 09:17:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddFinanceInvoice]
	@AccountID 		int,	
	@AccountType		int,
	@OrderID		int,
	@ChangedBy		int,
	@IsPrinted		char(10),
	@InvoiceID		int output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/30/2004 
--   Insert a new Finance Account Record For Canada Finance System.
--   Removed the Invoice Status field.  We are auto approving and no need for generated status.
--   Returns the InvoiceID identity value created on insert.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

INSERT QSPCanadaFinance..Invoice
			(Account_ID, 
			Account_Type_ID, 
			Order_ID, 
			Invoice_Date, 
			Invoice_Due_Date, 
			Invoice_Amount, 
			First_Print_Date, 
			Note_To_Print, 
			Datetime_Created, 
			Datetime_Modified, 
			Last_Updated_By, 
			Country_Code, 
			Is_Printed, 
			Datetime_Approved, 
			Invoice_Effective_Date)
VALUES(@AccountID,
	@AccountType,
	@OrderID,
	GETDATE(), 
	DATEADD(dd , 30, GETDATE() ), --30 days from now due date
	0, --Updated later
	NULL, --not printed yet
	NULL, --note to print
	GETDATE(),  --created
	NULL, --modified
	@ChangedBy, 
	'CA',
	@IsPrinted,
	GETDATE(), --auto approve
	GETDATE() )

SELECT @InvoiceID = SCOPE_IDENTITY()
SET NOCOUNT OFF
GO
