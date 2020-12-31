USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Select_BatchReportStatus]    Script Date: 06/07/2017 09:20:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Select_BatchReportStatus]

@BatchOrderId int,
@FMID varchar(4)='9999'
AS

if(@FMID='9999')
	begin


SELECT
	A.*
	, B.LastStatus
	, C.[Description] As 'ReportType'
FROM
	ReportRequest A
	LEFT OUTER JOIN [USPVL2K54].ReportServer_DMZ2K15.dbo.Subscriptions B ON A.RSSubscriptionId LIKE B.SubscriptionId
	--LEFT OUTER JOIN [USPVL2K20-DEV].ReportServer.dbo.Subscriptions B ON A.RSSubscriptionId LIKE B.SubscriptionId
	LEFT OUTER JOIN ReportType C ON A.ReportTypeId = C.Id
WHERE
	A.BatchOrderId = @BatchOrderId
ORDER BY
	A.CreateDate Desc

end
else
begin

SELECT
	A.*
	, B.LastStatus
	, C.[Description] As 'ReportType'
FROM
	ReportRequest A
	LEFT OUTER JOIN [USPVL2K54].ReportServer_DMZ2K15.dbo.Subscriptions B ON A.RSSubscriptionId LIKE B.SubscriptionId
	--LEFT OUTER JOIN [USPVL2K20-DEV].ReportServer.dbo.Subscriptions B ON A.RSSubscriptionId LIKE B.SubscriptionId
	LEFT OUTER JOIN ReportType C ON A.ReportTypeId = C.Id
WHERE
	A.BatchOrderId = @BatchOrderId
			AND ReportTypeID not in (3,4,13)
ORDER BY
	A.CreateDate Desc



end
GO
