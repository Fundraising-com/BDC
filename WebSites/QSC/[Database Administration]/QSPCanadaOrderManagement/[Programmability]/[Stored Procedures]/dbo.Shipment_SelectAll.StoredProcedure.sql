USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[Shipment_SelectAll]    Script Date: 06/07/2017 09:20:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Shipment_SelectAll]

	@FMID							VARCHAR(10) = NULL,
	@DMID							VARCHAR(10) = NULL,
	@CampaignID						INT = NULL,
	@AccountID						INT = NULL,
	@OrderID						INT = NULL,
	@WaybillNumber					VARCHAR(50) = NULL,
	@FiscalYear						INT = NULL,
	@CurrentFiscalYearOnly			BIT = NULL,
	@NotifyFM						BIT = NULL,
	@ShowBHE						BIT = NULL,
	@ShowOutstandingOnly			BIT = NULL,
	@DistributionCenterID			INT = NULL,
	@CurrentDayShipmentsOnly		BIT = NULL,
	@ShipmentDate					DATETIME = NULL,
	@SAPFile						BIT = 0

AS

DECLARE	@RunDate	DATETIME
SET @RunDate = GETDATE()

DECLARE	@SeasonStartDate	DATETIME,
		@SeasonEndDate		DATETIME

IF @FiscalYear IS NOT NULL
BEGIN
	SELECT	@SeasonStartDate = seas.StartDate,
			@SeasonEndDate = seas.EndDate
	FROM	QSPCanadaCommon..Season seas
	WHERE	seas.FiscalYear = @FiscalYear
	AND		seas.Season IN ('Y')
END
ELSE IF @CurrentFiscalYearOnly = 1
BEGIN
	SELECT	@SeasonStartDate = seas.StartDate,
			@SeasonEndDate = seas.EndDate
	FROM	QSPCanadaCommon..Season seas
	WHERE	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
	AND		seas.Season IN ('Y')
END

CREATE TABLE #ShipmentData
(
	ShipmentID					INT,
	DMID						VARCHAR(MAX),
	DMFirstName					VARCHAR(MAX),
	DMLastName					VARCHAR(MAX),
	FMID						VARCHAR(MAX),
	FMFirstName					VARCHAR(MAX),
	FMLastName					VARCHAR(MAX),
	AccountID					INT,
	AccountName					VARCHAR(MAX),
	CampaignID					INT,
	OrderID						INT,
	OrderDate					DATETIME,
	OrderType					VARCHAR(MAX),
	ShipToEntity				VARCHAR(MAX),
	RequestedShipmentDate		DATETIME,
	ShipmentDate				DATETIME,
	Carrier						VARCHAR(MAX),
	WaybillNumber				VARCHAR(MAX),
	NumBoxes					INT,
	CustomerOrderHeaderInstance	INT,
	TransID						INT,
	ProductCode					VARCHAR(MAX),
	SAPMaterialNumber			VARCHAR(MAX),
	ProductName					VARCHAR(MAX),
	ProductType					INT,
	QuantityOrdered				INT,
	QuantityShipped				INT,
	DistributionCenter			VARCHAR(MAX),
	ProductReplacementReason    VARCHAR(MAX),
	OnlineOrder					BIT,
	ProgramName					VARCHAR(MAX),
	BrochureCode				VARCHAR(MAX),
	StorageLocation				VARCHAR(MAX)
)

