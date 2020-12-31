USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[ValidateCurrentPeriod]    Script Date: 06/07/2017 09:17:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ValidateCurrentPeriod]	(@CountryCode		Varchar(10),
							 @ErrorMessage		Varchar(200) Output)
As
Declare @StartDate		DateTime,
	@EndDate		DateTime,
	@AccountingYear	Int,
	@AccountingPeriod	Int,
	@RecordCount		Int
Begin
	
	Set @CountryCode = 'CA'
	Set @ErrorMessage = Null

	Select 	@StartDate		= Start_Date ,
		@EndDate		= End_Date ,
		@AccountingYear	= Accounting_Year,
		@AccountingPeriod	= Accounting_Period
	From QSPCanadaFinance.dbo.Accounting_Period ap
	Where ap.Is_Closed = 'N'
	And ap.Country_Code = 	@CountryCode
	And ap.Start_Date = (Select Min(ap1.Start_Date)
			        From QSPCanadaFinance.dbo.Accounting_Period ap1
			        Where ap1.Is_Closed = 'N'
			        And ap1.Country_Code = @CountryCode)

	If @@RowCount <> 1 Or @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error selecting Start/End Date for accounting period in procedure dbo.ValidateCurrentPeriod'
		Return
	End

	-- Check unapproved Invoices
	Select @RecordCount = Count(*)
	From QSPCanadaFinance.dbo.Invoice
	Where  Invoice_Effective_Date >= @StartDate
	And Invoice_Effective_Date <= @EndDate
	And Country_Code = @CountryCode
	And DateTime_Approved Is Null
	

	If  @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error checking unapproved Invoices in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	If @RecordCount > 0
	Begin
		Set @ErrorMessage = 'Unapproved Invoices found in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	
	--Check Unapproved payments
	Select  @RecordCount = Count(*)
	From QSPCanadaFinance.dbo.Payment
	Where Payment_Effective_Date >= @StartDate
	And Payment_Effective_Date <= @EndDate
	And Country_Code = @CountryCode 
	And   DateTime_Created is Null  		--DateTime Is  Null         --?? Used for Payment Approval Date 
	
	If  @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error checking unapproved payments in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	If @RecordCount > 0
	Begin
		Set @ErrorMessage = 'Unapproved payments found in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	
	-- Unapproved adjustments
	Select  @RecordCount = Count(*)
	From QSPCanadaFinance.dbo.Adjustment
	Where Adjustment_Effective_Date >= @StartDate
	And Adjustment_Effective_Date < = @EndDate
	And Country_Code = @CountryCode 
	And  Date_Created Is Null        --?? Used for Adjustment Approval Date 
	
	If  @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error checking unapproved adjustments in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	If @RecordCount > 0
	Begin
		Set @ErrorMessage = 'Unapproved adjustments found in procedure dbo.ValidateCurrentPeriod'
		Return
	End

	--Check Unapproved GL Transaction
	Select @RecordCount = Count(*)
	From QSPCanadaFinance.dbo.GL_Transaction
	Where GL_Transaction_Status_Id <> 2
	And GL_Entry_Id In	(Select GL_Entry_Id
				 From QSPCanadaFinance.dbo.GL_Entry
				 Where Accounting_Year = @AccountingYear
				 And Accounting_Period = @AccountingPeriod
				 And Country_Code = @CountryCode)

	If  @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error checking unapproved GL transactions in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	If @RecordCount > 0
	Begin
		Set @ErrorMessage = 'Unapproved GL transactions found in procedure dbo.ValidateCurrentPeriod'
		Return
	End

	--Check Un-posted GL Transactions
	Select @RecordCount = Count(*)
	From QSPCanadaFinance.dbo.GL_Transaction
	Where GL_Transaction_Status_Id = 2
	And GL_Entry_Id In	(Select GL_Entry_Id
				 From QSPCanadaFinance.dbo.GL_Entry
				 Where Accounting_Year = @AccountingYear
				 And Accounting_Period = @AccountingPeriod
				 And Is_posted='N'
				 And Country_Code = @CountryCode)

	If  @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error checking un-posted GL transactions in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	If @RecordCount > 0
	Begin
		Set @ErrorMessage = 'Un-posted GL transactions found in procedure dbo.ValidateCurrentPeriod'
		Return
	End
	

End
GO
