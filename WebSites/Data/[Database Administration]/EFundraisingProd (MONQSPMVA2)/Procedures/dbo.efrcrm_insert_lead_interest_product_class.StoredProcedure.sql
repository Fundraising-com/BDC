USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_interest_product_class]    Script Date: 02/14/2014 13:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_interest_product_class
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_interest_product_class] @Lead_id int, @Product_class_id tinyint AS
begin

insert into Lead_interest_product_class(Lead_id, Product_class_id) values(@Lead_id, @Product_class_id)


end
GO
