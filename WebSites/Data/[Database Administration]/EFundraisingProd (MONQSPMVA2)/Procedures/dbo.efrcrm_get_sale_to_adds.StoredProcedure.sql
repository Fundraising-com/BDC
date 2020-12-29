USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_to_adds]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Sale_to_add
CREATE PROCEDURE [dbo].[efrcrm_get_sale_to_adds] AS
begin

select Sale_to_add_id, Consultant_id, Payment_method_id, Po_status_id, Sales_status_id, Lead_id, Payment_term_id, Carrier_id, Shipping_option_id, Upfront_payment_method_id, Po_number, Credit_card_no, Expiry_date, Sales_date, Shipping_fees, Shipping_fees_discount, Payment_due_date, Scheduled_delivery_date, Comment, Total_amount, Confirmed_date, Upfront_payment_required, Upfront_payment_due_date, Is_new, Sponsor_required, Ssn_number, Ssn_address, Ssn_city, Ssn_state_code, Ssn_country_code, Ssn_zip_code from Sale_to_add

end
GO
