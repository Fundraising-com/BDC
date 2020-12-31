USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceTotalsInfo_withProcessingFee]    Script Date: 06/07/2017 09:17:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInvoiceTotalsInfo_withProcessingFee]  
	@InvoiceID 	int
 AS
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/4/2004 
--   Get Invoice Totals For Canada Finance System
--   Re- written April 6, 2006 BY MS
--	 CRL 8/2/2011
--	 Inject processing fee into first invoice section (usually mag) when present
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SET NOCOUNT ON

--DECLARE @InvoiceID 	int
--SET @InvoiceID  = 269399 -- 484302


DECLARE @TotalProcessingFees decimal(10,2)
DECLARE @ProcessingFeeDueAmount decimal(10,2)

/* Make sure the temp table is not left over from a previous run */
IF OBJECT_ID('tempdb..#InvoiceTotals') IS NOT NULL
	DROP TABLE #InvoiceTotals

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
	ContactName varchar(50),
	Invoice_Date varchar(10), 
	Invoice_Due_Date varchar(10), 
	AcctName varchar(50),
	ShippingAddress varchar(50),
	ShippingAddress2 varchar(50),
	ShippingCity varchar(50),
	ShippingState varchar(10),
	ShippingZip varchar(7),
	ShippingZip4 varchar(4),
	Is_Printed bit,
	ContactId int,
	Lang varchar(10), 
	Section_Type_Id int, 
	Description varchar(100),
	Total_Tax_Included numeric(10,2), 
	Total_Tax_Excluded numeric(10,2), 
	Item_Count int,
	Group_Profit_Rate int, 
	Group_Profit_Amount numeric(10,2),
	Total_Taxable_Amount numeric(10,2), 
	Net_Before_Tax numeric(10,2),
	GST int,
	GST_Rate numeric(10,2),
	GST_Total numeric(10,2),
	PST int,
	PST_Rate numeric(10,2),
	PST_Total numeric(10,2),
	HST int,
	HST_Rate numeric(10,2),
	HST_Total numeric(10,2),
	Total_Tax_Amount numeric(10,2), 
	Due_Amount numeric(10,2),
	MagSalesTax numeric(10,2),
	MagSalesTaxrate int,
	GrossAmountLabel varchar(100),
	TotalMagSecTaxLabel varchar(100),
	TotalMagSecTaxQSTLabel varchar(100),
	MagGrossAmtWoutTaxLabel varchar(100),
	GroupProfitLabel varchar(100),
	AmountDueLabel varchar(100),
	TotalAmountDueLabel varchar(100),
	TotalGSTInAmtDueLabel varchar(100),
	TotalPSTInAmtDueLabel varchar(100),
	DuplicateLabel varchar(100),
	ApplicableProgLabel varchar(500),	
	MySortOrder int,
	PostageAmount numeric(10,2),
	PostageAmountLabel varchar(100),
	ProcessingFeeLabel varchar(100),
	ProcessingFeeAmount numeric(10,2),
	MagazineOrderPaper int,
	RegularMagazineProfitAmount numeric(10,2),
	OnlineProfitAmount numeric(10,2),
	ProcessingFeesCount int
)

-- Get Processing Fee totals for invoice
-- CHANGE SECTION_TYPE_ID if 8 is not the production value assigned to the processing fee catalog section
SELECT 
	@TotalProcessingFees = SUM(TOTAL_TAX_INCLUDED),
	@ProcessingFeeDueAmount = SUM(Due_Amount)
FROM 
	QSPCanadaFinance..Invoice_Section
WHERE
	SECTION_TYPE_ID = 8 AND
	INVOICE_ID = @InvoiceID

