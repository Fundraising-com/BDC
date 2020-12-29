USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_web_detail_by_id]    Script Date: 02/14/2014 13:05:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_web_detail
CREATE PROCEDURE [dbo].[efrstore_get_partner_web_detail_by_id] @Partner_id int AS
begin

select Partner_id, Top_menu, Left_menu, Right_menu, Images_path, Default_color, short_cut_menu, Product_image_map from Partner_web_detail where Partner_id=@Partner_id

end
GO
