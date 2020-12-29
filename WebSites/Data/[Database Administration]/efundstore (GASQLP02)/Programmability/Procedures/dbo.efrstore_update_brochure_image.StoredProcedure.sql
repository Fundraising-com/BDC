USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_brochure_image]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Brochure_image
CREATE PROCEDURE [dbo].[efrstore_update_brochure_image] @Brochure_image_id tinyint, @Product_id int, @Base_filename varchar(100), @File_ext varchar(5), @Number_page tinyint AS
begin

update Brochure_image set Product_id=@Product_id, Base_filename=@Base_filename, File_ext=@File_ext, Number_page=@Number_page where Brochure_image_id=@Brochure_image_id

end
GO
