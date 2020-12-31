USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddInvoiceByProduct]    Script Date: 06/07/2017 09:17:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddInvoiceByProduct]
	@InvoiceID		int,
	@ProductType		int,
	@Net			numeric(10,2)
	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/12/2004 
--   Insert an Invoice By Product Record(s) with Net Amount(s) For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

INSERT QSPCanadaFinance..INVOICE_BY_QSP_PRODUCT
			(INVOICE_ID,
			QSP_PRODUCT_LINE_ID,
			PRODUCT_AMOUNT
			)
VALUES(@InvoiceID,
	@ProductType,
	@Net
	)


SET NOCOUNT OFF
GO
