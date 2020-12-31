USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetProcessingFeeOrder]    Script Date: 06/07/2017 09:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- SELECT dbo.UDF_GetProcessingFeeOrder(484302,8)
CREATE FUNCTION [dbo].[UDF_GetProcessingFeeOrder]
(
	@InvoiceID		int,
	@SectionTypeID	int
)
RETURNS INT
AS
BEGIN
	DECLARE @ReturnValue int
	SET @ReturnValue = 0

	SELECT	 	TOP 1 
				@ReturnValue = 
				CASE @SectionTypeID 
					WHEN 8 THEN invSect.Item_Count	-- Make sure the current record we are processing, is the processing fees
					ELSE 0
				END					
	FROM		QSPCanadaOrderManagement..Batch					b
	JOIN		QSPCanadaOrderManagement..CustomerOrderHeader	coh		ON coh.orderbatchid = b.id and coh.orderbatchdate = b.date
	LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetail	codMag	ON codMag.customerorderheaderinstance = coh.instance	AND	codMag.DelFlag = 0
	LEFT JOIN	QSPCanadaFinance..INVOICE						inv		ON b.OrderID = inv.Order_ID
	LEFT JOIN	QSPCanadaFinance..Invoice_Section				invSect	ON inv.Invoice_ID = invSect.Invoice_ID
	WHERE		--b.orderQualifierID <> 39009 					-- Not Internet
				invSect.Section_Type_ID = @SectionTypeID	-- Processing Fees (Section_Type_ID == 8)
				AND codMag.ProductType = 46001					-- magazine (ProductType = 46001)
				AND inv.Invoice_ID = @InvoiceID

	RETURN @ReturnValue
END
GO
