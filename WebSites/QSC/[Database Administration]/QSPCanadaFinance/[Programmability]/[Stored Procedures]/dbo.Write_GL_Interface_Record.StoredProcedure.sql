USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Write_GL_Interface_Record]    Script Date: 06/07/2017 09:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Write_GL_Interface_Record] (	@CountryCode 	Varchar(2), 
							@FromDate 	DateTime,
							@ToDate 	DateTime,
							@RetVal	Int OutPut,
							@ErrorMessage	Varchar(150) OutPut)
As
Declare @AdjustmentTypeId 	Int

	If IsNull(@CountryCode,'@@') = '@@'
	Begin
		Set @CountryCode = 'CA'
	End

	Begin Transaction

	Insert  Into QSPOracleInterface.dbo.Om_Tbl_Gl_Interface(
			     Country_Code,
			     Input_Source,
			     Journal_Entry_Date,
			     Segment1,
			     Segment2,
			     Segment3,
			     Segment4,
			     Segment5,
			     Segment6,
			     Segment7,
			     Debt_Amount,
			     Credit_Amount,
			     Currency_Code,
			     Description
			    )
	Select 
			    @CountryCode as country,
			     'CA QSP' as qsp,
			     ge.Gl_Entry_Date Journal_Entry_Date,
			     Substring(gt.GL_Account_Number,1,3) Segment1,
			     Substring(gt.GL_Account_Number,5,4) Segment2,
			     Substring(gt.GL_Account_Number,10,4) Segment3,
			     Substring(gt.GL_Account_Number,15,4) Segment4,
			     Substring(gt.GL_Account_Number,20,2) Segment5,
			     Substring(gt.GL_Account_Number,23,2) Segment6,
			     Substring(gt.GL_Account_Number,26,3) Segment7,
			     (Case gt.Debit_Credit
				When 'D'Then gt.Amount
				Else 0
				End ) Debit_Amount,
			     (Case gt.debit_credit
				When 'C'Then gt.Amount
				Else 0
				End ) Credit_Amount,
			     'CAD' as Currency,
			     Substring(ge.description,1,30) description  
	  From QSPCanadaFinance.dbo.GL_Entry ge,   QspCanadaFinance.dbo.GL_Transaction gt
	  Where ge.Country_Code = @CountryCode
	  And ge.Is_Posted = 'N'
	  And Cast(ge.Accounting_Period as Varchar) + Cast(ge.Accounting_Year as Varchar) =( Select Cast(ap.Accounting_Period as Varchar) + Cast(ap.Accounting_Year as Varchar)
									             		     From QspCanadaFinance.dbo.Accounting_Period ap
									             		     Where ap.Country_Code = @CountryCode
									              	     And ap.Is_Closed = 'N'
									              	     And ap.Start_Date = (Select Min(ap2.Start_Date)
											     		             From QspCanadaFinance.dbo.Accounting_Period ap2
							                                       			         		Where ap2.Is_Closed = 'N'
							                                                                            		And ap2.Country_Code = @CountryCode
												       		)
											   )
	And ge.GL_Entry_Id = gt.GL_Entry_Id
    	And Convert(Varchar(10),ge.gl_entry_date,101) >= Convert(Varchar(10),@FromDate,101)
    	And Convert(Varchar(10),ge.gl_entry_date,101) <= Convert(Varchar(10),@ToDate,101)


	If @@Error  = 0
	Begin

	--Set the is_posted indicator and gl_posting_date to current date

	Update QSPCanadaFinance.dbo.gl_entry
	Set	is_posted = 'Y',
		gl_posting_date = GetDate()
	Where country_code = @CountryCode
	And is_posted = 'N'
	And Cast(Accounting_Period as Varchar) + Cast(Accounting_Year as Varchar)=(Select Cast(ap.Accounting_Period as Varchar) + Cast(ap.Accounting_Year as Varchar)
						    					  FROM QSPCanadaFinance.dbo.accounting_period ap
					                 					  WHERE country_code = @CountryCode
					                					  AND ap.is_closed = 'N'
					                					  AND ap.start_date = (SELECT MIN(ap2.start_date)
						                                     					         FROM QSPCanadaFinance.dbo.accounting_period ap2
						                                    					         WHERE ap2.is_closed = 'N'
						                                     					         AND ap2.country_code = @CountryCode
													        )
											)
	And Convert(Varchar(10),gl_entry_date,101) >= Convert(Varchar(10),@FromDate,101)
    	And Convert(Varchar(10),gl_entry_date,101) <= Convert(Varchar(10),@ToDate,101)

	Set @RetVal = 0
	Commit

	End
	Else
	Begin
		Rollback
		Set @RetVal = 1
		Set @ErrorMessage= 'Error Inserting record in GL_Interface table , procedure Write_GL_Interface_Record.'

	End
GO
