USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_item_coupon_sheets]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sales_Item_Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_get_sales_item_coupon_sheets] AS
begin

select Coupon_Sheet_ID, Sales_ID, Sales_Item_No, Date_Assigned, Sheet_Per_Booklet, Sponsor_Consultant_ID, Brand_ID, Local_Sponsor_ID from Sales_Item_Coupon_Sheet

end
GO
