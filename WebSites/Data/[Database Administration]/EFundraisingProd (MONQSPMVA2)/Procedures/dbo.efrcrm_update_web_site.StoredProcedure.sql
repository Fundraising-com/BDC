USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_web_site]    Script Date: 02/14/2014 13:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Web_Site
CREATE PROCEDURE [dbo].[efrcrm_update_web_site] @Web_Site_Id int, @Web_Site_Name varchar(50) AS
begin

update Web_Site set Web_Site_Name=@Web_Site_Name where Web_Site_Id=@Web_Site_Id

end
GO
