USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_package_descs]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package_desc
CREATE PROCEDURE [dbo].[efrcrm_get_package_descs] AS
begin

select Package_id, Language_id, Package_name, Package_short_desc, Package_long_desc, Package_extra_desc, Package_small_img, Package_large_img, Page_url from Package_desc

end
GO