INSERT INTO #ShipmentData
(
	ShipmentID,
	DMID,
	DMFirstName,
	DMLastName,
	FMID,
	FMFirstName,
	FMLastName,
	AccountID,
	AccountName,
	CampaignID,
	OrderID,
	OrderDate,
	OrderType,
	ShipToEntity,
	RequestedShipmentDate,
	ShipmentDate,
	Carrier,
	WaybillNumber,
	NumBoxes,
	CustomerOrderHeaderInstance,
	TransID,
	ProductCode,
	SAPMaterialNumber,
	ProductName,
	ProductType,
	QuantityOrdered,
	QuantityShipped,
	DistributionCenter,
	ProductReplacementReason,
	OnlineOrder,
	ProgramName,
	BrochureCode,
	StorageLocation
)
SELECT		s.ID,
			dm.FMID,
			dm.FirstName,
			dm.LastName,
			fm.FMID,
			fm.FirstName,
			fm.LastName,
			acc.ID,
			acc.Name,
			camp.ID,
			b.OrderID,
			b.Date,
			CASE b.OrderTypeCode
				WHEN 41001 THEN 'Campaign'
				WHEN 41002 THEN 'Campaign Field Supply'
				WHEN 41005 THEN 'Employee'
				WHEN 41006 THEN 'Field Manager'	
				WHEN 41007 THEN 'Field Manager (Bulk)'		
				WHEN 41008 THEN 'Campaign Straight'
				WHEN 41010 THEN 'POS'	
				WHEN 41011 THEN	'Field Manager (Closeout)'		
				ELSE			'UNKNOWN'
			END,
			CASE
				WHEN b.OrderTypeCode IN (41002) THEN										cdShipToCampCont.Description
				WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN 'Customer'
				WHEN b.OrderTypeCode IN (41001, 41006, 41007, 41008, 41009, 41010) THEN	CASE ISNULL(b.ShipToFMID, 0)
																						WHEN 0 THEN 'School'
																						ELSE		'FM'
																					END
			END,
			CASE
				WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN GETDATE()
				WHEN b.OrderDeliveryDate IS NULL THEN	GETDATE()
				ELSE b.OrderDeliveryDate - (prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep)
			END AS RequestedShipDate,
			s.ShipmentDate,
			cdCarrier.Description,
			swb.WayBillNumber,
			s.NumberBoxes,
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			ISNULL(pReplace.Product_Code, cod.ProductCode),
			ISNULL(cod.ReplacedProductCode, p.OracleCode),
			ISNULL(SUBSTRING(pReplace.Product_Sort_Name, 1, 27), SUBSTRING(cod.ProductName, 1, 27)),
			ISNULL(pReplace.Type, cod.ProductType),
			ISNULL(cod.ReplacedProductQty, CASE @SAPFile WHEN 1 THEN 0 ELSE cod.Quantity END), 	--Already have sent the Quantity Ordered (Reserved) to SAP, so don't send it once shipped
			ISNULL(cod.ReplacedProductQty, cod.QuantityShipped),
			dc.Name,
			ISNULL(cdprr.Description, ''),
			CASE b.OrderQualifierID WHEN 39009 THEN 1 ELSE 0 END,
			cdP.Description,
			pm.Code,
			CASE WHEN p.Type = 46018 AND addr.StateProvince IN ('BC','AB','SK','MB') THEN '1272' ELSE '1270' END
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaProduct..PRICING_DETAILS pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaOrderManagement.dbo.Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
JOIN		QSPCanadaCommon..Province prov
				ON	prov.Province_Code = cust.[State]
LEFT JOIN	Shipment s
				ON	s.ID = cod.ShipmentID
LEFT JOIN	ShipmentOrder so
				ON	s.ID = so.ShipmentID
LEFT JOIN	Shipmentwaybill swb
				ON	swb.ShipmentID = s.ID
JOIN		DistributionCenter dc
				ON	dc.ID = cod.DistributionCenterID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		QSPCanadaCommon..FieldManager dm
				ON	dm.FMID = fm.DMID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.ShipToAccountID
LEFT JOIN	QSPCanadaCommon..Address addr
				ON	addr.AddressListID = acc.AddressListID
				AND	addr.Address_Type = 54002
LEFT JOIN	QSPCanadaCommon..Codedetail cdCarrier
				ON	cdCarrier.Instance = s.CarrierID
LEFT JOIN	QSPCanadaCommon..CodeDetail cdShipToCampCont
				ON	cdShipToCampCont.Instance = camp.SuppliesShipToCampaignContactID
LEFT JOIN	QSPCanadaCommon..Codedetail cdprr
				ON	cdprr.Instance = cod.ProductReplacementReasonID
LEFT JOIN	QSPCanadaProduct..Product pReplace
				ON	pReplace.Product_Instance IN (SELECT TOP 1	Product_Instance 
												  FROM			QSPCanadaProduct..Product pReplaceTop 
												  WHERE			pReplaceTop.OracleCode = cod.ReplacedProductCode 
												  AND			pReplaceTop.Product_Year = p.Product_Year
												  AND			pReplaceTop.Product_Season = p.Product_Season
												  ORDER BY		pReplaceTop.OracleCode)
JOIN		QSPCanadaProduct..ProgramSection ps
				ON	ps.ID = pd.ProgramSectionID
