USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_ValidateOrder]    Script Date: 06/07/2017 09:20:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_ValidateOrder]

	@OrderID				INT,
	@DistributionCenterID	INT,
	@IsOrderValid			BIT OUTPUT

AS

SET @IsOrderValid = CONVERT(BIT, 1)

DECLARE	@Error			BIT,
		@ErrorMessage	VARCHAR(1000),
		@RecExist BIT

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@ErrorMessage = 'Missing coh.CustomerBillToInstance'
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE		ISNULL(coh.CustomerBillToInstance, 0) = 0
AND			b.OrderID = @OrderID
AND			ISNULL(cod.DelFlag, 0) = 0 
AND			cod.DistributionCenterID = @DistributionCenterID

IF ISNULL(@Error, 0) = 1
BEGIN
	SELECT	TOP 1
			@RecExist = 1
	FROM	QSPCanadaCommon..SystemErrorLog 
	WHERE	OrderID = @OrderID
	AND		ISNULL(IsFixed, 0) = 0

	IF ISNULL(@RecExist, 0) <> 1
	BEGIN
		INSERT INTO QSPCanadaCommon..SystemErrorLog
		(
			ErrorDate,
			OrderID,
			ProcName,
			Desc1,
			IsFixed
		)
		VALUES
		(
			GETDATE(),
			@OrderID,
			'ShipmentRequest_ValidateOrder',
			@ErrorMessage,
			0
		)
	END
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@ErrorMessage = 'Missing coh.StudentInstance'
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE		ISNULL(coh.StudentInstance, 0) = 0
AND			b.OrderID = @OrderID
AND			ISNULL(cod.DelFlag, 0) = 0 
AND			cod.DistributionCenterID = @DistributionCenterID

IF ISNULL(@Error, 0) = 1
BEGIN
	SELECT	TOP 1
			@RecExist = 1
	FROM	QSPCanadaCommon..SystemErrorLog 
	WHERE	OrderID = @OrderID
	AND		ISNULL(IsFixed, 0) = 0

	IF ISNULL(@RecExist, 0) <> 1
	BEGIN
		INSERT INTO QSPCanadaCommon..SystemErrorLog
		(
			ErrorDate,
			OrderID,
			ProcName,
			Desc1,
			IsFixed
		)
		VALUES
		(
			GETDATE(),
			@OrderID,
			'ShipmentRequest_ValidateOrder',
			@ErrorMessage,
			0
		)
	END
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@ErrorMessage = 'Missing cod.PricingDetailsID'
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE		ISNULL(cod.PricingDetailsID, 0) = 0
AND			b.OrderID = @OrderID
AND			ISNULL(cod.DelFlag, 0) = 0 
AND			cod.DistributionCenterID = @DistributionCenterID

IF ISNULL(@Error, 0) = 1
BEGIN
	SELECT	TOP 1
			@RecExist = 1
	FROM	QSPCanadaCommon..SystemErrorLog 
	WHERE	OrderID = @OrderID
	AND		ISNULL(IsFixed, 0) = 0

	IF ISNULL(@RecExist, 0) <> 1
	BEGIN
		INSERT INTO QSPCanadaCommon..SystemErrorLog
		(
			ErrorDate,
			OrderID,
			ProcName,
			Desc1,
			IsFixed
		)
		VALUES
		(
			GETDATE(),
			@OrderID,
			'ShipmentRequest_ValidateOrder',
			@ErrorMessage,
			0
		)
	END
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@ErrorMessage = 'cod.Quantity = 0'
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE		ISNULL(cod.Quantity, 0) = 0
AND			b.OrderID = @OrderID
AND			ISNULL(cod.DelFlag, 0) = 0 
AND			cod.DistributionCenterID = @DistributionCenterID

IF ISNULL(@Error, 0) = 1
BEGIN
	SELECT	TOP 1
			@RecExist = 1
	FROM	QSPCanadaCommon..SystemErrorLog 
	WHERE	OrderID = @OrderID
	AND		ISNULL(IsFixed, 0) = 0

	IF ISNULL(@RecExist, 0) <> 1
	BEGIN
		INSERT INTO QSPCanadaCommon..SystemErrorLog
		(
			ErrorDate,
			OrderID,
			ProcName,
			Desc1,
			IsFixed
		)
		VALUES
		(
			GETDATE(),
			@OrderID,
			'ShipmentRequest_ValidateOrder',
			@ErrorMessage,
			0
		)
	END
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderValid = CONVERT(BIT, 0)
END

/*SELECT		TOP 1
			@Error = CONVERT(BIT, 1),
			@ErrorMessage = 'Missing camp.CookieDoughDeliveryDate'
FROM		Batch b
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE		cod.DistributionCenterID = 4 --Shamrock
AND			(ISNULL(camp.CookieDoughDeliveryDate, '1995-01-01 00:00:00.000') <= '2012-07-01' AND b.OrderDeliveryDate IS NULL)
AND			b.OrderID = @OrderID
AND			ISNULL(cod.DelFlag, 0) = 0
AND			cod.DistributionCenterID = @DistributionCenterID

IF ISNULL(@Error, 0) = 1
BEGIN
	SELECT	TOP 1
			@RecExist = 1
	FROM	QSPCanadaCommon..SystemErrorLog 
	WHERE	OrderID = @OrderID
	AND		ISNULL(IsFixed, 0) = 0

	IF ISNULL(@RecExist, 0) <> 1
	BEGIN
		INSERT INTO QSPCanadaCommon..SystemErrorLog
		(
			ErrorDate,
			OrderID,
			ProcName,
			Desc1,
			IsFixed
		)
		VALUES
		(
			GETDATE(),
			@OrderID,
			'ShipmentRequest_ValidateOrder',
			@ErrorMessage,
			0
		)
	END
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsOrderValid = CONVERT(BIT, 0)
END*/

