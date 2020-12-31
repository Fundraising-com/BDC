USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetFirstOrderForBatch]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetFirstOrderForBatch] 
	@batchdate datetime,
	@batchid int
AS

Select top 1 OrderBatchDate, OrderBatchID from CustomerOrderHeader where OrderBatchDate = @batchdate and  OrderBatchID = @batchid
GO
