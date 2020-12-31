USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetInvoicePaymentTotalsFromOrderQualifierId]    Script Date: 06/07/2017 09:17:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Martinez
-- Create date: 2009-10-06
-- Description:	Gets the group profit amount total for display in the invoice
-- =============================================
CREATE FUNCTION [dbo].[UDF_GetInvoicePaymentTotalsFromOrderQualifierId]
(
	@InvoiceID			int, 
	@OrderQualifierID	int
)
RETURNS NUMERIC(10,2)
AS
BEGIN
	
	DECLARE @Total NUMERIC(10,2)

	SET @Total = (	SELECT	SUM(invoicesection.group_profit_amount) as total
					FROM	invoice 
							INNER JOIN QSPCanadaOrderManagement..Batch batch ON batch.OrderID = invoice.Order_ID
							INNER JOIN INVOICE_SECTION invoicesection on invoicesection.INVOICE_ID = invoice.INVOICE_ID
					where	printed_invoice_id = @InvoiceID
							AND batch.OrderQualifierID = @OrderQualifierID
							)
									
	RETURN ISNULL(@Total, 0)

END
GO
