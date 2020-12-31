USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AdjustmentDetailReport]    Script Date: 06/07/2017 09:17:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AdjustmentDetailReport]  	@AccountType 		Int,
							@AccountId		Int,
							@AdjEffectiveDateFrom	DateTime,
							@AdjEffectiveDateTo	DateTime,
							@OrderId		Int,
							@AdjType		Int

 AS


Declare 	@Rows			Int,
	     	@CreditAmount		Numeric(10,2),
	     	@DebitAmount   	Numeric(10,2),
	     	@Adj			Int,
	     	@cnt			Int,
	     	@MaxId		Int,
	     	@TotalAdj		Int

Declare  @AdjustmentWithMultipleTrans  Table(
			Id 		Int IDENTITY,
			AdjustmentId	Int,
			RowCnt		Int
			)


Declare @Adjustment Table (
			Id			Int IDENTITY,
			AdjustmentId		Int,
			AdjustmentEffectiveDate	Varchar(10), 
			DateCreated		Varchar(10), 
			Status			Varchar(15),
			AccountId		Int,
			AccountName		Varchar(50),
			AccountType		Varchar(50),
			AdjustmentType		Varchar(50), 
			AdjustmentAmount	Numeric(10,2), 
			AdjustmentAmountAbs	Numeric(10,2), 
			OrderId			Int,
			InvoiceId		Int,
			GLEntryId		Int,
			GLDebitAccount		Varchar(50),
			Debit			Varchar(1),
			DebitAmount		Numeric(10,2), 
			GLCreditAccount	Varchar(50),
			Credit			Varchar(1),
			CreditAmount		Numeric(10,2),
			CreditAmountForCalc	Numeric(10,2),
			TotalDebit		Numeric(10,2),
			TotalCredit		Numeric(10,2),
			GrandTotalD		Numeric(10,2),
			GrandTotalC		Numeric(10,2),
			TotalCount		Int,
			RowCnt			Int
			)

Insert Into @Adjustment
Select    A.Adjustment_Id,
	Convert(Varchar(10),A.Adjustment_Effective_Date,101)  Adjustment_Effective_Date, 
	Convert(Varchar(10),A.Adjustment_Id,101)  Date_Created,  
	(Case IsNull(A.Date_Created,'01/01/1995') 
		When '01/01/1995' Then 'NEW'
		When ''	     Then 'NEW'
		Else 		 'APPROVED'
	End)  Status,
	A.Account_Id  AccountId, 
	CAcc.Name  Account, 
	AccType.Description  AccountType, 
             AdjType.Description  AdjustmentType, 
	A.Adjustment_Amount  AdjustmentAmount, 
	Abs(A.Adjustment_Amount),
	A.Order_Id  OrderId, 
	I.Invoice_Id  InvoiceId, 
	E.GL_Entry_Id,
             TrD.GL_Account_Number  GLDebitAccount, 
	TrD.Debit_Credit,
	TrD.Amount  DebitAmount, 
             TrC.GL_Account_Number  GLCreditAccount, 
	TrC.Debit_Credit,
	TrC.Amount  CreditAmount,TrC.Amount,0,0,0,0,0,1

FROM            QSPCanadaFinance.dbo.ADJUSTMENT A LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.CodeDetail AdjType ON A.ADJUSTMENT_TYPE_ID = AdjType.Instance LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.CodeDetail AccType ON A.ACCOUNT_TYPE_ID = AccType.Instance LEFT OUTER JOIN
                      QSPCanadaCommon.dbo.CAccount CAcc ON A.ACCOUNT_ID = CAcc.Id LEFT OUTER JOIN
                      QSPCanadaFinance.dbo.GL_ENTRY e INNER JOIN
                      QSPCanadaFinance.dbo.GL_TRANSACTION TrC ON e.GL_ENTRY_ID = TrC.GL_ENTRY_ID INNER JOIN
                      QSPCanadaFinance.dbo.GL_TRANSACTION TrD ON e.GL_ENTRY_ID = TrD.GL_ENTRY_ID ON A.ADJUSTMENT_ID = e.ADJUSTMENT_ID LEFT OUTER JOIN
                      QSPCanadaFinance.dbo.INVOICE i ON A.ORDER_ID = i.ORDER_ID

