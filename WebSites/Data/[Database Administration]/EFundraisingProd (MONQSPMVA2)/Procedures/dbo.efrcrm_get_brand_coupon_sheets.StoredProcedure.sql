USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_brand_coupon_sheets]    Script Date: 02/14/2014 13:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Brand_Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_get_brand_coupon_sheets] AS
begin

select Brand_ID, Coupon_Sheet_ID, Coupon_Per_Sheet from Brand_Coupon_Sheet

end
GO
