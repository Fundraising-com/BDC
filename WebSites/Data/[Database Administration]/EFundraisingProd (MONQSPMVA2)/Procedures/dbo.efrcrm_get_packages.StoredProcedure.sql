USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_packages]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Package
CREATE PROCEDURE [dbo].[efrcrm_get_packages] AS
begin

select Package_Id, Description, Comments, Package_Image, Package_Profit, Package_Web_Desc, Package_Title_Image, Is_Displayable, profit_max, 
profit_min, profit_default, product_class_id from Package

end
GO
