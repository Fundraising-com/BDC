USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceSectionInfo_2]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInvoiceSectionInfo_2]
	@InvoiceID 	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/4/2004 
--   Get Invoice Section numbers For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT QSP_Product_Line_ID, section_type_id, PST.Description, total_tax_included, total_tax_excluded, 
	group_profit_rate, group_profit_amount, total_taxable_amount, net_before_tax,
	total_tax_amount, due_amount ,ISecTax.Tax_Rate, Tax_Desc, Tax_Amount
FROM Invoice_Section ISec
INNER JOIN Invoice_By_QSP_Product IProd on IProd.Invoice_Id = ISec.Invoice_ID
INNER JOIN Invoice_Section_Tax ISecTax on ISec.Invoice_Section_ID = ISecTax.Invoice_Section_ID
INNER JOIN QSPCanadaCommon..Tax Tax on ISecTax.Tax_ID = Tax.Tax_Id
INNER JOIN QSPCanadaProduct..ProgramSectionType PST on PST.ID = ISec.Section_Type_ID
WHERE ISec.Invoice_ID = @InvoiceID
AND QSP_Product_Line_ID IN (46001, 46002, 46003, 46005,  46006, 46007, 46010, 46012) --Magazine, Gift , WFC, Food, Book, Music, MMB, Video

SET NOCOUNT OFF
GO
