USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_descs_by_id]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
create PROCEDURE [dbo].[efrstore_get_package_descs_by_id] 
AS
begin

select Package_id
, culture_code
, name
, short_desc
, long_desc
, extra_desc
, page_name
, page_title
, image_name
, image_alt_text
, display_order
, enabled
, configuration 
from Package_desc

end
GO
