USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetOnlineProfitAmount]    Script Date: 06/07/2017 09:17:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GetOnlineProfitAmount]
(
	@InvoiceID		int,
	@SectionTypeID	int 
)
RETURNS numeric(10,2)
AS
BEGIN
	DECLARE @ReturnValue numeric(10,2)
	SET @ReturnValue = CAST('0.00' AS numeric(10,2))

	SELECT	@ReturnValue = SUM(ISECTION.group_profit_amount)  
	FROM	Invoice I 
			INNER JOIN Invoice invNonPrinted on invNonPrinted.Printed_Invoice_ID = I.Invoice_ID
			INNER JOIN QSPCanadaOrderManagement..Batch B ON B.OrderID = invNonPrinted.Order_ID
			INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
			INNER JOIN INVOICE_SECTION ISECTION on ISECTION.INVOICE_ID = invNonPrinted.INVOICE_ID
	WHERE	B.OrderQualifierID = 39009 AND I.Invoice_ID = @InvoiceID
	GROUP By I.Invoice_ID

	RETURN @ReturnValue
END
GO
