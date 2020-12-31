USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetAllPrinters]    Script Date: 06/07/2017 09:20:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_GetAllPrinters]

 AS

	Select 
		PrinterId,
		PrinterName
	from 
		QSPCanadaCommon..Printers with (nolock)
GO
