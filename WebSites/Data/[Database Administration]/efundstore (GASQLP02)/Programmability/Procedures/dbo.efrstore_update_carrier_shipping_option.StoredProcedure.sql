USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_carrier_shipping_option]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Carrier_shipping_option
CREATE PROCEDURE [dbo].[efrstore_update_carrier_shipping_option] @Shipping_option_id tinyint, @Description varchar(50) AS
begin

update Carrier_shipping_option set Description=@Description where Shipping_option_id=@Shipping_option_id

end
GO
