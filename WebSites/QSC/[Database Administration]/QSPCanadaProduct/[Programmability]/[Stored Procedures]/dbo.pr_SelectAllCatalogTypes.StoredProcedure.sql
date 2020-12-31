USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllCatalogTypes]    Script Date: 06/07/2017 09:18:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllCatalogTypes] AS

SELECT	Instance, Description
FROM		QSPCanadaOrderManagement..CodeDetail
WHERE	CodeHeaderInstance = 30300
ORDER BY	Instance
GO
