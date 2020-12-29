USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_subs_by_event_participant]    Script Date: 02/14/2014 13:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_subs_by_event_participant] (@event_Participation_id int)  
RETURNS int AS  
BEGIN 
	declare @retour as int
	declare @event_id int
	declare @member_hierarchy_id int
	declare @somme int

	select
		@member_hierarchy_id = member_hierarchy_id
		,@event_id = event_id
	from 
		event_participation 
	where 
		event_participation_id = @event_participation_id

	select @somme = isnull(sum(totalQuantity),0) from event_participation  ep
	inner join QSPStore.dbo.TotalsPerSaleTable tps
	on ep.event_participation_id = tps.suppid
	where event_participation_id = @event_participation_id
	
	set @retour = @somme
	set @somme = null

	select @somme = isnull(sum(totalQuantity),0) 
	from event_participation  ep
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
	and mh.parent_member_hierarchy_id = @member_hierarchy_id 
	inner join QSPStore.dbo.TotalsPerSaleTable tps
	on ep.event_participation_id = tps.suppid
	where event_id = @event_id
	
	set @retour = @retour + @somme

	
	
	return @retour


END
GO
