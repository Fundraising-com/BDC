USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[vw_TRTInvoiceSection]    Script Date: 06/07/2017 09:16:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_TRTInvoiceSection] AS

SELECT	inv.Invoice_ID InvoiceID,
		invSec.Invoice_Section_ID InvoiceSectionID,
		(	SELECT		TOP 1
						coh.FormCode
			FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
			JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
			WHERE		cod.InvoiceNumber = inv.INVOICE_ID
			AND			cod.ProductType = 46023
			ORDER BY	coh.FormCode
		) FormCode,
		(	SELECT		TOP 1
						coh.TRTGenerationCode
			FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
			JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
			WHERE		cod.InvoiceNumber = inv.INVOICE_ID
			AND			cod.ProductType = 46023
			ORDER BY	coh.TRTGenerationCode
		) TRTGenerationCode
FROM	Invoice inv
JOIN	Invoice_Section invSec ON invSec.INVOICE_ID = inv.INVOICE_ID
WHERE	invSec.Section_Type_ID IN (14)
GO
