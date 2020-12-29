USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_web_action_by_id]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_web_action_by_id]
	@web_action_id int
AS
BEGIN

	select event_participation_id
		, member_hierarchy_id
		, [type]
		, [value]
		, create_date
	from web_action
	where web_action_id = @web_action_id;

	
END
GO