DECLARE @IsCumulative BIT
SELECT		TOP 1
			@IsCumulative = 1
FROM		Batch b
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CampaignProgram cp
				ON	cp.CampaignID = camp.ID
				AND	cp.DeletedTF = 0
				AND	cp.ProgramID IN (11, 18, 22, 29, 33, 40, 43) --11: Planetary 18: Discover CA 22: Cumulative (Treasure Quest) 29: Cumulative (Prize Safari) 33: Cumulative (Go For Gold) 34: Cumulative (Game On)
JOIN	QSPCanadaCommon..CampaignProgram cpMag
				ON	cpMag.CampaignID = camp.ID
				AND	cpMag.DeletedTF = 0
				AND	cp.ProgramID IN (2) --1: Magazine
WHERE		b.OrderID = @OrderID
AND			b.OrderQualifierID IN (39001, 39002) --39001: Main 39002: Supplement
AND			cpMag.CampaignID IS NULL

IF ISNULL(@IsCumulative, 0) = 1
BEGIN

	DECLARE @Prizes INT
	SELECT	@Prizes = COUNT(*)
	FROM	Batch b
	JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
	JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
	WHERE	b.OrderID = @OrderID
	AND		cod.ProductType IN (46008, 46013, 46014, 46015) --46008: Incentives 46013: Incentives - Magazine 46014: Incentives - Gift 46015: Incentives - Food
	AND		ISNULL(cod.DelFlag, 0) = 0 

	IF ISNULL(@Prizes, 0) < 1
	BEGIN
		SET @Error = CONVERT(BIT, 1)
		SET @ErrorMessage = 'Order not picked because its prizes are not calculated'
	END

	IF ISNULL(@Error, 0) = 1
	BEGIN
		SELECT	TOP 1
				@RecExist = 1
		FROM	QSPCanadaCommon..SystemErrorLog 
		WHERE	OrderID = @OrderID
		AND		ISNULL(IsFixed, 0) = 0

		IF ISNULL(@RecExist, 0) <> 1
		BEGIN
			INSERT INTO QSPCanadaCommon..SystemErrorLog
			(
				ErrorDate,
				OrderID,
				ProcName,
				Desc1,
				IsFixed
			)
			VALUES
			(
				GETDATE(),
				@OrderID,
				'ShipmentRequest_ValidateOrder',
				@ErrorMessage,
				0
			)
		END
		
		SET @Error = CONVERT(BIT, 0)
		SET @IsOrderValid = CONVERT(BIT, 0)
	END

	IF ISNULL(@Prizes, 0) > 0
	BEGIN
		DECLARE @Students INT
		SELECT	@Students = COUNT(StudentInstance)
		FROM

		(SELECT	DISTINCT coh.StudentInstance --Landed Students
		FROM	Batch b
		JOIN	CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
		JOIN	CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
		WHERE	b.OrderID = @OrderID
		AND		ISNULL(cod.DelFlag, 0) = 0 
		AND		cod.ProductType NOT IN (46008, 46013, 46014, 46015) --46008: Incentives 46013: Incentives - Magazine 46014: Incentives - Gift 46015: Incentives - Food

		UNION --Union Landed and Online Students together, removing any duplicates

		SELECT		DISTINCT map.StudentInstance --Online Students
		FROM		Batch b
		JOIN		CustomerOrderHeader coh
						ON	coh.OrderBatchID = b.ID
						AND	coh.OrderBatchDate = b.Date
		JOIN		CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
		JOIN		OnlineOrderMappingTable map
					   ON	map.LandedOrderID = b.OrderID 
		WHERE		ISNULL(cod.DelFlag, 0) = 0 
		AND			cod.ProductType NOT IN (46008, 46013, 46014, 46015) --46008: Incentives 46013: Incentives - Magazine 46014: Incentives - Gift 46015: Incentives - Food
		AND			b.OrderID = @OrderID) AS Student

		IF ISNULL(@Prizes, 0) <> ISNULL(@Students, 0)
		BEGIN
			SET @Error = CONVERT(BIT, 1)
			SET @ErrorMessage = 'Order not picked because the number of prizes and students are not equal'
		END

		IF @Error = 1
		BEGIN
			SELECT	TOP 1
					@RecExist = 1
			FROM	QSPCanadaCommon..SystemErrorLog 
			WHERE	OrderID = @OrderID
			AND		ISNULL(IsFixed, 0) = 0

			IF ISNULL(@RecExist, 0) <> 1
			BEGIN
				INSERT INTO QSPCanadaCommon..SystemErrorLog
				(
					ErrorDate,
					OrderID,
					ProcName,
					Desc1,
					IsFixed
				)
				VALUES
				(
					GETDATE(),
					@OrderID,
					'ShipmentRequest_ValidateOrder',
					@ErrorMessage,
					0
				)
			END
			
			SET @Error = CONVERT(BIT, 0)
			SET @IsOrderValid = CONVERT(BIT, 0)
		END
	END
END

PRINT @IsOrderValid
GO
