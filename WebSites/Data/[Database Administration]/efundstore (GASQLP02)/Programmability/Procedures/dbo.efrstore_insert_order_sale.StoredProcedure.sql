USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_order_sale]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Order_sale
CREATE PROCEDURE [dbo].[efrstore_insert_order_sale] @Order_id int OUTPUT, @Sale_id int, @Date_created datetime AS
begin

insert into Order_sale(Sale_id, Date_created) values(@Sale_id, @Date_created)

select @Order_id = SCOPE_IDENTITY()

end
GO
