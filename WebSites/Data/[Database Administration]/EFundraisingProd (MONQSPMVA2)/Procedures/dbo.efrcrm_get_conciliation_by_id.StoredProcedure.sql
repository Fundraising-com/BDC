USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_conciliation_by_id]    Script Date: 02/14/2014 13:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Conciliation
CREATE PROCEDURE [dbo].[efrcrm_get_conciliation_by_id] @Conciliation_id int AS
begin

select Conciliation_id, Sales_id, Sales_item_no, Supplier_id, Conciliate_date, Is_conciliated, Is_ordered, Invoice_number from Conciliation where Conciliation_id=@Conciliation_id

end
GO
