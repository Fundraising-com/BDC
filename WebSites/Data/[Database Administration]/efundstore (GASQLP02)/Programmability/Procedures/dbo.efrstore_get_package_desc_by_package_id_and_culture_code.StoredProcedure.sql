USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_desc_by_package_id_and_culture_code]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
CREATE  PROCEDURE [dbo].[efrstore_get_package_desc_by_package_id_and_culture_code] 
                   @package_id int
                  ,@culture_code varchar(5)
AS
begin

select Package_id
, culture_code
, name
, short_desc
,template_id
, long_desc
, extra_desc
, page_name
, page_title
, image_name
, image_alt_text
, display_order
, enabled
, configuration
,create_date
from Package_desc
where package_id = @package_id and culture_code = @culture_code

end
GO
