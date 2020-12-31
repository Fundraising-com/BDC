USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_PreviewAdjustmentBatch]    Script Date: 06/07/2017 09:17:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_PreviewAdjustmentBatch]

	@iAdjustmentTypeID	INT,
	@dDateFrom			DATETIME,
	@dDateTo			DATETIME

AS

DECLARE	@zAdjustmentTypeName	VARCHAR(50)

IF(@iAdjustmentTypeID IN (49028, 49030))	-- Online, Cust Svc
BEGIN

	CREATE TABLE	#AdjustmentsToPreview
					(AdjustmentID			INT IDENTITY,
					CampaignID				INT,
					OrderId					INT,
					Amount					NUMERIC(14, 6))

	SET		@dDateTo = CONVERT(DATETIME, CONVERT(VARCHAR, @dDateTo, 112))

	IF(@iAdjustmentTypeID = 49028)		-- Online
	BEGIN
		INSERT INTO	#AdjustmentsToPreview
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
		INSERT INTO	#AdjustmentsToPreview
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

	SELECT		atp.AdjustmentID AS ADJUSTMENT_ID,
				acc.ID AS ACCOUNT_ID,
				acc.Name AS AccountName,
				camp.StartDate AS CampaignStartDate,
				camp.EndDate AS CampaignEndDate,
				50601 AS ACCOUNT_TYPE_ID,
				@iAdjustmentTypeID AS ADJUSTMENT_TYPE_ID,
				DATEADD(DAY, -1, @dDateTo) AS ADJUSTMENT_EFFECTIVE_DATE, --04/10/2007: JM: Set to day before so it's not included in future statement run in [GetAllStatementsByCampaignToPrint]
				SUM(atp.Amount) AS ADJUSTMENT_AMOUNT,
				'' AS NOTE_TO_PRINT,
				GETDATE() AS DATE_CREATED,
				'1995-01-01' AS DATETIME_MODIFIED,
				'' AS LAST_UPDATED_BY,
				'CA' AS COUNTRY_CODE,
				@zAdjustmentTypeName AS INTERNAL_COMMENT,
				0 AS ORDER_ID,
				camp.ID AS CAMPAIGN_ID,
				0 AS ADJUSTMENT_BATCH_ID
	FROM		#AdjustmentsToPreview atp
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = atp.CampaignID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = camp.BillToAccountID
	GROUP BY 	acc.ID,
				acc.Name,
				camp.Id,
				camp.StartDate,
				camp.EndDate,
				atp.AdjustmentID

	DROP TABLE #AdjustmentsToPreview
END
GO
