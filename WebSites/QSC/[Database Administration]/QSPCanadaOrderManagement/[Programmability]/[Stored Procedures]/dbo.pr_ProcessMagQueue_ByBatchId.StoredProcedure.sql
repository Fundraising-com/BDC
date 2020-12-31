USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProcessMagQueue_ByBatchId]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_ProcessMagQueue_ByBatchId]

@OrderId int

 AS

/*
---- Update COD's affected with new status.
UPDATE
	QSPCanadaOrderManagement..CustomerOrderDetail
SET
	StatusInstance = 508
FROM
	QSPCanadaOrderManagement..CustomerOrderDetail A
	INNER JOIN QSPCanadaOrderManagement..CustomerOrderHeader B ON A.CustomerOrderHeaderInstance = B.Instance 
	INNER JOIN QSPCanadaOrderManagement..Batch C ON B.OrderBatchId = C.Id AND B.OrderBatchDate = C.Date
WHERE
	C.OrderId = @OrderId

*/
---- UPDATE THE OVERALL BATCH STATUS HERE
UPDATE 
	QSPCanadaOrderManagement..Batch
SET	
	 IsMagQueueDone = 1
WHERE
	OrderId = @OrderId
GO
