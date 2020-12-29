USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sales_item_coupon_sheet]    Script Date: 02/14/2014 13:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sales_Item_Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_update_sales_item_coupon_sheet] @Coupon_Sheet_ID int, @Sales_ID int, @Sales_Item_No smallint, @Date_Assigned datetime, @Sheet_Per_Booklet smallint, @Sponsor_Consultant_ID int, @Brand_ID int, @Local_Sponsor_ID int AS
begin

update Sales_Item_Coupon_Sheet set Sales_ID=@Sales_ID, Sales_Item_No=@Sales_Item_No, Date_Assigned=@Date_Assigned, Sheet_Per_Booklet=@Sheet_Per_Booklet, Sponsor_Consultant_ID=@Sponsor_Consultant_ID, Brand_ID=@Brand_ID, Local_Sponsor_ID=@Local_Sponsor_ID where Coupon_Sheet_ID=@Coupon_Sheet_ID

end
GO
