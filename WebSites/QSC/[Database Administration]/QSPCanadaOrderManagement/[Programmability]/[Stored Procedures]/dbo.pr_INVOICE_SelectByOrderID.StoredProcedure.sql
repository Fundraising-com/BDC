USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_INVOICE_SelectByOrderID]    Script Date: 06/07/2017 09:20:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'INVOICE'
-- based on the Primary Key.
-- Gets: @iINVOICE_ID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_INVOICE_SelectByOrderID]
	@iOrder_ID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
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
WHERE
	[Order_ID] =@iOrder_ID
GO
