USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_web_action]    Script Date: 02/14/2014 13:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_web_action]
AS
BEGIN

	select event_participation_id
		, member_hierarchy_id
		, [type]
		, [value]
	from web_action;

	
END
GO
