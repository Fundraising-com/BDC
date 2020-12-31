USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[UpdateInvoicePrintedStatus]    Script Date: 06/07/2017 09:17:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateInvoicePrintedStatus]
	@InvoiceID 	varchar(30),
	@ChangedBy	varchar(30),
	@IsPrinted	char(10) = 'Y'
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/25/2004 
--   Update Is_Printed status after running print routine For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

UPDATE 
	Invoice
SET 
	Is_Printed = @IsPrinted, 
	Last_Updated_By = @ChangedBy,
	DateTime_Modified = getdate()
WHERE 
	Invoice_ID = @InvoiceID

SET NOCOUNT OFF
GO
