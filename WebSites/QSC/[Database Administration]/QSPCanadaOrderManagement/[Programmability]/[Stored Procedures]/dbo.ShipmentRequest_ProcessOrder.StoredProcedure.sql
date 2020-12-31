USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_ProcessOrder]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_ProcessOrder]

	@OrderID					INT,
	@DistributionCenterID		INT,
	@ShipmentRequestBatchID		INT OUTPUT

AS

DECLARE	@ShipmentRequestOrderID					INT,
		@ShipmentRequestCustomerOrderHeaderID	INT

DECLARE	@ServiceRequest		VARCHAR(20)
SET	@ServiceRequest = 'Ground'


BEGIN TRANSACTION

INSERT INTO ShipmentRequestBatch
(
	[Filename]
)
VALUES
(
	NULL
)

SET @ShipmentRequestBatchID = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@ShipmentRequestBatchID, 0) = 0
BEGIN
	ROLLBACK
	SET	@ShipmentRequestBatchID = 0
	RETURN
END

DECLARE	@OrderType			INT,
		@PDFFilename		VARCHAR(100),
		@CourierRequest		VARCHAR(100),
		@RequestedShipDate	DATETIME

DECLARE	ShipmentRequestOrder CURSOR FOR
SELECT		DISTINCT
			b.OrderID,
			CASE cod.ProductType
				WHEN 46006 THEN 1
				WHEN 46007 THEN 1
				WHEN 46012 THEN 1
				--WHEN 46018 THEN 3
				ELSE			2
			END AS OrderType,
			CASE cod.ProductType
				WHEN 46006 THEN ''
				WHEN 46007 THEN ''
				WHEN 46012 THEN ''
				--WHEN 46018 THEN ''
				ELSE			SUBSTRING(ISNULL(ost.PDFFileName, ''), 0, 20)
			END AS PDFFileName,			
			/*CASE cod.ProductType
				WHEN 46006 THEN 'Canada Post'
				WHEN 46007 THEN 'Canada Post'
				WHEN 46012 THEN 'Canada Post'
				--WHEN 46018 THEN '20/20'
				ELSE			'Purolator'
			END,*/
			carrier.[Description] AS Carrier,
			@ServiceRequest,
			CASE
				--WHEN @DistributionCenterID = 4 AND b.OrderDeliveryDate IS NULL THEN camp.CookieDoughDeliveryDate
				--WHEN @DistributionCenterID = 4 AND b.OrderDeliveryDate IS NOT NULL THEN b.OrderDeliveryDate
				WHEN b.OrderDeliveryDate IS NULL THEN	GETDATE()
				ELSE b.OrderDeliveryDate - CASE @DistributionCenterID
												WHEN 3 THEN	(prov.Lapse_Days_Delivery + prov.Lapse_Days_Chocolate_Prep)
												ELSE		(prov.Lapse_Days_Delivery + prov.Lapse_Days_Field_Supply_Prep)
											END
			END
FROM		Batch b
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
JOIN		QSPCanadaCommon..Province prov
				ON	prov.Province_Code = cust.[State]
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
LEFT JOIN	OrderStageTracking ost
				ON	ost.OrderID = b.OrderID
				AND	ost.PDFFilename IN (SELECT TOP 1 PDFFilename FROM OrderStageTracking WHERE OrderID = b.OrderID ORDER BY PDFFilename DESC, ID DESC)
JOIN		QSPCanadaCommon..CodeDetail carrier
				ON	carrier.Instance = dbo.udf_GetCarrierByOrderID(b.OrderID)
WHERE		b.OrderID = @OrderID
AND			cod.DistributionCenterID = @DistributionCenterID
AND			cod.StatusInstance = 509 --509: Order Detail Pending to TPL
AND			cod.Delflag <> 1			
ORDER BY	b.OrderID,
			OrderType


OPEN ShipmentRequestOrder
FETCH NEXT FROM ShipmentRequestOrder INTO @OrderID, @OrderType, @PDFFilename, @CourierRequest, @ServiceRequest, @RequestedShipDate

