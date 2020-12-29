USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_web_action]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_update_web_action]
	@web_action_id int,
	@event_participation_id int,
	@member_hierarchy_id int,
	@type int,
	@value varchar(50),
	@create_date datetime
AS
BEGIN

	update web_action
		set event_participation_id = @event_participation_id
		, member_hierarchy_id = @member_hierarchy_id
		, [type] = @type
		, [value] = @value
		, create_date = @create_date
	where web_action_id = @web_action_id;

	
END
GO
