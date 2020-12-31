USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[InvoiceRegisterReport]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceRegisterReport] 	@InvoiceEffectivedateFrom	DateTime,
							@InvoiceEffectivedateTo	DateTime,
							@FMId				Int,
							@InvoiceType			Int,
							@SortBy			Varchar(10)
/************************************************************************************************************************************
Re-Written March 23, 2006 MS

**************************************************************************************************************************************/
AS
Set NoCount ON

Select  I.Invoice_Id InvoiceId,
	Convert(Varchar(10),I.Invoice_Effective_Date,1)InvoiceIdEffectiveDate, 
	(Case IsNull(DateTime_Approved,'01/01/1995')
	When '01/01/1995' Then ''
	Else 'Approved'
	End) Status,
	I.Account_Id BilltoAccountId,
	A.Name AccountName,	
	--MagTotalTaxIncluded
 	(Case MagSection.Section_Type_Id
	When 2 Then MagSection.Total_Tax_Included
	Else 0 
	End ) +
	--GiftTotalTaxIncluded
	(Case GiftSection.Section_Type_Id
	When 1 Then GiftSection.Total_Tax_Included
	Else 0 
	End )+
	-- CookieDTotalTaxIncluded
	(Case CookieDSection.Section_Type_Id
	When 6 Then CookieDSection.Total_Tax_Included
	Else 0 
	End ) TotalInvoiceAmount,
	--MagSectionGP
	(Case MagSection.Section_Type_Id
	When 2 Then MagSection.Group_Profit_Amount
	Else 0 
	End)+
	--GiftSectionGP
	(Case GiftSection.Section_Type_Id
	When 1 Then GiftSection.Group_Profit_Amount
	Else 0 
	End) +
	--CookieDSectionGP 
	(Case CookieDSection.Section_Type_Id
	When 6 Then CookieDSection.Group_Profit_Amount
	Else 0 
	End) TotalGroupProfit ,
	B.OrderID, 
	F.FmId,
	Substring(f.LastName,1,30) +' '+Substring(f.FirstName,1,30)FMName,
	ShipAd.stateProvince ShiptoProvince,
	IsNull(QSPByProdMag.Product_Amount,0) MagazineTotal,
	IsNull(QSPByProdBook.Product_Amount,0) BookTotal,
	IsNull(QSPByProdMusic.Product_Amount,0) MusicTotal,
	IsNull(QSPByProdVideo.Product_Amount,0) VideoTotal,
	IsNull(QSPByProdGift.Product_Amount,0) GiftTotal,
	IsNull(QSPByProdFood.Product_Amount,0) FoodTotal,
	IsNull(QSPByProdWFC.Product_Amount,0) WFCTotal,
	0 TotalCount,
	0 RowCnt
	--isNull(MagSection.Invoice_Section_Id,0) MagSectionId,
	--IsNull(MagSection.Section_Type_Id,0)MagSectionType,
	--IsNull(GiftSection.Invoice_Section_Id,0) GiftSectionId,
	--IsNull(GiftSection.Section_Type_Id,0)    GiftSectionType,
	--IsNull(CookieDSection.Invoice_Section_Id,0) CookieDSectionId,
	--IsNull(CookieDSection.Section_Type_Id,0) CookieDSectionType
