USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_conciliation]    Script Date: 02/14/2014 13:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Conciliation
CREATE PROCEDURE [dbo].[efrcrm_update_conciliation] @Conciliation_id int, @Sales_id int, @Sales_item_no smallint, @Supplier_id tinyint, @Conciliate_date datetime, @Is_conciliated bit, @Is_ordered bit, @Invoice_number varchar(25) AS
begin

update Conciliation set Sales_id=@Sales_id, Sales_item_no=@Sales_item_no, Supplier_id=@Supplier_id, Conciliate_date=@Conciliate_date, Is_conciliated=@Is_conciliated, Is_ordered=@Is_ordered, Invoice_number=@Invoice_number where Conciliation_id=@Conciliation_id

end
GO
