USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_catalog_categorys]    Script Date: 02/14/2014 13:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Catalog_Category
CREATE PROCEDURE [dbo].[efrcrm_get_efo_catalog_categorys] AS
begin

select Catalog_Category_ID, Description from EFO_Catalog_Category

end
GO
