USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[InvoiceRegisterReport]    Script Date: 07/25/2011 14:15:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceRegisterReport_2] 	
	@InvoiceEffectivedateFrom	DateTime,
	@InvoiceEffectivedateTo	DateTime,
	@FMId				Int,
	@InvoiceType			Int,
	@SortBy			Varchar(10)
/************************************************************************************************************************************
Re-Written March 23, 2006 MS
-- CRL 8/2/2011
-- Removed music, video and WFC ana added postage and processing fees
**************************************************************************************************************************************/
AS

SET NOCOUNT ON

SELECT  
	INVOICE.Invoice_Id AS InvoiceId,
	CONVERT(VARCHAR(10),INVOICE.Invoice_Effective_Date,1) AS InvoiceIdEffectiveDate, 
	CASE IsNull(DateTime_Approved, '01/01/1995')
		WHEN '01/01/1995' THEN ''
		ELSE 'Approved'
	END AS Status,
	INVOICE.Account_Id AS BilltoAccountId,
	Account.Name AS AccountName,	
	--MagTotalTaxIncluded
 	(CASE MagSection.Section_Type_Id
		WHEN 2 THEN MagSection.Total_Tax_Included
		ELSE 0 
	END) +
	--GiftTotalTaxIncluded
	(CASE GiftSection.Section_Type_Id
		WHEN 1 THEN GiftSection.Total_Tax_Included
		ELSE 0 
	END) +
	-- CookieDTotalTaxIncluded
	(CASE CookieDSection.Section_Type_Id
		WHEN 6 Then CookieDSection.Total_Tax_Included
		ELSE 0 
	END) +
	--ProcessingFeeTotalTaxIncluded
	(CASE ProcessingFeeSection.Section_Type_ID
		WHEN 8 then ProcessingFeeSection.Total_Tax_Included
		ELSE 0
	END) AS TotalInvoiceAmount,
	--MagSectionGP
	(CASE MagSection.Section_Type_Id
		WHEN 2 Then MagSection.Group_Profit_Amount
		ELSE 0 
	END) +
	--GiftSectionGP
	(CASE GiftSection.Section_Type_Id
		WHEN 1 Then GiftSection.Group_Profit_Amount
		ELSE 0 
	END) +
	--CookieDSectionGP 
	(CASE CookieDSection.Section_Type_Id
		WHEN 6 Then CookieDSection.Group_Profit_Amount
		ELSE 0 
	END) AS TotalGroupProfit ,
	Batch.OrderID, 
	FM.FmId,
	Substring(FM.LastName,1,30) +' '+ Substring(FM.FirstName,1,30) AS FMName,
	ShipAd.stateProvince AS ShiptoProvince,
	IsNull(QSPByProdMag.Product_Amount,0) AS MagazineTotal,
	IsNull(QSPByProdBook.Product_Amount,0) AS BookTotal,
	IsNull(QSPByProdGift.Product_Amount,0) AS GiftTotal,
	IsNull(QSPByProdFood.Product_Amount,0) AS FoodTotal,
	ISNULL(QSPByProdProcessingFee.Product_Amount, 0) as ProcessingFeeTotal,
	ISNULL(QSPByProdMag.US_Postage_Amount, 0) as PostageFeeTotal,
	0 TotalCount,
	0 RowCnt
