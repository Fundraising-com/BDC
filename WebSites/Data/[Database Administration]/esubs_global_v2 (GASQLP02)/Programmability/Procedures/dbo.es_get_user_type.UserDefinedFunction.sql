USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_user_type]    Script Date: 02/14/2014 13:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 
Procedure that returns the type of customer in our business     
Created by : Fblais 2005-08-03
1 sponsor
2 participants
3 supporters
*/

CREATE FUNCTION [dbo].[es_get_user_type] (@hierarchyID int)
RETURNS int AS  
BEGIN 

declare @member_hierarchy_id int
declare @parent_member_hierarchy_id int
declare @creation_channel_id int

SELECT    
	 @member_hierarchy_id = dbo.member_hierarchy.member_hierarchy_id
	,@parent_member_hierarchy_id = dbo.member_hierarchy.parent_member_hierarchy_id
             ,@creation_channel_id = dbo.member_hierarchy.creation_channel_id
FROM        
	dbo.member_hierarchy
where 
	dbo.member_hierarchy.member_hierarchy_id = @hierarchyID

if(@parent_member_hierarchy_id is null)
begin
	return 1 -- un sponsor
end
else if(@creation_channel_id in( 7,8,20,22,23, 35, 36))
begin
	return 2 -- un participant
end
else if exists( select * from member_hierarchy where parent_member_hierarchy_id = @member_hierarchy_id)  
begin
	return 2  -- un supporter qui a invité donc un participant  
		--ou encore les participants avec des creations channels à null
end


return 3  --les supporters


END
GO
