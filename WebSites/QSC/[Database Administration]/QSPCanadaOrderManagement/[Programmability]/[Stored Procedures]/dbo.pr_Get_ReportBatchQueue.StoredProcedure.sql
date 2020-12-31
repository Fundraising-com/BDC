USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_ReportBatchQueue]    Script Date: 06/07/2017 09:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_Get_ReportBatchQueue]
   @ReportTypeID int
 , @Language varchar(2) 
AS

-- Josh&Saqib - April 2005
-- this proc is used to return report parameters to data driven subscription

DECLARE @FILE varchar(55), @PATH varchar(255)

 declare  @rowcount int

SELECT 	@FILE = DestDesc, @Path = ReportPath
   FROM dbo.ReportRequestBatchType
WHERE ReportTypeId = @ReportTypeID

IF @ReportTypeID = 1--PickList 
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pBatchId
		, RPT.pBatchDate
		, NULL as pCampaignID
		, Null as pAccountID
		, Null as FMID
		, RPT.pReportType
		, RPT.pShipDateFrom
		, RPT.pShipDateTo
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
		, RB.ShipmentGroupID
	FROM
		dbo.ReportRequestBatch_PrintPickList RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL
		AND RPT.pReportType = 1 -- 1= pick list


	UPDATE dbo.ReportRequestBatch_PrintPickList
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD and pReportType = 1) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')--+ @FILE + cast(id as varchar)+'.PDF') 
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_PrintPickList RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.pReportType = 1 -- 1= pick list
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)

END

ELSE IF @ReportTypeID =  2   -- Packing Slip
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pBatchId
		, RPT.pBatchDate
		, NULL as pCampaignID
		, Null as pAccountID
		, Null as FMID
		, RPT.pReportType
		, RPT.pShipDateFrom
		, RPT.pShipDateTo
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.ReportRequestBatch_PrintPickList RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL
		AND RPT.pReportType = 2 -- 2= packing slip


	UPDATE dbo.ReportRequestBatch_PrintPickList
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD and pReportType = 2) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_PrintPickList RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.pReportType = 2 -- 2= packing slip
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)

END

ELSE IF  @ReportTypeID = 3 --BHE Labels

BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.ReportRequestBatch_BHEShippingLabelsReport RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL
		AND getdate() > DATEADD(mi, 5, RPT.[CreateDate]) 


	UPDATE dbo.ReportRequestBatch_BHEShippingLabelsReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_BHEShippingLabelsReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)


END
ELSE IF   @ReportTypeID = 5 --Participant Listing
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.ReportRequestBatch_ParticipantListing RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL
		--AND Lang = @Language


	UPDATE dbo.ReportRequestBatch_ParticipantListing
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_ParticipantListing RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)
				-- AND Lang = @Language)


END
ELSE IF  @ReportTypeID = 6 --Homeroom Summary Report
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
--		, RPT.pBatchId
--		, RPT.pBatchDate
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.[ReportRequestBatch_HomeroomSummaryReport] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL


	UPDATE dbo.ReportRequestBatch_HomeroomSummaryReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_HomeroomSummaryReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)

END

ELSE IF  @ReportTypeID = 7 --Group Summary Report

BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
--		, RPT.pBatchId
--		, RPT.pBatchDate
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.[ReportRequestBatch_GroupSummaryReport] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL


	UPDATE dbo.ReportRequestBatch_GroupSummaryReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_GroupSummaryReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)   


END
ELSE IF   @ReportTypeID = 8 -- Magazine Item Summary Report
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pCampaignID
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.[ReportRequestBatch_MagazineItemsSummary] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL
		AND Lang = @Language


	UPDATE dbo.ReportRequestBatch_MagazineItemsSummary
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_MagazineItemsSummary RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL
				AND Lang = @Language)   

END
ELSE IF   @ReportTypeID = 9 -- Problem Solver Report
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pCampaignID
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.[ReportRequestBatch_ProblemSolverReport] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL
		--AND Lang = @Language


	UPDATE dbo.ReportRequestBatch_ProblemSolverReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_ProblemSolverReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)
--				AND Lang = @Language)   

END 

ELSE IF  @ReportTypeID = 4 -- Teacher/Classroom Box Labels
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pTeacherID
		, RPT.pTotalLabels
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
		, RB.ShipmentGroupID
	FROM
		dbo.[ReportRequestBatch_TeacherBoxLabelsReport] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL


	UPDATE dbo.ReportRequestBatch_TeacherBoxLabelsReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_TeacherBoxLabelsReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)   


END 
ELSE IF @ReportTypeID = 10 -- Order Entry Follow-up Report
BEGIN
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pCampaignID
		, RPT.pAccountID
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.[ReportRequestBatch_OrderEntryFollowupReport] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL


	UPDATE dbo.ReportRequestBatch_OrderEntryFollowupReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_OrderEntryFollowupReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)   

END 
ELSE IF @ReportTypeID = 11 -- Price Discrepancy Report
BEGIN 
	SELECT 
		  RPT.id AS ReportID
		, RB.BatchOrderId AS pOrderId
		, RPT.pCampaignID
		, RPT.pAccountID
		, RPT.pFMID
		, RPT.pInvoiceID
		, cast(RB.BatchOrderId as varchar) + '_' + cast(RB.id as varchar) + '_' + @FILE  as [FileName]
		, @PATH as [ReportPath]
	FROM
		dbo.[ReportRequestBatch_PriceDiscrepancyReport] RPT 
		INNER JOIN dbo.ReportRequestBatch RB ON RPT.ReportRequestBatchID = RB.[id]
	WHERE
		RPT.[QueueDate] IS NULL
		AND RPT.[RunDateStart] IS NULL


	UPDATE dbo.ReportRequestBatch_PriceDiscrepancyReport
	   SET [QueueDate] = getdate(),
	           [FileName] = (cast((Select batchorderid from dbo.[ReportRequestBatch] where id  = ReportRequestBatchiD ) as varchar) + '_' + cast(ReportRequestBatchID as varchar) + '_' + @FILE+'.PDF')
	 WHERE [Id] in (SELECT  RPT.id
			FROM 	dbo.ReportRequestBatch_PriceDiscrepancyReport RPT, 
				dbo.ReportRequestBatch RB 
			WHERE RPT.ReportRequestBatchID = RB.[id]
				 AND RPT.[QueueDate] IS NULL
				 AND RPT.[RunDateStart] IS NULL)   
END
GO
