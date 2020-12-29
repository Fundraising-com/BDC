USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_qualify_movie]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*retourne le nombre de email envoyé et de subs fait par un member dans un campagne*/

CREATE PROCEDURE [dbo].[es_qualify_movie]
	@event_participation_id int
as
declare @participation_channel tinyint
declare @event_id int
declare @member_hierarchy_id int

select 
	@participation_channel = participation_channel_id
	,@event_id = event_id
	,@member_hierarchy_id =mh.member_hierarchy_id
from 
	event_participation ep
	inner join member_hierarchy mh
	on ep.member_hierarchy_id = mh.member_hierarchy_id
where 
	event_participation_id = @event_participation_id	


if @participation_channel =3
begin
	select
		case when  nb_part >= 12 /*and sum(totalquantity) >=1*/ then 1 else 0 end as earned
		,nb_part as nb_email
		,0 as nb_subs --sum(totalquantity) as nb_subs
	from 
		event e
		inner join event_participation ep
		on ep.event_id = e.event_id
		--left outer join qspstore.dbo.es_totals_per_sale tps
		--on tps.suppid = ep.event_participation_id
		left outer join (
			select 
				event_id
				,count(event_participation_id) as nb_part
			from 
				event_participation ep
				inner join member_hierarchy mh
				on mh.member_hierarchy_id = ep.member_hierarchy_id
			where
				mh.creation_channel_id in(7,20,23)
				and event_id = @event_id
			group by 
				ep.event_id
		) a
		on a.event_id  = e.event_id
	group by 
		e.event_id
		,nb_part
end
else
begin
	select
		case when  nb_supp >= 12 /*and sum(totalquantity) >=1*/ then 1 else 0 end as earned
		,nb_supp as nb_email
		,0 as nb_subs --sum(totalquantity) as nb_subs
	from 
		event_participation ep
		--left outer join qspstore.dbo.es_totals_per_sale tps
		--on tps.suppid = ep.event_participation_id
		left outer join (
			select 
				parent_member_hierarchy_id
				,count(event_participation_id) as nb_supp
			from 
				event_participation ep
				inner join member_hierarchy mh
				on mh.member_hierarchy_id = ep.member_hierarchy_id
			where
				mh.creation_channel_id in(12,14)
				and mh.parent_member_hierarchy_id = @member_hierarchy_id
				and ep.event_id = @event_id
			group by 
				parent_member_hierarchy_id
		) a
		on a.parent_member_hierarchy_id  = ep.member_hierarchy_id
	where
		ep.event_participation_id = @event_participation_id
	group by 
		ep.event_participation_id
		,nb_supp

end
GO
