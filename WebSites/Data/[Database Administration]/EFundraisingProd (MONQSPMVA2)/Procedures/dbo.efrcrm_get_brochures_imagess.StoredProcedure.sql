USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_brochures_imagess]    Script Date: 02/14/2014 13:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Brochures_images
CREATE PROCEDURE [dbo].[efrcrm_get_brochures_imagess] AS
begin

select Brochures_images_id, Product_id, Base_filename, File_ext, Number_pages from Brochures_images

end
GO
