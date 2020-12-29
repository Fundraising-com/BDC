USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_web_detail]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_web_detail
CREATE PROCEDURE [dbo].[efrstore_insert_partner_web_detail] @Partner_id int OUTPUT, @Top_menu varchar(30), @Left_menu varchar(30), @Right_menu varchar(30), @Images_path varchar(30), @Default_color varchar(20), @short_cut_menu varchar(30), @Product_image_map varchar(30) AS
begin

insert into Partner_web_detail(Top_menu, Left_menu, Right_menu, Images_path, Default_color, short_cut_menu, Product_image_map) values(@Top_menu, @Left_menu, @Right_menu, @Images_path, @Default_color, @short_cut_menu, @Product_image_map)

select @Partner_id = SCOPE_IDENTITY()

end
GO
