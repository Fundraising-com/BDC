USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_hierarchy_backward]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	retourne un member_hierarchy_id pour un ancien identification de esubs
*/
CREATE procedure [dbo].[es_get_member_hierarchy_backward]
	@campaign_id int = null
	,@supporter_id int = null
	,@participant_id int =null
	,@organizer_id int =null
as
if @campaign_id is not null
begin
	select 
		member_hierarchy_id
	from 
		member_hierarchy_identification mhi
		inner join esubs_global.dbo.efo_campaign c
		on c.organizer_id = mhi.organizer_id
	where
		c.campaign_id = @campaign_id		
end
	
if @supporter_id is not null
begin
	select 
		member_hierarchy_id
	from 
		member_hierarchy_identification mhi
	where
		supporter_id = @supporter_id
end

if @participant_id is not null
begin
	select 
		member_hierarchy_id
	from 
		member_hierarchy_identification mhi
	where
		participant_id = @participant_id
end

if @organizer_id is not null
begin
	select 
		member_hierarchy_id
	from 
		member_hierarchy_identification mhi
	where
		organizer_id = @organizer_id
end
GO
