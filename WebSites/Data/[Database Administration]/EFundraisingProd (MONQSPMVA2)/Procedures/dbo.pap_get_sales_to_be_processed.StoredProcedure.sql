USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[pap_get_sales_to_be_processed]    Script Date: 02/14/2014 13:08:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Melissa Cote / Pavel Tarassov>
-- Create date: <26-02-2013>
-- Description:	<Get all the sale required to be send to PAP>
-- exec [dbo].[pap_get_sales_to_be_processed]
-- =============================================
CREATE PROCEDURE [dbo].[pap_get_sales_to_be_processed]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
select DISTINCT p.partner_name
	, pav.value as a_aid
	, (case when pr.script_name <> pav.value then pr.script_name else NULL end) as a_bid
	, pr.promotion_id
	, pr.promotion_name
	, l.lead_id
	, s.sales_id
	, s.total_amount - ISNULL(s.shipping_fees, 0.00) as total_amount
	, (case when pappc.product_category_desc is null then defaultpappc.product_category_desc else pappc.product_category_desc end ) as product_category_desc
	, pappc.product_category_code
	, defaultpappc.product_category_code as default_product_category_code
	, c.client_id
	, s.client_sequence_code
	, s.sales_date as sale_date

from efrcommon..partner p 
inner join efrcommon..partner_promotion pp on p.partner_id = pp.partner_id 
inner join efrcommon..promotion pr on pp.promotion_id = pr.promotion_id 
inner join efrcommon..partner_attribute_value pav on pav.partner_id = p.partner_id and pav.partner_attribute_id = 12
inner join efundraisingprod..lead l on l.promotion_id = pr.promotion_id 
inner join efundraisingprod..client c on c.lead_id = l.lead_id 
inner join efundraisingprod..sale s on s.client_id = c.client_id and  s.client_sequence_code = c.client_sequence_code
inner join efundraisingprod..sales_item si on s.sales_id = si.sales_id
inner join EfundraisingProd..scratch_book sb on sb.scratch_book_id = si.scratch_book_id
left join EfundraisingProd..pap_product_type pappt on sb.product_class_id = pappt.ext_product_type_id and pappt.application_id = 1
left join EfundraisingProd..pap_product_category pappc on pappt.pap_product_category_id = pappc.pap_product_category_id
inner join EfundraisingProd..pap_client_type papct on papct.pap_product_category_id = pappc.pap_product_category_id and papct.ext_client_type_id = c.client_sequence_code
left join EfundraisingProd..pap_suppressed_product_type papspt on papspt.ext_product_type_id = sb.product_class_id
left join EfundraisingProd..pap_product_category defaultpappc on defaultpappc.is_default = 1-- get default category
inner join EfundraisingProd..pap_client_type defaultpapct on defaultpapct.pap_product_category_id = defaultpappc.pap_product_category_id and defaultpapct.ext_client_type_id = c.client_sequence_code
left join (  select pap_transaction_id, order_id, ext_status_id,  LatestRecord
   from (
    select pap_transaction_id, order_id, ext_status_id,  rank()over (Partition BY order_id order by create_date DESC) as LatestRecord
   from EfundraisingProd..pap_transaction) tmp
   where LatestRecord = 1 and ext_status_id  IN ( 'P', 'A', 'D', 'EA', 'ED', 'EP' )) alreadyProcessed on  s.sales_id = alreadyProcessed.order_id
where papspt.ext_product_type_id is null  and alreadyProcessed.order_id is null-- exclude suppressed products 
and s.total_amount > 0

-- ADDED BY JIRO HIDAKA (JULY30, 2013): Only pull sales since the date PAP started which was April 2012
and s.sales_date > '04/15/2012'

-- UPDATED BY JIRO HIDAKA (AUG6, 2013): Only pull daily sales
--and s.sales_date > DATEADD(d,DATEDIFF(d,1,GETDATE()),0)

-- UPDATED BY JIRO HIDAKA (JAN7, 2014): Landed sales does not get confirmed right away so the above logic does not work
AND s.sales_id NOT IN (SELECT DISTINCT order_id FROM efundraisingprod.dbo.pap_transaction)

order by s.sales_id desc 
-- sales agind < 90 days 
-- sales shipped and fully paid 

END
GO
