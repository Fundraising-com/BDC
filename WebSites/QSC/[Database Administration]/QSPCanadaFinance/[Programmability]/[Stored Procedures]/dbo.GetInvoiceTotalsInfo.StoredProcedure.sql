USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceTotalsInfo]    Script Date: 06/07/2017 09:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoiceTotalsInfo]

	@InvoiceID 	int
AS

SET NOCOUNT ON

CREATE TABLE #InvoiceTotals
(
	IsStaffOrder bit, 
	OrderQualifierID int, 
	OrderTypeCode int, 
	Invoice_ID int, 
	AcctID int, 
	PhoneNumber varchar(20),
	CampaignID int, 
	Order_Id int, 
	FMID varchar(4), 
	FMName varchar(100), 
	ContactName varchar(100),
	Invoice_Date varchar(10), 
	Invoice_Due_Date varchar(10), 
	AcctName varchar(100),
	ShippingAddress varchar(100),
	ShippingAddress2 varchar(100),
	ShippingCity varchar(100),
	ShippingState varchar(20),
	ShippingZip varchar(7),
	ShippingZip4 varchar(4),
	Is_Printed bit,
	ContactId int,
	Lang varchar(15), 
	Section_Type_Id int, 
	Total_Tax_Included numeric(20,2), 
	Total_Tax_Excluded numeric(20,2), 
	Item_Count int,
	Group_Profit_Rate int, 
	Group_Profit_Amount numeric(20,2),
	Total_Taxable_Amount numeric(20,2), 
	Net_Before_Tax numeric(20,2),
	GST int,
	GST_Rate numeric(20,3),
	GST_Total numeric(20,2),
	PST int,
	PST_Rate numeric(20,3),
	PST_Total numeric(10,2),
	HST int,
	HST_Rate numeric(10,3),
	HST_Total numeric(10,2),
	Total_Tax_Amount numeric(10,2), 
	Due_Amount numeric(10,2),
	DescriptionLabel varchar(200),
	GrossAmountLabel varchar(200),  
	TotalSecGSTLabel varchar(100),
	TotalSecHSTLabel varchar(100),
	TotalSecQSTLabel varchar(100),
	AdjustedGrossLabel varchar(200),
	GroupProfitLabel varchar(200),
	AmountDueLabel varchar(200),
	TotalAmountDueLabel varchar(200),
	TotalGSTInAmtDueLabel varchar(200),
	TotalPSTInAmtDueLabel varchar(200),
	DuplicateLabel varchar(100),
	MySortOrder int,
	PostageAmount numeric(10,2),
	PostageAmountLabel varchar(200),
	ProcessingFeeLabel varchar(200),
	ShippingFeeLabel varchar(200),
	SectionTypeGroupingID		INT,
	PFEE_ItemCount				INT,
	PFEE_TotalWithoutTax		NUMERIC(14,2),
	PFEE_TotalTax				NUMERIC(14,2),
	PFEE_GST					NUMERIC(14,2),
	PFEE_HST					NUMERIC(14,2),
	PFEE_PST					NUMERIC(14,2),
	SFEE_ItemCount				INT,
	SFEE_TotalWithoutTax		NUMERIC(14,2),
	SFEE_TotalTax				NUMERIC(14,2),
	SFEE_GST					NUMERIC(14,2),
	SFEE_HST					NUMERIC(14,2),
	SFEE_PST					NUMERIC(14,2),
	InvoicePeriodBeginning		VARCHAR(10),
	GroupProfitSplitLabel		VARCHAR(200),
	GroupProfitSplitPaperLabel	VARCHAR(200),
	GroupProfitSplitOnlineLabel	VARCHAR(200)
)

