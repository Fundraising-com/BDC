USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_campaign_image]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Campaign_Image
CREATE PROCEDURE [dbo].[efrcrm_update_efo_campaign_image] @Campaign_Image_ID int, @Image_Catalog_Path varchar(50), @Image_Catalog_Path_Sel varchar(50), @Catalog_Category_ID int, @Is_Personalized bit AS
begin

update EFO_Campaign_Image set Image_Catalog_Path=@Image_Catalog_Path, Image_Catalog_Path_Sel=@Image_Catalog_Path_Sel, Catalog_Category_ID=@Catalog_Category_ID, Is_Personalized=@Is_Personalized where Campaign_Image_ID=@Campaign_Image_ID

end
GO
