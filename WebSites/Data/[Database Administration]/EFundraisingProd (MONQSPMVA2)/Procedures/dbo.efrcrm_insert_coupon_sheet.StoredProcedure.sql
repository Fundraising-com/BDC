USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_coupon_sheet]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Coupon_Sheet
CREATE PROCEDURE [dbo].[efrcrm_insert_coupon_sheet] @Coupon_Sheet_ID int OUTPUT, @Product_Code varchar(10), @Description varchar(50), @Sheet_Per_Booklet smallint, @Expiration_Date datetime, @Commission_Payable bit, @Is_Active bit AS
begin

insert into Coupon_Sheet(Product_Code, Description, Sheet_Per_Booklet, Expiration_Date, Commission_Payable, Is_Active) values(@Product_Code, @Description, @Sheet_Per_Booklet, @Expiration_Date, @Commission_Payable, @Is_Active)

select @Coupon_Sheet_ID = SCOPE_IDENTITY()

end
GO
