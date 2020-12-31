USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CAManagementReportByFMByProgram]    Script Date: 06/07/2017 09:19:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CAManagementReportByFMByProgram] @FmId Int, 
						@CAStartDate DateTime, 
						@CAEndDate DateTime, 
						@CAStatus Int, 
						@CAProgram Int
AS

CREATE TABLE #AllAccounts  (
			FMID			INT, 
			LastName		VARCHAR(50), 
			FirstName		VARCHAR(50), 
			Programs		VARCHAR(130),
			EstimatedGross	NUMERIC(10,2) 
			)

DECLARE @CurrentSeasonStartDate DateTime
DECLARE @CurrentSeasonEndDate  DateTime

SELECT @CurrentSeasonStartDate=StartDate,@CurrentSeasonEndDate=EndDate     
FROM QSPCanadacommon.dbo.Season 
WHERE @CAStartDate Between StartDate And EndDate
AND Season='Y' --Fiscal Year

INSERT INTO #AllAccounts
SELECT	F.FMID AS FMID, 
		F.LastName AS LastName, 
		F.FirstName AS FirstName, 
		QspCanadaOrderManagement.dbo.UDF_MainProgramsbyCampaign(C.ID) Programs,
		SUM(ISNULL(C.EstimatedGross, 0)) AS EstimatedGross
FROM	QSPCanadaCommon.dbo.Campaign C INNER JOIN
		QSPCanadaCommon.dbo.FieldManager F ON C.FMID = F.FMID
WHERE	CAST(CONVERT(VARCHAR(10),C.StartDate,101)  AS DATETIME)  >=  ISNULL(CAST(@CAStartDate AS DATETIME),  CAST(CONVERT(VARCHAR(10),C.StartDate,101)  AS DATETIME) )
AND     CAST(CONVERT(VARCHAR(10),C.StartDate,101)  AS DATETIME)  <=   ISNULL(CAST(@CAEndDate  AS DATETIME),  CAST(CONVERT(VARCHAR(10), C.StartDate,101)  AS DATETIME) )
AND     C.STATUS = ISNULL(@CAStatus,C.STATUS)
AND     C.Id In (SELECT CampaignId FROM QSPCanadaCommon.dbo.CampaignProgram 
	   	           WHERE ProgramID = ISNULL(@CAProgram, ProgramID) and DeletedTF=0 )	
GROUP BY	F.FMID,
			F.LastName,
			F.FirstName,
			QspCanadaOrderManagement.dbo.UDF_MainProgramsbyCampaign(C.ID)
	
SELECT  * FROM #AllAccounts ORDER BY LastName,FirstName 

Drop Table #AllAccounts
GO
