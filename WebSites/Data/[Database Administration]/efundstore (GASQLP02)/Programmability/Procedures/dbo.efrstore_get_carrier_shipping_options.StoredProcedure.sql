USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_carrier_shipping_options]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Carrier_shipping_option
CREATE PROCEDURE [dbo].[efrstore_get_carrier_shipping_options] AS
begin

select Shipping_option_id, Description from Carrier_shipping_option

end
GO
