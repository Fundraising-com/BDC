USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_ReportRequest]    Script Date: 06/07/2017 09:20:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Insert_ReportRequest]

@BatchOrderId int
, @ReportTypeId int
, @RSSubscriptionId varchar(100)
, @ModifiedBy varchar(50)
, @ReportRequestId int output

AS

INSERT INTO
	ReportRequest
		(
			BatchOrderId
			, ReportTypeId
			, RSSubscriptionId
			, ModifiedBy
		) VALUES (

			@BatchOrderId
			, @ReportTypeId
			, @RSSubscriptionId
			, @ModifiedBy
		)

SELECT @ReportRequestId = @@Identity

SELECT @ReportRequestId As 'ReportRequestId'
GO
