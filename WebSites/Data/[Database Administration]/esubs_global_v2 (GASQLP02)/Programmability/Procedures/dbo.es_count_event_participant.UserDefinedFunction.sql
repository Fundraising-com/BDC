USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_count_event_participant]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
create FUNCTION [dbo].[es_count_event_participant] (@event_id int)  
RETURNS int AS  
BEGIN 
	declare @retour as int

	select @retour = count(event_participation_id) from event_participation  ep
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id

	where event_id = @event_id
	
	return @retour


END
GO
