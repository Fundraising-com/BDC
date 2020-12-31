--Duplicate to delete and reverse GL
--drop table #ToDelete
Select order_Id,Account_Id,campaign_id,Payment_id,Payment_effective_date,credit_card_owner,payment_amount
into #ToDelete
from Payment 
Where payment_id in ( duplicate payment select	)

Select count(*) from  #ToDelete


--Interface Data
--Drop table #ReverseGL
SELECT  interface.country_code,
	interface.input_source,
	convert(varchar(10),DateAdd(Month,7, DateAdd(Day,-6,journal_entry_date)),101) journal_entry_date,
	interface.segment1,
	interface.segment2,
	interface.segment3,
	interface.segment4,
	interface.segment5,
	interface.segment6,
	interface.segment7,
	--Debt_Amount,
	--Credit_amount,
	interface.Debt_Amount Credit_Amount,
	interface.Credit_amount Debt_amount,
	interface.currency_code,
	interface.description+' REVERSAL'description
into  #ReverseGL
from gl_entry e ,#ToDelete d, QSPOracleInterface.dbo.OM_TBL_GL_INTERFACE interface
Where e.payment_id=d.payment_Id
and e.GL_ENTRY_DATe=interface.JOURNAL_ENTRY_DATE
and Convert(Datetime,convert(varchar(10),d.payment_effective_date,101)) <= '10/12/2007'
and interface.DESCRIPTION =Substring(e.description,1,30) 


-------------------------- reversal-----------------------------------------
--drop table #temp1
Select * from #ReverseGL
-------------------------file prepare --------------
--detail file
Select  Substring(QSPcanadaOrderManagement.dbo.udf_padstring(input_source,' ',15,'R'),1,15)+
	SubString(Convert(varchar(10),journal_ENtry_date,104),1,2)+ 
	SubString(Convert(varchar(10),journal_ENtry_date,104),4,2)+
	SubString(Convert(varchar(10),journal_ENtry_date,104),7,10)+
        Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment1,' ',3,'R'),1,3)+ 
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment2,' ',4,'R'),1,4)+
        Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment3,' ',4,'R'),1,4)+  
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment4,' ',4,'R'),1,4)+
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment5,' ',2,'R'),1,2)+
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment6,' ',2,'R'),1,2)+   
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment7,' ',3,'R'),1,3)+
	Case IsNull(debt_amount,0)
	When 0 Then '0000000000000000' 
	Else
	     Cast(Replace(Str((debt_amount*100),16),' ','0') As Varchar)	
	End +
	Case IsNull(Credit_amount,0)
	When 0 Then '0000000000000000' 
	Else
	     Cast(Replace(Str((credit_amount*100),16),' ','0')As Varchar)	
	End+
	QSPcanadaOrderManagement.dbo.udf_padstring(Currency_Code,' ',4 ,'R')+
        QSPcanadaOrderManagement.dbo.udf_padstring(Description  ,' ',30,'R') 
From #ReverseGL

----------------------------------SummaryFile
-- drop table #a
Select  
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(input_source,' ',15,'R'),1,15)+
	SubString(Convert(varchar(10),GetdAte(),104),1,2)+ 
	SubString(Convert(varchar(10),GetdAte(),104),4,2)+
	SubString(Convert(varchar(10),GetdAte(),104),7,10)+
        Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment1,' ',3,'R'),1,3)+ 
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment2,' ',4,'R'),1,4)+
        Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment3,' ',4,'R'),1,4)+  
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment4,' ',4,'R'),1,4)+
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment5,' ',2,'R'),1,2)+
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment6,' ',2,'R'),1,2)+   
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment7,' ',3,'R'),1,3)A,
	Sum(IsNull(debt_amount,0)) debt_amount,
	sum(IsNull(Credit_amount,0))Credit_amount,
	QSPcanadaOrderManagement.dbo.udf_padstring(Currency_Code,' ',4 ,'R')Description
Into #a
From #ReverseGL
group by
Substring(QSPcanadaOrderManagement.dbo.udf_padstring(input_source,' ',15,'R'),1,15)+
	SubString(Convert(varchar(10),GetdAte(),104),1,2)+ 
	SubString(Convert(varchar(10),GetdAte(),104),4,2)+
	SubString(Convert(varchar(10),GetdAte(),104),7,10)+
        Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment1,' ',3,'R'),1,3)+ 
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment2,' ',4,'R'),1,4)+
        Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment3,' ',4,'R'),1,4)+  
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment4,' ',4,'R'),1,4)+
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment5,' ',2,'R'),1,2)+
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment6,' ',2,'R'),1,2)+   
	Substring(QSPcanadaOrderManagement.dbo.udf_padstring(Segment7,' ',3,'R'),1,3),
QSPcanadaOrderManagement.dbo.udf_padstring(Currency_Code,' ',4 ,'R')


Select sum(debt_amount), sum(Credit_amount)
from #a

Select A+Case IsNull(debt_amount,0)
	When 0 Then '0000000000000000' 
	Else
	     Cast(Replace(Str((debt_amount*100),16),' ','0') As Varchar)	
	End +
	Case IsNull(Credit_amount,0)
	When 0 Then '0000000000000000' 
	Else
	     Cast(Replace(Str((credit_amount*100),16),' ','0')As Varchar)	
	End+ Description
from #a


--summary by account
select gl_account_number,
sum(amount) ,debit_credit
from gl_entry e ,#ToDelete d, gl_transaction t
Where e.payment_id=d.payment_Id
and e.GL_ENTRY_ID=t.GL_ENTRY_ID
and Convert(Datetime,convert(varchar(10),d.payment_effective_date,101)) <= '10/12/2007'
Group by gl_account_number,debit_credit