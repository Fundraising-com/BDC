USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetFilesToBePrinted]    Script Date: 06/07/2017 09:20:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetFilesToBePrinted]

 AS

	Select 
		QSPCanadaOrderManagement..PrintRequest.Id,
		Cast(QSPCanadaOrderManagement..PrintRequest.ReportRequestId as varchar) + '.pdf' as 'FileName',
		QSPCanadaOrderManagement..PrintRequest.PrinterId,
		QSPCanadaCommon..Printers.PrinterName
	from 
		QSPCanadaOrderManagement..PrintRequest inner join QSPCanadaCommon..Printers on
		QSPCanadaOrderManagement..PrintRequest.PrinterId = QSPCanadaCommon..Printers.PrinterId
	where
		QSPCanadaOrderManagement..PrintRequest.PrintRequestStatusId = 1
GO