JOIN		QSPCanadaProduct..Program_Master pm
				ON	pm.Program_ID = ps.Program_ID
JOIN		QSPCanadaCommon..CodeDetail cdP
				ON	cdP.Instance = pm.SubType
WHERE		fm.FMID = ISNULL(@FMID, fm.FMID)
AND			dm.FMID = ISNULL(@DMID, dm.FMID)
AND			camp.ID = ISNULL(@CampaignID, camp.ID)
AND			acc.ID = ISNULL(@AccountID, acc.ID)
AND			b.OrderID = ISNULL(@OrderID, b.OrderID)
AND			(swb.WayBillNumber = @WaybillNumber OR @WayBillNumber IS NULL)
AND			b.Date BETWEEN ISNULL(@SeasonStartDate, b.Date) AND ISNULL(@SeasonEndDate, b.Date)
AND			(ISNULL(@NotifyFM, 0) = 0 OR ISNULL(s.FMEmailNotificationSent, '1/1/95') = '1/1/95')
AND			b.StatusInstance NOT IN (40005) --40005: Cancelled
AND			cod.DelFlag <> 1
AND			(@ShowBHE = 1 OR cod.ProductType NOT IN (46006, 46007, 46012)) --46006: Books, 46007: Music, 46012: Videos
AND			cod.ProductType NOT IN (46023) -- Don't show TRT as this is shipped through Focus
AND			(ISNULL(@ShowOutstandingOnly, 0) = 0 OR cod.StatusInstance in (510, 511)) --509: Order Detail Pending to TPL
AND			dc.ID = ISNULL(@DistributionCenterID, dc.ID)
AND			(ISNULL(@CurrentDayShipmentsOnly, 0) = 0 OR s.DateModified BETWEEN CONVERT(DATETIME,CONVERT(VARCHAR,GETDATE(),112),112) AND CONVERT(DATETIME,CONVERT(VARCHAR,GETDATE()+1,112),112))
AND			(ISNULL(@ShipmentDate, 0) = 0 OR s.ShipmentDate BETWEEN CONVERT(DATETIME,CONVERT(VARCHAR,@ShipmentDate,112),112) AND CONVERT(DATETIME,CONVERT(VARCHAR,@ShipmentDate+1,112),112))
AND			(ISNULL(@SAPFile, 0) = 0 OR (s.ID IS NOT NULL AND s.SentToSAP IS NULL AND s.ShipmentDate > '2017-01-09'))

