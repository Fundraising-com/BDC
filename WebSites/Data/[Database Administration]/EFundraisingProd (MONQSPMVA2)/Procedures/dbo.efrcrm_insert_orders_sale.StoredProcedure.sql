USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_orders_sale]    Script Date: 02/14/2014 13:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Orders_sale
CREATE PROCEDURE [dbo].[efrcrm_insert_orders_sale] @Order_id int OUTPUT, @Sales_id int, @Date_created datetime AS
begin

insert into Orders_sale(Sales_id, Date_created) values(@Sales_id, @Date_created)

select @Order_id = SCOPE_IDENTITY()

end
GO
