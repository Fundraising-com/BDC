USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_payment_by_sale_id_and_payment_no]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment
CREATE PROCEDURE [dbo].[efrcrm_get_payment_by_sale_id_and_payment_no] @Sales_id int, @payment_no int AS
begin

select Sales_id, 	Payment_no, Payment_method_id, Collection_status_id, Payment_entry_date, Cashable_date, 
	Credit_card_no, Expiry_date, Name_on_card, Authorization_number, Payment_amount, Commission_paid, foreign_orderid 
from Payment 
where Sales_id=@Sales_id and Payment_no = @payment_no

end
GO
