USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderHeader_SelectAllByBatch]    Script Date: 06/07/2017 09:19:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderHeader_SelectAllByBatch]
	@daOrderBatchDate varchar(20),
	@iOrderBatchID int
	
AS

SELECT	*
FROM	CustomerOrderHeader
WHERE	OrderBatchDate = @daOrderBatchDate
AND		OrderBatchID = @iOrderBatchID
GO