INSERT INTO #InvoiceTotals
SELECT
	camp.IsStaffOrder, 
	b.OrderQualifierID, 
	b.OrderTypeCode, 
	inv.Invoice_ID, 
	ISNULL(accSchool.ID, acc.ID) AS AcctID, 
	QSPCanadaFinance.dbo.UDF_CleanPhoneNumber(ph.phoneNumber,'-') AS phoneNumber,
	b.CampaignID, 
	inv.Order_Id, 
	camp.FMID, 
	FM.FirstName + ' ' + FM.LastName AS FMName, 
	cont.FirstName + ' ' + cont.LastName AS ContactName,
	CONVERT(varchar, inv.Invoice_Date,101) AS Invoice_Date, 
	CONVERT(varchar, inv.Invoice_Due_Date,101) AS Invoice_Due_Date, 
	ISNULL(accSchool.Name, acc.Name) AS AcctName,
	adShip.Street1 AS ShippingAddress,
	adShip.Street2 AS ShippingAddress2,
	adShip.City AS ShippingCity,
	adShip.StateProvince AS ShippingState,
	adShip.Postal_Code AS ShippingZip,
	adShip.Zip4 AS ShippingZip4,
	CASE inv.Is_Printed	
		WHEN 'Y' THEN 1
		WHEN 'N' THEN 0
		ELSE 0
	END AS Is_Printed,
	MAX(cont.Id) AS ContactId,
	camp.Lang, 
	invSec.Section_Type_Id,
	invSec.Total_Tax_Included, 
	invSec.Total_Tax_Excluded, 
	invSec.Item_Count,
	invSec.Group_Profit_Rate, 
	invSec.Group_Profit_Amount,
	invSec.Total_Taxable_Amount, 
	invSec.Net_Before_Tax,
	ISNULL(GST.tax_id,0) AS GST,
	ISNULL(GST.tax_rate,0) AS GST_Rate,
	ISNULL((GST.tax_amount),0) AS GST_Total,
	ISNULL(PST.tax_id,0) PST,
	ISNULL(PST.tax_rate,0) PST_Rate,
	ISNULL((PST.tax_amount),0)  PST_Total,
	ISNULL(HST.tax_id,0) HST,
	ISNULL(HST.tax_rate,0) HST_Rate,
	ISNULL((HST.tax_amount),0)  HST_Total,
	ISNULL(invSec.total_tax_amount,0) Total_Tax_Amount, 
	invSec.Due_Amount,
	CASE WHEN invSec.Section_Type_Id IN (2, 8, 12, 14) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'COMMANDES DES MAGAZINES'
				ELSE		   'MAGAZINE ORDERS'
			END
		 WHEN invSec.Section_Type_Id IN (1) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'CADEAUX'
				ELSE 'GIFT'
			END
		 WHEN invSec.Section_Type_Id IN (3) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'APPROVISIONNEMENTS DE CHAMP'
				ELSE 'FIELD SUPPLIES'
			END
		 WHEN invSec.Section_Type_Id IN (7) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'PRIX'
				ELSE 'PRIZES'
			END
		 WHEN invSec.Section_Type_Id IN (9) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'PÂTE À BISCUITS'
				ELSE 'COOKIE DOUGH'
			END
		 WHEN invSec.Section_Type_Id IN (10) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MAÏS SOUFFLÉ'
				ELSE 'POPCORN'
			END
		 WHEN invSec.Section_Type_Id IN (11) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'BIJOUX / BLOOM'
				ELSE 'JEWELLERY / BLOOM'
			END
		 WHEN invSec.Section_Type_Id IN (13) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'CHANDELLES'
				ELSE 'CANDLES'
			END
		 WHEN invSec.Section_Type_Id IN (15) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'CARTE D''ÉPARGNE'
				ELSE 'DISCOUNT CARDS'
			END
		 WHEN invSec.Section_Type_Id IN (16) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'CARTE CADEAU'
				ELSE 'GIFT CARD'
			END
		 WHEN invSec.Section_Type_Id IN (17) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'BÂTONNETS DE BRETZEL'
				ELSE 'PRETZEL RODS'
			END
		 WHEN invSec.Section_Type_Id IN (18) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'BÂTONNETS DE BRETZEL'
				ELSE 'PRETZEL RODS'
			END
	END AS DescriptionLabel,
	CASE WHEN invSec.Section_Type_Id IN (2, 14) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (MAGAZINES) INCLUANT LE TARIF POSTAL, LES TAXES, LES FRAIS DE TRAITEMENT, ET LES FRAIS LIVRAISON'
				ELSE		   'MAGAZINE GROSS AMOUNT WITH POSTAGE, TAXES, PROCESSING AND SHIPPING FEES'
			END
		 WHEN invSec.Section_Type_Id IN (8) THEN
			CASE camp.LANG
				WHEN 'FR' THEN '*MONTANT DES FRAIS DE TRAITEMENT'
				ELSE '*TOTAL PROCESSING FEES'
			END
		 WHEN invSec.Section_type_Id IN (1) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (CADEAUX)'
				ELSE 'GIFT - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (3) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (APPROVISIONNEMENTS DE CHAMP)'
				ELSE 'Field Supplies - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (6) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (PRIX)'
				ELSE 'PRIZES - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (7) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (PRIX)'
				ELSE 'PRIZES - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (9) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT DE LA PÂTE À BISCUITS'
				ELSE 'COOKIE DOUGH - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (10) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT DU MAÏS SOUFFLÉ'
				ELSE 'POPCORN - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (11) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT DE LA BIJOUX'
				ELSE 'JEWELLERY / BLOOM - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (12) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT DE LA LIVRAISON'
				ELSE 'SHIPPING FEES'
			END
		 WHEN invSec.Section_type_Id IN (13) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (CHANDELLES)'
				ELSE 'CANDLES - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (15) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (QSP PASSE D''ÉPARGNE)'
				ELSE 'DISCOUNT CARDS - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (16) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (CARTE CADEAU)'
				ELSE 'GIFT CARD - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (17) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (BÂTONNETS DE BRETZEL)'
				ELSE 'PRETZEL RODS - Gross Amount'
			END
		 WHEN invSec.Section_type_Id IN (18) THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (BÂTONNETS DE BRETZEL)'
				ELSE 'PRETZEL RODS - Gross Amount'
			END
	END AS GrossAmountLabel,
	CASE camp.LANG
		WHEN 'FR' THEN 'TOTAL DES TAXES (TPS) Included in Amount Due'
		ELSE 'TOTAL TAXES (GST) Included in Amount Due'
	END	AS TotalSecGSTLabel,
	CASE camp.LANG
		WHEN 'FR' THEN 'TOTAL DES TAXES (TVH) Included in Amount Due'
		ELSE 'TOTAL TAXES (HST) Included in Amount Due'
	END	AS TotalSecHSTLabel,
	CASE
		WHEN camp.LANG = 'FR' THEN 'TOTAL DES TAXES (TVQ) Included in Amount Due'
		WHEN camp.Lang = 'EN' AND adShip.StateProvince = 'QC' THEN 'TOTAL TAXES (QST) Included in Amount Due'
		ELSE 'TOTAL TAXES (PST) Included in Amount Due'
	END	AS TotalSecQSTLabel,
	CASE invSec.Section_Type_Id
		WHEN 2 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (MAGAZINES) EXCLUANT LE TARIF POSTAL, LES TAXES ET LES FRAIS DE TRAITEMENT'
				ELSE 'MAGAZINE GROSS AMOUNT w/o Postage, Taxes and processing fee'
			END
		
		WHEN 10 then 'POPCORN GROSS AMOUNT' 
		
		WHEN 12 then 'DELIVERY AMOUNT w/o Taxes' 
	END AS AdjustedGrossLabel,
	CASE invSec.Section_Type_Id
		WHEN 1 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS GIFT TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 2 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE MAGAZINES MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS MAGAZINES TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 6 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 9 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE PÂTE À BISCUITS MONTANT DU PROFIT DU GROUPE'
				ELSE 'LESS COOKIE DOUGH TOTAL GROUP PROFIT AMOUNT'
			END			
		WHEN 10 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE MAÏS SOUFFLÉ MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS POPCORN TOTAL GROUP PROFIT AMOUNT'
			END			
		WHEN 11 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE BIJOUX MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS JEWELLERY / BLOOM TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 13 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE CHANDELLES MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS CANDLES TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 14 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE MAGAZINES MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS MAGAZINES TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 15 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE CARTE D''ÉPARGNE MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS DISCOUNT CARDS TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 16 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LA CARTE CADEAU MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS GIFT CARD TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 17 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE BÂTONNETS DE BRETZEL MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS PRETZEL RODS TOTAL GROUP PROFIT AMOUNT'
			END
		WHEN 18 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'MOINS LE BÂTONNETS DE BRETZEL MONTANT TOTAL DU PROFIT DU GROUPE'
				ELSE 'LESS PRETZEL RODS TOTAL GROUP PROFIT AMOUNT'
			END
	END AS GroupProfitLabel,
	CASE invSec.Section_Type_Id
		WHEN 1 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Cadeaux/Bijoux Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Gift Gross Less Group Profit)'
			END
		WHEN 2 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Magazine avec des Taxes, Tarif Postal et Frais, Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Magazine Gross w/Taxes, Postage & Fees, Less Group Profit)'  
			END
		WHEN 3 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Approvisionnements De Champ)'
				ELSE 'AMOUNT DUE QSP (Field Supplies Gross)'
			END
		WHEN 6 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Gross Less Group Profit)'
			END
		WHEN 7 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Prix)'
				ELSE 'AMOUNT DUE QSP (Prizes Gross)'
			END
		WHEN 9 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Pâte à Biscuits Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Cookie Dough Gross Less Group Profit)'
			END			
		WHEN 10 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut maïs soufflé Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Popcorn Gross Less Group Profit)'
			END			
		WHEN 11 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Bijoux Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Jewellery  / Bloom Gross Less Group Profit)'
			END
		WHEN 12 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Livraison]'
				ELSE 'AMOUNT DUE QSP (Delivery Fee)'
			END			
		WHEN 13 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Chandelles Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Candles Gross Less Group Profit)'
			END
		WHEN 14 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut TRT Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (To Remember This Gross Less Group Profit)'
			END
		WHEN 15 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Carte d''épargne Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Discount Card Gross Less Group Profit)'
			END
		WHEN 16 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Carte Cadeau Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Gift Card Gross Less Group Profit)'
			END
		WHEN 17 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Bâtonnets de Bretzel Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Pretzel Rods Gross Less Group Profit)'
			END
		WHEN 18 THEN
			CASE camp.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP (Montant Brut Bâtonnets de Bretzel Moins le Bénéfice Du Groupe)'
				ELSE 'AMOUNT DUE QSP (Pretzel Rods Gross Less Group Profit)'
			END
	END AS AmountDueLabel,
	CASE camp.LANG
		WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Magazines, Cadeaux, Pâte à Biscuits, Chandelles, Livraison]'
		ELSE 'TOTAL AMOUNT DUE QSP [Magazine, Gift, Cookie Dough, Candles, Delivery Fee]'
	END	AS TotalAmountDueLabel,
	(CASE camp.LANG
		WHEN 'FR' THEN 'TOTAL DES TAXES'
		ELSE 'TOTAL TAXES'
	END	+
	CASE SIGN(IsNull(gst.tax_id,0)-1)
		WHEN 0 THEN 
			CASE camp.LANG 
				WHEN 'FR' THEN ' [TPS]'
				ELSE ' [GST]'
			END
		ELSE ''
	END + 
	CASE SIGN(IsNull(Hst.tax_id,0)-1)
		WHEN 1 THEN
			CASE camp.LANG 
				WHEN 'FR' THEN ' [TVH]'
				ELSE ' [HST]'
			END
		ELSE ''
	END) +
	CASE camp.LANG
		WHEN 'FR' THEN ' incluses dans le montant dû'
		ELSE ' Included in Amount Due'
	END AS TotalGSTInAmtDueLabel,
	(CASE camp.LANG
		WHEN 'FR' THEN 'TOTAL DES TAXES'
		ELSE 'TOTAL TAXES'
	END	+
	CASE adShip.StateProvince
		WHEN 'QC' THEN 
			CASE camp.LANG 
				WHEN 'FR' THEN ' [TVQ]'
				ELSE ' [QST]'
            END
		ELSE
			CASE camp.LANG 
				WHEN 'FR' THEN ' [TVP]'
				ELSE ' [PST]'
			END
	END +
	CASE camp.LANG
		WHEN 'FR' THEN ' incluses dans le montant dû'
		ELSE ' Included in Amount Due'
	END) AS TotalPSTInAmtDueLabel,
	CASE inv.Is_Printed	
		WHEN 'Y' THEN 
			CASE camp.LANG
				WHEN 'FR' THEN 'Duplicata'
				ELSE ' Duplicate'
			END
		WHEN 'N' THEN ''
	END AS DuplicateLabel,
	CASE invSec.Section_Type_Id
		WHEN 2 THen 0
		WHEN 1 THEN 1
		WHEN 6 THEN 2
		ELSE 9
	END	AS MySortOrder,
	ISNULL(invSec.US_Postage_Amount, 0.00) AS PostageAmount,
	CASE invSec.Section_Type_Id when 2 then
		CASE camp.Lang
			WHEN 'FR' THEN	'**TOTAL DU TARIF POSTAL PAYÉ À LA MAISON DE PRESSE'
			ELSE '**TOTAL POSTAGE PAID TO PUBLISHERS'
		END
		else ''
	END AS PostageAmountLabel,
	CASE camp.Lang
		WHEN 'FR' THEN '*FRAIS DE TRAITEMENT'
		ELSE '*TOTAL PROCESSING FEES'
	END AS ProcessingFeeLabel,
	CASE camp.Lang
		WHEN 'FR' THEN 'FRAIS DE LIVRAISON'
		ELSE 'SHIPPING FEES'
	END AS ShippingFeeLabel,
	CASE WHEN invSec.Section_Type_Id IN (2, 8, 12, 14) THEN 0 ELSE invSec.Section_Type_ID END AS SectionTypeGroupingID,
	0 AS PFEE_ItemCount, 
	0.00 AS PFEE_TotalWithoutTax,
	0.00 AS PFEE_TotalTax,
	0.00 AS PFEE_TotalGST,
	0.00 AS PFEE_TotalHST,
	0.00 AS PFEE_TotalPST,
	0 AS SFEE_ItemCount, 
	0.00 AS SFEE_TotalWithoutTax,
	0.00 AS SFEE_TotalTax,
	0.00 AS SFEE_TotalGST,
	0.00 AS SFEE_TotalHST,
	0.00 AS SFEE_TotalPST,
	CONVERT(varchar, dbo.UDF_GetInvoicePeriodBeginning(@InvoiceID),101) AS InvoicePeriodBeginning,
	CASE camp.Lang
		WHEN 'FR' THEN 'MONTANT DU PROFIT DU GROUPE - Magazines'
		ELSE 'GROUP PROFIT AMOUNT - Magazines'
	END AS GroupProfitSplitLabel,
	CASE camp.Lang
		WHEN 'FR' THEN 'Montant Du Profit Papier'
		ELSE 'Paper Order Profit'
	END AS GroupProfitSplitPaperLabel,
	CASE camp.Lang
		WHEN 'FR' THEN 'Montant Du Profit En Ligne'
		ELSE 'Online Profit'
	END AS GroupProfitSplitOnlineLabel
