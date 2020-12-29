USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_partner_web_details]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_web_details
CREATE PROCEDURE [dbo].[efrcrm_update_partner_web_details] @Partner_id int, @Top_menu varchar(30), @Left_menu varchar(30), @Right_menu varchar(30), @Images_path varchar(30), @Default_color varchar(20), @Short_cut_menu varchar(30), @Product_image_map varchar(30) AS
begin

update Partner_web_details set Top_menu=@Top_menu, Left_menu=@Left_menu, Right_menu=@Right_menu, Images_path=@Images_path, Default_color=@Default_color, Short_cut_menu=@Short_cut_menu, Product_image_map=@Product_image_map where Partner_id=@Partner_id

end
GO
