USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_catalog_category_by_id]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Catalog_Category
CREATE PROCEDURE [dbo].[efrcrm_get_efo_catalog_category_by_id] @Catalog_Category_ID int AS
begin

select Catalog_Category_ID, Description from EFO_Catalog_Category where Catalog_Category_ID=@Catalog_Category_ID

end
GO
