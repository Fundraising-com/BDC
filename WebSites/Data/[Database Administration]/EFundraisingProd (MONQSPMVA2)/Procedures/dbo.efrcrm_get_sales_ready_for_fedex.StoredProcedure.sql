USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sales_ready_for_fedex]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_sales_ready_for_fedex]
AS
begin

SELECT 
   s.Sales_id, s.Consultant_id, s.Carrier_id, s.Shipping_option_id, s.Payment_term_id, s.Client_sequence_code, s.Client_id, 
   s.Sales_status_id, s.Payment_method_id, s.Po_status_id, s.Production_status_id, s.Sponsor_consultant_id, s.Ar_consultant_id, 
   s.Ar_status_id, s.Lead_id, s.Billing_company_id, s.Upfront_payment_method_id, s.Confirmer_id, s.Collection_status_id, 
   s.Confirmation_method_id, s.Credit_approval_method_id, s.Po_number, s.Credit_card_no, s.Expiry_date, s.Sales_date, 
   s.Shipping_fees, s.Shipping_fees_discount, s.Payment_due_date, s.Confirmed_date, s.Scheduled_delivery_date, 
   s.Scheduled_ship_date, s.Actual_ship_date, s.Waybill_no, s.Comment, s.Coupon_sheet_assigned, s.Total_amount, s.Invoice_date, 
   s.Cancellation_date, s.Is_ordered, s.Po_received_on, s.Is_delivered, s.Local_sponsor_found, s.Box_return_date, s.Reship_date, 
   s.Upfront_payment_required, s.Upfront_payment_due_date, s.Sponsor_required, s.Actual_delivery_date, s.Accounting_comments, 
   s.Ssn_number, s.Ssn_address, s.Ssn_city, s.Ssn_state_code, s.Ssn_country_code, s.Ssn_zip_code, s.Is_validated, 
   s.Promised_due_date, s.General_flag, s.Fuelsurcharge, s.is_packed_by_participant, s.carrier_tracking_id
FROM Sale s inner join fedex ON s.carrier_tracking_id = fedex.fedex_id
and fedex.inter_tracking_number is not null
and fedex.cancelled <> 1
and s.carrier_id = 2 -- make sure its a fedex tracking_id
and inter_update_sale_date is null

end
GO
