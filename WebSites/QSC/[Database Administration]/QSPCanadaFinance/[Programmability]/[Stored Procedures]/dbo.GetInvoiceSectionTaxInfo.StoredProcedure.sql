USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceSectionTaxInfo]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoiceSectionTaxInfo]
	@InvoiceID 	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/8/2004 
--   Get Invoice Section Tax numbers For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT -- ISecTax.Tax_Rate, Tax_Desc, Tax_Registration, SUM(Tax_Amount) Tax_Amount
Case I.Invoice_ID
	When 73269 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	
	When 73270 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	
	When 73278 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73283 Then 
			Case Tax.tax_id
			 When 2 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73312 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73313 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73281 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73280 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73279 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73275 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73332 Then 
			Case Tax.tax_id
			 When 2 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73333 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73332 Then 
			Case Tax.tax_id
			 When 2 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73273 Then 
			Case Tax.tax_id
			 When 2 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73272 Then 
			Case Tax.tax_id
			 When 2 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73283 Then 
			Case Tax.tax_id
			 When 2 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	When 73300 Then 
			Case Tax.tax_id
			 When 1 Then Tax.tax_rate+1
			Else Tax.tax_rate
			End
	Else Tax.tax_rate
  End Tax_Rate, 
 Tax_Desc, 
 Tax_Registration, 
 SUM(Tax_Amount) Tax_Amount
FROM QSPCanadaOrdermanagement..Batch B
INNER JOIN Invoice I on B.OrderId=I.Order_Id
INNER JOIN Invoice_Section ISec on I.Invoice_Id=ISec.Invoice_Id 
INNER JOIN  Invoice_Section_Tax ISecTax on ISec.Invoice_Section_ID = ISecTax.Invoice_Section_ID
INNER JOIN QSPCanadaCommon..Tax Tax on ISecTax.Tax_ID = Tax.Tax_Id
--Disable: MS Aug03, Tax clac for older order still at 7% Issue#641
/*FROM Invoice_Section ISec
INNER JOIN  Invoice_Section_Tax ISecTax on ISec.Invoice_Section_ID = ISecTax.Invoice_Section_ID
INNER JOIN QSPCanadaCommon..Tax Tax on ISecTax.Tax_ID = Tax.Tax_Id
*/
WHERE I.Invoice_ID = @InvoiceID
GROUP BY ISecTax.Tax_Rate, Tax_Desc, Tax_Registration,
I.Invoice_ID,Tax.tax_rate,Tax.tax_id

SET NOCOUNT OFF
GO
