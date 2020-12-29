USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_brochure_images]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Brochure_image
CREATE PROCEDURE [dbo].[efrstore_get_brochure_images] AS
begin

select Brochure_image_id, Product_id, Base_filename, File_ext, Number_page from Brochure_image

end
GO
