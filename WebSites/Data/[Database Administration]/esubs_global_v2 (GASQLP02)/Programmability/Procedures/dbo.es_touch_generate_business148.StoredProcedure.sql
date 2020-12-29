USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business148]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Melissa Cote
-- ALTER  date: 2008/11/27
-- Description:	Participant – Daily notification of sale
--		Rule: Send on event of sale associated with participant
--		Repeat: Yes – Once a day max
--		Subject: One of your friends bought a magazine from your fundraiser!
--		Reply to Name: <Partner Name>
--		Reply to Email: efr-online@magfundraising.com
--		From Name: <Partner Name>
--		From Email: efr-online@magfundraising.com
--		Email Template : 426 
-- =================================================

CREATE PROCEDURE [dbo].[es_touch_generate_business148]
AS
BEGIN
	SET NOCOUNT ON;
    
    declare @touch_info_id int
	declare @from_date datetime
	declare @to_date datetime
	declare @to_date2 varchar(30)

	set @from_date = getdate() 
	set @from_date = CONVERT(varchar(20), @from_date, 101) 
    set @to_date2 = convert(varchar(30), @from_date, 101) + ' 23:59:59'
    set @to_date = convert(datetime, @to_date2)


--set @from_date = DATEADD(day, -1-DATEPART(dw, @from_date), @from_date)
--	set @to_date = DATEADD(day, , @from_date)

	--print @from_date
	--print @to_date
	--print DATEDIFF(day, '2008-10-10', @to_date)

    begin transaction

    insert into touch_info(
	    business_rule_id
	    ,launch_date
	    ,create_date
    )values(
	    148
	    ,getdate()
	    ,getdate()
    )
	
	set @touch_info_id = SCOPE_IDENTITY()

    if @@error <> 0
    begin
	    rollback transaction
    end
    else
    begin
    	
	    

	    insert into touch(event_participation_id,touch_info_id,processed,create_date)
	    select 
			    ep.event_participation_id
			    ,@touch_info_id as touch_info_id
			    ,0 as processed
			    --,dbo.es_count_event_supp_invited_date(ep.event_id, @from_date, @to_date) as nb_emails
				--,dbo.es_subs_by_event_date(ep.event_id, @from_date, @to_date) as nb_subs
			    , getdate() as create_date
				--, isnull(nb_emails, 0) as nb_emails
				--, isnull(nb_subs, 0) as nb_subs
		    from 
			    event e  with(nolock)
			    inner join event_group eg with(nolock)
			    on eg.event_id = e.event_id
			    inner join [group] g with(nolock)
			    on g.group_id = eg.group_id
			    inner join event_participation ep with(nolock)
			    on e.event_id = ep.event_id
			    and ep.participation_channel_id = 3 --sponsor
			    left outer join (
				    select t.event_participation_id
						, max(t.create_date) as create_date
				    from 
					    touch t		 with(nolock)
				    inner join touch_info ti with(nolock)
				    on ti.touch_info_id = t.touch_info_id
				    and business_rule_id = 148
					group by t.event_participation_id
			    ) t
			    on t.event_participation_id = ep.event_participation_id
    			left outer join (
					select count(distinct mh.member_id) as nb_emails
						, event_id
					from event_participation  ep with(nolock)
					inner join member_hierarchy mh with(nolock)
						on mh.member_hierarchy_id = ep.member_hierarchy_id
					where mh.creation_channel_id in(12,14,29,32, 35,38)
						and ep.create_date between @from_date and @to_date 
					group by event_id
				)supp on supp.event_id = e.event_id
				inner join ( -- email need to be sent only is sales assiciated to participant mcote 2012-01-13
					select 
						 sum(od.quantity) as nb_subs
						 , event_id
					from qspecommerce.dbo.efundraisingtransaction et with(nolock)
						inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid
						inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
						inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
						inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id -- mcote mcote 2012-01-13
					where --o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701)and
  				  		 o.create_date between @from_date and @to_date 
					group by event_id
				)tps on tps.event_id = e.event_id

		    where 
			(t.event_participation_id is null or DATEDIFF(day, t.create_date, @to_date) > 7)
		    -- and e.create_date > '2008-09-01 00:00:00' -- la date du lancement
			and e.culture_code = 'en-US'
			and e.active = 1
		    --and 	g.partner_id <> 143
			and (isnull(nb_emails, 0) > 0 or isnull(nb_subs, 0) > 0)

	    IF @@ROWCOUNT = 0 -- no rows inserted
	    begin
		    rollback transaction
	    end
	    else
	    begin
		    commit transaction
	    end
    end
END
GO
