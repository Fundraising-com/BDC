USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Invoice_By_QSP_Product_Insert]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Invoice_By_QSP_Product_Insert]

	@InvoiceID	INT

AS

CREATE TABLE #Invoice_By_QSP_Product
(
	Invoice_ID INT,
	QSP_Product_Line_ID INT,
	Product_Amount NUMERIC(14, 6),
	ProductLine_GP NUMERIC(14, 6),
	ProductLine_Tax1 NUMERIC(14, 6),
	ProductLine_Tax2 NUMERIC(14, 6),
	US_Postage_Amount NUMERIC(14, 6)
)

INSERT INTO #Invoice_By_QSP_Product
(
	Invoice_ID,
	QSP_Product_Line_ID,
	Product_Amount,
	ProductLine_GP,
	ProductLine_Tax1,
	ProductLine_Tax2,
	US_Postage_Amount
)
SELECT		inv.Invoice_ID,
			prodLine.ID,
			CASE prodLine.ID
				WHEN 46004 THEN	cod.Price
				WHEN 46008 THEN	cod.Price
				WHEN 46013 THEN	cod.Price
				WHEN 46014 THEN	cod.Price
				WHEN 46015 THEN	cod.Price
				WHEN 46019 THEN	cod.Price
				WHEN 46021 THEN	cod.Price
				WHEN 46025 THEN	CASE ps.Type WHEN 18 THEN cod.Price - Tax - Tax2 ELSE cod.Price END
				ELSE			cod.Price - Tax - Tax2
			END,
			CASE prodLine.ID
				WHEN 46001 THEN	ISNULL((cod.Price - Tax - Tax2 - isnull(pd.PostageAmount*pd.PostageRemitRate*isnull(pd.ConversionRate,0),0)) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46002 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46004 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46008 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46013 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46014 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46015 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46019 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46020 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46022 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46023 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46024 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				WHEN 46025 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
				ELSE			ISNULL((cod.Price - Tax - Tax2) * (invSec.Group_Profit_Rate / 100.00), 0)
			END,
			ISNULL(Tax, 0),
			ISNULL(Tax2, 0),
			ISNULL(pd.PostageAmount*pd.PostageRemitRate*isnull(pd.ConversionRate,0), 0)
FROM		Invoice inv
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.InvoiceNumber = inv.Invoice_ID
JOIN		QSPCanadaCommon..QSPProductLine prodLine
				ON	prodLine.ID = cod.ProductType
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..ProgramSection ps
				ON	ps.ID = pd.ProgramSectionID
JOIN		QSPCanadaProduct..PROGRAM_MASTER pm
				ON	pm.Program_ID = ps.Program_ID
JOIN		Invoice_Section invSec
				ON	invSec.Invoice_ID = inv.Invoice_ID
				AND	ISNULL(invSec.ProgramType, pm.SubType) = pm.SubType
				AND	invSec.Section_Type_ID = CASE cod.ProductType
												WHEN 46017 THEN 8 --Proc Fee switch from 2 to 8 so GP is not included
												ELSE			ps.[Type]
											END
WHERE		inv.Invoice_ID = @InvoiceID
AND			((SELECT dbo.UDF_Section_Vs_Entity(@InvoiceID, invSec.Section_Type_ID, '62')) = 'Y' OR cod.ProductType = 46017)
AND			ISNULL(cod.IsVoucherRedemption, 0) = 0

INSERT	Invoice_By_QSP_Product
(
	Invoice_ID,
	QSP_Product_Line_ID,
	Product_Amount,
	ProductLine_GP,
	ProductLine_Tax1,
	ProductLine_Tax2,
	US_Postage_Amount
)
SELECT		Invoice_ID,
			QSP_Product_Line_ID,
			ROUND(SUM(Product_Amount), 2),
			ROUND(SUM(ProductLine_GP), 2),
			ROUND(SUM(ProductLine_Tax1), 2),
			ROUND(SUM(ProductLine_Tax2), 2),
			ROUND(SUM(US_Postage_Amount), 2)
FROM		#Invoice_By_QSP_Product
GROUP BY	Invoice_ID,
			QSP_Product_Line_ID
ORDER BY	QSP_Product_Line_ID

DROP TABLE #Invoice_By_QSP_Product
GO
