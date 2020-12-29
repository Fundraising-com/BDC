USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_by_id]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sale
CREATE   PROCEDURE [dbo].[efrcrm_get_sale_by_id] @Sales_id int AS
begin

select Sales_id, Consultant_id, Carrier_id, Shipping_option_id, Payment_term_id, Client_sequence_code, Client_id, Sales_status_id, Payment_method_id, Po_status_id, Production_status_id, Sponsor_consultant_id, Ar_consultant_id, Ar_status_id, Lead_id, Billing_company_id, Upfront_payment_method_id, Confirmer_id, Collection_status_id, Confirmation_method_id, Credit_approval_method_id, Po_number, Credit_card_no, Expiry_date, Sales_date, Shipping_fees, Shipping_fees_discount, Payment_due_date, Confirmed_date, Scheduled_delivery_date, Scheduled_ship_date, Actual_ship_date, Waybill_no, Comment, Coupon_sheet_assigned, Total_amount, Invoice_date, Cancellation_date, Is_ordered, Po_received_on, Is_delivered, Local_sponsor_found, Box_return_date, Reship_date, Upfront_payment_required, Upfront_payment_due_date, Sponsor_required, Actual_delivery_date, Accounting_comments, Ssn_number, Ssn_address, Ssn_city, Ssn_state_code, Ssn_country_code, Ssn_zip_code, Is_validated, Promised_due_date, General_flag, Fuelsurcharge,
is_packed_by_participant, carrier_tracking_id, ext_order_id, qsp_order_id, po_consultant_commission, ext_sales_status_id 
from Sale where Sales_id=@Sales_id

end
GO
