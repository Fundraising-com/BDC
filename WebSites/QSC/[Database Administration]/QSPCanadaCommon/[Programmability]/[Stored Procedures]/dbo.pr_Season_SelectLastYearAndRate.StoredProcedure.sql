USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_SelectLastYearAndRate]    Script Date: 06/07/2017 09:33:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Season_SelectLastYearAndRate]

AS

SELECT		[FiscalYear], MAX([DefaultConversionRate])
FROM		[Season]
WHERE		[FiscalYear] = (SELECT MAX([FiscalYear]) FROM [Season])
GROUP BY	[FiscalYear]
GO
