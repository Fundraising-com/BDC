USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsor_profit_report]    Script Date: 02/14/2014 13:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <17-07-2009>
-- Description:	<Provides info for the sponsor how many emails each member sent, what kind of product
-- was purchased, quantities, profit>
-- UPDATE MARCH 14, 2013 by Jiro Hidaka:
--			Participant name was not coming from [users] table
-- exec [dbo].[es_rpt_sponsor_profit_report] 1002418
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_sponsor_profit_report]
	-- Add the parameters for the stored procedure here
	@event_id int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	SET NOCOUNT ON;



	-- supporters orders 
	select
    (case 
	-- sponsor order must be under his name
	when (mp.first_name + ' ' + mp.last_name) is null 
	then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
	          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
	     END
	-- participant orders must be under his name
	when cc.member_type_id = 2
	then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
	          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
	     END
	else CASE WHEN (up.first_name + ' ' + up.last_name) IS NOT NULL THEN dbo.TitleCase(lower(up.first_name + ' ' + up.last_name))
	          ELSE dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
	     END
	end) as part_name
    , tps.order_item_id --CAST(1 as int) as orderdetailid
	, dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) as supp_name
	, tps.quantity as nb_subs--CAST(1 as int) as nb_subs
	, tps.sub_total as amount--CAST(1.00 as money) as amount
	
	,CASE  		    	
		when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
			tps.sub_total * 25.0/100.0
		else
			COALESCE(tps.sub_total * Isnull(ppc.profit_percentage, 40.0)/100.0,0) END as profit--CAST(10.00 as money)  as profit
		
	, convert(varchar(10), (tps.create_date), 121) as updatedate--convert(varchar(10), (GETDATE()), 121) as updatedate
	from event_participation ep
	inner join event_group eg on eg.event_id = ep.event_id 
	inner join [group] g on g.group_id = eg.group_id 
	-- profit
	inner join partner prt
	on prt.partner_id = g.partner_id
	left join partner_payment_config ppc
	on ppc.partner_id = prt.partner_id
	-- order
	inner join  [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	-- enfant
	inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id 
	inner join member m on m.member_id = mh.member_id
	left outer join users u with (nolock) on m.user_id = u.user_id
	-- parent
	left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp on mp.member_id = mhp.member_id
	left outer join users up with (nolock) on mp.user_id = up.user_id
	inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
	where ep.event_id = @event_id
	and mh.active = 1
	order by 1,2

END
GO
