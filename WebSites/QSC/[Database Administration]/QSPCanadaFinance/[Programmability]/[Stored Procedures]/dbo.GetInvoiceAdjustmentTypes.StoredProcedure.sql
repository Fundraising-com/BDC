USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoiceAdjustmentTypes]    Script Date: 06/07/2017 09:17:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoiceAdjustmentTypes]
	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Invoice Adjustment Types For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT		Adjustment_Type_ID AS Instance,
			[Name] AS [Description]
FROM		Adjustment_Type
WHERE		[Disabled] = 0
AND			User_Creatable = 1
ORDER BY	[Name]
		
SET NOCOUNT OFF
GO
