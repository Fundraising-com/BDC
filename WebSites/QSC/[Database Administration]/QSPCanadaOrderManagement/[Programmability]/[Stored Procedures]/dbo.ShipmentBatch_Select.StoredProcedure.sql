USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentBatch_Select]    Script Date: 06/07/2017 09:20:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentBatch_Select]

	@ShipmentBatchID	INT

AS

DECLARE @ShipmentBatchDate	DATETIME
SELECT	@ShipmentBatchDate = CreationDate
FROM	ShipmentBatch
WHERE	ID = @ShipmentBatchID

SELECT		CONVERT(VARCHAR(10), @ShipmentBatchDate, 120) AS Date,
			pd.Product_Code AS ProductCode,
			SUM(cod.QuantityShipped) AS QtyOrder,
			so.DistributionCenterID
FROM		ShipmentBatch sb
JOIN		ShipmentOrder so
				ON	so.ShipmentBatchID = sb.ID
JOIN		CustomerOrderDetail cod
				ON	cod.ShipmentID = so.ShipmentID
				AND	cod.Delflag <> 1
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.Date = coh.OrderBatchDate
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaCommon..Season seas
				ON	seas.Season = pd.Pricing_Season
				AND	seas.FiscalYear = pd.Pricing_Year
WHERE		sb.ID = @ShipmentBatchID
--AND			GETDATE() BETWEEN seas.StartDate and seas.EndDate
AND			cod.StatusInstance = 508 --508: Order Detail Shipped
AND			pd.Product_Code NOT IN (
'250062',
'12C',
'24C',
'26C',
'35B',
'42C',
'46C',
'53B',
'53C',
'63C',
'75C',
'81C',
'82C',
'84C',
'85C',
'91C',
'92C',
'94C',
'SRPF09GST',
'TQFY09A',
'TQFY09B',
'TQFY09C',
'MAGS09EGST'
)
GROUP BY	pd.Product_Code,
			so.DistributionCenterID
GO
