USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetYearNetSale]    Script Date: 06/07/2017 09:21:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION  [dbo].[UDF_GetYearNetSale]	( @GroupId 		Int,
						  @FmId	 	Int,
             						  @StartDate                   DateTime,
						  @EndDate                    DateTime,
						  @SectionType		Int,		--2 for Magazine 1 for Gift, 6 Cookie Dough
						  @IsStaff		Varchar(1)	-- 'Y' or 'N'
						           )
Returns Numeric(10,2)
  As  
Begin
	Declare	@TotalAmount 	Numeric(10,2)

	Declare @TabTotal Table(  Amount  Numeric(10,2)   )

	Insert into @TabTotal	
	Select   (Case b.OrderTypeCode
					When '41004' Then Sum(s.Net_Before_Tax)
					When '41003' Then Sum(s.Net_Before_Tax)
					Else 0
	  			    End)+
				   (Case b.OrderTypeCode
					When '41004' Then 0
					When '41003' Then 0
					Else  Sum(s.Net_Before_Tax)
	   			   End) 
  	From   QSPCanadaFinance.dbo.Invoice i Inner Join
           		QSPCanadaFinance.dbo.Invoice_Section s On i.Invoice_Id = s.Invoice_Id Inner Join
	             QSPCanadaOrderManagement.dbo.Batch b On i.Order_Id = b.OrderId  Inner Join
	             QSPCanadaCommon.dbo.Campaign c On b.CampaignID = c.ID
	Where  b.AccountId =  IsNull(@GroupId, b.AccountId)
	And(s.Section_Type_Id = @SectionType) 		
	And c.FmId = IsNull(@FmId,c.FmId)
	And  (b.OrderQualifierID <> 39006)				--Exclude Kanata /* As per DP July05*/
	And (IsNull(i.DateTime_Approved, '9/9/1900') <> '9/9/1900')	-- Approved Invoices
	And c.IsStaffOrder  = ( Case Upper(@IsStaff)			-- Checking IsStaffOrderFlag from Campaign record MS July12, 05
				      When 'Y' then  1
				      When 'N' then   0
				      Else c.IsStaffOrder
				      End )
	And not exists (Select 1  from QSPcanadaCommon..campaignprogram cp --MS Exclude Loonie Lib 
			Where cp.ProgramId=24 and cp.deletedTF=0
			and cp.campaignId=c.id)

	--And (i.ACCOUNT_TYPE_ID IN (50601, 50603)) 			--Group Account
	And (             Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as Datetime)    >= @StartDate
		And  Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as DateTime) <= @EndDate
	       ) 
	Group By b.OrderTypeCode

	Select   @TotalAmount =  Sum(Amount) From @TabTotal

	Return IsNull(@TotalAmount,0)
End
GO
