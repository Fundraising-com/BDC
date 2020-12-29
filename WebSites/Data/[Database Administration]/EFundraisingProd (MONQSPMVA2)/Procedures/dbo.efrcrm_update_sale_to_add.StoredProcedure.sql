USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sale_to_add]    Script Date: 02/14/2014 13:08:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sale_to_add
CREATE PROCEDURE [dbo].[efrcrm_update_sale_to_add] @Sale_to_add_id int, @Consultant_id int, @Payment_method_id tinyint, @Po_status_id tinyint, @Sales_status_id int, @Lead_id int, @Payment_term_id tinyint, @Carrier_id tinyint, @Shipping_option_id tinyint, @Upfront_payment_method_id tinyint, @Po_number varchar(50), @Credit_card_no varchar(16), @Expiry_date varchar(7), @Sales_date datetime, @Shipping_fees decimal, @Shipping_fees_discount decimal, @Payment_due_date datetime, @Scheduled_delivery_date datetime, @Comment text, @Total_amount decimal, @Confirmed_date datetime, @Upfront_payment_required decimal, @Upfront_payment_due_date smalldatetime, @Is_new bit, @Sponsor_required bit, @Ssn_number varchar(9), @Ssn_address varchar(50), @Ssn_city varchar(50), @Ssn_state_code varchar(10), @Ssn_country_code varchar(10), @Ssn_zip_code varchar(10) AS
begin

update Sale_to_add set Consultant_id=@Consultant_id, Payment_method_id=@Payment_method_id, Po_status_id=@Po_status_id, Sales_status_id=@Sales_status_id, Lead_id=@Lead_id, Payment_term_id=@Payment_term_id, Carrier_id=@Carrier_id, Shipping_option_id=@Shipping_option_id, Upfront_payment_method_id=@Upfront_payment_method_id, Po_number=@Po_number, Credit_card_no=@Credit_card_no, Expiry_date=@Expiry_date, Sales_date=@Sales_date, Shipping_fees=@Shipping_fees, Shipping_fees_discount=@Shipping_fees_discount, Payment_due_date=@Payment_due_date, Scheduled_delivery_date=@Scheduled_delivery_date, Comment=@Comment, Total_amount=@Total_amount, Confirmed_date=@Confirmed_date, Upfront_payment_required=@Upfront_payment_required, Upfront_payment_due_date=@Upfront_payment_due_date, Is_new=@Is_new, Sponsor_required=@Sponsor_required, Ssn_number=@Ssn_number, Ssn_address=@Ssn_address, Ssn_city=@Ssn_city, Ssn_state_code=@Ssn_state_code, Ssn_country_code=@Ssn_country_code, Ssn_zip_code=@Ssn_zip_code where Sale_to_add_id=@Sale_to_add_id

end
GO
