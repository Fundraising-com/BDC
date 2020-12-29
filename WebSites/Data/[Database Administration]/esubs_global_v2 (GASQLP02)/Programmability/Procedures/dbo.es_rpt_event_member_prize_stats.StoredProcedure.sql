USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_event_member_prize_stats]    Script Date: 10/28/2014 15:28:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Description : procedure qui retourne les infos pour les movies tickets
              returns all the participant with the emails they sent the subs they made

              Sort them into the two tables on the webpage

Ex: EXEC [dbo].[es_rpt_event_member_prize_stats] 1001011222

mod pgirard     2006-12-15
                changé l'utilisation du ALTER  date pour update date

mod pgirard     2007-01-16
                Ajouté les creation channel 32 et 33

mod pgirard     2007-05-11
                Modifié la requete pour allé cherché les ventes des sous-membres
mod mcote 	2008-08-28
		Add creadtion channel 37, 41

mode jhidaka	2013-03-14
				first_name must come from users table

exec [dbo].[es_rpt_event_member_prize_stats] 1072141
*/
ALTER  PROCEDURE [dbo].[es_rpt_event_member_prize_stats]-- 1072141
        @event_id int
AS
BEGIN

 declare @parent_member_hierarchy_id int
 select @parent_member_hierarchy_id = member_hierarchy_id from event_participation ep with (nolock)
  where event_id = @event_id and participation_channel_id=3

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
		    end)  as first_name
            ,'' as last_name
            , 0 as event_participation_id 
            , count ( distinct case when mh.creation_channel_id in(12,14,29,33,37,38,41)
		    then m.member_id else NULL end ) as nb_supp
	    , sum(quantity) as nb_subs

    from event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
	    -- order
            left outer join [es_get_valid_orders_items_by_event_id] (@event_id) tps
	    on tps.supp_id = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    left join users u with (nolock) on m.user_id = u.user_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    left join users up with (nolock) on mp.user_id = up.user_id
	    left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	    and 	mh.active = 1 and mh.unsubscribe = 0 --and mh.parent_member_hierarchy_id<>@parent_member_hierarchy_id
    group by  (case 
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
		    end)  
    order by 1, 2


END

