USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetRegularMagazineProfitAmount]    Script Date: 06/07/2017 09:17:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- SELECT dbo.UDF_GetRegularMagazineProfitAmount(269385, 2)
CREATE FUNCTION [dbo].[UDF_GetRegularMagazineProfitAmount]
(
	@InvoiceID		int,
	@SectionTypeID	int
)
RETURNS numeric(10,2)
AS
BEGIN
	DECLARE @ReturnValue numeric(10,2)
	SET @ReturnValue = CAST('0.00' AS numeric(10,2))

	SELECT	 	TOP 1 
				@ReturnValue = 
				CASE @SectionTypeID 
					WHEN 2 THEN invSect.Group_Profit_Amount	-- Make sure the current record we are processing, is a magazine
					ELSE 0
				END					
	FROM		QSPCanadaOrderManagement..Batch					b
	JOIN		QSPCanadaOrderManagement..CustomerOrderHeader	coh		ON coh.orderbatchid = b.id and coh.orderbatchdate = b.date
	LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetail	codMag	ON codMag.customerorderheaderinstance = coh.instance	AND	codMag.DelFlag = 0
	LEFT JOIN	QSPCanadaFinance..INVOICE						inv		ON b.OrderID = inv.Order_ID
	LEFT JOIN	QSPCanadaFinance..Invoice_Section				invSect	ON inv.Invoice_ID = invSect.Invoice_ID
	WHERE		b.orderQualifierID <> 39009 		-- Not Internet
				AND invSect.Section_Type_ID = 2		-- Magazine (Section_Type_ID == 2)
				AND codMag.ProductType = 46001		-- Magazine (ProductType = 46001)
				AND inv.Invoice_ID = @InvoiceID

	RETURN @ReturnValue
END
GO
