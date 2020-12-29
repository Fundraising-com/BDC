USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_catalog_item_by_order_detail_id]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================

-- Author:        <Pavel Tarassov>

-- Create date: <January 31, 2011>

-- Description:   <Gets catalog item info by order_detail_id>

-- ==========================================================================

CREATE PROCEDURE [dbo].[es_get_catalog_item_by_order_detail_id] 

      @order_detail_id int

      

AS

BEGIN 

      SELECT [QSPFulfillment]..[catalog_item].[catalog_item_id]

      ,[QSPFulfillment]..[catalog_item].[catalog_id]

      ,[QSPFulfillment]..[catalog_item].[product_id]

      ,[QSPFulfillment]..[catalog_item].[catalog_item_code]

      ,[QSPFulfillment]..[catalog_item].[catalog_item_name]

      ,[QSPFulfillment]..[catalog_item].[description]

      ,[QSPFulfillment]..[catalog_item].[nb_units]

      ,[QSPFulfillment]..[catalog_item].[price]

      ,[QSPFulfillment]..[catalog_item].[image_url]

      ,[QSPFulfillment]..[catalog_item].[deleted]

      ,[QSPFulfillment]..[catalog_item].[create_date]

      ,[QSPFulfillment]..[catalog_item].[create_user_id]

      ,[QSPFulfillment]..[catalog_item].[update_date]

      ,[QSPFulfillment]..[catalog_item].[update_user_id]

      --,[QSPFulfillment]..[catalog_item].[catalog_item_status_id]

      ,[QSPFulfillment]..[catalog_item].[catalog_item_export_name]

      --,[QSPFulfillment]..[catalog_item].[must_order_in_multiples_of]

  FROM [QSPFulfillment]..[catalog_item] with (nolock) inner join [QSPFulfillment]..[catalog_item_detail] with (nolock) on [QSPFulfillment]..[catalog_item].catalog_item_id = [QSPFulfillment]..[catalog_item_detail].catalog_item_id

            inner join [QSPFulfillment]..[order_detail] with (nolock) on [QSPFulfillment]..[catalog_item_detail].catalog_item_detail_id = [QSPFulfillment]..[order_detail].catalog_item_detail_id

  WHERE [QSPFulfillment]..[order_detail].order_detail_id = @order_detail_id

END
GO
