USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_payment]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment
CREATE PROCEDURE [dbo].[efrcrm_update_payment] @Sales_id int, @Payment_no int, @Payment_method_id tinyint, @Collection_status_id int, @Payment_entry_date datetime, @Cashable_date datetime, @Credit_card_no varchar(16), @Expiry_date varchar(7), @Name_on_card varchar(50), @Authorization_number varchar(10), @Payment_amount decimal(15,4), @Commission_paid bit, @foreign_orderid int AS
begin

update Payment set Payment_no=@Payment_no, Payment_method_id=@Payment_method_id, Collection_status_id=@Collection_status_id, Payment_entry_date=@Payment_entry_date, Cashable_date=@Cashable_date, Credit_card_no=@Credit_card_no, Expiry_date=@Expiry_date, Name_on_card=@Name_on_card, Authorization_number=@Authorization_number, Payment_amount=@Payment_amount, Commission_paid=@Commission_paid, foreign_orderid=@foreign_orderid where Sales_id=@Sales_id

end
GO
