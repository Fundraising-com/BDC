USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sales_item_coupon_sheet]    Script Date: 02/14/2014 13:07:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sales_Item_Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_insert_sales_item_coupon_sheet] @Coupon_Sheet_ID int OUTPUT, @Sales_ID int, @Sales_Item_No smallint, @Date_Assigned datetime, @Sheet_Per_Booklet smallint, @Sponsor_Consultant_ID int, @Brand_ID int, @Local_Sponsor_ID int AS
begin

insert into Sales_Item_Coupon_Sheet(Sales_ID, Sales_Item_No, Date_Assigned, Sheet_Per_Booklet, Sponsor_Consultant_ID, Brand_ID, Local_Sponsor_ID) values(@Sales_ID, @Sales_Item_No, @Date_Assigned, @Sheet_Per_Booklet, @Sponsor_Consultant_ID, @Brand_ID, @Local_Sponsor_ID)

select @Coupon_Sheet_ID = SCOPE_IDENTITY()

end
GO
