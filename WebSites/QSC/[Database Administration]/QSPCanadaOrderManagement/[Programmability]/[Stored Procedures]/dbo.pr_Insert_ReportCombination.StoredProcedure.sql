USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_ReportCombination]    Script Date: 06/07/2017 09:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insert_ReportCombination]

@ReportRequestId int
, @CombinedReportRequestId int

AS

INSERT INTO
	ReportCombination
	(
		ReportRequestId
		, CombinedReportRequestId
	) VALUES (
		@ReportRequestId
		, @CombinedReportRequestId
	)
GO
