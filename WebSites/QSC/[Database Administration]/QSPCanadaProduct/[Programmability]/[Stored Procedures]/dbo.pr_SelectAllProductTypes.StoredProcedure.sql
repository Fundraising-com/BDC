USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllProductTypes]    Script Date: 06/07/2017 09:18:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllProductTypes] AS

SELECT	cd.Instance,
		cd.CodeHeaderInstance,
		cd.Description,
		cd.Gross,
		cd.ADPCode
FROM		QSPCanadaCommon..CodeDetail cd
WHERE	cd.CodeHeaderInstance = 46000
AND		cd.Instance NOT IN (46009, 46010, 46011, 46015) -- Valid product types
GO
