USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[pap_get_sales_to_be_processed_v2]    Script Date: 06/15/2015 16:32:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Javier Arellano>
-- Create date: <17-09-2014>
-- Description:	<Get all the sale required to be send to PAP with the new PAP Campaign structure>
-- exec [dbo].[pap_get_sales_to_be_processed_v2]
-- =============================================
ALTER PROCEDURE [dbo].[pap_get_sales_to_be_processed_v2]
	
AS
BEGIN	
	SET NOCOUNT ON;
SELECT DISTINCT
PARTNER.partner_name
,CLIENT.client_id
,pav.value as a_aid
,(case when PROMOTION.script_name <> pav.value then PROMOTION.script_name else NULL end) as a_bid
,PROMOTION.promotion_id
,PROMOTION.promotion_name
,LEAD.lead_id
,SALE.sales_id
,SALE.total_amount - ISNULL(SALE.shipping_fees, 0.00) as total_amount
,pappc.product_category_code as product_category_desc
,CLIENT.client_id
,SALE.client_sequence_code
,SALE.sales_date as sale_date
FROM
eFundraisingProd.DBO.sale SALE (NOLOCK)
JOIN eFundraisingProd.dbo.payment PAYMENT (NOLOCK) ON SALE.sales_id = PAYMENT.sales_id
JOIN eFundraisingProd.DBO.client CLIENT (NOLOCK) ON SALE.client_id = CLIENT.client_id and SALE.client_sequence_code = CLIENT.client_sequence_code
JOIN EFRCommon.DBO.promotion PROMOTION (NOLOCK) ON CLIENT.promotion_id = PROMOTION.promotion_id
JOIN EFRCommon.DBO.partner_promotion PARTNER_PROMOTION (NOLOCK) ON PROMOTION.promotion_id = PARTNER_PROMOTION.promotion_id
JOIN EFRCommon.DBO.partner PARTNER (NOLOCK) ON PARTNER_PROMOTION.partner_id = PARTNER.partner_id
JOIN eFundraisingProd.DBO.lead LEAD (NOLOCK) ON CLIENT.lead_id = LEAD.lead_id
JOIN EFRCommon.dbo.partner_attribute_value PAV (NOLOCK) ON PAV.partner_id = PARTNER.partner_id AND PAV.partner_attribute_id = 12
JOIN eFundraisingProd.DBO.sales_item SALES_ITEM (NOLOCK) ON SALE.sales_id = SALES_ITEM.sales_id
join EfundraisingProd..scratch_book sb on sb.scratch_book_id = SALES_ITEM.scratch_book_id
LEFT JOIN EfundraisingProd..pap_scratchbook_campaign psc ON sb.scratch_book_id = psc.scratch_book_id
LEFT JOIN EfundraisingProd..pap_product_category pappc ON psc.pap_product_category_id = pappc.pap_product_category_id
LEFT JOIN EfundraisingProd..pap_suppressed_product_type papspt on papspt.ext_product_type_id = sb.product_class_id
LEFT JOIN (  select pap_transaction_id, order_id, ext_status_id,  LatestRecord
   from (
    select pap_transaction_id, order_id, ext_status_id,  rank()over (Partition BY order_id order by create_date DESC) as LatestRecord
   from EfundraisingProd..pap_transaction) tmp
   where LatestRecord = 1 and ext_status_id  IN ( 'P', 'A', 'D', 'EA', 'ED', 'EP' )) alreadyProcessed on  SALE.sales_id = alreadyProcessed.order_id
WHERE
papspt.ext_product_type_id is null
AND alreadyProcessed.order_id is null
AND SALE.total_amount > 0
AND sale.sales_id NOT IN (SELECT DISTINCT order_id FROM efundraisingprod.dbo.pap_transaction)
AND SALE.sales_date > getDate()-365 -- One year in the past.
ORDER BY SALE.sales_id DESC
END
