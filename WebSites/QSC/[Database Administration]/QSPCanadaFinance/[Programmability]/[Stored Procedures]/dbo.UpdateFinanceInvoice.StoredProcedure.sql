USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[UpdateFinanceInvoice]    Script Date: 06/07/2017 09:17:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateFinanceInvoice]
	@InvoiceID		int,
	@ChangedBy		int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/6/2004 
--   Update the Invoice Record Amount For Canada Finance System.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

UPDATE QSPCanadaFinance..Invoice
	SET 	Invoice_Amount 	= (SELECT SUM(ISNULL(Due_Amount,0))
				    	    FROM QSPCanadaFinance..Invoice_Section
				    	    WHERE Invoice_ID = @InvoiceID),
		Last_Updated_By 	= @ChangedBy,
		DateTime_Modified 	= GETDATE()		
WHERE Invoice_Id = @InvoiceID

SET NOCOUNT OFF
GO
