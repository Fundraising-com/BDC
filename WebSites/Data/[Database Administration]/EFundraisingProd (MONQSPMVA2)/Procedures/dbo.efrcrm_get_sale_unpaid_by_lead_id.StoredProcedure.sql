USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sale_unpaid_by_lead_id]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Sale

--152372
CREATE    PROCEDURE [dbo].[efrcrm_get_sale_unpaid_by_lead_id] --152372
                  @lead_id int AS
begin



select Sales_id, Consultant_id, Carrier_id, Shipping_option_id, Payment_term_id, s.Client_sequence_code, s.Client_id, Sales_status_id, Payment_method_id, Po_status_id, Production_status_id, Sponsor_consultant_id, Ar_consultant_id, Ar_status_id, s.Lead_id, Billing_company_id, Upfront_payment_method_id, Confirmer_id, Collection_status_id, Confirmation_method_id, Credit_approval_method_id, Po_number, Credit_card_no, Expiry_date, Sales_date, Shipping_fees, Shipping_fees_discount, Payment_due_date, Confirmed_date, Scheduled_delivery_date, Scheduled_ship_date, Actual_ship_date, Waybill_no, Comment, Coupon_sheet_assigned, Total_amount, Invoice_date, Cancellation_date, Is_ordered, Po_received_on, Is_delivered, Local_sponsor_found, Box_return_date, Reship_date, Upfront_payment_required, Upfront_payment_due_date, Sponsor_required, Actual_delivery_date, Accounting_comments, Ssn_number, Ssn_address, Ssn_city, Ssn_state_code, Ssn_country_code, Ssn_zip_code, Is_validated, Promised_due_date, General_flag, Fuelsurcharge, s.carrier_tracking_id, s.ext_order_id
from Sale s inner join client c on s.client_id = c.client_id and s.client_sequence_code = c.client_sequence_code
where c.lead_id=@lead_id
     and s.ar_status_id not in (20,22) --paid and credited
end
GO
