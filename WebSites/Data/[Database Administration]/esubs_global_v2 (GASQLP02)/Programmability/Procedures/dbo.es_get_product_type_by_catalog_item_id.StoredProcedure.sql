USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_product_type_by_catalog_item_id]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pavel Tarassov
-- Create date: 09-12-2011
-- Description:	Get product type via catalog_item_id
-- =============================================
CREATE PROCEDURE [dbo].[es_get_product_type_by_catalog_item_id]

	@Catalog_item_id INT 
AS
BEGIN
SELECT pt.[product_type_id]
      ,pt.[product_line_id]
      ,pt.[product_type_name]
      ,pt.[administration_supply]
      ,pt.[fulfillment_charge]
  FROM [QSPFulfillment].[dbo].[product_type] pt with (nolock) inner join [QSPFulfillment].[dbo].[product] p with (nolock) on p.product_type_id =  pt.Product_type_id
  INNER JOIN [QSPFulfillment].[dbo].[catalog_item] ci with (nolock) on p.product_id =  ci.Product_id
  WHERE ci.catalog_item_id =  @Catalog_item_id
END
GO
