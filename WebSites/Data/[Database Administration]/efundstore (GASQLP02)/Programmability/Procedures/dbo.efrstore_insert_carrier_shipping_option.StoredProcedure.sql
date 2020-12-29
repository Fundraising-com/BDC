USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_carrier_shipping_option]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Carrier_shipping_option
CREATE PROCEDURE [dbo].[efrstore_insert_carrier_shipping_option] @Shipping_option_id int OUTPUT, @Description varchar(50) AS
begin

insert into Carrier_shipping_option(Description) values(@Description)

select @Shipping_option_id = SCOPE_IDENTITY()

end
GO
