USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_catalog_item_by_id]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_catalog_item_by_id] @catalog_item_id int

AS

BEGIN


SELECT ci.catalog_item_id, 

ci.catalog_id, 

ci.product_id, 

ci.catalog_item_code,

ci.catalog_item_name,

ci.description,

ci.nb_units,

cid.price as price,

ci.image_url,

ci.deleted,

ci.create_date,

ci.create_user_id,

ci.update_date,

ci.update_user_id

FROM qspfulfillment..catalog_item ci inner join qspfulfillment..catalog_item_detail cid on ci.catalog_item_id = cid.catalog_item_id

WHERE ci.catalog_item_id = @catalog_item_id and cid.deleted = 0;

END
GO
