USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetProblemSolverReportHeader]    Script Date: 06/07/2017 09:17:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetProblemSolverReportHeader] 
        @OrderID        int, 
        @CampaignID        int 
AS 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--   MTC 8/31/2004 
--   Get Problem Solver Header Information For Canada Finance System 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
SET NOCOUNT ON 

SELECT FirstName + ' ' + LastName as FMName, C.FMID, B.CampaignID, OrderID, DateReceived 
FROM QSPCanadaOrderManagement..Batch B   
INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID 
INNER JOIN QSPCanadaCommon..FieldManager FM on FM.FMID = C.FMID 
WHERE B.OrderID = @OrderID 
        AND B.CampaignID = @CampaignID 

SET NOCOUNT OFF
GO