Into #a
From      QSPCanadaFinance.dbo.INVOICE I 			      (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION MagSection       (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION GiftSection         (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION CookieDSection (NOLOCK),
	QSPCanadaCommon.dbo.CAccount A 			      (NOLOCK),
	QSPCanadaOrderManagement.dbo.Batch B 		      (NOLOCK),
	QSPCanadaCommon.dbo.Campaign C 		  	      (NOLOCK),
	QSPCanadaCommon.dbo.FieldManager F			      (NOLOCK),
	QSPCanadaCommon.dbo.Address ShipAd		                   (NOLOCK),
	QSPCanadaCommon.dbo.CAccount ShipAcc	                   (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdMag   (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdBook  (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdMusic (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdVideo (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdGift     (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdFood  (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdWFC  (NOLOCK)
Where I.ORDER_ID = B.OrderID
And   B.CampaignID = C.ID
And   C.FMID = F.FMID
And   (I.INVOICE_ID *= MagSection.INVOICE_ID  And MagSection.Section_Type_Id=2)
And   (I.INVOICE_ID *= GiftSection.Invoice_Id And GiftSection.Section_Type_Id=1)
And   (I.INVOICE_ID *= CookieDSection.Invoice_ID And CookieDSection.section_Type_ID=6)
And   I.ACCOUNT_ID = A.Id
And   B.ShipToAccountID = ShipAcc.Id
And   ShipAd.AddressListID = ShipAcc.AddressListID
And   ShipAd.address_type 	= 54001
And   (QSPByProdMag.invoice_id =*I.Invoice_id   And QSPByProdMag.QSP_Product_Line_Id=46001)
And   (QSPByProdBook.invoice_id =*I.Invoice_id  And QSPByProdBook.QSP_Product_Line_Id=46006)
And   (QSPByProdMusic.invoice_id =*I.Invoice_id And QSPByProdMusic.QSP_Product_Line_Id=46007)
And   (QSPByProdVideo.invoice_id =*I.Invoice_id And QSPByProdVideo.QSP_Product_Line_Id=46012)
And   (QSPByProdGift.invoice_id =*I.Invoice_id    And QSPByProdGift.QSP_Product_Line_Id=46002)
And   (QSPByProdFood.invoice_id =*I.Invoice_id And QSPByProdFood.QSP_Product_Line_Id=46005)
And   (QSPByProdWFC.invoice_id =*I.Invoice_id And QSPByProdWFC.QSP_Product_Line_Id=46003)
And   F.FmId 	= IsNull(@FMId,f.FMID)
And   B.OrderTypeCode = IsNull(@InvoiceType,B.OrderTypeCode)  -- 41003 CreditCM  41004  DebitCM
And   Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime)  >=  IsNull(@InvoiceEffectivedateFrom, Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime) )
And   Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime)  <=  IsNull(@InvoiceEffectivedateTo,    Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime) )
And   MagSection.Section_Type_Id <> 5  	--Misc
And   GiftSection.Section_Type_Id <> 5
And    CookieDSection.Section_Type_Id <> 5

Create Index InvoiceIDIndexA on #A (InvoiceId)

Select	 i.Invoice_id InvoiceId,
	 Sum(IsNull(gst.tax_amount,0))GSTAmount,
	 Sum(IsNull(pst.tax_amount,0))HSTAmount,
	 Sum(IsNull(Hst.tax_amount,0))PSTAmount
Into #b
From 	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX  GST   (NOLOCK), 
	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX  HST	(NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX  PST   (NOLOCK),
	QSPCanadaFinance..Invoice_section s, Invoice i     	(NOLOCK)
Where i.invoice_id=s.invoice_id
And (s.invoice_section_id *=gst.invoice_section_id  And gst.tax_id=1)
And (s.invoice_section_id *=pst.invoice_section_id  And pst.tax_id in(2,4,5))
And (s.invoice_section_id *=Hst.invoice_section_id  And Hst.tax_id not in(1,2,4,5))
And i.invoice_id in (Select InvoiceId From #a) 
Group By i.invoice_id

Create Index  InvoiceIDIndexB on #B (InvoiceId)

If IsNull(@SortBy,'INVOICE') = 'INVOICE'
Begin
	Select #A.*,#B.GstAmount,#b.HSTAmount,#b.PstAmount
	From  #A , #B
	Where #A.InvoiceId=#B.InvoiceId
	Order By ShiptoProvince,#A.InvoiceId
	
End
Else 
Begin
	If IsNull(@SortBy,'INVOICE') = 'ACCOUNTNAME'
	Begin
		Select #A.*,#B.GstAmount,#b.HSTAmount,#b.PstAmount
		From  #A , #B
		Where #A.InvoiceId=#B.InvoiceId
		Order By ShiptoProvince,Ltrim(Rtrim(AccountName ))
	End
	Else
	Begin	
		Select #A.*,#B.GstAmount,#b.HSTAmount,#b.PstAmount
		From  #A , #B
		Where #A.InvoiceId=#B.InvoiceId
		Order By ShiptoProvince,BilltoAccountId    -- 'ACCOUNT'
	End
End

Drop Table #a
Drop Table #b

/*
Declare 	@Rows			Int,
	     	@MagAmount		Numeric(10,2),
	     	@BooktAmount   	Numeric(10,2),
	     	@MusicAmount   	Numeric(10,2),
		@VideoAmount   	Numeric(10,2),
		@GiftAmount   		Numeric(10,2),
		@FoodAmount   	Numeric(10,2),
		@WFCAmount   	Numeric(10,2),
	     	@cnt			Int,
	     	@InvId		Int
	     	

Declare  @DistinctInvoices  Table(
			Id 		Int IDENTITY,
			InvoiceId	Int
			--RowCnt	Int
			)

Declare  @ProductLineTotal  Table(
			InvoiceId	Int,
			ProductLine	Int,
			TotalAmount	Numeric(10,2)
			)

Declare @Invoice Table (
			Id			Int IDENTITY,
			InvoiceId		Int,
			InvoiceIdEffectiveDate	Varchar(10), 
			Status			Varchar(15),
			BilltoAccountId		Int,
			AccountName		Varchar(50),
			TotalInvoiceAmount	Numeric(10,2), 
			TotalGroupProfit	Numeric(10,2), 
			TaxGST			Varchar(3),
			GSTAmount		Numeric(10,2),
			TaxHST			Varchar(10),
			HSTAmount		Numeric(10,2),
			TaxPST			Varchar(10),
			PSTAmount		Numeric(10,2),
			OrderId			Int,
			FmId			Int,
			FmName			Varchar(50),
			ShiptoProvince		Varchar(10),
			MagazineTotal		Numeric(10,2),
			BookTotal		Numeric(10,2),
			MusicTotal		Numeric(10,2),
			VideoTotal		Numeric(10,2),
			GiftTotal		Numeric(10,2),
			FoodTotal		Numeric(10,2),
			WFCTotal		Numeric(10,2),
			TotalCount		Int,
			RowCnt			Int
						
			)

Declare @InvoiceTotals Table (
			Id			Int IDENTITY,
			InvoiceId		Int,
			InvoiceIdEffectiveDate	Varchar(10), 
			Status			Varchar(15),
			BilltoAccountId		Int,
			AccountName		Varchar(50),
			TotalInvoiceAmount	Numeric(10,2), 
			TotalGroupProfit	Numeric(10,2), 
			GSTAmount		Numeric(10,2),
			HSTAmount		Numeric(10,2),
			PSTAmount		Numeric(10,2),
			OrderId			Int,
			FmId			Int,
			FmName			Varchar(50),
			ShiptoProvince		Varchar(10),
			MagazineTotal		Numeric(10,2),
			BookTotal		Numeric(10,2),
			MusicTotal		Numeric(10,2),
			VideoTotal		Numeric(10,2),
			GiftTotal		Numeric(10,2),
			FoodTotal		Numeric(10,2),
			WFCTotal		Numeric(10,2),
			TotalCount		Int,
			RowCnt			Int
						
			)

Insert into @Invoice
Select  I.Invoice_Id,
	Convert(Varchar(10),I.Invoice_Effective_Date,1), 
	(Case IsNull(DateTime_Approved,'01/01/1995')
	When '01/01/1995' Then ''
	Else 'Approved'
	End) ,
	I.Account_Id,
	A.Name, 
	Sum(Sect.Total_Tax_Included) ,
	Sum(Sect.Group_Profit_Amount) ,
	T1.Tax_Desc , 
	IsNUll(Sum(St1.Tax_Amount),0) , --GstAmount
	T2.Tax_Desc , 
	IsNull(Sum(St2.Tax_Amount),0) ,  --Hst Amount
        	T3.Tax_Desc , 
	IsNull(Sum(St3.Tax_Amount),0) ,	--Pst AMount
	B.OrderID, 
	F.FmId,
	Substring(f.LastName,1,30) +' '+Substring(f.FirstName,1,30),
	ShipAd.stateProvince,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0
FROM   QSPCanadaFinance.dbo.INVOICE_SECTION_TAX St3 INNER JOIN
       	QSPCanadaCommon.dbo.Tax T3 ON St3.TAX_ID = T3.TAX_ID RIGHT OUTER JOIN
       	QSPCanadaFinance.dbo.INVOICE I INNER JOIN
       	QSPCanadaCommon.dbo.CAccount A ON I.ACCOUNT_ID = A.Id INNER JOIN
       	QSPCanadaOrderManagement.dbo.Batch b ON I.ORDER_ID = b.OrderID INNER JOIN
       	QSPCanadaCommon.dbo.Campaign c ON b.CampaignID = c.ID INNER JOIN
       	QSPCanadaCommon.dbo.FieldManager f ON c.FMID = f.FMID INNER JOIN
       	QSPCanadaFinance.dbo.INVOICE_SECTION Sect ON I.INVOICE_ID = Sect.INVOICE_ID ON St3.INVOICE_SECTION_ID = Sect.INVOICE_SECTION_ID AND St3.TAX_ID NOT IN (1,2, 4, 5) LEFT OUTER JOIN
       	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX St2 INNER JOIN 
       	QSPCanadaCommon.dbo.Tax T2 ON St2.TAX_ID = T2.TAX_ID ON Sect.INVOICE_SECTION_ID = St2.INVOICE_SECTION_ID AND St2.TAX_ID IN (2, 4, 5) LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.Tax T1 INNER JOIN
       	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX St1 ON T1.TAX_ID = St1.TAX_ID ON Sect.INVOICE_SECTION_ID = St1.INVOICE_SECTION_ID AND St1.TAX_ID = 1 LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.Address ShipAd INNER JOIN
       	QSPCanadaCommon.dbo.CAccount ShipAcc ON ShipAd.AddressListID = ShipAcc.AddressListID ON b.ShipToAccountID = ShipAcc.Id
Where ShipAd.address_type 	= 54001 	--Shipto
And 	F.FmId 			= IsNull(@FMId,f.FMID)
And 	B.OrderTypeCode 	= IsNull(@InvoiceType,B.OrderTypeCode)  -- 41003 CreditCM  41004  DebitCM
And 	Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime)  >=  IsNull(@InvoiceEffectivedateFrom, Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime) )
And 	Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime)  <=  IsNull(@InvoiceEffectivedateTo,    Cast(Convert(Varchar(10),I.Invoice_Effective_Date,101) As DateTime) )
And 	Sect.Section_Type_Id <> 5  	--Misc 
Group By I.Invoice_Id, I.Invoice_Effective_Date, I.Account_Id, A.Name, b.OrderID, f.FirstName, f.LastName, ShipAd.stateProvince,DateTime_Approved,
	F.FmId,T1.Tax_Desc ,T2.Tax_Desc,T3.Tax_Desc  


Insert into @InvoiceTotals
Select 	InvoiceId,
	InvoiceIdEffectiveDate,
	Status,
	BilltoAccountId,
	AccountName,
	Sum(TotalInvoiceAmount) ,
	Sum(TotalGroupProfit),
	Sum(GSTAmount),
	Sum(HSTAmount),
	Sum(PSTAmount),
	OrderId,
	FMId,
	FMName,
	ShiptoProvince,
	MagazineTotal,
	BookTotal,
	MusicTotal,
	VideoTotal,
	GiftTotal,
	FoodTotal,
	WFCTotal,
	TotalCount,
	RowCnt
From @Invoice
Group By InvoiceId,
	InvoiceIdEffectiveDate,
	Status,
	BilltoAccountId,
	AccountName,
	OrderId,
	FMId,
	FMName,
	ShiptoProvince,
	MagazineTotal,
	BookTotal,
	MusicTotal,
	VideoTotal,
	GiftTotal,
	FoodTotal,
	WFCTotal,
	TotalCount,
	RowCnt


Insert into @DistinctInvoices
	Select Distinct InvoiceId From  @Invoice


Insert Into @ProductLineTotal
	Select * From Invoice_By_QSP_Product
	Where Invoice_Id in (Select InvoiceId From  @DistinctInvoices)


Select @Rows = Count(*) From @DistinctInvoices

While @Rows > 0
	Begin
		Select  @InvId = InvoiceId From @DistinctInvoices Where  Id= @Rows	

		Select @MagAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46001'  And InvoiceId = @InvId	--Magazine

		Select @BooktAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46006'  And InvoiceId= @InvId	--Books

		Select @MusicAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46007'  And InvoiceId= @InvId	--Music

		Select @VideoAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46012'  And InvoiceId= @InvId	--Video

		Select @GiftAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46002'  And InvoiceId= @InvId	--Gift

		Select @FoodAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46005'  And InvoiceId= @InvId	--Food

		Select @WFCAmount= Sum(TotalAmount)  From @ProductLineTotal Where ProductLine = '46003'  And InvoiceId= @InvId	--WFC
	

				
			Update @InvoiceTotals 
			Set 	MagazineTotal  = @MagAmount, 
				BookTotal = IsNull(@BooktAmount,0),
				MusicTotal=IsNull(@MusicAmount,0),
				VideoTotal = IsNull(@VideoAmount,0),
				GiftTotal = IsNull(@GiftAmount,0),
				FoodTotal = IsNull(@FoodAmount,0),
				WFCTotal = IsNull(@WFCAmount,0)
			Where InvoiceId = @InvId
			

	Set @Rows=@Rows-1
End

If IsNull(@SortBy,'INVOICE') = 'INVOICE'
Begin
	Select * from @InvoiceTotals Order By ShiptoProvince,InvoiceId
	
End
Else If IsNull(@SortBy,'INVOICE') = 'ACCOUNTNAME'
Begin
	Select 	*from @InvoiceTotals Order By ShiptoProvince,Ltrim(Rtrim(AccountName ))
End
Else
	Select 	*from @InvoiceTotals Order By ShiptoProvince,BilltoAccountId    -- 'ACCOUNT'
*/
GO
