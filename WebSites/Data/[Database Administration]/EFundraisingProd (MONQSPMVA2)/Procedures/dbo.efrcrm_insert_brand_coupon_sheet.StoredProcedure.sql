USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_brand_coupon_sheet]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Brand_Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_insert_brand_coupon_sheet] @Brand_ID int OUTPUT, @Coupon_Sheet_ID int, @Coupon_Per_Sheet smallint AS
begin

insert into Brand_Coupon_Sheet(Coupon_Sheet_ID, Coupon_Per_Sheet) values(@Coupon_Sheet_ID, @Coupon_Per_Sheet)

select @Brand_ID = SCOPE_IDENTITY()

end
GO
