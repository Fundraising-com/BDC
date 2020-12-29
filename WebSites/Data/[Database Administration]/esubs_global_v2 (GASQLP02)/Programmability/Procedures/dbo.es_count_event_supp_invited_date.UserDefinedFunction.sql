USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_count_event_supp_invited_date]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_count_event_supp_invited_date] (
		@event_id int
		, @from_date datetime
		, @end_date datetime
)  
RETURNS int AS  
BEGIN 
	declare @retour as int

	select @retour = count(distinct mh.member_id)
	from event_participation  ep
		inner join member_hierarchy mh
			on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join creation_channel cc
			on mh.creation_channel_id = cc.creation_channel_id
	where mh.creation_channel_id in(12,14,29,32, 35, 38)
		and ep.create_date between @from_date and @end_date 
		and event_id = @event_id
	
	return @retour
END
GO
