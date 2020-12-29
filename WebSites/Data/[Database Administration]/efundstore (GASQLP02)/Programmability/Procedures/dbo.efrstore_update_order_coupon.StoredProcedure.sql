USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_order_coupon]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Order_coupon
CREATE PROCEDURE [dbo].[efrstore_update_order_coupon] @Order_id int, @Coupon_id int AS
begin

update Order_coupon set Coupon_id=@Coupon_id where Order_id=@Order_id

end
GO
