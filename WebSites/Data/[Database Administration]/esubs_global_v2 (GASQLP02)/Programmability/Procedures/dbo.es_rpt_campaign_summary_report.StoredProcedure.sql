USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_summary_report]    Script Date: 04/17/2015 16:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Description: procedure qui affiche un rapport sommaire pour les campagnes
Ex: EXEC [dbo].[es_rpt_campaign_summary_report] 1554624
mod mcote 	    2006-02-18
                - creation de table temporaire (optimisation)
				- obtention du 100% profit 1rst sub
				- enlever le select imbriquer.
mod pgirard     2006-12-13
                Utilise update_date a la place de create_date
mod pgirard     2006-01-16
                added channel 32 and 33
mod mcote		2010-04-01 -- new profit structure
*/
ALTER PROCEDURE [dbo].[es_rpt_campaign_summary_report] 
	@event_id int
WITH RECOMPILE
AS
BEGIN

	declare @event_id1 int
	DECLARE @nb_subs int 
	DECLARE @amount MONEY
	DECLARE @donation_amount MONEY
	DECLARE @profit MONEY
	DECLARE @LastActivity DATETIME

	SET @event_id1 = @event_id

	SET NOCOUNT OFF

    /*
    < 1 mai 2004        tout est 40%
    <16 sept 2004        le 100% n'a pas de maximum
    <16 sept 2005        le 100% a un maximum de 25$
    >16 sept 2005        le 100%  est sur le premier 25$
    >16 mai 2006	fin de 100% profit sur first sub
    */


SELECT 
  @nb_subs = sum(case 
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then 0
			else quantity end) --as nb_subs
  , @amount = sum(case 
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then 0
			else sub_total end) --as amount--_sold
  , @donation_amount = sum(case 
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then
				sub_total
			else 0 end) --as donation_amount
  , @profit = sum(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then
				(case when tps.create_date < '2011-04-01' then 
					sub_total * 93.5/100.0
				else
					sub_total  * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
		    	when tps.item_type_id in (6,24) and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
					sub_total * 25.0/100.0
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end) -- as profit
  , @LastActivity = max(tps.create_date) --as last_activity
FROM event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
		inner join [event] e on e.event_id = eg.event_id
	    inner join [group] g on g.group_id = eg.group_id 
		INNER JOIN [es_get_valid_orders_items_by_event_id] (@event_id1) tps on tps.supp_id = ep.event_participation_id
		left outer join efrcommon.dbo.profit donation_profit 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11



    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35, 38) then m.member_id else NULL end ) as nb_group_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35, 38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33, 37) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33, 37) then mh.member_id else null end) as nb_supporters
	     , @nb_subs  as nb_subs
             , @amount as amount_sold
	     , @donation_amount as donation_amount_sold
	     , @profit AS profit
	     , @LastActivity AS last_activity 
from event_participation ep
		-- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [event] e on e.event_id = eg.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    -- get donation profit from efrcommon.dbo.profit
	    left outer join efrcommon.dbo.profit donation_profit with (nolock) 
		on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
    where ep.event_id = @event_id1
      and mh.active = 1
    group by ep.event_id
    order by 1, 2

END

