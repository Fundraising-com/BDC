USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_BusinessUnit_IsTimeOrder]    Script Date: 06/07/2017 09:17:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_BusinessUnit_IsTimeOrder]
(
	@OrderID INT
)

RETURNS BIT

AS

BEGIN
	
	RETURN
	(
		SELECT		TOP 1 CONVERT(BIT, 1)
		FROM		QSPCanadaOrderManagement..Batch b
		JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
						ON	coh.OrderBatchID = b.ID
						AND	coh.OrderBatchDate = b.Date
		JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
		LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
						ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
						AND	codrh.TransID = codrh.TransID
		LEFT JOIN	QSPCanadaOrderManagement..RemitBatch rb
						ON	rb.ID = codrh.RemitBatchID
		LEFT JOIN	QSPCanadaOrderManagement..ShipmentRequestOrder sro
						ON	sro.OrderID = b.OrderID
		WHERE		b.OrderID = @OrderID
		AND			(rb.RunID <= 1382 OR sro.DateCreated < '2012-01-20')
	)

END
GO
