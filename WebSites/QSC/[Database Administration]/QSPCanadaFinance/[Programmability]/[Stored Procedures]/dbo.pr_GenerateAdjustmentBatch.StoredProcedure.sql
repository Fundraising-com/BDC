USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateAdjustmentBatch]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GenerateAdjustmentBatch]

	@iAdjustmentTypeID	INT,
	@dDateFrom			DATETIME,
	@dDateTo			DATETIME,
	@iUserID			INT

AS

DECLARE	@iAdjustmentBatchID		INT,
		@zAdjustmentTypeName	VARCHAR(50)

SET		@iAdjustmentBatchID = 0

IF(@iAdjustmentTypeID IN (49028, 49030)) -- Online, Cust Svc
BEGIN
	-- Group in an AdjustmentBatch
	INSERT INTO	AdjustmentBatch
				(AdjustmentTypeID,
				[Status],
				DateFrom,
				DateTo,
				CreateUserID,
				CreateDate,
				ChangeUserID,
				ChangeDate)
	VALUES		(@iAdjustmentTypeID,
				64001,
				@dDateFrom,
				@dDateTo,
				@iUserID,
				GETDATE(),
				NULL,
				NULL)
	
	SET	@iAdjustmentBatchID = SCOPE_IDENTITY()
	
	CREATE TABLE #AdjustmentsToMake
	(CampaignID	INT,
	 OrderID	INT,
	 Amount		NUMERIC(14, 6))

	SET	@dDateTo = CONVERT(DATETIME, CONVERT(VARCHAR, @dDateTo, 112))

	IF(@iAdjustmentTypeID = 49028) -- Online
	BEGIN
		INSERT INTO	#AdjustmentsToMake
					(CampaignID,
					 OrderId,
					 Amount)
		SELECT		b.CampaignID,
					b.OrderID,
					ROUND(SUM((cod.Price - (cod.Tax + cod.Tax2)) * cp.GroupProfit * .01), 6)
		FROM		QSPCanadaOrderManagement..Batch b	 	        
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.ID = b.CampaignID
						AND	camp.IsStaffOrder = 0
		JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
						ON	coh.OrderBatchID = b.ID
						AND	coh.OrderBatchDate = b.[Date]
		JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
						AND	cod.DelFlag = 0
		LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
						ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
						AND	codrh.TransID = cod.TransID
		JOIN		QSPCanadaCommon..CampaignProgram cp
						ON	cp.CampaignID = camp.ID
						AND	cp.ProgramID IN (1,2)
						AND cp.DeletedTF = 0
		WHERE		b.OrderQualifierID = 39009
		AND			cod.CreationDate BETWEEN CONVERT(NVARCHAR, @dDateFrom, 101) AND CONVERT(NVARCHAR, @dDateTo, 101)
		AND			(codrh.Status IN (42000, 42001, 42004, 42010)
		OR			cod.StatusInstance = 508)
		GROUP BY	b.CampaignID,
					b.OrderID,
					cp.GroupProfit
	END
	ELSE IF(@iAdjustmentTypeID = 49030)	-- Cust Svc
	BEGIN
		INSERT INTO	#AdjustmentsToMake
					(CampaignID,
					 OrderId,
					 Amount)
		SELECT		b.CampaignID,
					b.OrderID,
					ROUND(SUM((cod.Price - (cod.Tax + cod.Tax2)) * cp.GroupProfit * .01), 6)
		FROM		QSPCanadaOrderManagement..Batch b	 	        
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.ID = b.CampaignID
						AND	camp.IsStaffOrder = 0
		JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
						ON	coh.OrderBatchID = b.ID
						AND	coh.OrderBatchDate = b.Date
		JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
						AND	cod.DelFlag = 0
		LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
						ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
						AND	codrh.TransID = cod.TransID
		JOIN		QSPCanadaCommon..CampaignProgram cp
						ON	cp.CampaignID = camp.ID
						AND	cp.ProgramID IN (1, 2)
						AND cp.DeletedTF = 0
		LEFT JOIN	QSPCanadaOrderManagement..CustomerPaymentHeader cph
						ON	cph.CustomerOrderHeaderInstance = coh.Instance 
		LEFT JOIN	QSPCanadaOrderManagement..CreditCardPayment ccp
						ON	ccp.CustomerPaymentHeaderInstance = cph.Instance
		WHERE		b.OrderQualifierID IN (39013, 39015)
		AND			(coh.PaymentMethodInstance = 50002 OR ccp.StatusInstance = 19000) -- Cash/Cheque or CC Payment Good
		AND			cod.CreationDate BETWEEN CONVERT(NVARCHAR, @dDateFrom, 101) AND CONVERT(NVARCHAR, @dDateTo, 101)
		AND			(codrh.Status IN (42000, 42001, 42004, 42010)
		OR			cod.StatusInstance = 508)
		GROUP BY	b.CampaignID,
					b.OrderID,
					cp.GroupProfit
	END

	SELECT	@zAdjustmentTypeName = at.Name
	FROM	Adjustment_Type at
	WHERE	at.Adjustment_Type_ID = @iAdjustmentTypeID

	-- Insert the Adjustments
	INSERT INTO	QSPCanadaFinance..Adjustment
				(ACCOUNT_ID,
				ACCOUNT_TYPE_ID,
				ADJUSTMENT_TYPE_ID,
				ADJUSTMENT_EFFECTIVE_DATE,
				ADJUSTMENT_AMOUNT,
				DATE_CREATED,
				LAST_UPDATED_BY,
				COUNTRY_CODE,
				INTERNAL_COMMENT,
				CAMPAIGN_ID,
				ADJUSTMENT_BATCH_ID)
	SELECT		camp.BillToAccountID,
				50601,
				@iAdjustmentTypeID,
				DATEADD(DAY, -1, @dDateTo), --04/10/2007: JM: Set to day before so it's not included in future statement run in [GetAllStatementsByCampaignToPrint]
				SUM(atm.Amount),
				GETDATE(),
				CONVERT(VARCHAR, @iUserID),
				'CA',
				@zAdjustmentTypeName,
				camp.ID,
				@iAdjustmentBatchID
	FROM		#AdjustmentsToMake atm
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = atm.CampaignID
	GROUP BY 	camp.BillToAccountID,
				camp.ID

	DROP TABLE #AdjustmentsToMake
END

SELECT	@iAdjustmentBatchID
GO
