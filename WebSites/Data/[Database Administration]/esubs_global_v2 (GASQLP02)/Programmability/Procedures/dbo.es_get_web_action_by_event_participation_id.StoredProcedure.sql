USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_web_action_by_event_participation_id]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_web_action_by_event_participation_id]
	@event_participation_id int
	, @type int
AS
BEGIN

	if(@type is not null) 
	begin
		select top 1 web_action_id
			, event_participation_id
			, member_hierarchy_id
			, [type]
			, [value]
			, create_date
		from web_action
		where event_participation_id = @event_participation_id
			and [type] = @type
		order by create_date desc;
	end
	else
	begin
		select top 1 web_action_id
			,event_participation_id
			, member_hierarchy_id
			, [type]
			, [value]
			, create_date
		from web_action
		where event_participation_id = @event_participation_id
		order by create_date desc;
	end
	
END
GO
