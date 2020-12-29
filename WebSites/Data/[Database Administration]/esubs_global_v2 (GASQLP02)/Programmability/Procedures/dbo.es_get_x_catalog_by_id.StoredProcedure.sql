USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_x_catalog_by_id]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================
-- Author:		<Pavel Tarassov>
-- Create date: <January 31, 2011>
-- Description:	<Gets XCatalog info by x_catalog_id>
-- ==========================================================================
CREATE PROCEDURE [dbo].[es_get_x_catalog_by_id]
@X_Catalog_Id int 
AS
begin

SELECT [X_Catalog_Id] 
      ,[Catalog_Name]
      ,[Language_id]
      ,[Homepage_Order]
      ,[Create_Date]
      ,[Modify_Date]
      ,[Modified_By]
      ,[Deleted_TF]
      ,[X_Catalog_Type_Id]
  FROM [QSPECommerce]..[X_Catalog] WITH (nolock)
WHERE [X_Catalog_Id] = @X_Catalog_Id
		
end
GO