INSERT INTO #InvoiceTotals
SELECT
	Campaign.IsStaffOrder, 
	Batch.OrderQualifierID, 
	Batch.OrderTypeCode, 
	Invoice.Invoice_ID, 
	Account.ID AS AcctID, 
	QSPCanadaFinance.dbo.UDF_CleanPhoneNumber(Phone.phoneNumber,'-') AS phoneNumber,
	Batch.CampaignID, 
	Invoice.Order_Id, 
	Campaign.FMID, 
	FM.FirstName + ' ' + FM.LastName AS FMName, 
	Contact.FirstName + ' ' + Contact.LastName AS ContactName,
	CONVERT(varchar, Invoice.Invoice_Date,101) AS Invoice_Date, 
	CONVERT(varchar, Invoice.Invoice_Due_Date,101) AS Invoice_Due_Date, 
	Account.Name AS AcctName,
	ShipAddress.Street1 AS ShippingAddress,
	ShipAddress.Street2 AS ShippingAddress2,
	ShipAddress.City AS ShippingCity,
	ShipAddress.StateProvince AS ShippingState,
	ShipAddress.Postal_Code AS ShippingZip,
	ShipAddress.Zip4 AS ShippingZip4,
	CASE Invoice.Is_Printed	
		WHEN 'Y' THEN 1
		WHEN 'N' THEN 0
		ELSE 0
	END AS Is_Printed,
	MAX(Contact.Id) AS ContactId,
	Campaign.Lang, 
	Invoice_Section.Section_Type_Id, 
	CASE Invoice_Section.Section_Type_Id
		WHEN 1 THEN (CASE Campaign.Lang
					WHEN 'EN' Then 'Gift'   --Inventory Products
					WHEN 'FR' Then 'Cadeaux'   --French
					ELSE 'Inventory Products'			
				   END) 	
		WHEN 2 THEN (CASE Campaign.Lang
					WHEN 'EN' Then 'Magazine'   --Magazine
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	
		WHEN 3 THEN (CASE Campaign.Lang
					WHEN 'EN' Then 'Field Supplies'   --Field Supplies
					WHEN 'FR' Then 'Approvisionnements De Champ'   --French
					ELSE 'Field Supplies'			
				   END)
		WHEN 4 THEN (CASE Campaign.Lang
					WHEN 'EN' Then 'Incentives'   --Incentives
					WHEN 'FR' Then 'Incitations'   --French
					ELSE 'Incentives'			
				   END)
		WHEN 5 THEN (CASE Campaign.Lang
					WHEN 'EN' Then 'Misc'   --Misc
					WHEN 'FR' Then 'Misc'   --French
					ELSE 'Misc'			
				   END)
		WHEN 6 THEN (CASE Campaign.Lang
					WHEN 'EN' Then 'Inventory products without tax'   --Inventory products without tax
					WHEN 'FR' Then 'Produits en stock (taxe de vente non comprise)'   --French
					ELSE 'Inventory products without tax'			
				   END)
		ELSE ''
	END AS Description,
	Invoice_Section.Total_Tax_Included, 
	Invoice_Section.Total_Tax_Excluded, 
	Invoice_Section.Item_Count,
	Invoice_Section.Group_Profit_Rate, 
	Invoice_Section.Group_Profit_Amount,
	Invoice_Section.Total_Taxable_Amount, 
	Invoice_Section.Net_Before_Tax,
	ISNULL(GST.tax_id,0) AS GST,
	ISNULL(GST.tax_rate,0) AS GST_Rate,
	ISNULL((GST.tax_amount),0) AS GST_Total,
	ISNULL(PST.tax_id,0) PST,
	ISNULL(PST.tax_rate,0) PST_Rate,
	ISNULL((PST.tax_amount),0)  PST_Total,
	ISNULL(HST.tax_id,0) HST,
	ISNULL(HST.tax_rate,0) HST_Rate,
	ISNULL((HST.tax_amount),0)  HST_Total,
	ISNULL(Invoice_Section.total_tax_amount,0) Total_Tax_Amount, 
	Invoice_Section.Due_Amount,
	CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN 
			CASE ISNULL(GST.tax_id,0)
				WHEN 0 THEN 
					CASE ISNULL(HST.tax_id,0)
						WHEN 0 THEN (PST.tax_Amount)
						ELSE (HST.tax_Amount)
					END
				ELSE (GST.tax_amount)
			END
		ELSE 0
	END AS MagSalesTax,
	CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN 
			CASE ISNULL(GST.tax_id,0)
				WHEN 0 THEN 
					CASE ISNULL(HST.tax_id,0)
						WHEN 0 THEN CAST(PST.tax_rate AS INT)
						ELSE CAST (HST.tax_rate AS INT)
					END
				ELSE CAST (GST.tax_rate AS INT)
			END
		Else 0
	End AS MagSalesTaxrate,
	---------------------------------------------------------------Labels----------------------------------------
	CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN
			CASE ISNULL(Campaign.IsStaffOrder ,0)
				WHEN 1 THEN	
					CASE Campaign.LANG
						WHEN 'FR' THEN 'MONTANT BRUT (MAGAZINES) moins les remises du Personnel *'
						ELSE 'MAGAZINE GROSS AMOUNT'
					END  
				WHEN 0 THEN	
					CASE Campaign.LANG
						WHEN 'FR' THEN 
							CASE WHEN @TotalProcessingFees = 0 then 'MONTANT BRUT (MAGAZINES) INCLUANT LE TARIF POSTAL ET LES TAXES*'
							     ELSE 'MONTANT BRUT (MAGAZINES) INCLUANT LE TARIF POSTAL, LES TAXES ET LES FRAIS DE TRAITEMENT*' /* Can be inserted here since magazine section is always first */
							END
						ELSE CASE WHEN @TotalProcessingFees = 0 then  'MAGAZINE GROSS AMOUNT WITH POSTAGE AND TAXES*'
								  ELSE 'MAGAZINE GROSS AMOUNT WITH POSTAGE, TAXES AND PROCESSING FEE' /* Can be inserted here since magazine section is always first */
							 END
					END 
			END
		WHEN 1 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (CADEAUX)'
				ELSE 'GIFT GROSS AMOUNT'
			END
		WHEN 6 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'MONTANT BRUT'
				ELSE 'GROSS AMOUNT'
			END
		WHEN 7 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'MONTANT BRUT (KANATA)'
				ELSE 'KANATA GROSS AMOUNT'
			END
	END AS GrossAmountLabel,
	(CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'TOTAL DES TAXES'
				ELSE 'TOTAL TAXES'
			END	
	END +
	CASE SIGN(IsNull(gst.tax_id,0)-1)
		WHEN 0 THEN 
		    CASE Campaign.LANG 
				WHEN 'FR' THEN ' [TPS]'
				ELSE ' [GST]'
            END
		ELSE ''
	END) AS	TotalMagSecTaxLabel,
	(CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'TOTAL DES TAXES'
				ELSE 'TOTAL TAXES'
			END	
	END+
	CASE SIGN(IsNull(Pst.tax_id,0)-1)
		WHEN 1 THEN
			CASE Campaign.LANG 
				WHEN 'FR' THEN ' [TVQ]'
				ELSE ' [QST]'
            END 
		ELSE ''
	END) AS TotalMagSecTaxQSTLabel,
	(CASE Invoice_Section.Section_Type_Id
	WHEN 2 THEN
		CASE Campaign.LANG
			WHEN 'FR' THEN 
				CASE WHEN @TotalProcessingFees = 0 THEN 'MONTANT BRUT (MAGAZINES) EXCLUANT LE TARIF POSTAL ET LES TAXES'
					 ELSE 'MONTANT BRUT (MAGAZINES) EXCLUANT LE TARIF POSTAL, LES TAXES ET LES FRAIS DE TRAITEMENT'
				END
			ELSE 
				CASE WHEN @TotalProcessingFees = 0 THEN 'MAGAZINE GROSS AMOUNT w/o Postage and Taxes'
					 ELSE 'MAGAZINE GROSS AMOUNT w/o Postage, Taxes and processing fee'
				END
		END
	END +
	CASE SIGN(IsNull(gst.tax_id,0)-1)
		WHEN 0 THEN 
			CASE  SIGN(IsNull(Pst.tax_id,0)-1)
				WHEN 1 THEN 
					CASE Campaign.LANG 
						WHEN 'FR' THEN ' [TPS/TVQ]'
						ELSE ' [GST/QST]'
					END 
				ELSE
					CASE Campaign.LANG 
						WHEN 'FR' THEN ' [TPS]'
						ELSE 	''
					END
			END
		ELSE
            CASE Campaign.LANG 
				WHEN 'FR' THEN ''
				ELSE 	''
            END
	END) AS MAgGrossAmtWoutTaxLabel,
	CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN
			CASE ISNULL(Campaign.IsStaffOrder ,0)
				WHEN 1 THEN	
					CASE Campaign.LANG
						WHEN 'FR' THEN 'MONTANT DE LA REMISE [basé sur le montant brut (Magazines) excluant les taxes]'
						ELSE 'REBATE AMOUNT (Adjusted)'
					END
				ELSE
					CASE Campaign.LANG
						WHEN 'FR' THEN 'MONTANT DU PROFIT DU GROUPE [basé sur le montant brut (Magazines) excluant les taxes]'
						ELSE 'GROUP PROFIT AMOUNT'
					END
			END		
		WHEN 1 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'MONTANT DU PROFIT DU GROUPE [basé sur le montant brut (Cadeaux)]'
				ELSE 'GROUP PROFIT AMOUNT [Based on Gift Gross Amount]'
			END
		WHEN 6 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'MONTANT DU PROFIT DU GROUPE [basé sur le montant brut ]'
				ELSE 'GROUP PROFIT AMOUNT [Based on Gross Amount]'
			END
	END AS GroupProfitLabel,
	CASE Invoice_Section.Section_Type_Id
		WHEN 2 THEN
			CASE ISNULL(Campaign.IsStaffOrder ,0)
				WHEN 0 THEN	
					CASE Campaign.LANG
						WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Montant brut (Magazines) incluant les taxes moins le bénéfice du groupe] - Commandes papier'
						ELSE 'AMOUNT DUE QSP [Magazine Gross w/Taxes Less Group Profit] - Paper Orders'  
					END
			ELSE
				CASE Campaign.LANG			
					WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Montant brut (Magazines) incluant les taxes moins la remise]'
					ELSE 'AMOUNT DUE QSP [Magazine Gross w/Taxes Less Rebate]' --MS Feb20, 2007
				END
			END
		WHEN 1 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Montant brut (Cadeaux) moins le bénéfice du groupe]'
				ELSE 'AMOUNT DUE QSP [Gift Gross Less Group Profit]'
			END
		WHEN 6 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Montant brut (Douceurs Exquises) moins le bénéfice du groupe]'
				ELSE 'AMOUNT DUE QSP [Gross Less Group Profit]'
			END
		WHEN 7 THEN
			CASE Campaign.LANG
				WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Montant brut (Kanata) moins le bénéfice du groupe]'
				ELSE 'AMOUNT DUE QSP [Kanata Gross Less Group Profit]'
			END
	END AS AmountDueLabel,
	CASE Campaign.LANG
		WHEN 'FR' THEN 'SOLDE À PAYER À QSP [Magazines, Cadeaux, Douceurs Exquises]'
		ELSE 'TOTAL AMOUNT DUE QSP [Magazine, Gift & Sweet Sensations]'
	END	AS TotalAmountDueLabel,
	(CASE Campaign.LANG
		WHEN 'FR' THEN 'TOTAL DES TAXES'
		ELSE 'TOTAL TAXES'
	END	+
	CASE SIGN(IsNull(gst.tax_id,0)-1)
		WHEN 0 THEN 
			CASE Campaign.LANG 
				WHEN 'FR' THEN ' [TPS]'
				ELSE ' [GST]'
			END
		ELSE ''
	END + 
	CASE SIGN(IsNull(Hst.tax_id,0)-1)
		WHEN 1 THEN
			CASE Campaign.LANG 
				WHEN 'FR' THEN ' [TVH]'
				ELSE ' [HST]'
			END
		ELSE ''
	END) +
	CASE Campaign.LANG
		WHEN 'FR' THEN ' incluses dans le montant dû'
		ELSE ' Included in Amount Due'
	END AS TotalGSTInAmtDueLabel,
	(CASE Campaign.LANG
		WHEN 'FR' THEN 'TOTAL DES TAXES'
		ELSE 'TOTAL TAXES'
	END	+
	CASE ShipAddress.StateProvince
		WHEN 'QC' THEN 
			CASE Campaign.LANG 
				WHEN 'FR' THEN ' [TVQ]'
				ELSE ' [QST]'
            END
		ELSE
			CASE Campaign.LANG 
				WHEN 'FR' THEN ' [TVP]'
				ELSE ' [PST]'
			END
	END +
	CASE Campaign.LANG
		WHEN 'FR' THEN ' incluses dans le montant dû'
		ELSE ' Included in Amount Due'
	END) AS TotalPSTInAmtDueLabel,
	CASE Invoice.Is_Printed	
		WHEN 'Y' THEN 
			CASE Campaign.LANG
				WHEN 'FR' THEN 'Duplicata'
				ELSE ' Duplicate'
			END
		WHEN 'N' THEN ''
	END AS DuplicateLabel,
	--- Only Applicable Prog Label
	QSPCanadaFinance.dbo.UDF_ListInvoiceSectionbyLang(@InvoiceID,Campaign.Lang) AS ApplicableProgLabel,	
	CASE Invoice_Section.Section_Type_Id
		WHEN 2 THen 0
		WHEN 1 THEN 1
		WHEN 6 THEN 2
		ELSE 9
	END	AS MySortOrder,
	ISNULL(Invoice_Section.US_Postage_Amount, 0.00) AS PostageAmount,
	CASE Campaign.Lang
		WHEN 'FR' THEN	'TOTAL DU TARIF POSTAL PAYÉ À LA MAISON DE PRESSE'
		ELSE 'TOTAL POSTAGE PAID TO PUBLISHERS**'
	END AS PostageAmountLabel,
	CASE Campaign.Lang
		WHEN 'FR' THEN 'FRAIS DE TRAITEMENT (1$ PAR COMMANDE)'
		ELSE 'TOTAL PROCESSING FEE ($1.00 PER ORDER FORM)'
	END AS ProcessingFeeLabel,
	0 as ProcessingFeeAmount,
	QSPCanadaFinance.dbo.UDF_GetRegularMagazineOrder(@InvoiceID, Invoice_Section.Section_Type_Id) AS MagazineOrderPaper,
	QSPCanadaFinance.dbo.UDF_GetRegularMagazineProfitAmount(@InvoiceID, Invoice_Section.Section_Type_Id) AS RegularMagazineProfitAmount,
	QSPCanadaFinance.dbo.UDF_GetOnlineProfitAmount(@InvoiceID, Invoice_Section.Section_Type_Id) AS OnlineProfitAmount,
	QSPCanadaFinance.dbo.UDF_GetProcessingFeeOrder(@InvoiceID, 8) AS ProcessingFeesCount
