USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Update_ReportRequest_SubscriptionId]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Update_ReportRequest_SubscriptionId]

@ReportRequestId int
, @RSSubscriptionId varchar(100)

AS

UPDATE
	ReportRequest
SET
	RSSubscriptionId = @RSSubscriptionId
WHERE
	Id = @ReportRequestId
GO
