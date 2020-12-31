USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceAdjustmentStatus]    Script Date: 06/07/2017 09:17:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInvoiceAdjustmentStatus]
	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Invoice Adjustment Status For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT ADJUSTMENT_STATUS_ID , Description
FROM  QSPCanadaFinance..ADJUSTMENT_STATUS AST
	LEFT JOIN QSPCanadaCommon..OM_TBL_RESOURCE R on R.RESOURCE_ID = AST.NAME_RESOURCE_ID 
ORDER BY Description
	
SET NOCOUNT OFF
GO
