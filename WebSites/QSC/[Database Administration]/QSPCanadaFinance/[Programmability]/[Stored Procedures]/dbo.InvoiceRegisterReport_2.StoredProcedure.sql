USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[InvoiceRegisterReport_2]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InvoiceRegisterReport_2] 
	@InvoiceEffectivedateFrom	DateTime,
	@InvoiceEffectivedateTo		DateTime,
	@FMId						Int,
	@InvoiceType				Int,
	@SortBy						Varchar(10)
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
	
	IsNull(QSPByProdMag.Product_Amount,0)	+ 
	IsNull(QSPByProdBook.Product_Amount,0)	+
	IsNull(QSPByProdMusic.Product_Amount,0) +
	IsNull(QSPByProdVideo.Product_Amount,0) +
	IsNull(QSPByProdGift.Product_Amount,0)  +
	IsNull(QSPByProdFood.Product_Amount,0)  +
	ISNULL(QSPByProdProcessingFee.Product_Amount, 0) +					-- pfee
	ISNULL(QSPByProdMag.US_Postage_Amount, 0) AS TotalInvoiceAmount,	-- postage
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
	IsNull(QSPByProdBook.Product_Amount,0)AS BookTotal,
	IsNull(QSPByProdMusic.Product_Amount,0)AS MusicTotal,
	IsNull(QSPByProdVideo.Product_Amount,0)AS  VideoTotal,
	IsNull(QSPByProdGift.Product_Amount,0) AS GiftTotal,
	IsNull(QSPByProdFood.Product_Amount,0) AS FoodTotal,
	ISNULL(QSPByProdProcessingFee.Product_Amount, 0) AS ProcessingFeeTotal,
	ISNULL(QSPByProdMag.US_Postage_Amount, 0) AS PostageFeeTotal,
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
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdMusic (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_By_QSP_Product QSPByProdVideo (NOLOCK),
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
    (QSPByProdMusic.invoice_id =*INVOICE.Invoice_id And QSPByProdMusic.QSP_Product_Line_Id=46007) And
    (QSPByProdVideo.invoice_id =*INVOICE.Invoice_id And QSPByProdVideo.QSP_Product_Line_Id=46012) And
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

-- SELECT * FROM #InvoiceByProductInformation


/*
Update the columns magazines, Books, Gift, etc to include the sales, taxes and Fees (postage and processing)
*/
/*
UPDATE	t
SET		t.MagazineTotal = tmag.MagazineTotal,
		t.BookTotal		= tbook.BookTotal,
		t.MusicTotal	= tmusic.MusicTotal,
		t.VideoTotal	= tvideo.VideoTotal,
		t.GiftTotal		= tgift.GiftTotal,
		t.FoodTotal		= tfood.FoodTotal
FROM	#InvoiceByProductInformation t
LEFT JOIN
(
SELECT		t.invoiceID, ISNULL(SUM(mag.Product_Amount) + SUM(mag.ProductLine_Tax1) + SUM(mag.ProductLine_Tax2) + SUM(mag.US_Postage_Amount), CAST('0.00' as numeric(5,2))) AS MagazineTotal
From		#InvoiceByProductInformation				t
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_By_QSP_Product mag		ON t.InvoiceId = mag.Invoice_ID		And (mag.QSP_Product_Line_Id=46001   OR mag.QSP_Product_Line_ID = 46017)  
GROUP BY	t.invoiceID
)AS tmag ON t.invoiceID = tmag.invoiceID

LEFT JOIN
(
SELECT		t.invoiceID, ISNULL(SUM(book.Product_Amount) + SUM(book.ProductLine_Tax1) + SUM(book.ProductLine_Tax2) + SUM(book.US_Postage_Amount), CAST('0.00' as numeric(5,2))) AS BookTotal
From		#InvoiceByProductInformation				t
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_By_QSP_Product book	ON t.InvoiceId = book.Invoice_ID	And (book.QSP_Product_Line_Id=46006)  
GROUP BY	t.invoiceID
)AS tbook  ON t.invoiceID = tbook.invoiceID

LEFT JOIN
(
SELECT		t.invoiceID, ISNULL(SUM(music.Product_Amount) + SUM(music.ProductLine_Tax1) + SUM(music.ProductLine_Tax2) + SUM(music.US_Postage_Amount), CAST('0.00' as numeric(5,2)))  AS MusicTotal
From		#InvoiceByProductInformation				t
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_By_QSP_Product music	ON t.InvoiceId = music.Invoice_ID	And (music.QSP_Product_Line_Id=46007) 
GROUP BY	t.invoiceID
)AS tmusic  ON t.invoiceID = tmusic.invoiceID

LEFT JOIN
(
SELECT		t.invoiceID, ISNULL(SUM(video.Product_Amount) + SUM(video.ProductLine_Tax1) + SUM(video.ProductLine_Tax2) + SUM(video.US_Postage_Amount), CAST('0.00' as numeric(5,2)))  AS VideoTotal
From		#InvoiceByProductInformation				t
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_By_QSP_Product video	ON t.InvoiceId = video.Invoice_ID	And (video.QSP_Product_Line_Id=46012) 
GROUP BY	t.invoiceID
)AS tvideo  ON t.invoiceID = tvideo.invoiceID

LEFT JOIN
(
SELECT		t.invoiceID, ISNULL(SUM(gift.Product_Amount) + SUM(gift.ProductLine_Tax1) + SUM(gift.ProductLine_Tax2) + SUM(gift.US_Postage_Amount), CAST('0.00' as numeric(5,2)))  AS GiftTotal
From		#InvoiceByProductInformation				t
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_By_QSP_Product gift	ON t.InvoiceId = gift.Invoice_ID	And (gift.QSP_Product_Line_Id=46002) 
GROUP BY	t.invoiceID
)AS tgift   ON t.invoiceID = tgift.invoiceID

LEFT JOIN
(
SELECT		t.invoiceID, ISNULL(SUM(food.Product_Amount) + SUM(food.ProductLine_Tax1) + SUM(food.ProductLine_Tax2) + SUM(food.US_Postage_Amount), CAST('0.00' as numeric(5,2)))  AS FoodTotal
From		#InvoiceByProductInformation				t
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_By_QSP_Product food	ON t.InvoiceId = food.Invoice_ID	And (food.QSP_Product_Line_Id=46005) 
GROUP BY	t.invoiceID
)AS tfood	ON t.invoiceID = tfood.invoiceID
*/


-- SELECT top 1 * FROM QSPCanadaFinance.dbo.INVOICE WHERE invoiceid= 484912

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


/*

SELECT * FROM QSPCanadaFinance.dbo.Invoice_By_QSP_Product WHERE Invoice_ID = 475894
SELECT * FROM QSPCanadaFinance.dbo.Invoice_By_QSP_Product WHERE Invoice_ID = 484911


SELECT * FROM QSPCanadaFinance.dbo.Invoice_By_QSP_Product WHERE Invoice_ID IN (46002, 46005, 46006, 46007, 46012)
*/
GO
