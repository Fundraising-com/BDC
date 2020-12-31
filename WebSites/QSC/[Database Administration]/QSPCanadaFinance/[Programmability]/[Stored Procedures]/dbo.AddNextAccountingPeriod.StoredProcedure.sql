USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddNextAccountingPeriod]    Script Date: 06/07/2017 09:17:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddNextAccountingPeriod]  @CountryCode 	Varchar(10),
						          @ErrorMessage	Varchar(200) Output

 As
Declare @CurrentYear 	 Int,
	@CurrentPeriod   Int,
	@StartDate	 DateTime,
	@IsLeapYear	 Varchar(1),
	@DaystoEndFeb Int	


Begin
	If Isnull(@CountryCode, '@@') = '@@'
	Begin
		Set @CountryCode = 'CA'
	End

	Exec QSPCanadaFinance..GetCurrentAccountingYearandPeriod  @CountryCode, @ErrorMessage Output , @CurrentYear Output , @CurrentPeriod Output ,@StartDate Output

	Select  @IsLeapYear = QSPCanadaFinance.dbo.UDF_IsLeapYear(@CurrentYear )

	If @IsLeapYear = 'Y'
	Begin
		Set @DaystoEndFeb = 28
	End
	Else
	Begin
		Set @DaystoEndFeb = 27
	End


	If (IsNull(@ErrorMessage ,'@@') = '@@' or @ErrorMessage = '') Or @@Error =0
	Begin
		Set @CountryCode = 'CA'

		Insert QSPCanadaFinance..Accounting_Period
		Select  (Case (12 - IsNull(@CurrentPeriod,0))
			When 0 Then @CurrentYear+1 
			Else  @CurrentYear
			End)  ACCOUNTING_YEAR , 
			
			(Case (12 - IsNull(@CurrentPeriod,0))
			When 0   Then 1
			When  1  Then  12
			When 2   Then  11
			When 3   Then 10
			When 4   Then  9
			When 5   Then  8
			When 6   Then  7
			When 7   Then  6
			When 8   Then 5
			When 9   Then  4
			When 10 Then 3
			When 11 Then  2
			Else
				Null
			End)  ACCOUNTING_PERIOD, 

		 (Case (12 - IsNull(@CurrentPeriod,0))
			When 0    Then  dateadd(month,1,@StartDate)
			When  1   Then  dateadd(month,1,@StartDate) 
			When 2    Then  dateadd(month,1,@StartDate)
			When 3    Then  dateadd(month,1,@StartDate) 
			When 4    Then  dateadd(month,1,@StartDate) 
			When 5    Then  dateadd(month,1,@StartDate) 
			When 6    Then  dateadd(month,1,@StartDate)  
			When 7    Then  dateadd(month,1,@StartDate) 
			When 8    Then  dateadd(month,1,@StartDate) 
			When 9    Then  dateadd(month,1,@StartDate) 
			When 10  Then  dateadd(month,1,@StartDate) 
			When 11  Then  dateadd(month,1,@StartDate)   
			Else
				Null
			End)  START_DATE,
		 
		 (Case (12 - IsNull(@CurrentPeriod,0))
			 When  0  then  DateAdd(day, 30, dateadd(month,1,@StartDate) )			--Form End date according to number of Days in a month 
			 When  1  then  DateAdd(day, 29, dateadd(month,1,@StartDate) )
			 When  2  then  DateAdd(day, 30, dateadd(month,1,@StartDate) )
			 When  3  then  DateAdd(day, 29, dateadd(month,1,@StartDate) )
			 When  4  then  DateAdd(day, 30, dateadd(month,1,@StartDate) )
			 When  5  then  DateAdd(day, @DaystoEndFeb, dateadd(month,1,@StartDate) )	-- Consider Leap year 
			 When  6  then  DateAdd(day, 30, dateadd(month,1,@StartDate) )			--If Current Period is Dec 
			 When  7  then  DateAdd(day, 30, dateadd(month,1,@StartDate) )			--if period Nov
			 When  8  then  DateAdd(day, 29, dateadd(month,1,@StartDate) )			--if period oct
			 When  9  then  DateAdd(day, 30, dateadd(month,1,@StartDate) )		
			 When 10 then  DateAdd(day, 29, dateadd(month,1,@StartDate) )		
			 When 11 then  DateAdd(day, 30, dateadd(month,1,@StartDate) )		
			Else
				Null
			End)  END_DATE,
			@CountryCode,
			'N'

		/*Update QSPCanadaFinance..Accounting_Period
		Set Is_Closed = 'Y'
		Where Accounting_Year = @CurrentYear
		And  Accounting_Period =  @CurrentPeriod*/
		
	End
	Else
	Begin
		Set @ErrorMessage = 'Error Inserting Next Accounting Period'
		Return
	End

End
GO
