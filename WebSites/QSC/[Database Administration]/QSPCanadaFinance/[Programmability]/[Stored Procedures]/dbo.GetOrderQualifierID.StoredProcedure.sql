USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderQualifierID]    Script Date: 06/07/2017 09:17:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetOrderQualifierID] 
        @OrderID         int, 
        @OrderQualifierID      int output 
AS 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--   MTC 8/30/2004 
--   Find out Order Qualifier ID For Canada Finance System 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
SET NOCOUNT ON 

SELECT TOP 1 @OrderQualifierID = OrderQualifierID 
FROM QSPCanadaOrderManagement..Batch   
WHERE OrderID = @OrderID 

SET NOCOUNT OFF
GO