FROM    
	QSPcanadaOrdermanagement.dbo.Batch Batch (NOLOCK)
		LEFT  JOIN QSPCanadaCommon.dbo.CAccount Account 
			ON Batch.AccountID =Account.Id
		LEFT JOIN QSPCanadaCommon.dbo.Phone Phone (NOLOCK)
			ON Account.phonelistId = Phone.PhoneListid and Phone.type=30505  --Main
		LEFT  JOIN QSPCanadaCommon.dbo.AddressList AddressList 
			ON Account.AddressListID = AddressList.ID
		LEFT  JOIN QSPCanadaCommon.dbo.Address ShipAddress 
			ON AddressList.ID = ShipAddress.AddressListID AND ShipAddress.address_type = 54002, 
    QSPCanadaCommon.dbo.Campaign Campaign (NOLOCK)
		LEFT  JOIN QSPCanadaCommon.dbo.Contact Contact ON Contact.ID = Campaign.BillToCampaignContactID,
	QSPCanadaCommon.dbo.FieldManager FM ,
	QSPCanadaFinance.dbo.Invoice Invoice,
	QSPCanadaCommon..Tax Tax (NOLOCK),
	QSPCanadaProduct..ProgramSectionType ProgramSectionType	 (NOLOCK),
	QSPCanadaFinance.dbo.Invoice_Section Invoice_Section (NOLOCK)
     	LEFT  JOIN QSPCanadaFinance.dbo.Invoice_Section_Tax GST (NOLOCK) ON Invoice_Section.Invoice_Section_ID = GST.Invoice_Section_ID  AND GST.Tax_ID=1
     	LEFT  JOIN QSPCanadaFinance.dbo.Invoice_Section_Tax PST (NOLOCK) ON Invoice_Section.Invoice_Section_ID = PST.Invoice_Section_ID  AND PST.Tax_ID NOT IN (1,2,4,5,6,10)
     	LEFT  JOIN QSPCanadaFinance.dbo.Invoice_Section_Tax HST (NOLOCK) ON Invoice_Section.Invoice_Section_ID = HST.Invoice_Section_ID  AND HST.Tax_ID IN (2,4,5,6,10) 
