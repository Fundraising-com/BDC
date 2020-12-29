USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_payment]    Script Date: 02/14/2014 13:07:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment
CREATE PROCEDURE [dbo].[efrcrm_insert_payment] @Sales_id int , @Payment_no int, @Payment_method_id tinyint, @Collection_status_id int, @Payment_entry_date datetime, @Cashable_date datetime, @Credit_card_no varchar(16), @Expiry_date varchar(7), @Name_on_card varchar(50), @Authorization_number varchar(10), @Payment_amount decimal(15,4), @Commission_paid bit, @foreign_orderid int = NULL AS
begin

declare @cc_no varchar(16)
select @cc_no = '****' + RIGHT(@credit_card_no,4)

insert into Payment(Sales_id , Payment_no, Payment_method_id, Collection_status_id, Payment_entry_date, Cashable_date, Credit_card_no, Expiry_date, Name_on_card, Authorization_number, Payment_amount, Commission_paid, foreign_orderid) values(@Sales_id, @Payment_no, @Payment_method_id, @Collection_status_id, @Payment_entry_date, @Cashable_date, @cc_no, @Expiry_date, @Name_on_card, @Authorization_number, @Payment_amount, @Commission_paid, @foreign_orderid)


end
GO
