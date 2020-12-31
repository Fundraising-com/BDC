USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_PAYMENT_SelectOne]    Script Date: 06/07/2017 09:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'PAYMENT'
-- based on the Primary Key.
-- Gets: @iPAYMENT_ID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_PAYMENT_SelectOne]
	@iPAYMENT_ID int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
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
WHERE
	[PAYMENT_ID] = @iPAYMENT_ID
GO