/*
From     QSPCanadaFinance.dbo.Adjustment A Left Outer Join
             QSPCanadaCommon.dbo.CodeDetail AccType ON A.Adjustment_Type_Id = AccType.Instance Left Outer Join
             QSPCanadaCommon.dbo.CAccount CAcc On A.Account_Id = CAcc.Id Left Outer Join
             QSPCanadaFinance.dbo.GL_Entry e Inner Join
             QSPCanadaFinance.dbo.GL_Transaction TrC On e.GL_Entry_Id = trC.GL_Entry_Id Inner Join
             QSPCanadaFinance.dbo.GL_Transaction TrD On e.GL_Entry_Id = trD.GL_Entry_Id On A.Adjustment_Id = e.Adjustment_Id Left Outer Join
             QSPCanadaCommon.dbo.CodeDetail AdjType On A.Account_Type_Id = AdjType.Instance Left Outer Join
             QSPCanadaFinance.dbo.Invoice i On A.Order_Id = i.Order_Id
*/

Where   TrD.Debit_Credit 	= 'D'
And 	TrC.Debit_Credit 	= 'C'  
And 	A.Order_Id 		= IsNull(@OrderId,A.Order_Id)
And 	A.Account_Id 		= IsNull(@AccountId, A.Account_Id)
And 	A.Account_Type_Id 	= IsNull(@AccountType,A.Account_Type_Id)
And 	A.Adjustment_Type_Id 	= IsNull(@AdjType,A.Adjustment_Type_Id)
And 	Cast(Convert(Varchar(10),A.Adjustment_Effective_Date,101) As DateTime)  >=  IsNull(@AdjEffectiveDateFrom, Cast(Convert(Varchar(10),A.Adjustment_Effective_Date,101) As DateTime) )
And 	Cast(Convert(Varchar(10),A.Adjustment_Effective_Date,101) As DateTime)  <=  IsNull(@AdjEffectiveDateTo,    Cast(Convert(Varchar(10),A.Adjustment_Effective_Date,101) As DateTime) )
And 	A.Adjustment_Type_Id Not In (49028,49012)	--Exclude Online Profit, Magnet Profit

--Total distinct Adjustments from the result set
Select @TotalAdj =  Count( Distinct AdjustmentId) From @Adjustment

--Update total record count 
Update @Adjustment Set TotalCount = @TotalAdj

-- Find Adjustment record Debited / Credited to  multiple GL Accounts
Insert Into @AdjustmentWithMultipleTrans
	Select AdjustmentId,Count(*)
	From @adjustment
	Group By AdjustmentId
	Having Count(*) > 1

Select @Rows = Count(*) From @AdjustmentWithMultipleTrans

--For each Adjustment with multiple GL Transaction Hide Amount for duplicate by setting them to Null
While @Rows > 0
	Begin
		Select @Adj=AdjustmentId  From @AdjustmentWithMultipleTrans Where Id= @Rows

		--Get first transaction from top so remaining can be set to Null using the Seq #
		Select Top 1 @DebitAmount = DebitAmount, @CreditAmount = CreditAmount, @MaxId = Id
		From    @Adjustment 
		Where AdjustmentId = @Adj  
		Order by id 
		
		--If Debit has duplicate entries
		If @DebitAmount > @CreditAmount
		Begin
			Update @Adjustment 
			Set DebitAmount = Null, AdjustmentAmountAbs = Null
			Where AdjustmentId = @Adj  
			And Id > @MaxId
		End

		--If Credit has multiple entries
		If @DebitAmount < @CreditAmount
		Begin
			Update @Adjustment 
			Set CreditAmount  = Null, AdjustmentAmountAbs = Null
			Where AdjustmentId = @Adj  
			And Id > @MaxId
		End
		
	
	Set @Rows=@Rows-1
	End

Select * From @Adjustment
GO
