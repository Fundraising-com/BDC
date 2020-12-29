USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_group_lead]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_update_group_lead]
		@lead_id int
		,@group_id int
as
update [group]
set lead_id = @lead_id
where group_id = @group_id

if @@error <> 0 
begin
	return - 1
end
else
begin
	return 0 
end
GO
