USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetProblemSolverReportToPrint]    Script Date: 06/07/2017 09:17:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProblemSolverReportToPrint]
	@FromDate 	datetime,
	@ToDate 	datetime
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 9/1/2004 
--   Get Problem Solver Reports ready to print For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT OrderID,
	C.ID as CampaignID,
	A.Name,
	MAX(C.FMID) AS FMID, 
	MAX(FM.LastName) as LastName,
	MAX(FM.FirstName) as FirstName,
	MAX(C.Lang) as Lang
FROM QSPCanadaOrderManagement..Batch B 
	INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
	INNER JOIN QSPCanadaCommon..FieldManager FM on FM.FMID = C.FMID
	INNER JOIN QSPCanadaCommon..CAccount A on C.BillToAccountID = A.ID 
WHERE StartDate BETWEEN  @FromDate AND @ToDate
GROUP BY OrderID, A.Name, C.ID
ORDER BY A.Name

SET NOCOUNT OFF
GO
