USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[CloseCurrentPeriod]    Script Date: 06/07/2017 09:17:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CloseCurrentPeriod]	(@CountryCode		Varchar(10),
						 @ErrorMessage		Varchar(200) Output)
As
Declare @StartDate		DateTime,
	@EndDate		DateTime,
	@AccountingYear	Int,
	@AccountingPeriod	Int,
	@NextAccountingYear	Int,
	@NextAccountingPeriod	Int,
	@RecordCount		Int,
	@ReturnErrorMessage	Varchar(200),
	@MaxIndex		Int,
	@Cnt			Int,
	@ErrorFlag		Varchar(1),

	@GLAccountNumber	Varchar(50),
	@GLAccountingYear	Int,
	@GLAccountingPeriod	Int,
	@OpeningBalance	Numeric(10,2),
	
	@DebitSum		Numeric(10,2),
	@CreditSum 		Numeric(10,2),
	@ClosingBalance	Numeric(10,2)

Declare @TabAccountingPeriod Table (
		Tindex			Int  Identity,
		Accounting_Year 	Int,
		Accounting_Period	Int,
		Start_Date		DateTime ,
		End_Date		DateTime ,
		Country_Code		Varchar(10),
		Is_Closed		Varchar(1)
	           				)

Declare @TabGLAccountBalance Table(
		Tindex			Int  Identity,
		GL_Account_Number	Varchar(50),
		GL_Accounting_Year	Int,
		GL_Accounting_Period	Int,
		Opening_Balance	Numeric(10,2)
					)
