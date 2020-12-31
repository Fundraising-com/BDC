USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPayGroupLookUpCodes]    Script Date: 06/07/2017 09:18:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPayGroupLookUpCodes] AS

SELECT DISTINCT
		coalesce(p.PayGroupLookUpCode, 'N/A') PayGroupLookUpCode

FROM		Product p

ORDER BY	coalesce(p.PayGroupLookUpCode, 'N/A')
GO
