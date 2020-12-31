USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_INVOICE_SelectAll]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'INVOICE'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_INVOICE_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[INVOICE_ID],
	[ACCOUNT_ID],
	[ACCOUNT_TYPE_ID],
	[ORDER_ID],
	[INVOICE_DATE],
	[INVOICE_DUE_DATE],
	[INVOICE_AMOUNT],
	[FIRST_PRINT_DATE],
	[NOTE_TO_PRINT],
	[DATETIME_CREATED],
	[DATETIME_MODIFIED],
	[LAST_UPDATED_BY],
	[COUNTRY_CODE],
	[IS_PRINTED],
	[DATETIME_APPROVED],
	[INVOICE_EFFECTIVE_DATE]
FROM qspcanadafinance..[INVOICE]
ORDER BY 
	[INVOICE_ID] ASC
GO
