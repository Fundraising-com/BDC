USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_package]    Script Date: 02/14/2014 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Package
CREATE PROCEDURE [dbo].[efrcrm_insert_package] @Package_Id int OUTPUT, @Description varchar(50), @Comments text, @Package_Image varchar(50), @Package_Profit varchar(50), @Package_Web_Desc text, @Package_Title_Image varchar(50), @Is_Displayable bit AS
begin

insert into Package(Description, Comments, Package_Image, Package_Profit, Package_Web_Desc, Package_Title_Image, Is_Displayable) values(@Description, @Comments, @Package_Image, @Package_Profit, @Package_Web_Desc, @Package_Title_Image, @Is_Displayable)

select @Package_Id = SCOPE_IDENTITY()

end
GO
