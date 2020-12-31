USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAdjustmentAmountForOrder]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAdjustmentAmountForOrder]
	@OrderID 		int	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/3292004 
--   Get Adjustment Amount For Canada Finance System (Credits)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT ISNULL(SUM(Adjustment_Amount),0) as AdjustmentAmount
FROM Adjustment
WHERE Order_ID = @OrderID

SET NOCOUNT OFF
GO
