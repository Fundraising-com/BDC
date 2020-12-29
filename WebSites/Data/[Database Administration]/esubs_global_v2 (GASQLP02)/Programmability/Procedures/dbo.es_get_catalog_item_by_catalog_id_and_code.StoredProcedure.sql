USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_catalog_item_by_catalog_id_and_code]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================
-- Author:		<Jiro Hidaka>
-- Create date: <August 21, 2009>
-- Description:	<Gets catalog item info by catalog_id and catalog_item_code>
-- ==========================================================================
CREATE PROCEDURE [dbo].[es_get_catalog_item_by_catalog_id_and_code] 
	@catalog_id int,
	@catalog_item_code varchar(50)
AS
BEGIN	
	SELECT ci.catalog_item_id, 
		   ci.catalog_id, 
		   ci.product_id, 
		   ci.catalog_item_code,
		   ci.catalog_item_name,
		   ci.description,
		   ci.nb_units,
		   COALESCE(ci.price, cid.price) as price,
		   ci.image_url,
		   ci.deleted,
		   ci.create_date,
		   ci.create_user_id,
		   ci.update_date,
		   ci.update_user_id
	FROM   qspfulfillment..catalog_item  ci inner join qspfulfillment..catalog_item_detail cid on ci.catalog_item_id = cid.catalog_item_id
	WHERE ci.catalog_id = @catalog_id and ci.catalog_item_code = @catalog_item_code and ci.deleted = 0;
END
GO