IF @SAPFile = 1
BEGIN

	UPDATE	s
	SET		s.SentToSAP = @RunDate
	FROM	Shipment s
	JOIN	#ShipmentData srd
				ON	srd.ShipmentID = s.ID

	INSERT INTO #ShipmentData
	(
		ShipmentID,
		DMID,
		DMFirstName,
		DMLastName,
		FMID,
		FMFirstName,
		FMLastName,
		AccountID,
		AccountName,
		CampaignID,
		OrderID,
		OrderDate,
		OrderType,
		ShipToEntity,
		RequestedShipmentDate,
		ShipmentDate,
		Carrier,
		WaybillNumber,
		NumBoxes,
		CustomerOrderHeaderInstance,
		TransID,
		ProductCode,
		SAPMaterialNumber,
		ProductName,
		ProductType,
		QuantityOrdered,
		QuantityShipped,
		DistributionCenter,
		ProductReplacementReason,
		OnlineOrder,
		ProgramName,
		BrochureCode,
		StorageLocation
	)
	SELECT		NULL,
				dm.FMID,
				dm.FirstName,
				dm.LastName,
				fm.FMID,
				fm.FirstName,
				fm.LastName,
				acc.ID,
				acc.Name,
				camp.ID,
				b.OrderID,
				b.Date,
				CASE b.OrderTypeCode
					WHEN 41001 THEN 'Campaign'
					WHEN 41002 THEN 'Campaign Field Supply'
					WHEN 41005 THEN 'Employee'
					WHEN 41006 THEN 'Field Manager'	
					WHEN 41007 THEN 'Field Manager (Bulk)'		
					WHEN 41008 THEN 'Campaign Straight'
					WHEN 41010 THEN 'POS'	
					WHEN 41011 THEN	'Field Manager (Closeout)'		
					ELSE			'UNKNOWN'
				END,
				CASE
					WHEN b.OrderTypeCode IN (41002) THEN										cdShipToCampCont.Description
					WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN 'Customer'
					WHEN b.OrderTypeCode IN (41001, 41006, 41007, 41008, 41009, 41010) THEN	CASE ISNULL(b.ShipToFMID, 0)
																							WHEN 0 THEN 'School'
																							ELSE		'FM'
																						END
				END,
				CASE
					WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN GETDATE()
					WHEN b.OrderDeliveryDate IS NULL THEN	GETDATE()
					ELSE b.OrderDeliveryDate - (prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep)
				END AS RequestedShipDate,
				NULL,
				NULL,
				NULL,
				NULL,
				cod.CustomerOrderHeaderInstance,
				cod.TransID,
				cod.ProductCode,
				p.OracleCode,
				SUBSTRING(cod.ProductName, 1, 27),
				cod.ProductType,
				cod.Quantity,
				0,
				dc.Name,
				'',
				CASE b.OrderQualifierID WHEN 39009 THEN 1 ELSE 0 END,
				cdP.Description,
				pm.Code,
				CASE WHEN p.Type = 46018 AND addr.StateProvince IN ('BC','AB','SK','MB') THEN '1272' ELSE '1270' END
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		QSPCanadaProduct..PRICING_DETAILS pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN		QSPCanadaProduct..Product p
					ON	p.Product_Instance = pd.Product_Instance
	JOIN		QSPCanadaOrderManagement.dbo.Customer cust
					ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
											WHEN 0 THEN coh.CustomerBillToInstance
											ELSE		cod.CustomerShipToInstance
										END
	JOIN		QSPCanadaCommon..Province prov
					ON	prov.Province_Code = cust.[State]
	JOIN		DistributionCenter dc
					ON	dc.ID = cod.DistributionCenterID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	JOIN		QSPCanadaCommon..FieldManager fm
					ON	fm.FMID = camp.FMID
	JOIN		QSPCanadaCommon..FieldManager dm
					ON	dm.FMID = fm.DMID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = camp.ShipToAccountID
	LEFT JOIN	QSPCanadaCommon..Address addr
					ON	addr.AddressListID = acc.AddressListID
					AND	addr.Address_Type = 54002
	LEFT JOIN	QSPCanadaCommon..CodeDetail cdShipToCampCont
					ON	cdShipToCampCont.Instance = camp.SuppliesShipToCampaignContactID
	JOIN		QSPCanadaProduct..ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
	JOIN		QSPCanadaProduct..Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
	JOIN		QSPCanadaCommon..CodeDetail cdP
					ON	cdP.Instance = pm.SubType	
	WHERE		b.StatusInstance IN (40010, 40013, 40014)
	AND			cod.DelFlag <> 1
	AND			cod.ProductType NOT IN (46023) -- Don't show TRT as this is shipped through Focus
	AND			b.SentToSAP IS NULL

	UPDATE	b
	SET		b.SentToSAP = @RunDate
	FROM	Batch b
	JOIN	#ShipmentData srd
				ON	srd.OrderID = b.OrderID
	WHERE	b.SentToSAP IS NULL
	AND		srd.ShipmentID IS NULL

	--Replacements - Unreserve stock of original item
	INSERT INTO #ShipmentData
	(
		ShipmentID,
		DMID,
		DMFirstName,
		DMLastName,
		FMID,
		FMFirstName,
		FMLastName,
		AccountID,
		AccountName,
		CampaignID,
		OrderID,
		OrderDate,
		OrderType,
		ShipToEntity,
		RequestedShipmentDate,
		ShipmentDate,
		Carrier,
		WaybillNumber,
		NumBoxes,
		CustomerOrderHeaderInstance,
		TransID,
		ProductCode,
		SAPMaterialNumber,
		ProductName,
		ProductType,
		QuantityOrdered,
		QuantityShipped,
		DistributionCenter,
		ProductReplacementReason,
		OnlineOrder,
		ProgramName,
		BrochureCode,
		StorageLocation
	)
	SELECT		s.ID,
				dm.FMID,
				dm.FirstName,
				dm.LastName,
				fm.FMID,
				fm.FirstName,
				fm.LastName,
				acc.ID,
				acc.Name,
				camp.ID,
				b.OrderID,
				b.Date,
				CASE b.OrderTypeCode
					WHEN 41001 THEN 'Campaign'
					WHEN 41002 THEN 'Campaign Field Supply'
					WHEN 41005 THEN 'Employee'
					WHEN 41006 THEN 'Field Manager'	
					WHEN 41007 THEN 'Field Manager (Bulk)'		
					WHEN 41008 THEN 'Campaign Straight'
					WHEN 41010 THEN 'POS'	
					WHEN 41011 THEN	'Field Manager (Closeout)'		
					ELSE			'UNKNOWN'
				END,
				CASE
					WHEN b.OrderTypeCode IN (41002) THEN										cdShipToCampCont.Description
					WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN 'Customer'
					WHEN b.OrderTypeCode IN (41001, 41006, 41007, 41008, 41009, 41010) THEN	CASE ISNULL(b.ShipToFMID, 0)
																							WHEN 0 THEN 'School'
																							ELSE		'FM'
																						END
				END,
				CASE
					WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN GETDATE()
					WHEN b.OrderDeliveryDate IS NULL THEN	GETDATE()
					ELSE b.OrderDeliveryDate - (prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep)
				END AS RequestedShipDate,
				s.ShipmentDate,
				cdCarrier.Description,
				swb.WayBillNumber,
				s.NumberBoxes,
				cod.CustomerOrderHeaderInstance,
				cod.TransID,
				cod.ProductCode,
				p.OracleCode,
				SUBSTRING(cod.ProductName, 1, 27),
				cod.ProductType,
				cod.Quantity * -1, --Unreserve Stock
				0,
				dc.Name,
				ISNULL(cdprr.Description, ''),
				CASE b.OrderQualifierID WHEN 39009 THEN 1 ELSE 0 END,
				cdP.Description,
				pm.Code,
				CASE WHEN p.Type = 46018 AND addr.StateProvince IN ('BC','AB','SK','MB') THEN '1272' ELSE '1270' END
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		QSPCanadaProduct..PRICING_DETAILS pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN		QSPCanadaProduct..Product p
					ON	p.Product_Instance = pd.Product_Instance
	JOIN		QSPCanadaOrderManagement.dbo.Customer cust
					ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
											WHEN 0 THEN coh.CustomerBillToInstance
											ELSE		cod.CustomerShipToInstance
										END
	JOIN		QSPCanadaCommon..Province prov
					ON	prov.Province_Code = cust.[State]
	LEFT JOIN	Shipment s
					ON	s.ID = cod.ShipmentID
	LEFT JOIN	ShipmentOrder so
					ON	s.ID = so.ShipmentID
	LEFT JOIN	Shipmentwaybill swb
					ON	swb.ShipmentID = s.ID
	JOIN		DistributionCenter dc
					ON	dc.ID = cod.DistributionCenterID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	JOIN		QSPCanadaCommon..FieldManager fm
					ON	fm.FMID = camp.FMID
	JOIN		QSPCanadaCommon..FieldManager dm
					ON	dm.FMID = fm.DMID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = camp.ShipToAccountID
	LEFT JOIN	QSPCanadaCommon..Address addr
					ON	addr.AddressListID = acc.AddressListID
					AND	addr.Address_Type = 54002
	LEFT JOIN	QSPCanadaCommon..Codedetail cdCarrier
					ON	cdCarrier.Instance = s.CarrierID
	LEFT JOIN	QSPCanadaCommon..CodeDetail cdShipToCampCont
					ON	cdShipToCampCont.Instance = camp.SuppliesShipToCampaignContactID
	LEFT JOIN	QSPCanadaCommon..Codedetail cdprr
					ON	cdprr.Instance = cod.ProductReplacementReasonID
	JOIN		QSPCanadaProduct..ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
	JOIN		QSPCanadaProduct..Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
	JOIN		QSPCanadaCommon..CodeDetail cdP
					ON	cdP.Instance = pm.SubType
	JOIN		#ShipmentData srd
					ON	srd.ShipmentID = s.ID
	WHERE		cod.ReplacedProductCode > 0

