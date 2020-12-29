USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_brand_coupon_sheet]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Brand_Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_update_brand_coupon_sheet] @Brand_ID int, @Coupon_Sheet_ID int, @Coupon_Per_Sheet smallint AS
begin

update Brand_Coupon_Sheet set Coupon_Sheet_ID=@Coupon_Sheet_ID, Coupon_Per_Sheet=@Coupon_Per_Sheet where Brand_ID=@Brand_ID

end
GO
