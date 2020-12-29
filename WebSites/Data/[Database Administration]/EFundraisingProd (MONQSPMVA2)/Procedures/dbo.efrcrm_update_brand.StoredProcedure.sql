USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_brand]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Brand
CREATE PROCEDURE [dbo].[efrcrm_update_brand] @Brand_ID int, @Name varchar(50), @Promotion varchar(255) AS
begin

update Brand set Name=@Name, Promotion=@Promotion where Brand_ID=@Brand_ID

end
GO
