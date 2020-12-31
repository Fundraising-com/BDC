USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateCustomerEmailByCOD]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateCustomerEmailByCOD]

	@iCustomerOrderHeaderInstance int,
	@iTransID int,
	@Email varchar(50)
AS

UPDATE	cust
SET		Email = @Email
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		cod.TransID = @iTransID

DECLARE	@ProductType INT,
		@StatusInstance INT,
		@Status	VARCHAR(50)

SELECT	@ProductType = cod.ProductType,
		@StatusInstance = cod.StatusInstance
FROM	CustomerOrderDetail cod
WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		cod.TransID = @iTransID

IF (@ProductType = 46001 AND @StatusInstance = 501)
BEGIN

	UPDATE	cod
	SET		StatusInstance = 502
	FROM	CustomerOrderDetail cod
	WHERE	cod.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
	AND		cod.TransID = @iTransID

	exec spRemitIndividualItem @iCustomerOrderHeaderInstance, @iTransID, @Status output
END

DECLARE		@OrderID int
SELECT		@OrderID = b.OrderID
FROM		Batch b
LEFT JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
WHERE		coh.Instance = @iCustomerOrderHeaderInstance

Update	ReportRequestBatch_OrderEntryFollowupReport
Set		createdate = getdate(), QUEUEDATE = NULL,RUNDATESTART= NULL,FILENAME =NULL 
where	reportrequestbatchid  IN (select id from ReportRequestBatch
								where batchorderid  = @OrderID)
GO