WHILE @@FETCH_STATUS = 0
BEGIN	INSERT INTO ShipmentRequestOrder
	(
		ShipmentRequestBatchID,
		OrderID,
		OrderType,
		PDFFilename,
		CourierRequest,
		ServiceRequest,
		RequestedShipDate
	)
	SELECT	@ShipmentRequestBatchID,
			@OrderID,
			@OrderType,
			@PDFFilename,
			@CourierRequest,
			@ServiceRequest,
			@RequestedShipDate

	SET @ShipmentRequestOrderID = SCOPE_IDENTITY()

	IF @@ERROR <> 0 OR ISNULL(@ShipmentRequestOrderID, 0) = 0
	BEGIN
		ROLLBACK
		SET	@ShipmentRequestBatchID = 0
		RETURN
	END


	DECLARE	ShipmentRequestCustomerOrderHeader CURSOR FOR
	SELECT		DISTINCT
				coh.Instance,
				SUBSTRING(ISNULL(ISNULL(stud.FirstName, '') + ' ' + ISNULL(stud.LastName, ''), ''), 1, 30),
				teach.Instance,
				SUBSTRING(ISNULL(teach.LastName, ''), 1, 20)
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		Student stud
					ON	stud.Instance = coh.StudentInstance
	JOIN		Teacher teach
					ON	teach.Instance = stud.TeacherInstance
	WHERE		b.OrderID = @OrderID
	AND			cod.DistributionCenterID = @DistributionCenterID
	AND			cod.StatusInstance = 509 --509: Order Detail Pending to TPL
	AND			cod.Delflag <> 1			
	AND			CASE cod.ProductType
					WHEN 46006 THEN 1
					WHEN 46007 THEN 1
					WHEN 46012 THEN 1
					--WHEN 46018 THEN 3
					ELSE			2 
				END = @OrderType
	ORDER BY	coh.Instance

	DECLARE	@CustomerOrderHeaderInstance	INT,
			@StudentName					VARCHAR(100),
			@ClassID						INT,
			@ClassName						VARCHAR(100)

	OPEN ShipmentRequestCustomerOrderHeader
	FETCH NEXT FROM ShipmentRequestCustomerOrderHeader INTO @CustomerOrderHeaderInstance, @StudentName, @ClassID, @ClassName

	WHILE @@FETCH_STATUS = 0
	BEGIN			INSERT INTO ShipmentRequestCustomerOrderHeader
		(
			ShipmentRequestOrderID,
			CustomerOrderHeaderInstance,
			StudentName,
			ClassID,
			ClassName
		)
		SELECT	@ShipmentRequestOrderID,
				@CustomerOrderHeaderInstance,
				@StudentName,
				@ClassID,
				@ClassName

		SET @ShipmentRequestCustomerOrderHeaderID = SCOPE_IDENTITY()

		IF @@ERROR <> 0 OR ISNULL(@ShipmentRequestOrderID, 0) = 0
		BEGIN
			ROLLBACK
			SET	@ShipmentRequestBatchID = 0
			RETURN
		END


		DECLARE	ShipmentRequestCustomerOrderDetail CURSOR FOR
		SELECT		DISTINCT
					cod.TransID,
					SUBSTRING(ISNULL(cod.ProductCode, ''), 1, 20),
					ISNULL(cod.Quantity, 0),
					CASE @OrderType
						WHEN 1 THEN	SUBSTRING(ISNULL(ISNULL(cod.Recipient, ISNULL(cust.FirstName, '') + ' ' + ISNULL(cust.LastName, '')), ''), 1, 35)
						ELSE		SUBSTRING(ISNULL(acc.Name , ''), 1, 35)
					END,					
					SUBSTRING(ISNULL(cust.Address1, ''), 1, 35),
					SUBSTRING(ISNULL(cust.Address2, ''), 1, 35),
					SUBSTRING(ISNULL(cust.City, ''), 1, 35),
					SUBSTRING(ISNULL(cust.Zip, ''), 1, 10),
					'CA',
					SUBSTRING(ISNULL(cust.State, ''), 1, 2),
					CASE WHEN cod.ProductType IN (46002, 46008, 46014, 46018, 46019) THEN	SUBSTRING(ISNULL(cont.Firstname + ' ' + cont.LastName, 'Fundraiser Coordinator'), 1, 35)
						 ELSE																SUBSTRING(ISNULL(cust.Firstname + ' ' + cust.LastName, 'Fundraiser Coordinator'), 1, 35)
					END,
					CASE WHEN cod.ProductType IN (46002, 46008, 46014, 46018, 46019) THEN	SUBSTRING(ISNULL(contPh.PhoneNumber, '1-800-667-2536'), 1, 20)
						 ELSE																SUBSTRING(ISNULL(cust.Phone, '1-800-667-2536'), 1, 20)
					END
		FROM		Batch b
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
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.ID = b.CampaignID
		JOIN		QSPCanadaCommon..CAccount acc
						ON	acc.ID = camp.ShipToAccountID
		JOIN		QSPCanadaCommon..Contact cont
						ON	cont.ID = camp.ShipToCampaignContactID
		LEFT JOIN	QSPCanadaCommon..Phone contPh
						ON	contPh.PhoneListID = cont.PhoneListID
						AND	contPh.[Type] = 30501
		WHERE		b.OrderID = @OrderID
		AND			cod.DistributionCenterID = @DistributionCenterID
		AND			cod.StatusInstance = 509 --509: Order Detail Pending to TPL
		AND			cod.Delflag <> 1			
		AND			coh.Instance = @CustomerOrderHeaderInstance
		AND			CASE cod.ProductType
						WHEN 46006 THEN 1
						WHEN 46007 THEN 1
						WHEN 46012 THEN 1
						--WHEN 46018 THEN 3
						ELSE			2 
					END = @OrderType
		ORDER BY	cod.TransID

		DECLARE	@TransID			INT,
				@ProductCode		VARCHAR(100),
				@QtyOrder			INT,
				@ShipToName			VARCHAR(100),
				@ShipToAddress1		VARCHAR(100),
				@ShipToAddress2		VARCHAR(100),
				@ShipToCity			VARCHAR(100),
				@ShipToZipCode		VARCHAR(100),
				@ShipToCountry		VARCHAR(100),
				@ShipToProvince		VARCHAR(100),
				@ShipToContactName	VARCHAR(100),
				@ShipToPhoneNumber	VARCHAR(100)

		OPEN ShipmentRequestCustomerOrderDetail
		FETCH NEXT FROM ShipmentRequestCustomerOrderDetail INTO @TransID, @ProductCode, @QtyOrder,	@ShipToName, @ShipToAddress1,
				@ShipToAddress2,@ShipToCity, @ShipToZipCode, @ShipToCountry, @ShipToProvince, @ShipToContactName, @ShipToPhoneNumber

		WHILE @@FETCH_STATUS = 0
		BEGIN				
			INSERT INTO ShipmentRequestCustomerOrderDetail
			(
				ShipmentRequestCustomerOrderHeaderID,
				TransID,
				ProductCode,
				QtyOrder,
				ShipToName,
				ShipToAddress1,
				ShipToAddress2,
				ShipToCity,
				ShipToZipCode,
				ShipToCountry,
				ShipToProvince,
				ShipToContactName,
				ShipToPhoneNumber
			)
			SELECT	@ShipmentRequestCustomerOrderHeaderID,
					@TransID,
					@ProductCode,
					@QtyOrder,
					@ShipToName,
					@ShipToAddress1,
					@ShipToAddress2,
					@ShipToCity,
					@ShipToZipCode,
					@ShipToCountry,
					@ShipToProvince,
					@ShipToContactName,
					@ShipToPhoneNumber


			FETCH NEXT FROM ShipmentRequestCustomerOrderDetail INTO @TransID, @ProductCode, @QtyOrder,	@ShipToName, @ShipToAddress1,
					@ShipToAddress2,@ShipToCity, @ShipToZipCode, @ShipToCountry, @ShipToProvince, @ShipToContactName, @ShipToPhoneNumber
			
		END
		CLOSE ShipmentRequestCustomerOrderDetail
		DEALLOCATE ShipmentRequestCustomerOrderDetail


		FETCH NEXT FROM ShipmentRequestCustomerOrderHeader INTO @CustomerOrderHeaderInstance, @StudentName, @ClassID, @ClassName
		
	END
	CLOSE ShipmentRequestCustomerOrderHeader
	DEALLOCATE ShipmentRequestCustomerOrderHeader

	FETCH NEXT FROM ShipmentRequestOrder INTO @OrderID, @OrderType, @PDFFilename, @CourierRequest, @ServiceRequest, @RequestedShipDate
	
END
CLOSE ShipmentRequestOrder
DEALLOCATE ShipmentRequestOrder

COMMIT TRANSACTION
GO
