USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_package]    Script Date: 02/14/2014 13:08:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Package
CREATE PROCEDURE [dbo].[efrcrm_update_package] @Package_Id int, @Description varchar(50), @Comments text, @Package_Image varchar(50), @Package_Profit varchar(50), @Package_Web_Desc text, @Package_Title_Image varchar(50), @Is_Displayable bit AS
begin

update Package set Description=@Description, Comments=@Comments, Package_Image=@Package_Image, Package_Profit=@Package_Profit, Package_Web_Desc=@Package_Web_Desc, Package_Title_Image=@Package_Title_Image, Is_Displayable=@Is_Displayable where Package_Id=@Package_Id

end
GO