END

IF @NotifyFM = 1
BEGIN
	INSERT INTO #ShipmentData
	(
		ShipmentID,
		DMID,
		DMFirstName,
		DMLastName,
		FMID,
		FMFirstName,
		FMLastName,
		AccountID,
		AccountName,
		CampaignID,
		OrderID,
		OrderDate,
		OrderType,
		ShipToEntity,
		RequestedShipmentDate,
		ShipmentDate,
		Carrier,
		WaybillNumber,
		NumBoxes,
		CustomerOrderHeaderInstance,
		TransID,
		ProductCode,
		SAPMaterialNumber,
		ProductName,
		ProductType,
		QuantityOrdered,
		QuantityShipped,
		DistributionCenter,
		ProductReplacementReason,
		OnlineOrder,
		ProgramName,
		BrochureCode,
		StorageLocation
	)
	SELECT		s.ID,
				dm.FMID,
				dm.FirstName,
				dm.LastName,
				fm.FMID,
				fm.FirstName,
				fm.LastName,
				acc.ID,
				acc.Name,
				camp.ID,
				b.OrderID,
				b.Date,
				CASE b.OrderTypeCode
					WHEN 41001 THEN 'Campaign'
					WHEN 41002 THEN 'Campaign Field Supply'
					WHEN 41005 THEN 'Employee'
					WHEN 41006 THEN 'Field Manager'	
					WHEN 41007 THEN 'Field Manager (Bulk)'		
					WHEN 41008 THEN 'Campaign Straight'
					WHEN 41010 THEN 'POS'	
					WHEN 41011 THEN	'Field Manager (Closeout)'		
					ELSE			'UNKNOWN'
				END,
				CASE
					WHEN b.OrderTypeCode IN (41002) THEN										cdShipToCampCont.Description
					WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN 'Customer'
					WHEN b.OrderTypeCode IN (41001, 41006, 41007, 41008, 41009, 41010) THEN	CASE ISNULL(b.ShipToFMID, 0)
																							WHEN 0 THEN 'School'
																							ELSE		'FM'
																						END
				END,
				CASE
					WHEN b.OrderQualifierID = 39009 AND cod.IsShippedToAccount = 0 THEN GETDATE()
					WHEN b.OrderDeliveryDate IS NULL THEN	GETDATE()
					ELSE b.OrderDeliveryDate - (prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep)
				END AS RequestedShipDate,
				s.ShipmentDate,
				cdCarrier.Description,
				swb.WayBillNumber,
				s.NumberBoxes,
				cod.CustomerOrderHeaderInstance,
				cod.TransID,
				ISNULL(pReplace.Product_Code, cod.ProductCode),
				ISNULL(pReplace.OracleCode, p.OracleCode),
				ISNULL(SUBSTRING(pReplace.Product_Sort_Name, 1, 27), SUBSTRING(cod.ProductName, 1, 27)),
				ISNULL(pReplace.Type, cod.ProductType),
				cod.Quantity,
				cod.QuantityShipped,
				dc.Name,
				ISNULL(cdprr.Description, ''),
				CASE b.OrderQualifierID WHEN 39009 THEN 1 ELSE 0 END,
				cdP.Description,
				pm.Code,
				CASE WHEN p.Type = 46018 AND addr.StateProvince IN ('BC','AB','SK','MB') THEN '1272' ELSE '1270' END
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		QSPCanadaProduct..PRICING_DETAILS pd
					ON	pd.MagPrice_Instance = cod.PricingDetailsID
	JOIN		QSPCanadaProduct..Product p
					ON	p.Product_Instance = pd.Product_Instance
	JOIN		QSPCanadaCommon..QSPProductLine pl
					ON	pl.ID = p.Type
	JOIN		QSPCanadaOrderManagement.dbo.Customer cust
					ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
											WHEN 0 THEN coh.CustomerBillToInstance
											ELSE		cod.CustomerShipToInstance
										END
	JOIN		QSPCanadaCommon..Province prov
					ON	prov.Province_Code = cust.[State]
	LEFT JOIN	Shipment s
					ON	s.ID = cod.ShipmentID
	LEFT JOIN	ShipmentOrder so
					ON	s.ID = so.ShipmentID
	LEFT JOIN	Shipmentwaybill swb
					ON	swb.ShipmentID = s.ID
	JOIN		DistributionCenter dc
					ON	dc.ID = cod.DistributionCenterID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	JOIN		QSPCanadaCommon..FieldManager fm
					ON	fm.FMID = camp.FMID
	JOIN		QSPCanadaCommon..FieldManager dm
					ON	dm.FMID = fm.DMID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = camp.ShipToAccountID
	LEFT JOIN	QSPCanadaCommon..Address addr
					ON	addr.AddressListID = acc.AddressListID
					AND	addr.Address_Type = 54002
	LEFT JOIN	QSPCanadaCommon..Codedetail cdCarrier
					ON	cdCarrier.Instance = s.CarrierID
	LEFT JOIN	QSPCanadaCommon..CodeDetail cdShipToCampCont
					ON	cdShipToCampCont.Instance = camp.SuppliesShipToCampaignContactID
	LEFT JOIN	QSPCanadaCommon..Codedetail cdprr
					ON	cdprr.Instance = cod.ProductReplacementReasonID
	LEFT JOIN	QSPCanadaProduct..Product pReplace
					ON	pReplace.Product_Instance IN (SELECT TOP 1	Product_Instance 
													  FROM			QSPCanadaProduct..Product pReplaceTop 
													  WHERE			pReplaceTop.OracleCode = cod.ReplacedProductCode 
													  AND			pReplaceTop.Product_Year = p.Product_Year
													  AND			pReplaceTop.Product_Season = p.Product_Season
													  ORDER BY		pReplaceTop.OracleCode)
	JOIN		QSPCanadaProduct..ProgramSection ps
					ON	ps.ID = pd.ProgramSectionID
	JOIN		QSPCanadaProduct..Program_Master pm
					ON	pm.Program_ID = ps.Program_ID
	JOIN		QSPCanadaCommon..CodeDetail cdP
					ON	cdP.Instance = pm.SubType
	JOIN		(SELECT DISTINCT sh.OrderID, sp.ShipmentGroupID FROM #ShipmentData sh JOIN Shipment sp ON sp.ID = sh.ShipmentID) sd
					ON	sd.OrderID = b.OrderID
	WHERE		swb.WayBillNumber IS NULL
	AND			cod.DelFlag <> 1
	AND			(@ShowBHE = 1 OR cod.ProductType NOT IN (46006, 46007, 46012)) --46006: Books, 46007: Music, 46012: Videos
	AND			cod.ProductType NOT IN (46023) -- Don't show TRT as this is shipped through Focus
	AND			(cod.StatusInstance in (510, 511))
	AND	NOT		(cod.IsShippedToAccount = 0 AND b.OrderQualifierID = 39009)
	AND			pl.ShipmentGroupID = sd.ShipmentGroupID

	SELECT		DISTINCT
				sd.OrderID,
				acc.Name SchoolAccountName,
				acc.Id SchoolAccountID
	INTO		#GroupData
	FROM		#ShipmentData sd
	JOIN		Batch b
					ON b.OrderID = sd.OrderID
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		Customer cust
					ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
											WHEN 0 THEN coh.CustomerBillToInstance
											ELSE		cod.CustomerShipToInstance
										END
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.Name = cust.FirstName
					AND	acc.Id <> b.AccountID
	WHERE		b.OrderQualifierID = 39006

	UPDATE	sd
	SET		sd.CampaignID = 0,
			sd.AccountID = SchoolAccountID,
			sd.AccountName = SchoolAccountName
	FROM	#ShipmentData sd
	JOIN	#GroupData gd
				ON	gd.OrderID = sd.OrderID

	UPDATE	s
	SET		s.FMEmailNotificationSent = @RunDate
	FROM	Shipment s
	JOIN	#ShipmentData srd
				ON	srd.ShipmentID = s.ID
END

SELECT	DMID,
		DMFirstName,
		DMLastName,
		FMID,
		FMFirstName,
		FMLastName,
		AccountID,
		AccountName,
		CampaignID,
		OrderID,
		OrderDate,
		OrderType,
		ShipToEntity,
		RequestedShipmentDate,
		ShipmentDate,
		Carrier,
		WaybillNumber,
		NumBoxes,
		CustomerOrderHeaderInstance,
		TransID,
		ProductCode,
		SAPMaterialNumber,
		ProductName,
		ProductType,
		QuantityOrdered,
		QuantityShipped,
		DistributionCenter,
		ProductReplacementReason,
		OnlineOrder,
		ProgramName,
		BrochureCode,
		StorageLocation
FROM	#ShipmentData
ORDER BY	DMID, FMID, AccountID, CampaignID, OrderID, WaybillNumber DESC, ProductCode
GO