Into #InvoiceByProductInformation
From      
	QSPCanadaFinance.dbo.INVOICE INVOICE 			      (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION MagSection       (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION GiftSection         (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION CookieDSection (NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION ProcessingFeeSection (NOLOCK),
	QSPCanadaCommon.dbo.CAccount Account 			      (NOLOCK),
	QSPCanadaOrderManagement.dbo.Batch Batch 		      (NOLOCK),
	QSPCanadaCommon.dbo.Campaign Campaign 		  	      (NOLOCK),
	QSPCanadaCommon.dbo.FieldManager FM			      (NOLOCK),
	QSPCanadaCommon.dbo.Address ShipAd		                   (NOLOCK),
	QSPCanadaCommon.dbo.CAccount ShipAcc	                   (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdMag   (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdBook  (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdGift     (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdFood  (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdProcessingFee  (NOLOCK)
Where 
	INVOICE.ORDER_ID = Batch.OrderID And   
	Batch.CampaignID = Campaign.ID And   
	Campaign.FMID = FM.FMID And   
	(INVOICE.INVOICE_ID *= MagSection.INVOICE_ID  And MagSection.Section_Type_Id=2) And   
	(INVOICE.INVOICE_ID *= GiftSection.Invoice_Id And GiftSection.Section_Type_Id=1) And   
	(INVOICE.INVOICE_ID *= CookieDSection.Invoice_ID And CookieDSection.section_Type_ID=6) And
	(INVOICE.INVOICE_ID *= ProcessingFeeSection.Invoice_ID AND ProcessingFeeSection.section_Type_ID = 8) AND
	INVOICE.ACCOUNT_ID = Account.Id And   
	Batch.ShipToAccountID = ShipAcc.Id And   
	ShipAd.AddressListID = ShipAcc.AddressListID And   
	ShipAd.address_type 	= 54001 And   
	(QSPByProdMag.invoice_id =*INVOICE.Invoice_id   And QSPByProdMag.QSP_Product_Line_Id=46001) And   
	(QSPByProdBook.invoice_id =*INVOICE.Invoice_id  And QSPByProdBook.QSP_Product_Line_Id=46006) And    
	(QSPByProdGift.invoice_id =*INVOICE.Invoice_id    And QSPByProdGift.QSP_Product_Line_Id=46002) And   
	(QSPByProdFood.invoice_id =*INVOICE.Invoice_id And QSPByProdFood.QSP_Product_Line_Id=46005) And 
	(QSPByProdProcessingFee.invoice_id =* Invoice.Invoice_id AND QSPByProdProcessingFee.QSP_Product_Line_ID = 46017) AND  
	FM.FmId 	= IsNull(@FMId,FM.FMID) And   
	Batch.OrderTypeCode = IsNull(@InvoiceType,Batch.OrderTypeCode) And  -- 41003 CreditCM  41004  DebitCM 
	Cast(Convert(Varchar(10),INVOICE.Invoice_Effective_Date,101) As DateTime)  >=  IsNull(@InvoiceEffectivedateFrom, Cast(Convert(Varchar(10),INVOICE.Invoice_Effective_Date,101) As DateTime) ) And   
	Cast(Convert(Varchar(10),INVOICE.Invoice_Effective_Date,101) As DateTime)  <=  IsNull(@InvoiceEffectivedateTo,    Cast(Convert(Varchar(10),INVOICE.Invoice_Effective_Date,101) As DateTime) ) And   
	MagSection.Section_Type_Id <> 5  And 	--Misc
	GiftSection.Section_Type_Id <> 5 And
	CookieDSection.Section_Type_Id <> 5 AND
	ProcessingFeeSection.Section_Type_ID <> 5

Create Index InvoiceIDIndexProductInformation on #InvoiceByProductInformation (InvoiceId)

Select	 
	Invoice.Invoice_id AS InvoiceId,
	Sum(IsNull(GST.tax_amount,0)) AS GSTAmount,
	Sum(IsNull(PST.tax_amount,0)) AS HSTAmount,
	Sum(IsNull(HST.tax_amount,0)) AS PSTAmount
Into 
	#InvoiceTaxInformation
From 	
	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX  GST   (NOLOCK), 
	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX  HST	(NOLOCK),
	QSPCanadaFinance.dbo.INVOICE_SECTION_TAX  PST   (NOLOCK),
	QSPCanadaFinance..Invoice_section Invoice_section, 
	Invoice Invoice (NOLOCK)
Where 
	Invoice.invoice_id=Invoice_section.invoice_id And 
	(Invoice_section.invoice_section_id *=gst.invoice_section_id  And gst.tax_id=1) And 
	(Invoice_section.invoice_section_id *=pst.invoice_section_id  And pst.tax_id in(2,4,5)) And 
	(Invoice_section.invoice_section_id *=Hst.invoice_section_id  And Hst.tax_id not in(1,2,4,5)) And 
	Invoice.invoice_id in 
		(Select 
			InvoiceId 
		From 
			#InvoiceByProductInformation) 

Group By Invoice.invoice_id

Create Index  InvoiceIDIndexTaxInformation on #InvoiceTaxInformation (InvoiceId)

Select 
	#InvoiceByProductInformation.*,
	#InvoiceTaxInformation.GstAmount,
	#InvoiceTaxInformation.HSTAmount,
	#InvoiceTaxInformation.PstAmount
From  
	#InvoiceByProductInformation
		INNER JOIN #InvoiceTaxInformation ON #InvoiceByProductInformation.InvoiceId=#InvoiceTaxInformation.InvoiceId
Order By 
	ShiptoProvince,
	(CASE IsNull(@SortBy, 'INVOICE')
		WHEN 'INVOICE' THEN #InvoiceByProductInformation.InvoiceId
		WHEN 'ACCOUNTNAME' THEN Ltrim(Rtrim(AccountName ))
		ELSE  BilltoAccountId -- 'ACCOUNT'
	END)

Drop Table #InvoiceByProductInformation;
Drop Table #InvoiceTaxInformation;