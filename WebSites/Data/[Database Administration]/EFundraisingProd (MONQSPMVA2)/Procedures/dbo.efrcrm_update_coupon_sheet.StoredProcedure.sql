USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_coupon_sheet]    Script Date: 02/14/2014 13:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_update_coupon_sheet] @Coupon_Sheet_ID int, @Product_Code varchar(10), @Description varchar(50), @Sheet_Per_Booklet smallint, @Expiration_Date datetime, @Commission_Payable bit, @Is_Active bit AS
begin

update Coupon_Sheet set Product_Code=@Product_Code, Description=@Description, Sheet_Per_Booklet=@Sheet_Per_Booklet, Expiration_Date=@Expiration_Date, Commission_Payable=@Commission_Payable, Is_Active=@Is_Active where Coupon_Sheet_ID=@Coupon_Sheet_ID

end
GO
