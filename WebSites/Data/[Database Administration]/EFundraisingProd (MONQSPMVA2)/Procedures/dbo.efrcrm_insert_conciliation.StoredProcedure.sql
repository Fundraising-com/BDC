USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_conciliation]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Conciliation
CREATE PROCEDURE [dbo].[efrcrm_insert_conciliation] @Conciliation_id int OUTPUT, @Sales_id int, @Sales_item_no smallint, @Supplier_id tinyint, @Conciliate_date datetime, @Is_conciliated bit, @Is_ordered bit, @Invoice_number varchar(25) AS
begin

insert into Conciliation(Sales_id, Sales_item_no, Supplier_id, Conciliate_date, Is_conciliated, Is_ordered, Invoice_number) values(@Sales_id, @Sales_item_no, @Supplier_id, @Conciliate_date, @Is_conciliated, @Is_ordered, @Invoice_number)

select @Conciliation_id = SCOPE_IDENTITY()

end
GO
