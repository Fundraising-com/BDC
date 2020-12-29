USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_latest_catalog_item_by_catalog_item_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pavel Tarassov
-- Create date: 18-10-2011
-- Description:	Get da latest catalog_item_id
-- (helps if catalog season was changed)
--   exec es_get_latest_catalog_item_by_catalog_item_id 62595, 335
-- =============================================
CREATE PROCEDURE [dbo].[es_get_latest_catalog_item_by_catalog_item_id]

	@Catalog_item_id INT ,
	@Catalog_id INT
	
AS
BEGIN
    -- Temp hack for flower    
	IF @Catalog_item_id = 60079
		SET @Catalog_item_id = 64871;
	ELSE IF @Catalog_item_id = 57186
		SET @Catalog_item_id = 64871;
	
	-- Temp hack for GA
	IF @Catalog_item_id = 100206
		SET @Catalog_item_id = 64871;
	ELSE IF @Catalog_item_id = 100798
		SET @Catalog_item_id = 65323;
	ELSE IF @Catalog_item_id = 101829
		SET @Catalog_item_id = 65461;
	ELSE IF @Catalog_item_id = 101828
		SET @Catalog_item_id = 65430;
	ELSE IF @Catalog_item_id = 101177
		SET @Catalog_item_id = 65276;
	ELSE IF @Catalog_item_id = 101896
		SET @Catalog_item_id = 65450;
	ELSE IF @Catalog_item_id = 101216
		SET @Catalog_item_id = 65359;
	ELSE IF @Catalog_item_id = 101242
		SET @Catalog_item_id = 65550;
	ELSE IF @Catalog_item_id = 101880
		SET @Catalog_item_id = 65431;
	ELSE IF @Catalog_item_id = 101178
		SET @Catalog_item_id = 65277;
	ELSE IF @Catalog_item_id = 101874
		SET @Catalog_item_id = 62614;
	ELSE IF @Catalog_item_id = 100705
		SET @Catalog_item_id = 62344;
	ELSE IF @Catalog_item_id = 102548
		SET @Catalog_item_id = 62293;
	ELSE IF @Catalog_item_id = 100712
		SET @Catalog_item_id = 62348;
	ELSE IF @Catalog_item_id = 100808
		SET @Catalog_item_id = 62366;
	ELSE IF @Catalog_item_id = 101206
		SET @Catalog_item_id = 62550;
	ELSE IF @Catalog_item_id = 102551
		SET @Catalog_item_id = 62296;
	ELSE IF @Catalog_item_id = 100633
		SET @Catalog_item_id = 65103;
	ELSE IF @Catalog_item_id = 101602
		SET @Catalog_item_id = 64995;
	ELSE IF @Catalog_item_id = 101153
		SET @Catalog_item_id = 65345;
	ELSE IF @Catalog_item_id = 100808
		SET @Catalog_item_id = 65158;
	ELSE IF @Catalog_item_id = 101245
		SET @Catalog_item_id = 65457;
	ELSE IF @Catalog_item_id = 102492
		SET @Catalog_item_id = 65007;
		
	SELECT [catalog_item_id]
      ,[catalog_id]
      ,[product_id]
      ,[catalog_item_code]
      ,[catalog_item_name]
      ,[description]
      ,[nb_units]
      ,[price]
      ,[image_url]
      ,[deleted]
      ,[create_date]
      ,[create_user_id]
      ,[update_date]
      ,[update_user_id]
      FROM  
(
	SELECT [catalog_item_id]
      ,[catalog_id]
      ,[product_id]
      ,[catalog_item_code]
      ,[catalog_item_name]
      ,[description]
      ,[nb_units]
      ,[price]
      ,[image_url]
      ,[deleted]
      ,[create_date]
      ,[create_user_id]
      ,[update_date]
      ,[update_user_id]
      ,[catalog_item_export_name]
      , RANK() OVER (PARTITION BY catalog_item_code ORDER BY catalog_item_id DESC) AS RANK
	FROM qspfulfillment..catalog_item ci WITH (NOLOCK)  
	WHERE ci.deleted = 0 AND ci.catalog_item_code IS NOT NULL AND ci.catalog_item_code <> '' and ci.catalog_id = @Catalog_id and
	(
		ci.catalog_item_code IN (SELECT catalog_item_code FROM qspfulfillment..catalog_item WITH (NOLOCK) WHERE catalog_item_id = @Catalog_item_id) OR
		ci.catalog_item_code IN (SELECT DISTINCT catalog_item_code FROM qspfulfillment..catalog_item WITH (NOLOCK) WHERE catalog_item_name in (
									SELECT catalog_item_name FROM qspfulfillment..catalog_item WITH (NOLOCK) WHERE catalog_item_id = @Catalog_item_id)) OR		
		ci.catalog_item_code IN (SELECT catalog_item_code FROM qspfulfillment..catalog_item WITH (NOLOCK) WHERE catalog_item_name in (
									SELECT replace(catalog_item_name,' ','') FROM qspfulfillment..catalog_item WITH (NOLOCK) WHERE catalog_item_id = @Catalog_item_id))	
	) 
) AS temp
WHERE rank = 1

END
GO
