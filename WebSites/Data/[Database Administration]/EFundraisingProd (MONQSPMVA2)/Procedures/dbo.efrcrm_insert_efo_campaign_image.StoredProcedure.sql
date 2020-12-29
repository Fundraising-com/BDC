USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_campaign_image]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Campaign_Image
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_campaign_image] @Campaign_Image_ID int OUTPUT, @Image_Catalog_Path varchar(50), @Image_Catalog_Path_Sel varchar(50), @Catalog_Category_ID int, @Is_Personalized bit AS
begin

insert into EFO_Campaign_Image(Image_Catalog_Path, Image_Catalog_Path_Sel, Catalog_Category_ID, Is_Personalized) values(@Image_Catalog_Path, @Image_Catalog_Path_Sel, @Catalog_Category_ID, @Is_Personalized)

select @Campaign_Image_ID = SCOPE_IDENTITY()

end
GO
