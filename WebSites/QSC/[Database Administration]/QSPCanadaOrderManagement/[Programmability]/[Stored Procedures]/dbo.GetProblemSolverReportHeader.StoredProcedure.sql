USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetProblemSolverReportHeader]    Script Date: 06/07/2017 09:19:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetProblemSolverReportHeader] 
        @ReportRequestID int, 
        @OrderID                int, 
        @CampaignID         int 
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



--Following lines are written by saqib on 13-Apr-2005 to update data driven subscription support tables
IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_ProblemSolverReport
   set  RunDateStart = getdate()
   where [id]  = @ReportRequestID

END

SET NOCOUNT OFF
GO
