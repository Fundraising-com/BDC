USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_interest_product_class]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_interest_product_class
CREATE PROCEDURE [dbo].[efrcrm_update_lead_interest_product_class] @Lead_id int, @Product_class_id tinyint AS
begin

update Lead_interest_product_class set Product_class_id=@Product_class_id where Lead_id=@Lead_id

end
GO
