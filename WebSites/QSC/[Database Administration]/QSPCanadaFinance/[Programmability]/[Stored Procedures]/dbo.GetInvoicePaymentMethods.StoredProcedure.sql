USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoicePaymentMethods]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInvoicePaymentMethods]
	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Invoice Payment Types For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT Instance,  Description
FROM  QSPCanadaCommon..CodeDetail CD 
WHERE CodeHeaderInstance = 50000
ORDER BY Description
	
SET NOCOUNT OFF
GO
