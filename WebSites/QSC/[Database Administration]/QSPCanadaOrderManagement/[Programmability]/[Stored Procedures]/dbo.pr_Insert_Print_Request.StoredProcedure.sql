USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_Print_Request]    Script Date: 06/07/2017 09:20:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_Insert_Print_Request]

@ReportRequestId int
, @PrinterId int
, @PrintRequestStatusId int
, @ModifiedBy varchar(50)
, @PrintRequestId int output

AS

INSERT INTO
	PrintRequest
		(
			ReportRequestId
			, PrinterId
			, PrintRequestStatusId
			, CreatedBy
		)VALUES(
			@ReportRequestId
			, @PrinterId
			, @PrintRequestStatusId
			, @ModifiedBy
		)


SELECT @PrintRequestId = @@Identity

SELECT @PrintRequestId As 'PrintRequestId'
GO
