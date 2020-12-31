USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[CreditBalanceReport]    Script Date: 06/07/2017 09:17:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CreditBalanceReport] @AsOfDate	Varchar(10), 
						@AccountId 	Int, 
						@SortBy	Varchar(10)
AS
	Declare @IPA Table 
	(
	OrderQualifier		Int,
	OrderType		Int,
	AdjustmentType		Int,
	AccountID		Int,
	AccountName		Varchar(50), 
	CampaignID		Int,
	TransactionType 	Varchar(50), 
	TransactionAmount	Numeric(10,2)
	) 

	--Declare @CA Table( CampaignId Int)


	Declare @AccountTransactions Table 
	(
	AccountID		Int,
	AccountName		Varchar(50), 
	TotalInvoice		Numeric(10,2),
	TotalPayment		Numeric(10,2),
	TotalMagnetAdjustment	Numeric(10,2),
	TotalOtherAdjustment	Numeric(10,2)
	) 
	
	Declare @AccountBalance Table 
	(
	AccountID		Int,
	AccountName		Varchar(50), 
	TotalInvoice		Numeric(10,2),
	TotalPayment		Numeric(10,2),
	TotalMagnetAdjustment	Numeric(10,2),
	TotalOtherAdjustment	Numeric(10,2),
	Balance			Numeric(10,2)
	) 

	
	Insert into @IPA
	Select   OrderQualifierId,
		OrderTypeCode,
		Null,
		AccountID,
		Name,
		CampaignID,
		'Invoice',
		Invoice_Amount
	From 	QSPCanadaFinance..Invoice I, 
		QSPCanadaOrderManagement..Batch B  ,
		QSPCanadaCommon..CAccount A 
	Where	B.OrderID = I.Order_ID
	And  	A.ID = B.AccountID
	And	B.OrderQualifierID    <> 39008 --Exclude Cust Svc
	And	B.OrderQualifierID    <> 39009 --Internet
	And 	B.OrderTypeCode    <> 41009 --Magnet
	And 	I.Account_Id = IsNull(@AccountID,I.Account_Id)
	--And 	Convert(Datetime, Convert(Varchar,Invoice_Date,101)) Between @StartDate And @EndDate
	And 	Convert(Datetime, Convert(Varchar,Invoice_Date,101)) <= @AsOfDate
	-- Temp for one FM 
	--And I.Account_Id in (Select BilltoAccountId From QSPCanadaCommon..Campaign Where FMID ='0088')


	Insert into @IPA
	Select	OrderQualifierId,
		OrderTypeCode,Null,
		AccountID,
		Name,
		CampaignID,
		'Payment',
		(-1 * Payment_Amount)
	From	QSPCanadaOrderManagement..Batch B , 
	           	QSPCanadaCommon..CAccount A ,
		QSPCanadaFinance..Payment P Left Outer Join 
     		QSPCanadaFinance..Invoice I  On p.order_id=i.order_id
	Where	B.OrderID = P.Order_ID
	And 	 A.ID = B.AccountID
	And 	p.Account_Id = IsNull(@AccountID,p.Account_Id)
	--And 	Convert(Datetime, Convert(Varchar,Payment_Effective_Date,101)) Between @StartDate and @EndDate
	And 	Convert(Datetime, Convert(Varchar,Payment_Effective_Date,101))<= @AsOfDate
	And 	B.OrderQualifierID    <> 39008 	--Exclude Cust Svc
	And	B.OrderQualifierID    <> 39009	--Internet
	And 	B.OrderTypeCode    <> 41009	--Magnet
	-- Temp for one FM 
	--And p.Account_Id  in (Select BilltoAccountId From QSPCanadaCommon..Campaign Where FMID ='0088')


	--Insert into @CA Select Distinct CampaignId from @IPA

	Insert into @IPA
	Select  	Null,Null,
		Adjustment_Type_ID,
		Account_ID,
		Name	,
		Campaign_ID,
		'Adjustment',
		Case Adjustment_Type_ID
		When 49021 Then ABS(Adjustment_Amount) --Write off debit
		When 49002 Then ABS(Adjustment_Amount) --NSF Check
		When 49009 Then ABS(Adjustment_Amount) --Other Debit
		When 49024 Then ABS(Adjustment_Amount) --Refund Check
		When 49029 Then
			Case 	When coalesce((Select Adjustment_Amount From QSPCanadaFinance..Adjustment adj Where adj.Adjustment_Type_ID = 49016), 0) > Adjustment_Amount
				Then 0
				Else Adjustment_Amount - Coalesce((Select Adjustment_Amount From QSPCanadaFinance..Adjustment adj Where adj.Adjustment_Type_ID = 49016), 0)
			End
		Else (-1 * Adjustment_Amount)
		End as Adjustment_Amount
	From  QSPCanadaFinance..Adjustment A , QSPCanadaCommon..CAccount Acc
	Where A.Account_Id = Acc.Id
	And A.Account_Id = IsNull(@AccountID,A.Account_Id)
	--And Convert(Datetime, Convert(Varchar,Adjustment_Effective_Date,101))Between @StartDate And @EndDate
	And Convert(Datetime, Convert(Varchar,Adjustment_Effective_Date,101)) <=@AsOfDate
	And A.Adjustment_Type_ID <> 49016
	-->And A.Campaign_Id In (Select CampaignId from @CA) (MS Nov 17, 2006)
	--And A.Account_Id  in (Select BilltoAccountId From QSPCanadaCommon..Campaign Where FMID ='0088')
	

	Insert into @AccountTransactions
	Select 	AccountId, AccountName,
		Case TransactionType 
		When 'Invoice' Then Sum(TransactionAmount)
		Else 0
		End Invoice,
		Case TransactionType 
		When 'Payment' Then Sum(TransactionAmount)
		Else 0
		End Payment,
		Case IsNull(AdjustmentType,0)
		When 49012 Then Sum(TransactionAmount)
		When 49016 Then Sum(TransactionAmount)
		When 49017 Then Sum(TransactionAmount)
		When 49018 Then Sum(TransactionAmount)
		When 49020 Then Sum(TransactionAmount)
		When 49029 Then Sum(TransactionAmount)
		Else 0
		End  MagnetAdjustments,
		Case IsNull(AdjustmentType,0)
		When 49012 Then 0
		When 49016 Then 0
		When 49017 Then 0
		When 49018 Then 0
		When 49020 Then 0
		When 49029 Then 0
		Else
			Case TransactionType 
			When 'Adjustment' Then Sum(TransactionAmount)
			Else 0
			End
		End  NonMagnetAdjustments
	From @IPA
	Group by TransactionType,AccountId,AccountName,AdjustmentType
	Order by Accountid

	Insert into @AccountBalance
	Select Distinct AccountId , AccountName, Sum(TotalInvoice),Sum(TotalPayment),Sum(TotalMagnetAdjustment),Sum(TotalOtherAdjustment),(Sum(TotalInvoice)+Sum(TotalPayment)+Sum(TotalMagnetAdjustment)+Sum(TotalOtherAdjustment))
	From @AccountTransactions
	Group By AccountId,AccountName

	If Upper(@SortBy) ='NAME'
	Begin
	Select * from @AccountBalance Where ABS(Balance) >0
	Order by AccountName
	End
	If Upper(@SortBy) ='AMOUNT'
	Begin
	Select * from @AccountBalance Where ABS(Balance) >0
	Order by Balance
	End
	If Upper(@SortBy) Not in ('NAME','AMOUNT')
	Begin
	Select * from @AccountBalance Where ABS(Balance) >0
	Order by AccountId
	End
GO
