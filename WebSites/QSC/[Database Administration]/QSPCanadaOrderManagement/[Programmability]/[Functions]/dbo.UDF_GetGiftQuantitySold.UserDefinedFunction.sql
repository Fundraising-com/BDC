USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetGiftQuantitySold]    Script Date: 06/07/2017 09:21:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION  [dbo].[UDF_GetGiftQuantitySold]           ( @GroupId 		Int,
							  @FmId	 	Int,
             							  @StartDate                   DateTime,
							  @EndDate                   	 DateTime,
							  @SectionType		Int
							 
						           )
/************************************************************************************************************************
Added Section Type to get Cookie Dough count with regular gift counts	March 14, 2006 MS
*************************************************************************************************************************/
Returns Int
  As  
Begin
	Declare	@Cnt 	Int
	
	Set @SectionType = IsNull(@SectionType,1)
	
	If @SectionType = 6
	Begin
			-- Cookie Dought	
	
			Select @Cnt =  Sum(IsNull(QuantityShipped,0))
			From  QSPCanadaOrderManagement.dbo.Batch b Inner Join
                      		 QSPCanadaOrderManagement.dbo.CustomerOrderHeader oh On b.ID = oh.OrderBatchID And b.[Date] = oh.OrderBatchDate Inner Join
                      		 QSPCanadaOrderManagement.dbo.CustomerOrderDetail od On oh.Instance = od.CustomerOrderHeaderInstance Inner Join
                      		 QSPCanadaFinance.dbo.Invoice i On od.InvoiceNumber = i.Invoice_Id Inner Join
                       		 QSPCanadaFinance.dbo.Invoice_Section invs On i.Invoice_Id = invs.Invoice_Id Inner Join
                      		 QSPCanadaCommon.dbo.Campaign c On b.CampaignID = c.ID
			Where b.AccountId =  IsNull(@GroupId, b.AccountId)
			And c.FmId = IsNull(@FmId,c.FmId)
			--And  (b.OrderQualifierID <> 39006)				--Exclude Kanata /* Included Jan 28 MS e.g  invoice 33601*/
			And (IsNull(i.DateTime_Approved, '9/9/1900') <> '9/9/1900')	-- Approved Invoices
			And(invs.Section_Type_Id = 6) 					--Inventory product Tax Included
			And od.producttype in ( '46002')					--Gift, Choc and Food
			And od.StatusInstance in (508,513)				--Shipped or Unshippable (invoiced)
			--And (i.ACCOUNT_TYPE_ID IN (50601, 50603)) 			--Group Account
			And (             Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as Datetime)    >= @StartDate
				And  Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as DateTime) <= @EndDate
	      		       ) 
	End	
	Else
	Begin
			-- For Magazine the quantity is number of Issue, There may be more than one order with the possibility of a staff order for an account	
	
			Select @Cnt =  Sum(IsNull(QuantityShipped,0))
			From  QSPCanadaOrderManagement.dbo.Batch b Inner Join
                      		 QSPCanadaOrderManagement.dbo.CustomerOrderHeader oh On b.ID = oh.OrderBatchID And b.[Date] = oh.OrderBatchDate Inner Join
                      		 QSPCanadaOrderManagement.dbo.CustomerOrderDetail od On oh.Instance = od.CustomerOrderHeaderInstance Inner Join
                      		 QSPCanadaFinance.dbo.Invoice i On od.InvoiceNumber = i.Invoice_Id Inner Join
                       		QSPCanadaFinance.dbo.Invoice_Section invs On i.Invoice_Id = invs.Invoice_Id Inner Join
                      		QSPCanadaCommon.dbo.Campaign c On b.CampaignID = c.ID
			Where b.AccountId =  IsNull(@GroupId, b.AccountId)
			And c.FmId = IsNull(@FmId,c.FmId)
			--And  (b.OrderQualifierID <> 39006)				--Exclude Kanata /* Included Jan 28 MS e.g  invoice 33601*/
			And (IsNull(i.DateTime_Approved, '9/9/1900') <> '9/9/1900')	-- Approved Invoices
			And(invs.Section_Type_Id = 1) 					--Inventory product Tax Included
			And od.producttype in ( '46002','46003','46005')			--Gift, Choc and Food
			And od.StatusInstance in (508,513)				--Shipped or Unshippable (invoiced)
			--And (i.ACCOUNT_TYPE_ID IN (50601, 50603)) 			--Group Account
			And (             Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as Datetime)    >= @StartDate
				And  Cast(  IsNull(Convert(Varchar(10),i.DateTime_Approved, 101), '9/9/1900')  as DateTime) <= @EndDate
	      		       ) 
	End
	
	

	Return IsNull(@Cnt,0)
End
GO
