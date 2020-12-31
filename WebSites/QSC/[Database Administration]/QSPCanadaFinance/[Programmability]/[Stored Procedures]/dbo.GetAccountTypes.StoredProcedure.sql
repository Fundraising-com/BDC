USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAccountTypes]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAccountTypes]
	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Account Types For Canada Finance System
--   REGULAR		50601
--   FM			50602
--   ACCOUNT		50603
--   EMPLOYEE		50604
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT Instance,  Description
FROM  QSPCanadaCommon..CodeDetail CD 
WHERE CodeHeaderInstance = 50600
ORDER BY Description
	
SET NOCOUNT OFF
GO
