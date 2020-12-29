USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_brochure_image_by_id]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Brochure_image
CREATE PROCEDURE [dbo].[efrstore_get_brochure_image_by_id] @Brochure_image_id int AS
begin

select Brochure_image_id, Product_id, Base_filename, File_ext, Number_page from Brochure_image where Brochure_image_id=@Brochure_image_id

end
GO
