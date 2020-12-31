USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceSectionInfo]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoiceSectionInfo]
	@InvoiceID 	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/4/2004 
--   Get Invoice Section numbers For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT C.Lang, 
	section_type_id, 
	CASE section_type_id
		WHEN 1 THEN (CASE C.Lang
					WHEN 'EN' Then 'Gift'   --Inventory Products
					WHEN 'FR' Then 'Cadeau'   --French
					ELSE 'Inventory Products'			
				   END) 	
		WHEN 2 THEN (CASE C.Lang
					WHEN 'EN' Then 'Magazine'   --Magazine
					WHEN 'FR' Then 'Magazine'   --French
					ELSE 'Magazine'			
				   END) 	
		WHEN 3 THEN (CASE C.Lang
					WHEN 'EN' Then 'Field Supplies'   --Field Supplies
					WHEN 'FR' Then 'Approvisionnements De Champ'   --French
					ELSE 'Field Supplies'			
				   END)
		WHEN 4 THEN (CASE C.Lang
					WHEN 'EN' Then 'Incentives'   --Incentives
					WHEN 'FR' Then 'Incitations'   --French
					ELSE 'Incentives'			
				   END)
		WHEN 5 THEN (CASE C.Lang
					WHEN 'EN' Then 'Misc'   --Misc
					WHEN 'FR' Then 'Misc'   --French
					ELSE 'Misc'			
				   END)
		WHEN 6 THEN (CASE C.Lang
					WHEN 'EN' Then 'Inventory products without tax'   --Inventory products without tax
					WHEN 'FR' Then 'Produits en stock (taxe de vente non comprise)'   --French
					ELSE 'Inventory products without tax'			
				   END)
		ELSE ''
	END AS Description,
	--LTRIM(RTRIM(SUBSTRING(PST.Description, CHARINDEX('-',PST.Description)+1 , LEN(PST.Description)))) as Description,
	total_tax_included, total_tax_excluded, 
	group_profit_rate, group_profit_amount, total_taxable_amount, net_before_tax,
	total_tax_amount, due_amount --,ISecTax.Tax_Rate, Tax_Desc, Tax_Amount	
FROM Invoice_Section ISec
INNER JOIN Invoice I on I.Invoice_Id = ISec.Invoice_Id
INNER JOIN QSPCanadaOrderManagement..Batch B on B.OrderID = I.Order_ID
INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
--INNER JOIN Invoice_Section_Tax ISecTax on ISec.Invoice_Section_ID = ISecTax.Invoice_Section_ID
--INNER JOIN QSPCanadaCommon..Tax Tax on ISecTax.Tax_ID = Tax.Tax_Id
INNER JOIN QSPCanadaProduct..ProgramSectionType PST on PST.ID = ISec.Section_Type_ID
WHERE ISec.Invoice_ID = @InvoiceID

SET NOCOUNT OFF
GO
