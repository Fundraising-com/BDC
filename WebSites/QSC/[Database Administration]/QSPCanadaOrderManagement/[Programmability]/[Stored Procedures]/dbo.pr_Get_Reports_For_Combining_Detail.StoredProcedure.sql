USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Reports_For_Combining_Detail]    Script Date: 06/07/2017 09:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Get_Reports_For_Combining_Detail]

@CombinedReportRequestId int

AS

SELECT
	 B.*
	, D.LastStatus
FROM
	ReportRequest A
	INNER JOIN ReportCombination B ON A.Id = B.CombinedReportRequestId
	INNER JOIN ReportRequest C ON B.ReportRequestId = C.Id
	LEFT OUTER JOIN [USPVL2K54].ReportServer_DMZ2K15.dbo.Subscriptions D ON C.RSSubscriptionId LIKE D.SubscriptionId
	
WHERE
	A.ReportTypeId = 13
	AND ( A.RSSubscriptionID is null OR A.RSSubscriptionId <> 'COMPLETE' )  --- Being used as a status flag
	AND PATINDEX('%was written to%', D.LastStatus) > 0
	AND B.CombinedReportRequestId = @CombinedReportRequestId
GO
