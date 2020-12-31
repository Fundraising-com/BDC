USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePrintRequestStatus]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdatePrintRequestStatus]
(
	@RequestId int,
	@PrintRequestStatusId int
)
 AS
	Update PrintRequest set
		PrintRequestStatusId = @PrintRequestStatusId
	where
		Id = @RequestId
GO
