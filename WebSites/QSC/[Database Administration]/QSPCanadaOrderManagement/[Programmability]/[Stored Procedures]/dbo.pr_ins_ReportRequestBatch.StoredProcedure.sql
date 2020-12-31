USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_ReportRequestBatch]    Script Date: 06/07/2017 09:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ins_ReportRequestBatch]
  @BatchOrderId int
, @TypeId int = 1
, @CreatedBy int = null
, @ShipmentGroupID int = null
, @ReportRequestBatchID int out

AS

INSERT INTO dbo.ReportRequestBatch (
	  BatchOrderId
	, TypeId
	,CreateDate
	, CreatedBy
	, IsPrinted
	, ShipmentGroupID
) VALUES (
	  @BatchOrderId
	, @TypeId
	,GetDate()
	, @CreatedBy
	, 0
	, @ShipmentGroupID
);

SELECT @ReportRequestBatchID = @@IDENTITY ;
GO