FROM		QSPCanadaOrderManagement..Batch b (NOLOCK)
LEFT JOIN	QSPCanadaCommon..CAccount acc (NOLOCK) ON b.AccountID = acc.Id
LEFT JOIN	QSPCanadaCommon..Phone ph (NOLOCK) ON acc.PhoneListID = ph.PhoneListid AND ph.type = 30505 --Main
LEFT JOIN	QSPCanadaCommon..AddressList adList ON acc.AddressListID = adList.ID
LEFT JOIN	QSPCanadaCommon..Address adShip ON adList.ID = adShip.AddressListID AND adShip.address_type = 54002
JOIN		QSPCanadaCommon..Campaign camp (NOLOCK) ON camp.Id = b.CampaignId
LEFT JOIN	QSPCanadaCommon..Contact cont ON cont.ID = camp.BillToCampaignContactID
JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK) ON fm.FMID = camp.FMID
JOIN		QSPCanadaFinance..Invoice inv (NOLOCK) ON inv.Order_Id = b.OrderId
JOIN		QSPCanadaFinance..Invoice_Section invSec (NOLOCK) ON invSec.Invoice_Id = inv.Invoice_Id
JOIN		QSPCanadaProduct..ProgramSectionType progSecType (NOLOCK) ON progSecType.ID = invSec.Section_Type_ID
LEFT JOIN	(SELECT 1 Tax_ID, ist.INVOICE_SECTION_ID, AVG(ISNULL(Tax_Rate, 0.000)) Tax_Rate, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax ist (NOLOCK) WHERE ist.Tax_ID = 1 GROUP BY ist.INVOICE_SECTION_ID) AS GST ON invSec.Invoice_Section_ID = GST.Invoice_Section_ID
LEFT JOIN	(SELECT 3 Tax_ID, ist.INVOICE_SECTION_ID, AVG(ISNULL(Tax_Rate, 0.000)) Tax_Rate, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax ist (NOLOCK) WHERE ist.Tax_ID NOT IN (1,2,4,5,6,7) GROUP BY ist.INVOICE_SECTION_ID) AS PST ON invSec.Invoice_Section_ID = PST.Invoice_Section_ID
LEFT JOIN	(SELECT 6 Tax_ID, ist.INVOICE_SECTION_ID, AVG(ISNULL(Tax_Rate, 0.000)) Tax_Rate, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax ist (NOLOCK) WHERE ist.Tax_ID IN (2,4,5,6,7) GROUP BY ist.INVOICE_SECTION_ID) AS HST ON invSec.Invoice_Section_ID = HST.Invoice_Section_ID				
LEFT JOIN	QSPCanadaCommon..CAccount accSchool ON accSchool.Id = QSPCanadaOrderManagement.dbo.UDF_GetSchoolAccountIDFromOrderInFMAccount(b.OrderID)
WHERE		(inv.Invoice_ID = @InvoiceID)
AND			invSec.Section_Type_ID NOT IN (8, 12) --Processing Fee, Shipping Fee
GROUP BY	invSec.Section_Type_Id,
			GST.Tax_Id,
			PST.Tax_Id,
			HST.Tax_Id,
			invSec.Total_Tax_Amount,
			invSec.Total_Tax_Included, 
			invSec.Total_Tax_Excluded, 
			invSec.Item_Count,
			invSec.Group_Profit_Rate, 
			invSec.Group_Profit_Amount, 
			invSec.Total_Taxable_Amount, 
			invSec.Net_Before_Tax,
			invSec.Total_Tax_Amount, 
			invSec.Due_amount,
			camp.Lang,
			camp.IsStaffOrder,  			
			b.OrderQualifierID, 
			b.OrderTypeCode, 
			inv.Invoice_ID, 
			ISNULL(accSchool.ID, acc.ID), 
			b.CampaignID, 
			inv.Order_Id, 
			camp.FMID, 
			fm.FirstName + ' ' + FM.LastName , 
			cont.FirstName + ' ' + cont.LastName,
			CONVERT(varchar, inv.Invoice_Date,101) , 
			CONVERT(varchar, inv.Invoice_Due_Date,101) , 
			ISNULL(accSchool.Name, acc.Name),
			adShip.Street1 		,
			adShip.Street2 		,
			adShip.City      	,
			adShip.StateProvince	,
			adShip.Postal_Code      ,
			adShip.Zip4		,
			inv.Is_Printed,
			GST.tax_rate,
			PST.tax_rate,
			HST.tax_rate,
			ph.phoneNumber,
			GST.TAX_AMOUNT,
			HST.TAX_AMOUNT,
			PST.TAX_AMOUNT,
			invSec.US_Postage_Amount
