USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_site_by_id]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Site
CREATE PROCEDURE [dbo].[efrcrm_get_site_by_id] @Site_Id int AS
begin

select Site_Id, Site_Title, Site_Content from Site where Site_Id=@Site_Id

end
GO
