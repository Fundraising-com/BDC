USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetMagSubQuantitySold]    Script Date: 06/07/2017 09:21:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION  [dbo].[UDF_GetMagSubQuantitySold]  ( @GroupId 		Int,
							  @FmId	 	Int,
             							  @StartDate                   DateTime,
							  @EndDate                   	 DateTime,
							  @IsStaff		Varchar(1)
						           )
Returns Numeric(10,1)
  As  
Begin
	Declare	@Cnt 		Numeric(10,1),
		@StaffIndicator	Int,
		@StaffMultiplier	Numeric(2,2)
		
	Declare @TabCount Table(
			 TIndex		   	Int Identity,
			 MagCount		Numeric(7,2)
			 )

	Set @StaffMultiplier = 0.50

	-- If StaffOrder flag is null 
	Set @StaffIndicator = 0
	If IsNull(@IsStaff, 'Y') = 'Y' 
	Begin
		Set @StaffIndicator = 1
	End

	-- For Magazine the quantity is number of Issue, There may be more than one order with the possibility of a staff order for an account	
	Insert into @TabCount
	Select  Count(Quantity)* (Case IsNull(Upper(@IsStaff) ,'@')
				   When 'Y' then  @StaffMultiplier
	  			   When 'N' then 	1
				   Else   
 					Case  IsNull(C.isStafforder,'9') 
					When 1 then  @StaffMultiplier
					When 0 then 	1
					End 
				
				  End) 
	From  QSPCanadaOrderManagement.dbo.Batch b Inner Join
                       QSPCanadaOrderManagement.dbo.CustomerOrderHeader oh On b.ID = oh.OrderBatchID And b.[Date] = oh.OrderBatchDate Inner Join
                       QSPCanadaOrderManagement.dbo.CustomerOrderDetail od On oh.Instance = od.CustomerOrderHeaderInstance Inner Join
                       QSPCanadaFinance.dbo.Invoice i On od.InvoiceNumber = i.Invoice_Id Inner Join
                       QSPCanadaFinance.dbo.Invoice_Section invs On i.Invoice_Id = invs.Invoice_Id Inner Join
                       QSPCanadaCommon.dbo.Campaign c On b.CampaignID = c.ID
	Where b.AccountId =    IsNull(@GroupId, b.AccountId)
	And c.FmId = IsNull(@FmId,c.FmId)
	And  (b.OrderQualifierID <> 39006)				--Exclude Kanata /* */
	And (IsNull(i.DateTime_Approved, '9/9/1900') <> '9/9/1900')	-- Approved Invoices
	And(invs.Section_Type_Id = 2) 					--Magazine
	And od.producttype in ( '46001' , '46006')				--Magazine 
	And od.statusInstance in (507,508,512,513,514)			--Sent to remit/ship ,unremitable/shippable still invoiced
	And c.IsStaffOrder  = ( Case Upper(@IsStaff)
				      When 'Y' then  1
				      When 'N' then   0
				      Else c.IsStaffOrder
				      End )
         -- And (i.ACCOUNT_TYPE_ID IN (50601, 50603)) 			--Group Account
	And (             Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as Datetime)    >= @StartDate
		And  Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as DateTime) <= @EndDate
	       ) 
	And not exists (Select 1  from QSPcanadaCommon..campaignprogram cp --MS Exclude Loonie Lib 
			Where cp.ProgramId=24 and cp.deletedTF=0
			and cp.campaignId=c.id)

	Group By C.IsStaffOrder

	Select @Cnt = Round(Sum(MagCount),1) from @TabCount

	Return IsNull(@Cnt,0)
End
GO
