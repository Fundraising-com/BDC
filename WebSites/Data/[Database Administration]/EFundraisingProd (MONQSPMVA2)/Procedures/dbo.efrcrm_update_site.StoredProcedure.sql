USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_site]    Script Date: 02/14/2014 13:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Site
CREATE PROCEDURE [dbo].[efrcrm_update_site] @Site_Id int, @Site_Title varchar(150), @Site_Content varchar(150) AS
begin

update Site set Site_Title=@Site_Title, Site_Content=@Site_Content where Site_Id=@Site_Id

end
GO
