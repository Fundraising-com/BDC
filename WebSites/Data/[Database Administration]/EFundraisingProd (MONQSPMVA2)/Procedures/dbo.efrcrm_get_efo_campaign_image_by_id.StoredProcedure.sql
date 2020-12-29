USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_campaign_image_by_id]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Campaign_Image
CREATE PROCEDURE [dbo].[efrcrm_get_efo_campaign_image_by_id] @Campaign_Image_ID int AS
begin

select Campaign_Image_ID, Image_Catalog_Path, Image_Catalog_Path_Sel, Catalog_Category_ID, Is_Personalized from EFO_Campaign_Image where Campaign_Image_ID=@Campaign_Image_ID

end
GO
