USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CatalogCode_SelectAll]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CatalogCode_SelectAll] AS
SET NOCOUNT ON


SELECT DISTINCT [Content_Catalog_Code]
FROM [QSPCanadaCommon]..[CampaignToContentCatalog]
ORDER BY [Content_Catalog_Code]
GO
