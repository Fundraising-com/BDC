USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_catalog_category]    Script Date: 02/14/2014 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Catalog_Category
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_catalog_category] @Catalog_Category_ID int OUTPUT, @Description varchar(40) AS
begin

insert into EFO_Catalog_Category(Description) values(@Description)

select @Catalog_Category_ID = SCOPE_IDENTITY()

end
GO
