USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_ReportRequestDetail]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insert_ReportRequestDetail]

@ReportRequestId int
, @ReportId int
, @ModifiedBy varchar(50)

AS

INSERT INTO
	ReportRequestDetail
	(
		ReportRequestId
		, ReportId
		, LoggedBy
	) VALUES(
		@ReportRequestId
		, @ReportId
		, @ModifiedBy
	)
GO
