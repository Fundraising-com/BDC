USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_partner_web_detail]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_web_detail
CREATE PROCEDURE [dbo].[efrstore_update_partner_web_detail] @Partner_id int, @Top_menu varchar(30), @Left_menu varchar(30), @Right_menu varchar(30), @Images_path varchar(30), @Default_color varchar(20), @short_cut_menu varchar(30), @Product_image_map varchar(30) AS
begin

update Partner_web_detail set Top_menu=@Top_menu, Left_menu=@Left_menu, Right_menu=@Right_menu, Images_path=@Images_path, Default_color=@Default_color, short_cut_menu=@short_cut_menu, Product_image_map=@Product_image_map where Partner_id=@Partner_id

end
GO
