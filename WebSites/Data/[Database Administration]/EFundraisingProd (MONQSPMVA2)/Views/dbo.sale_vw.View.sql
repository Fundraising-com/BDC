USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[sale_vw]    Script Date: 02/14/2014 13:02:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[sale_vw]
 as
 SELECT [sales_id]
        ,[consultant_id]
        ,[carrier_id]
        ,[sales_date]
        ,[scheduled_ship_date]
        -- Sales status ID = 17 represents a special case for WFC sales
	-- which we want to exclude from certain reporting and screen so this status was
	-- introduced to allow for this.
	-- The logic below represents the changes contract with WFC where the sales are now
	-- process through their systems and we only want to do minimal sales reporting off of this
	-- data which his loaded periodically. We cant populate actual ship date after 14-APR-2009
	-- else these sales would end up in the financial systems which we don't want to happen
	-- this is why there is special logic below to determine how the ship date is derrived
	, CASE
	  WHEN sales_status_id = 17 AND [sales_date] >= '14-APRIL-2009' THEN
			[scheduled_ship_date]
	  ELSE
			[actual_ship_date]
	END as actual_ship_date
	,[actual_ship_date] as orig_actual_ship_date
      	,[shipping_fees]
        ,[shipping_fees_discount]
        ,[payment_due_date]
        ,[confirmed_date]
        ,[scheduled_delivery_date]
        ,[shipping_option_id]
        ,[payment_term_id]
        ,[client_sequence_code]
        ,[client_id]
        ,[sales_status_id]
        ,[payment_method_id]
        ,[po_status_id]
        ,[production_status_id]
        ,[sponsor_consultant_id]
        ,[ar_consultant_id]
        ,[ar_status_id]
        ,[lead_id]
        ,[billing_company_id]
        ,[upfront_payment_method_id]
        ,[confirmer_id]
        ,[collection_status_id]
        ,[confirmation_method_id]
        ,[credit_approval_method_id]
        ,[po_number]
        ,[credit_card_no]
        ,[expiry_date]
        ,[waybill_no]
        ,[comment]
        ,[coupon_sheet_assigned]
        ,[total_amount]
        ,[invoice_date]
        ,[cancellation_date]
        ,[is_ordered]
        ,[po_received_on]
        ,[is_delivered]
        ,[local_sponsor_found]
        ,[box_return_date]
        ,[reship_date]
        ,[upfront_payment_required]
        ,[upfront_payment_due_date]
        ,[sponsor_required]
        ,[actual_delivery_date]
        ,[accounting_comments]
        ,[ssn_number]
        ,[ssn_address]
        ,[ssn_city]
        ,[ssn_state_code]
        ,[ssn_country_code]
        ,[ssn_zip_code]
        ,[is_validated]
        ,[promised_due_date]
        ,[general_flag]
        ,[fuelsurcharge]
        ,[is_packed_by_participant]
        ,[carrier_tracking_id]
        ,[qsp_order_id]
        ,[ext_order_id]
		,[wfc_invoice_number]
		,[cvv2]
    FROM [eFundraisingProd].[dbo].[sale] s
GO
