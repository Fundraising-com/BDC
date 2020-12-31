USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Kanata_Batch_Update]    Script Date: 06/07/2017 09:20:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Kanata_Batch_Update]

	@iOrderID					int,
	@iOrderQualifierID			int,
	@dOrderDeliveryDate			datetime

AS

SET NOCOUNT ON

UPDATE	Batch
SET		OrderQualifierID = @iOrderQualifierID,
		OrderDeliveryDate = @dOrderDeliveryDate,
		ChangeDate = GetDate()
WHERE	OrderID = @iOrderID

SET NOCOUNT OFF
GO
