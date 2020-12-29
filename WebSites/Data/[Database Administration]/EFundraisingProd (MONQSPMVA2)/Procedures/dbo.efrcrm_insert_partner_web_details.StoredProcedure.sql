USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_partner_web_details]    Script Date: 02/14/2014 13:07:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_web_details
CREATE PROCEDURE [dbo].[efrcrm_insert_partner_web_details] @Partner_id int OUTPUT, @Top_menu varchar(30), @Left_menu varchar(30), @Right_menu varchar(30), @Images_path varchar(30), @Default_color varchar(20), @Short_cut_menu varchar(30), @Product_image_map varchar(30) AS
begin

insert into Partner_web_details(Top_menu, Left_menu, Right_menu, Images_path, Default_color, Short_cut_menu, Product_image_map) values(@Top_menu, @Left_menu, @Right_menu, @Images_path, @Default_color, @Short_cut_menu, @Product_image_map)

select @Partner_id = SCOPE_IDENTITY()

end
GO
