USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_Invoice_SelectMissing]    Script Date: 06/07/2017 09:17:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Invoice_SelectMissing]

AS

DECLARE	@FYStartDate 	DATETIME
DECLARE	@FYEndDate   	DATETIME

SELECT	@FYStartDate = Startdate
FROM	QSPCanadaCommon..Season
WHERE	GETDATE() BETWEEN StartDate AND EndDate
AND		Season  = 'Y'

SELECT		b.StatusInstance, b.OrderQualifierID, b.OrderTypeCode, b.IsInvoiced, cod.ProductType, b.orderid, cod.*
FROM		QSPCanadaOrderManagement..Batch b
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.[Date]
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.Id = b.AccountID
LEFT JOIN	InvoiceGenerationLog igl
				ON	igl.OrderID = b.OrderID
				AND	ISNULL(igl.IsFixed, 0) <> 1
WHERE		ISNULL(cod.InvoiceNumber,0) = 0
AND			cod.DelFlag = 0
AND			b.StatusInstance not in (40005, 40014, 40012, 40010)
AND			cod.StatusInstance not in (500, 501, 506, 509)
AND			b.OrderQualifierID not in (39004,39005,39007,39008,39010,39011,39012,39014,39018,39019,39023)
AND			cod.ProductCode not in ('NNNN')
AND			cod.ProductType not in (46004, 46008, 46013, 46014, 46017)
AND			b.OrderTypeCode not in (41012)
--AND	NOT		(cod.ProductType IN (46008, 46013, 46014, 46015) AND acc.CAccountCodeClass <> 'FM')
AND			cod.CreationDate BETWEEN @FYStartDate AND DATEADD(dd, -1, GETDATE())
AND			isnull(b.IsInvoiced,0) = 0
AND			igl.InvoiceGenLogID IS NULL
ORDER BY	cod.CustomerOrderHeaderInstance DESC
GO
