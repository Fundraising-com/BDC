USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddInvoiceSectionTax]    Script Date: 06/07/2017 09:17:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddInvoiceSectionTax]
	@InvoiceSectionID	int,
	@TaxID			int,
	@TotalTaxableAmount	numeric(10,2),
	@TaxRate		numeric(10,3),
	@Tax			numeric(10,2),
	@UserID		varchar(30)
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/10/2004 
--   Insert an Invoice Section Tax Record For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

INSERT QSPCanadaFinance..Invoice_Section_Tax
			(INVOICE_SECTION_ID,
			TAX_ID,
			TAXABLE_AMOUNT,
			TAX_RATE,
			TAX_AMOUNT,
			DateCreated,
			CreatedBy
			)
VALUES(@InvoiceSectionID,
	@TaxID,
	@TotalTaxableAmount,
	@TaxRate,
	@Tax, 
	GETDATE(),
	@UserID	
	)

SET NOCOUNT OFF
GO
