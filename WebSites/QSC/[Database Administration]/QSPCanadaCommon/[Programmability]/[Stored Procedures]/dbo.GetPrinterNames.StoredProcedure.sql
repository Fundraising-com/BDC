USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetPrinterNames]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetPrinterNames]
	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 7/28/2004 
--   Get List of Printer Names For Canada Fulfillment System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT PrinterID, PrinterName
FROM Printers
ORDER BY PrinterName

SET NOCOUNT OFF
GO
