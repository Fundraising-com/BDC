USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_order_coupon]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Order_coupon
CREATE PROCEDURE [dbo].[efrstore_insert_order_coupon] @Order_id int OUTPUT, @Coupon_id int AS
begin

insert into Order_coupon(Coupon_id) values(@Coupon_id)

select @Order_id = SCOPE_IDENTITY()

end
GO
