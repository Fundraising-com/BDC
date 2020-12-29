USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_participation_channel]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/* 
Procedure that returns the type of customer in our business     
Created by : Fblais 2005-08-03
1 sponsor
2 got invited
3 self registered and null
*/

CREATE FUNCTION [dbo].[es_get_participation_channel] (@hierarchyID int)
RETURNS int AS  
BEGIN 

declare @member_hierarchy_id int
declare @parent_member_hierarchy_id int
declare @creation_channel_id int
declare @email varchar(100)

SELECT    
	 @member_hierarchy_id = dbo.member_hierarchy.member_hierarchy_id
	,@parent_member_hierarchy_id = dbo.member_hierarchy.parent_member_hierarchy_id
             ,@creation_channel_id = dbo.member_hierarchy.creation_channel_id
	, @email = m.email_address
FROM        
	dbo.member_hierarchy
	inner join member m
	on dbo.member_hierarchy.member_id = m.member_id
where 
	dbo.member_hierarchy.member_hierarchy_id = @hierarchyID

if(@parent_member_hierarchy_id is null)
begin
	return 1 -- un sponsor
end
else if(@creation_channel_id in( 7,9,12,14,20,23) or @email not like '%@efundraising.com')
begin
	return 2 -- une personne invité
end


return 3  --les creations channel à null et les gens self-registered.... pour le moment!


END
GO
