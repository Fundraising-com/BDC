USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetBankAccount]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetBankAccount] @FieldName varchar(20), @Value Varchar(20)
AS

	Select 	@FieldName = Case IsNull(@FieldName,'')
				When '' Then '@@@'
				Else  @FieldName
				End

	Select 	@Value = Case IsNull(@Value,'')
				When '' Then Null
				When '0' Then Null
				Else  @Value
				End

	If Lower(@FieldName)= 'bank_account_id'
	Begin
	Select bank_account_id,bank_account_number,bank_account_name 
	from bank_account
	where bank_account_id = IsNull(@Value,'0')
	End

	If Lower(@FieldName)= 'bank_account_number'
	Begin
	Select bank_account_id,bank_account_number,bank_account_name 
	from bank_account
	where bank_account_number = IsNull(@Value,'0')
	End
	
	If Lower(@FieldName) not in ( 'bank_account_number','bank_account_id')
	Begin
		Select bank_account_id,bank_account_number,bank_account_name 
		from bank_account
	End
GO