Begin
	Set NoCount On
	Set @CountryCode = 'CA'
	Set @ErrorMessage = Null
	Set @ErrorFlag = 'N'

	Select 	@StartDate		= Start_Date ,
		@EndDate		= End_Date ,
		@AccountingYear	= Accounting_Year,
		@AccountingPeriod	= Accounting_Period
	From QSPCanadaFinance.dbo.Accounting_Period ap
	Where ap.Is_Closed  = 'N'
	And ap.Country_Code = 	@CountryCode
	And ap.Start_Date = (Select Min(ap1.Start_Date)
			        From QSPCanadaFinance.dbo.Accounting_Period ap1
			        Where ap1.Is_Closed = 'N'
			        And ap1.Country_Code = @CountryCode)

	If @@RowCount <> 1 Or @@Error <> 0
	Begin
		Set @ErrorMessage = 'Error selecting Start/End Date for accounting period in procedure dbo.CloseCurrentPeriod'
		Return
	End

	--Check Next Accounting Period
	Insert Into @TabAccountingPeriod
		Select   Accounting_Year ,Accounting_Period,Start_Date,End_Date,Country_Code,Is_Closed		
		From QSPCanadaFinance.dbo.Accounting_Period ap
		Where ap.Is_Closed  = 'N'
		And ap.Country_Code = 	@CountryCode
		And ap.Start_Date > (Select Start_Date
				        From QSPCanadaFinance.dbo.Accounting_Period 
				        Where Accounting_Year = @AccountingYear
				         And  Accounting_Period = @AccountingPeriod
				         And  Country_Code = @CountryCode)
		Order By Start_Date


	Select 	@NextAccountingYear	= Accounting_Year,
		@NextAccountingPeriod	= Accounting_Period
	From	@TabAccountingPeriod
	Where Tindex =1

	If  @@Error <> 0 or  @@RowCount =0
	Begin
		Set @ErrorMessage = 'Error selecting subsequent accounting period in procedure dbo.CloseCurrentPeriod'
		Return
	End	
	
	--Check if current period can be closed
	Exec dbo.ValidateCurrentPeriod	'CA',
					 @ReturnErrorMessage Output

	If  IsNull(@ReturnErrorMessage,'@') <>  '@' 
	Begin
		Set @ErrorMessage = @ReturnErrorMessage
		Return
	End	

	Begin Transaction
	--Close Accounting period
	Update QSPCanadaFinance.dbo.Accounting_Period 
	Set Is_Closed = 'Y'
	Where Accounting_Year = @AccountingYear
	 And  Accounting_Period = @AccountingPeriod
	 And  Country_Code = @CountryCode


	If @@Error <> 0
	Begin
		Set @ErrorFlag = 'Y'
		Set @ErrorMessage = 'Error Updating Accounting Period in  dbo.CloseCurrentPeriod'
	End

	--Get all accounts for current period/year
	Insert Into @TabGLAccountBalance
		Select GL_Account_Number, Accounting_Year, Accounting_Period, Opening_Balance	
		From  QSPCanadaFinance.dbo.GL_Account_Balance	
		Where  Accounting_Year = @AccountingYear
		And Accounting_Period    = @AccountingPeriod
		And Country_Code 	 = @CountryCode

	Select @RecordCount =Count(*) , @MaxIndex = Max(Tindex)  From @TabGLAccountBalance
	Set @Cnt = 0
	While @RecordCount > 0
	Begin
		Set @DebitSum = 0
		Set @CreditSum = 0
		Set @ClosingBalance = 0

		Select   @GLAccountNumber	= Gl_Account_Number,
			@GLAccountingYear	= GL_Accounting_Year,
			@GLAccountingPeriod	= GL_Accounting_Period,
			@OpeningBalance	= Opening_Balance
 		From @TabGLAccountBalance Where Tindex = @MaxIndex-@Cnt

		Select @DebitSum = IsNull(Sum(Amount),0)
		From QSPCanadaFinance.dbo.GL_Transaction
		Where GL_Account_Number = @GLAccountNumber
		And Debit_Credit = 'D'
		And GL_Entry_Id In ( Select GL_Entry_Id
				       From QSPCanadaFinance.dbo.GL_Entry
 				        Where  Accounting_Year = @AccountingYear
				         And Accounting_Period    = @AccountingPeriod
				         And Country_Code 	 = @CountryCode)

		Select @CreditSum = IsNull(Sum(Amount),0)
		From QSPCanadaFinance.dbo.GL_Transaction
		Where GL_Account_Number = @GLAccountNumber
		And Debit_Credit = 'C'
		And GL_Entry_Id In ( Select GL_Entry_Id
				        From QSPCanadaFinance.dbo.GL_Entry
 				        Where  Accounting_Year = @AccountingYear
				         And Accounting_Period    = @AccountingPeriod
				         And Country_Code 	 = @CountryCode)


		Set @ClosingBalance = IsNull(@OpeningBalance,0) - @CreditSum + @DebitSum


		Update QSPCanadaFinance.dbo.GL_Account_Balance	
		Set Closing_Balance = @ClosingBalance
		Where  GL_Account_Number = @GLAccountNumber
		And Accounting_Year       = @GLAccountingYear
		And Accounting_Period    = @GLAccountingPeriod
		And Country_Code 	 =  @CountryCode

		If @@Error <> 0
		Begin
			Set @ErrorMessage = 'Error Updating Accounting Balance in  dbo.CloseCurrentPeriod'
			Set @ErrorFlag = 'Y'
			Break
		End

		Insert Into  QSPCanadaFinance.dbo.GL_Account_Balance	( GL_Account_Number,
									  Accounting_Year,
									  Accounting_Period,
									  Opening_Balance,
									  Closing_Balance,
									  Country_Code
									)
								Values	(@GLAccountNumber,
									 @NextAccountingYear,
									  @NextAccountingPeriod,
									  @ClosingBalance,
									  0,   --Opening Balance
									  @CountryCode
									)
		If @@Error <> 0
		Begin
			Set @ErrorMessage = 'Error Inserting New record for Accounting Balance in  dbo.CloseCurrentPeriod'
			Set @ErrorFlag = 'Y'
			Break
		End							

		Set @Cnt = @Cnt+1
		Set @RecordCount = @RecordCount-1
	End
	
	If @@Error <> 0 Or @ErrorFlag = 'Y'
		Rollback
	Else
		Commit	

End
GO