ORDER BY	MySortOrder

UPDATE		i
SET			PFEE_ItemCount = iSecPFee.ITEM_COUNT,
			PFEE_TotalWithoutTax = iSecPFee.TOTAL_TAX_EXCLUDED,
			PFEE_TotalTax = iSecPFee.TOTAL_TAX_AMOUNT,
			PFEE_GST = GSTPFee.Tax_Amount,
			PFEE_HST = HSTPFee.Tax_Amount,
			PFEE_PST = PSTPFee.Tax_Amount
FROM		#InvoiceTotals i
JOIN		QSPCanadaFinance..Invoice_Section iSecPFee (NOLOCK)
				ON	iSecPFee.Invoice_ID = i.Invoice_ID
LEFT JOIN	(SELECT 1 Tax_ID, istPFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istPFee (NOLOCK) WHERE istPFee.Tax_ID = 1 GROUP BY istPFee.INVOICE_SECTION_ID) AS GSTPFee ON iSecPFee.Invoice_Section_ID = GSTPFee.Invoice_Section_ID
LEFT JOIN	(SELECT 3 Tax_ID, istPFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istPFee (NOLOCK) WHERE istPFee.Tax_ID NOT IN (1,2,4,5,6,7) GROUP BY istPFee.INVOICE_SECTION_ID) AS PSTPFee ON iSecPFee.Invoice_Section_ID = PSTPFee.Invoice_Section_ID
LEFT JOIN	(SELECT 6 Tax_ID, istPFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istPFee (NOLOCK) WHERE istPFee.Tax_ID IN (2,4,5,6,7) GROUP BY istPFee.INVOICE_SECTION_ID) AS HSTPFee ON iSecPFee.Invoice_Section_ID = HSTPFee.Invoice_Section_ID
WHERE		iSecPFee.SECTION_TYPE_ID = 8 --PFee
AND			i.Section_Type_ID = 2 --Mag

