USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Reports_For_Combining]    Script Date: 06/07/2017 09:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Get_Reports_For_Combining]

AS

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempCombine]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempCombine]

CREATE TABLE [dbo].[#TempCombine] (
	[CombinedReportRequestId] [int] NOT NULL ,
	[ReportRequestId] [int] NULL ,
	[ReportStatus] [varchar] (500) NULL 
) ON [PRIMARY]


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempCombine2]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempCombine2]

CREATE TABLE [dbo].[#TempCombine2] (
	[CombinedReportRequestId] [int] NOT NULL ,
	[TotalReports] [int] NULL ,
	[ReportsFinished] [int] NULL 
) ON [PRIMARY]


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempCombine3]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempCombine3]

CREATE TABLE [dbo].[#TempCombine3] (
	[CombinedReportRequestId] [int] NOT NULL ,
	[ReportsFinished] [int] NULL 
) ON [PRIMARY]

INSERT INTO
	#TempCombine
SELECT
	Distinct
	B.CombinedReportRequestId
	, B.ReportRequestId
	, D.LastStatus
FROM
	ReportRequest A
	INNER JOIN ReportCombination B ON A.Id = B.CombinedReportRequestId
	INNER JOIN ReportRequest C ON B.ReportRequestId = C.Id
	LEFT OUTER JOIN [USPVL2K54].ReportServer_DMZ2K15.dbo.Subscriptions D ON C.RSSubscriptionId LIKE D.SubscriptionId
	
WHERE
	A.ReportTypeId = 13
	AND IsNull(A.RSSubscriptionID, '') = ''  --- Being used as a status flag
	

--SELECT * FROM #TempCombine

INSERT INTO
	#TempCombine2
SELECT
	CombinedReportRequestId
	, Count(ReportRequestId)
	, 0
FROM
	#TempCombine
GROUP BY
	CombinedReportRequestId


--SELECT * FROM #TempCombine2

UPDATE #TempCombine SET ReportStatus = ' was written to ' WHERE ReportRequestId IN (7114,7116,7117)

INSERT INTO
	#TempCombine3
SELECT
	CombinedReportRequestId
	, Count(ReportRequestId)
FROM
	#TempCombine
WHERE
	PATINDEX('%was written to%', ReportStatus) > 0
GROUP BY
	CombinedReportRequestId


--SELECT * FROM #TempCombine3

UPDATE
	#TempCombine2
SET
	ReportsFinished = B.ReportsFinished
FROM
	#TempCombine2 A
	INNER JOIN #TempCombine3 B ON A.CombinedReportRequestId = B.CombinedReportRequestId

--SELECT * FROM #TempCombine
--SELECT * FROM #TempCombine2

SELECT CombinedReportRequestId FROM #TempCombine2 WHERE TotalReports = ReportsFinished AND ReportsFinished > 0
GO
