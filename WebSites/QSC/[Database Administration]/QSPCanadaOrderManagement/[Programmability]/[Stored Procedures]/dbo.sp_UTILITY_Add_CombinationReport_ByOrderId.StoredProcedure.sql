USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_UTILITY_Add_CombinationReport_ByOrderId]    Script Date: 06/07/2017 09:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UTILITY_Add_CombinationReport_ByOrderId]

@BatchOrderId int

AS


DECLARE @CombinedReportRequestId int
DECLARE @ReportRequestId int

INSERT INTO
	ReportRequest
	(
		BatchOrderId
		, ReportTypeId
		, RSSubscriptionId
		, CreateDate
		, ModifiedBy
	) VALUES (

		@BatchOrderId
		, 13
		, ''
		, getdate()
		, 'ADMIN'
	)

SELECT @CombinedReportRequestId = @@Identity

DECLARE C1 CURSOR FOR
	SELECT Id FROM ReportRequest
	WHERE BatchOrderId = @BatchOrderId AND CREATEDATE >= '11/02/2004' AND ReportTypeId NOT IN (3,4, 13)

OPEN C1

-- Perform the first fetch.
FETCH NEXT FROM C1 INTO
	@ReportRequestId

-- Check @@FETCH_STATUS to see if there are any more rows to fetch.
WHILE @@FETCH_STATUS = 0
BEGIN

	INSERT INTO 
		ReportCombination
		(
			ReportRequestId
			, CombinedReportRequestId
		) VALUES (
			@ReportRequestId
			, @CombinedReportRequestId
		)
			

   -- This is executed as long as the previous fetch succeeds.
   FETCH NEXT FROM C1 INTO
	@ReportRequestId
END

CLOSE C1
DEALLOCATE C1
GO