UPDATE		i
SET			SFEE_ItemCount = iSecSFee.ITEM_COUNT,
			SFEE_TotalWithoutTax = iSecSFee.TOTAL_TAX_EXCLUDED,
			SFEE_TotalTax = iSecSFee.TOTAL_TAX_AMOUNT,
			SFEE_GST = GSTSFee.Tax_Amount,
			SFEE_HST = HSTSFee.Tax_Amount,
			SFEE_PST = PSTSFee.Tax_Amount
FROM		#InvoiceTotals i
JOIN		QSPCanadaFinance..Invoice_Section iSecSFee (NOLOCK)
				ON	iSecSFee.Invoice_ID = i.Invoice_ID
LEFT JOIN	(SELECT 1 Tax_ID, istSFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istSFee (NOLOCK) WHERE istSFee.Tax_ID = 1 GROUP BY istSFee.INVOICE_SECTION_ID) AS GSTSFee ON iSecSFee.Invoice_Section_ID = GSTSFee.Invoice_Section_ID
LEFT JOIN	(SELECT 3 Tax_ID, istSFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istSFee (NOLOCK) WHERE istSFee.Tax_ID NOT IN (1,2,4,5,6,7) GROUP BY istSFee.INVOICE_SECTION_ID) AS PSTSFee ON iSecSFee.Invoice_Section_ID = PSTSFee.Invoice_Section_ID
LEFT JOIN	(SELECT 6 Tax_ID, istSFee.INVOICE_SECTION_ID, SUM(ISNULL(Tax_Amount, 0.00)) Tax_Amount FROM QSPCanadaFinance.dbo.Invoice_Section_Tax istSFee (NOLOCK) WHERE istSFee.Tax_ID IN (2,4,5,6,7) GROUP BY istSFee.INVOICE_SECTION_ID) AS HSTSFee ON iSecSFee.Invoice_Section_ID = HSTSFee.Invoice_Section_ID
WHERE		iSecSFee.SECTION_TYPE_ID = 12 --SFee
AND			i.Section_Type_ID = 17 --$2 Pretzel Rods

UPDATE	#InvoiceTotals
SET		Group_Profit_Amount = Group_Profit_Amount - Total_Tax_Amount - SFEE_TotalWithoutTax - SFEE_TotalTax,
		Total_Tax_Included = Total_Tax_Included - Total_Tax_Amount - SFEE_TotalWithoutTax - SFEE_TotalTax
WHERE	Section_Type_Id = 17
AND		Group_Profit_Amount > 0

SELECT * FROM #InvoiceTotals

SET NOCOUNT OFF
GO
