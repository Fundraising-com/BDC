USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_coupon_sheet_by_id]    Script Date: 02/14/2014 13:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_get_coupon_sheet_by_id] @Coupon_Sheet_ID int AS
begin

select Coupon_Sheet_ID, Product_Code, Description, Sheet_Per_Booklet, Expiration_Date, Commission_Payable, Is_Active from Coupon_Sheet where Coupon_Sheet_ID=@Coupon_Sheet_ID

end
GO
