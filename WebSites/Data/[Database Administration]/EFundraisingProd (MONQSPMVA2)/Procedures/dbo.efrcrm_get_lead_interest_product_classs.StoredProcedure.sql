USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_interest_product_classs]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_interest_product_class
CREATE PROCEDURE [dbo].[efrcrm_get_lead_interest_product_classs] AS
begin

select Lead_id, Product_class_id from Lead_interest_product_class

end
GO
