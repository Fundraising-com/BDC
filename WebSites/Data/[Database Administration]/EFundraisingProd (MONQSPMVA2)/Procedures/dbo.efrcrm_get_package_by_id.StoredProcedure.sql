USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_package_by_id]    Script Date: 02/14/2014 13:05:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE PROCEDURE [dbo].[efrcrm_get_package_by_id] @Package_Id int AS
begin

select Package_Id, Description, Comments, Package_Image, Package_Profit, Package_Web_Desc, Package_Title_Image, Is_Displayable, profit_max, 
profit_min, profit_default from Package where Package_Id=@Package_Id

end
GO
