USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_web_site_by_id]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Web_Site
CREATE PROCEDURE [dbo].[efrcrm_get_web_site_by_id] @Web_Site_Id int AS
begin

select Web_Site_Id, Web_Site_Name from Web_Site where Web_Site_Id=@Web_Site_Id

end
GO
