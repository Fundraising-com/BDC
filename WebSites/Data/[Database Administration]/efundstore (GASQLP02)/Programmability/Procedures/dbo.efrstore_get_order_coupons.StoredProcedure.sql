USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_order_coupons]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Order_coupon
CREATE PROCEDURE [dbo].[efrstore_get_order_coupons] AS
begin

select Order_id, Coupon_id from Order_coupon

end
GO
