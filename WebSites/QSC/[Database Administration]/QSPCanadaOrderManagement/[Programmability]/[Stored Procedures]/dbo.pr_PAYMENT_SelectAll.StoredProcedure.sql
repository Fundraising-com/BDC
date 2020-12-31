USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_PAYMENT_SelectAll]    Script Date: 06/07/2017 09:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'PAYMENT'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_PAYMENT_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[PAYMENT_ID],
	[ACCOUNT_ID],
	[ACCOUNT_TYPE_ID],
	[PAYMENT_METHOD_ID],
	[PAYMENT_EFFECTIVE_DATE],
	[CHEQUE_NUMBER],
	[CHEQUE_DATE],
	[CHEQUE_PAYER],
	[CREDIT_CARD_OWNER],
	[CREDIT_CARD_AUTHORIZATION],
	[PAYMENT_AMOUNT],
	[NOTE_TO_PRINT],
	[DATETIME_CREATED],
	[DATETIME_MODIFIED],
	[LAST_UPDATED_BY],
	[ORDER_ID],
	[COUNTRY_CODE],
	[CAMPAIGN_ID]
FROM qspcanadafinance.[PAYMENT]
ORDER BY 
	[PAYMENT_ID] ASC
GO
