USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_catalog_category]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Catalog_Category
CREATE PROCEDURE [dbo].[efrcrm_update_efo_catalog_category] @Catalog_Category_ID int, @Description varchar(40) AS
begin

update EFO_Catalog_Category set Description=@Description where Catalog_Category_ID=@Catalog_Category_ID

end
GO
