USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_products_packages]    Script Date: 02/14/2014 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Products_packages
CREATE PROCEDURE [dbo].[efrcrm_insert_products_packages] @Product_id int OUTPUT, @Package_id tinyint, @Display_order tinyint, @Displayable bit AS
begin

insert into Products_packages(Package_id, Display_order, Displayable) values(@Package_id, @Display_order, @Displayable)

select @Product_id = SCOPE_IDENTITY()

end
GO
