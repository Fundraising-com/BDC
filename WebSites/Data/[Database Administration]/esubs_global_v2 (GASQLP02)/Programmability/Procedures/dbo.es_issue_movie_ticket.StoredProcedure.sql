USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_issue_movie_ticket]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 2006/04/28
-- Description:	Generate movie ticket touch emails 
-- Modified by: Melissa Cote 
-- Modif date:  2010/07/29
-- Description: Correct the script to reactivate the touches
--				due to the hard coded data on the orders query 
-- =============================================
CREATE PROCEDURE [dbo].[es_issue_movie_ticket]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- =======================
    -- pre-generate the sales
    -- =======================
    create table #tps (
	    rownum int identity(1,1)
	    , act int
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
	    , event_id int
        , createdate datetime
    )

    -- ==================================
    -- container for the prize assigned
    -- ==================================
    
    create table #movie_ticket (
        event_participation_id int
        , business_rule_id smallint
        , prize_id smallint
    )
    
    
	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate
	from
	(
		SELECT orders.order_id AS orderid
			, orders.quantity
			, orders.sub_total AS price
			, orders.supp_id AS suppid
			, orders.event_id
			, orders.create_date AS createdate
		FROM dbo.es_get_valid_orders_items() orders join event_participation ep with (nolock)
		  ON orders.supp_id = ep.event_participation_id join event e with (nolock)
		  ON ep.event_id = e.event_id
		WHERE e.active = 1 AND supp_id NOT IN -- IGNORE ROWS WHERE THEY HAVE ALREADY BEEN ISSUED A PRIZE
			(SELECT event_participation_id FROM earned_prize WHERE event_participation_id IS NOT NULL)
	) t
	order by createdate
 		, price
    
    -- Put an index on suppid since that is the only join will do
    create index ix_tps_suppid on #tps (suppid)    
    
    /*
    ==================================
    Participant inviting supporter
    ==================================
    */
    
    insert into #movie_ticket
    select ep.event_participation_id
           , 75 -- business_rule_id
           , 6  -- prize_id
           -- debug info
           --, child.nb
           --, isnull(child.nb_order,0) + isnull(tps.quantity,0) as nb_order
           --, t.event_participation_id
           -- validation
           --, 'exec es_rpt_supporters_invited ' + CAST(ep.event_participation_id as varchar(20))
    from member m with(nolock)
        inner join member_hierarchy mh with(nolock)
            on mh.member_id = m.member_id
        inner join event_participation ep with(nolock)
            on ep.member_hierarchy_id = mh.member_hierarchy_id
        inner join event_group eg with(nolock)
            on eg.event_id = ep.event_id
        inner join [group] g with(nolock)
			on g.group_id = eg.group_id
		inner join event e  with(nolock)
			on e.event_id = eg.event_id
        left join (
            select suppID, sum(tps.quantity) as quantity, max(tps.createDate) as createDate 
            from #tps tps
            group by suppID
        ) tps
            on tps.suppID = ep.event_participation_id
        inner join (
            -- Childs
            select parent_member_hierarchy_id, count(distinct mh.member_hierarchy_id) as nb, sum(tps.quantity) as nb_order,  max(tps.createDate) as createDate 
            from member_hierarchy mh with(nolock)
                inner join member m with(nolock)
                    on m.member_id = mh.member_id
                inner join event_participation ep with(nolock)
                    on ep.member_hierarchy_id = mh.member_hierarchy_id
                left join #tps tps
                    on tps.suppID = ep.event_participation_id
            where m.bounced = 0 
              and (m.email_address not like '%@efundraising.com' or m.email_address not like '%@fundraising.com')
              and mh.creation_channel_id in (12,14)
            group by mh.parent_member_hierarchy_id
        ) child on child.parent_member_hierarchy_id = mh.member_hierarchy_id
        -- mcote 2010.07.29 we don't want to send email if already redeam
        left join (
            select event_participation_id
            from earned_prize epr with(nolock)
                inner join prize_item pri with(nolock)
                    on pri.prize_item_id = epr.prize_item_id
            where pri.prize_id = 6
        ) prize on prize.event_participation_id = ep.event_participation_id
       
        -- We need to check the touch to make sure we send only one email
        left join (
            select t.event_participation_id
            from touch t with(nolock)
                inner join touch_info ti with(nolock)
                    on ti.touch_info_id = t.touch_info_id
            where ti.business_rule_id in (11,75,110)
        ) t on t.event_participation_id = ep.event_participation_id
		left join efundweb..[partner] p  with(nolock) on p.partner_id = g.partner_id 
		
		-- jhidaka 2013.10.16 Do not send to members who are themselves sponsors
		/*left join (
			select distinct u.user_id
			from [users] u with (nolock)
				join member m with (nolock) on u.user_id = m.user_id
				join member_hierarchy mh with (nolock) on m.member_id = mh.member_id
				join event_participation ep with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
			where ep.participation_channel_id=3
		) sponsor on m.user_id = sponsor.user_id*/
    where m.bounced = 0
      and (m.email_address not like '%@efundraising.com' or m.email_address not like '%@fundraising.com')
      and child.nb >= 12
      and (isnull(child.nb_order,0) + isnull(tps.quantity,0)) > 0
      and t.event_participation_id is null
	  and  prize.event_participation_id is null --mcote 2010.07.29 validate the code is not already redeam
	  --and sponsor.user_id is null --jhidaka 2013.10.16 Do not send to members who are themselves sponsors
      and ((g.partner_id in (57,589,593,606,609) and ep.create_date > '2006-03-21') 
			or g.partner_id not in (57,143,589,593,606,609))
	  and (child.createDate < '2009-10-01' or child.createDate > '2010-06-01' or child.createDate is null )	-- mcote 2010.07.29 reactivate movie ticket after 2010 issue 
	  and (tps.createDate < '2009-10-01' or tps.createDate > '2010-06-01' or tps.createDate is null)			-- email were not going out because order query blocked as of 2009.10.01
	  and (p.prize_eligible = 1 or p.prize_eligible is null) -- mcote 2010.07.29 add restriction base on the partner table values
	  and e.culture_code = 'en-US'  -- mcote 2010.07.29 exclude the CA groups
	  and ep.participation_channel_id<>3
	  and e.active = 1

    /*select * from PArtner where partner_name like 'Kappa%'
    ===============================================
        Sponsor inviting participant
    ===============================================
    */
    /*
	-- disabled by phil on october 3rd 2006
	-- sponsor does not receive movie ticket anymore
	
    insert into #movie_ticket
    select ep.event_participation_id
        , 73
        , 2
        -- debug info
        --, child.nb
        --, isnull(tps.quantity,0) as nb_order
        --, prize.event_participation_id
        --, t.event_participation_id
        -- validation
        --, 'exec es_rpt_campaign_summary_report ' + CAST(ep.event_id as varchar(20))
    from member m
        inner join member_hierarchy mh
            on mh.member_id = m.member_id
        inner join event_participation ep
            on ep.member_hierarchy_id = mh.member_hierarchy_id
        inner join event_group eg
            on eg.event_id = ep.event_id
        inner join [group] g
            on g.group_id = eg.group_id
        left join (
            select ep.event_id, sum(tps.quantity) as quantity
            from #tps tps
                inner join event_participation ep
                    on ep.event_participation_id = tps.suppID
            group by ep.event_id
        ) tps
            on tps.event_id = eg.event_id
        inner join (
            -- Childs
            select parent_member_hierarchy_id, count(mh.member_hierarchy_id) as nb, sum(tps.quantity) as nb_order
            from member_hierarchy mh
                inner join member m
                    on m.member_id = mh.member_id
                inner join event_participation ep
                    on ep.member_hierarchy_id = mh.member_hierarchy_id
                left join #tps tps
                    on tps.suppID = ep.event_participation_id
            where m.bounced = 0 
              and m.email_address not like '%@efundraising.com'
              and mh.creation_channel_id in (7,20,23)
            group by mh.parent_member_hierarchy_id
        ) child on child.parent_member_hierarchy_id = mh.member_hierarchy_id
        -- We need to check the touch to make sure we send only one email
        left join (
            select t.event_participation_id
            from touch t
                inner join touch_info ti
                    on ti.touch_info_id = t.touch_info_id
            where ti.business_rule_id in (10,73,110)
        ) t on t.event_participation_id = ep.event_participation_id
        -- And we check the prizes too coz old sponsor are only in prizes table... 
        left join (
            select event_participation_id
            from earned_prize epr
                inner join prize_item pri
                    on pri.prize_item_id = epr.prize_item_id
            where pri.prize_id = 2
              and epr.create_date < '2005-09-15 21:30:00' --launch date
        ) prize on prize.event_participation_id = ep.event_participation_id
    where m.bounced = 0
      and m.email_address not like '%@efundraising.com'
      and child.nb >= 12
      and isnull(tps.quantity,0) > 0
      and t.event_participation_id is null
      and prize.event_participation_id is null
      and ((g.partner_id in (57,143,589,593,606,609) and ep.create_date > '2006-03-21') or g.partner_id not in (57,143,589,593,606,609))    
	*/

    /*
    =================================
        Sponsor inviting supporters
    =================================
    */
    /*
    -- disabled by phil on october 3rd 2006
	-- sponsor does not receive movie ticket anymore
	
    insert into #movie_ticket
    select ep.event_participation_id
        , 74
        , 5
        -- debug info
        --, child.nb
        --, isnull(tps.quantity,0) as nb_order
        --, prize.event_participation_id
        --, t.event_participation_id
        -- validation
        --, 'exec es_rpt_supporters_invited ' + CAST(ep.event_participation_id as varchar(20))
    from member m
        inner join member_hierarchy mh
            on mh.member_id = m.member_id
        inner join event_participation ep
            on ep.member_hierarchy_id = mh.member_hierarchy_id
        inner join event_group eg
            on eg.event_id = ep.event_id
        inner join [group] g
            on g.group_id = eg.group_id
        left join (
            select ep.event_id, sum(tps.quantity) as quantity
            from #tps tps
                inner join event_participation ep
                    on ep.event_participation_id = tps.suppID
            group by ep.event_id
        ) tps
            on tps.event_id = eg.event_id
        inner join (
            -- Childs
            select parent_member_hierarchy_id, count(mh.member_hierarchy_id) as nb, sum(tps.quantity) as nb_order
            from member_hierarchy mh
                inner join member m
                    on m.member_id = mh.member_id
                inner join event_participation ep
                    on ep.member_hierarchy_id = mh.member_hierarchy_id
                left join #tps tps
                    on tps.suppID = ep.event_participation_id
            where m.bounced = 0 
              and m.email_address not like '%@efundraising.com'
              and mh.creation_channel_id = 29
            group by mh.parent_member_hierarchy_id
        ) child on child.parent_member_hierarchy_id = mh.member_hierarchy_id
        -- We need to check the touch to make sure we send only one email
        left join (
            select t.event_participation_id
            from touch t
                inner join touch_info ti
                    on ti.touch_info_id = t.touch_info_id
            where ti.business_rule_id in (74,110)
        ) t on t.event_participation_id = ep.event_participation_id
        -- we need to check the prizes coz we dont have the old ones in the touch
        -- but we only check the old prize
        left join (
            select event_participation_id
            from earned_prize epr
                inner join prize_item pri
                    on pri.prize_item_id = epr.prize_item_id
            where pri.prize_id = 5
              and epr.create_date < '2005-09-15 21:30:00' --launch date
        ) prize on prize.event_participation_id = ep.event_participation_id
    where m.bounced = 0
      and m.email_address not like '%@efundraising.com'
      and child.nb >= 12
      and isnull(tps.quantity,0) > 0
      and t.event_participation_id is null
      and prize.event_participation_id is null
      and ((g.partner_id in (57,143,589,593,606,609) and ep.create_date > '2006-03-21') or g.partner_id not in (57,143,589,593,606,609))    
    */
    
    declare @prize_item_id int
    declare @event_participation_id int
    declare @business_rule_id int
    declare @touch_info_id int
    declare @prize_id int

    while exists (select event_participation_id from #movie_ticket)
    begin
	    set @prize_item_id = null
	    set @event_participation_id = null
	    set @business_rule_id = null
	    set @touch_info_id =null
	    set @prize_id = null
        
	    select top 1
		    @event_participation_id = event_participation_id
		    ,@business_rule_id = business_rule_id
		    ,@prize_id = prize_id
	    from 
		    #movie_ticket
	
	/*
	    select top 1 @prize_item_id = pt.prize_item_id
	    from prize_item pt
		    left outer join earned_prize pe
		        on pe.prize_item_id = pt.prize_item_id
	    where
		    pt.prize_id = @prize_id
	        and pe.prize_item_id is null
	        and	pt.expiration_date > getdate() + 20
    	

	    if @prize_item_id is null
		    break  -- il n'y a plus de prize à donner
    	
	    begin transaction 
	    
	    insert into earned_prize (
            event_participation_id
            , prize_item_id
            , create_date
        ) values (
            @event_participation_id
            , @prize_item_id
            , getdate()
        )

	    if @@error <> 0
	    begin
		    rollback transaction
            
		    delete from #movie_ticket
		    where event_participation_id = @event_participation_id
			  and business_rule_id = @business_rule_id

		    if @@error <> 0
			    break

		    continue
	    end
        */
	 begin transaction 

	    insert into touch_info (
            business_rule_id
            , visitor_log_id
            , launch_date
            , create_date
        ) values (
            @business_rule_id
            , null
            , getdate()
            , getdate()
        )

	    if @@error <> 0
	    begin
		    rollback transaction
            
		    delete from #movie_ticket
		    where event_participation_id = @event_participation_id
			  and business_rule_id = @business_rule_id
            
		    if @@error <> 0
			    break
            
		    continue
	    end
        
	    set @touch_info_id = @@identity
        
	    insert into touch (
            event_participation_id
            , member_hierarchy_id
            , touch_info_id
            , processed
            , create_date
        ) values (
            @event_participation_id
            , null
            , @touch_info_id
            , 0
            , getdate()
        )

	    if @@error <> 0
	    begin
		    rollback transaction
            
		    delete from #movie_ticket
		    where event_participation_id = @event_participation_id
			  and business_rule_id = @business_rule_id
            
		    if @@error <> 0
			    break
            
		    continue
	    end

	    commit transaction
        
	    delete from #movie_ticket
	    where event_participation_id = @event_participation_id
          and business_rule_id = @business_rule_id

	    if @@error <> 0
		    break
    end

    drop table #movie_ticket
    drop table #tps

END
GO
