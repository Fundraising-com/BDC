USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_BHEReport]    Script Date: 06/07/2017 09:19:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jeff Miles>
-- Create date: <10/28/2008>
-- Description:	<For BHE Report>
-- =============================================
CREATE PROCEDURE [dbo].[pr_BHEReport]

	@FromDate	DATETIME,
	@ToDate		DATETIME

AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF ISNULL(@FromDate, '') = ''
BEGIN
	SELECT	@Fromdate = StartDate
	FROM	QSPCanadaCommon..Season seas
	WHERE	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
	AND		seas.Season IN ('Y')
END

IF ISNULL(@ToDate, '') = ''
BEGIN
	SET	@ToDate = GETDATE()
END


DECLARE @LYFromDate	DATETIME,
		@LYToDate	DATETIME
SET		@LYFromDate = DATEADD(YEAR, -1, @FromDate)
SET		@LYToDate = DATEADD(YEAR, -1, @ToDate)

CREATE TABLE #BHEInfoCY
(
	ProductCode		NVARCHAR(20),
	ProductName		NVARCHAR(55),
	ProductType		INT,
	Currency		NVARCHAR(4),
	TotalOrdersCY	INT,
	GrossSalesCY	NUMERIC(10, 2)
)

CREATE TABLE #BHEInfoLY
(
	ProductCode		NVARCHAR(20),
	TotalOrdersLY	INT,
	GrossSalesLY	NUMERIC(10, 2)
)

INSERT INTO #BHEInfoCY (ProductCode, ProductName, ProductType, Currency, TotalOrdersCY, GrossSalesCY)
SELECT		p.Product_Code AS ProductCode,
			p.Product_Sort_Name AS ProductName,
			p.Type AS ProductType,
			p.Currency,
			COUNT(cod.CustomerOrderHeaderInstance),
			SUM(cod.Price)
FROM		QSPCanadaProduct..Product p
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.Product_Instance = p.Product_Instance
JOIN		CustomerOrderDetail cod
				ON	cod.PricingDetailsID = pd.MagPrice_Instance
				AND	cod.DelFlag = 0
				AND	cod.StatusInstance in (508) --Order Detail Shipped
				AND	cod.CreationDate BETWEEN @FromDate AND @ToDate
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.[Date] = coh.OrderBatchDate
				AND	batch.StatusInstance <> 40005 --Cancelled
WHERE		p.Type IN (46006, 46007, 46012) --46006: Book, 46007: Music, 46012: Video
GROUP BY	p.Product_Code,
			p.Product_Sort_Name,
			p.Type,
			p.Currency


INSERT INTO #BHEInfoLY (ProductCode, TotalOrdersLY, GrossSalesLY)
SELECT		p.Product_Code AS ProductCode,
			COUNT(cod.CustomerOrderHeaderInstance),
			SUM(cod.Price)
FROM		QSPCanadaProduct..Product p
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.Product_Instance = p.Product_Instance
JOIN		CustomerOrderDetail cod
				ON	cod.PricingDetailsID = pd.MagPrice_Instance
				AND	cod.DelFlag = 0
				AND	cod.StatusInstance in (508) --Order Detail Shipped
				AND	cod.CreationDate BETWEEN @LYFromDate AND @LYToDate
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.[Date] = coh.OrderBatchDate
				AND	batch.StatusInstance <> 40005 --Cancelled
WHERE		p.Type IN (46006, 46007, 46012) --46006: Book, 46007: Music, 46012: Video
GROUP BY	p.Product_Code


SELECT		cy.ProductCode,
			cy.ProductName,
			cd.Description AS ProductType,
			CASE cy.Currency
				WHEN 801 THEN 'CAD'
				WHEN 802 THEN 'USD'
			END AS Currency,
			cy.TotalOrdersCY,
			ISNULL(ly.TotalOrdersLY, 0) AS TotalOrdersLY,
			cy.GrossSalesCY,
			ISNULL(ly.GrossSalesLY, 0.00) AS GrossSalesLY
FROM		#BHEInfoCY cy
JOIN		QSPCanadaCommon..CodeDetail cd
				ON	cd.Instance = cy.ProductType
LEFT JOIN	#BHEInfoLY ly
				ON	ly.ProductCode = cy.ProductCode
ORDER BY	cy.ProductType,
			cy.Currency,
			cy.ProductCode

DROP TABLE #BHEInfoCY
DROP TABLE #BHEInfoLY

END
GO
