USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_param_email_429]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_param_email_429]
		@identification int
		,@source_id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
select 
		 @source_id as [source_id]
		, dbo.[es_insert_space_before_CapCase]( em.recipient_name) as [supporter]
		, em.email_address as [email_address]
		, 429 as [email_template_id]
		, case 
			when ppo.product_offer_id in (3,4) then 'eFundraising Online'
			else p.partner_name
		end as [partner_name]
		, g.group_name as [group_name]
		, e.event_name as [event_name]
		, efrtr.SuppID  as [supp_id]
		, efrtr.qxtrak as [qx_trak]
		, st.opportunity_id as account_number 
--		, ci.catalog_item_code as [item_code]
		, (select top 1 catalog_item_id from qspfulfillment..catalog_item ci1 where ci1.catalog_item_code = ci.catalog_item_code order by catalog_item_id desc) as [catalog]
		, ci.catalog_item_name as [magazine]
		, @identification as [identification]
		
		from
		member m with(nolock)
		inner join member_hierarchy mh with(nolock)
		on mh.member_id = m.member_id
		inner join event_participation ep with(nolock)
		on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join event e with(nolock)
		on ep.event_id = e.event_id
		inner join event_group eg with(nolock)
		on eg.event_id = e.event_id
		inner join [group] g with(nolock)
		on g.group_id = eg.group_id
		inner join partner p with(nolock)
		on g.partner_id = p.partner_id
		inner join partner_store_template pit with(nolock)
		on  p.partner_id = pit.partner_id 
		inner join partner_product_offer ppo with (nolock) 
		on ppo.partner_id = p.partner_id
		inner join store_template st with(nolock)
		on pit.store_template_id = st.store_template_id
		inner join qspecommerce..efundraisingtransaction efrtr with(nolock)
		on ep.event_participation_id = efrtr.SuppId
		inner join qspfulfillment..[order] ord with(nolock)
		on efrtr.OrderId = ord.order_id
		inner join qspfulfillment..email em with(nolock)
		on ord.billing_email_id = em.email_id
		inner join qspfulfillment..order_detail ord_d with(nolock)
		on ord.order_id = ord_d.order_id
		inner join qspfulfillment..catalog_item_detail cid with(nolock)
		on cid.catalog_item_detail_id = ord_d.catalog_item_detail_id
		inner join qspfulfillment..catalog_item ci with(nolock)
		on ci.catalog_item_id = cid.catalog_item_id
		where 
			efrtr.Id = @identification 
			and ord_d.order_detail_id =
				(
					select top 1 odd.order_detail_id 
					from qspfulfillment..order_detail odd  with(nolock)
					where odd.order_id = ord_d.order_id
					order by odd.price desc )
			
END
GO