WHERE 
	Batch.OrderId=Invoice.Order_Id AND 	
	Batch.CampaignId=Campaign.Id AND 	
	FM.FMID = Campaign.FMID AND	
	Invoice.Invoice_Id = Invoice_Section.Invoice_Id AND 	
	ProgramSectionType.ID = Invoice_Section.Section_Type_ID AND
	Invoice_Section.Section_Type_ID <> 8 AND --Exclude processing fee sections for now because we don't want it as a separate section
	Invoice_Section.Invoice_ID = @InvoiceID 
GROUP BY 
	Invoice_Section.Section_Type_Id,
	GST.Tax_Id,
	PST.Tax_Id,
	HST.Tax_Id,
	Invoice_Section.Total_Tax_Amount,
	Invoice_Section.Total_Tax_Included, 
	Invoice_Section.Total_Tax_Excluded, 
	Invoice_Section.Item_Count,
	Invoice_Section.Group_Profit_Rate, 
	Invoice_Section.Group_Profit_Amount, 
	Invoice_Section.Total_Taxable_Amount, 
	Invoice_Section.Net_Before_Tax,
	Invoice_Section.Total_Tax_Amount, 
	Invoice_Section.Due_amount,
	Campaign.Lang,
	Campaign.IsStaffOrder,  			
	Batch.OrderQualifierID, 
	Batch.OrderTypeCode, 
	Invoice.Invoice_ID, 
	Account.ID, 
	Batch.CampaignID, 
	Invoice.Order_Id, 
	Campaign.FMID, 
	FM.FirstName + ' ' + FM.LastName , 
	Contact.FirstName + ' ' + Contact.LastName,
	CONVERT(varchar, Invoice.Invoice_Date,101) , 
	CONVERT(varchar, Invoice.Invoice_Due_Date,101) , 
	Account.Name ,
	ShipAddress.Street1 		,
	ShipAddress.Street2 		,
	ShipAddress.City      	,
	ShipAddress.StateProvince	,
	ShipAddress.Postal_Code      ,
	ShipAddress.Zip4		,
	Invoice.Is_Printed,
	GST.tax_rate,
	PST.tax_rate,
	HST.tax_rate,
	Phone.phoneNumber,
	GST.TAX_AMOUNT,
	HST.TAX_AMOUNT,
	PST.TAX_AMOUNT,
	Invoice_Section.US_Postage_Amount
ORDER BY 
	MySortOrder

SET @TotalProcessingFees = ISNULL(@TotalProcessingFees, 0)

/* Add Processing fees to first section appearing on invoice. Will usually be magazines, but not always */
--SELECT * FROM #InvoiceTotals
--IF @TotalProcessingFees > 0
UPDATE 
	#InvoiceTotals
SET 
	Total_Tax_Included = Total_Tax_Included + @TotalProcessingFees,
	ProcessingFeeAmount = @TotalProcessingFees
WHERE
	MySortOrder = (SELECT MIN(MySortOrder) FROM #InvoiceTotals)


UPDATE 
	#InvoiceTotals
SET 
	Due_Amount = Total_Tax_Included - Group_Profit_Amount
WHERE
	MySortOrder = (SELECT MIN(MySortOrder) FROM #InvoiceTotals)



SELECT * FROM #InvoiceTotals

SET NOCOUNT OFF
GO
