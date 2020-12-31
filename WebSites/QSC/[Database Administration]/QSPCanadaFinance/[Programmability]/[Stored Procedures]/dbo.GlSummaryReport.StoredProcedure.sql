USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GlSummaryReport]    Script Date: 06/07/2017 09:17:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GlSummaryReport] 
AS
BEGIN

Declare @StartDate Datetime
Declare @EndDate Datetime
Declare @SendEmailToIT Varchar(500)
Declare @SendEmailTo Varchar(500)
Declare @ErrorMessage Varchar(1000)
Declare @AccountingYear Int
Declare @AccountingPeriod Int
Declare @cnt Int
Declare @SQLcommand 	Varchar(1000)
Declare @Filename	Varchar(100)
Declare @Body 		Varchar(500)
Declare @path 		Varchar(200)
Declare @FileAttachment	Varchar(200)
Declare @RunDate	Datetime

Create Table #Debit(GL_Entry_Id Int,
		    GL_Account_Number Varchar(50),
		    Total Numeric(16,2))

Create Table #Credit(GL_Entry_Id Int,
		     GL_Account_Number Varchar(50),
		     Total Numeric(16,2))

If Exists (Select * From dbo.sysobjects 
	Where ID = object_id(N'[dbo].##GLAccountSummary]') 
	And OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	Drop Table [dbo].[##GLAccountSummary]
End

Create Table ##GLAccountSummary (GL_Account_Number Varchar(50),
								DebitCredit varchar(1),
		     					Total Numeric(16,2))

Set @SendEmailToIT =  'qsp-IT-canada@qsp.com'
Set @SendEmailTo ='qsp-IT-canada@qsp.com,qsp-finance-canada@qsp.com'

Select 	@AccountingYear=Accounting_Year,@AccountingPeriod=Accounting_Period,
		@StartDate=Start_Date ,
		@EndDate=DATEADD(day, 23, Start_Date) -- Every 24th of Month
From QSPCanadaFinance.dbo.Accounting_Period ap
Where ap.Is_Closed  = 'N'
And ap.Start_Date = (Select Min(ap1.Start_Date)
		        From QSPCanadaFinance.dbo.Accounting_Period ap1
		        Where ap1.Is_Closed = 'N'
		     )

select @cnt=count(*) from gl_entry
where (isnull(accounting_period,0) = 0 or isnull(accounting_year,0)=0)
and cast( convert(varchar(10),GL_entry_date,101)as datetime) >= @StartDate
and cast( convert(varchar(10),GL_entry_date,101)as datetime) <= @EndDate
and description not like 'REMITAP-Invoice#%'

If @cnt >0
Begin

	Set @ErrorMessage = 'Blank Accounting Year or period found in GL entries, please fix these before running the report.'
	Exec QSPCanadaCommon..Send_EMail  'GLSummaryReport@qsp.com',@SendEmailToIT, 'Error in GL Summary Report', @ErrorMessage
	Drop Table #Debit
	Drop Table #Credit
	Drop table ##GLAccountSummary 
	Return
End 

Insert #Debit
Select  e.gl_entry_id,t.GL_Account_Number,	Sum(Amount)DebitTotal
from 	QSPcanadaFinance..gl_entry e, 
		QSPcanadaFinance..Gl_Transaction t
Where e.gl_entry_id=t.gl_entry_id
and  e.GL_ENTRY_DATE between @StartDate and @EndDate
and debit_credit='D'
Group By e.gl_entry_id,t.GL_Account_Number

Insert #Credit
Select  e.gl_entry_id,t.GL_Account_Number,	Sum(Amount)
from	QSPcanadaFinance..gl_entry e, QSPcanadaFinance..Gl_Transaction t
Where e.gl_entry_id=t.gl_entry_id
and  e.GL_ENTRY_DATE between @StartDate and @EndDate
and debit_credit='C'
Group By e.gl_entry_id,t.GL_Account_Number


Select Sum(Total)Total,GL_Entry_Id
Into #DebitTotal From #Debit Group By GL_Entry_Id

Select Sum(Total) Total,GL_Entry_Id
Into #CreditTotal From #Credit Group By GL_Entry_Id

Select #DebitTotal.GL_Entry_Id,Abs(Sum(#DebitTotal.Total) - sum(#CreditTotal.Total))  
from #DebitTotal,#CreditTotal
Where #DebitTotal.GL_Entry_Id=#CreditTotal.Gl_Entry_ID
Group By #DebitTotal.GL_Entry_Id
Having Abs(Sum(#DebitTotal.Total) - sum(#CreditTotal.Total))<> 0

-- If GL is not balance donot generate report and inform IT
If @@RowCount >0
Begin

	Set @ErrorMessage = 'GL entries are not balanced, please fix these before running the report.'
	Exec QSPCanadaCommon..Send_EMail  'GLSummaryReport@qsp.com',@SendEmailToIT, 'Error in GL Summary Report', @ErrorMessage

End 
Else
Begin
	-- Insert  into temp table and make SummaryFile and Email
	Insert  ##GLAccountSummary
	select gl_account_number,debit_credit,sum(amount) 
	from gl_entry e, gl_transaction t
	where e.gl_entry_id = t.gl_entry_id
	And accounting_period= @AccountingPeriod
	and accounting_year = @AccountingYear
	group by gl_account_number,debit_credit
	order by 1

	Select @RunDate =  GetDate()

	Set @path = 'E:\Projects\Paylater\QSPCAFinance\GLSummaryLogs\' 
	Set @Filename =  'GLSummaryByAccount_' + 
		Cast(Datepart(YEAR,	@RunDate) 	AS Varchar) +
		Cast(Datepart(MONTH,	@RunDate) 	AS Varchar) +
		Cast(Datepart(DAY,	@RunDate) 	AS Varchar) + 
		Cast(Datepart(HOUR,	@RunDate) 	AS Varchar) + 
		Cast(Datepart(MINUTE,	@RunDate)	AS Varchar) + 
		Cast(Datepart(SECOND,	@RunDate) 	AS Varchar)+'.txt'

		
	Set @SQLcommand = 'bcp "##GLAccountSummary" out "E:\Projects\Paylater\QSPCAFinance\GLSummaryLogs\' + @Filename + '" -c -q -T '
	Exec master..xp_cmdshell @SQLcommand

	Set @Body= 	'GL Account Summary as of '+
			Convert(Varchar(30),@RunDate,113)+Char(13)+Char(13)+
			'Please review attached file '+Char(13)+Char(13)	
	Set @FileAttachment = @path+@Filename
	Exec  QSPCanadaCommon.dbo.Send_EMAIL_ATTACH 'GLSummaryReport@QSP.com', @SendEmailTo,'GL Summary by Account',@Body,@FileAttachment

End

Drop Table #Debit
Drop Table #DebitTotal
Drop Table #Credit
Drop Table #CreditTotal
Drop table ##GLAccountSummary 

END
GO
