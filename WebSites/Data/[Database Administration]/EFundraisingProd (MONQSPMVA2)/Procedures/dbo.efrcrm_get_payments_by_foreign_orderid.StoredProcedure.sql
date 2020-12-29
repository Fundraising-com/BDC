USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payments_by_foreign_orderid]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment
CREATE PROCEDURE [dbo].[efrcrm_get_payments_by_foreign_orderid] 
@foreign_orderid int AS
begin

select Sales_id, Payment_no, Payment_method_id, Collection_status_id, Payment_entry_date, Cashable_date, Credit_card_no, Expiry_date, Name_on_card, Authorization_number, Payment_amount, Commission_paid, foreign_orderid from Payment
where foreign_orderid = @foreign_orderid

end
GO
