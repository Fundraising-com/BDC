USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_order_sales]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Order_sale
CREATE PROCEDURE [dbo].[efrstore_get_order_sales] AS
begin

select Order_id, Sale_id, Date_created from Order_sale

end
GO
