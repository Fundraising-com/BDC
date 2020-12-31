USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[PrintInvoiceProductDetails]    Script Date: 06/07/2017 09:17:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PrintInvoiceProductDetails] -- 484302

	@InvoiceID	INT

WITH RECOMPILE

AS

SELECT		--od.Invoice_ID,
			od.ProductCode,
			od.ProductName,
			od.CatalogPrice,
			od.IsQSPExclusive,
			od.SectionType,
			od.ProductType,
			od.Lang,
			od.OrderQualifierID,
			od.InvoiceGridID,
			od.NumIssues,
			od.ProductTypeName,
			od.Price,
			SUM(od.QTYOrdered) QTYOrdered,
			SUM(od.QuantityShipped) QuantityShipped,
			SUM(od.TotalPrice) TotalPrice,
			SUM(od.PostageAmount) PostageAmount 
FROM		UDF_Invoice_GetOrderDetails() od
WHERE		od.Invoice_ID IN	(SELECT	Invoice_ID
								FROM	Invoice inv
								WHERE	inv.Printed_Invoice_ID = @InvoiceID
								OR		inv.Invoice_ID = @InvoiceID)
GROUP BY	od.ProductCode,
			od.ProductName,
			od.CatalogPrice,
			od.IsQSPExclusive,
			od.SectionType,
			od.ProductType,
			od.Lang,
			od.OrderQualifierID,
			od.InvoiceGridID,
			od.NumIssues,
			od.ProductTypeName,
			od.Price			
ORDER BY	od.OrderQualifierID,
			od.ProductName
GO
