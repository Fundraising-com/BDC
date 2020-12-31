USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch_ParticipantListing]    Script Date: 06/07/2017 09:20:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch_ParticipantListing]
  @ReportRequestBatchID int
, @Lang varchar(2)
, @CreatedBy int = null
AS

INSERT INTO dbo.ReportRequestBatch_ParticipantListing (
	  ReportRequestBatchID
	, Lang
	 ,CreateDate
	, CreatedBy
) VALUES (
	  @ReportRequestBatchID
	, @Lang
	,GetDate()
	, @CreatedBy
) ;
GO
