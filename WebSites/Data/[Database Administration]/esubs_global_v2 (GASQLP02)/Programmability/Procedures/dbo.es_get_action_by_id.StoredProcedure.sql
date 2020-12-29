USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_action_by_id]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Action
CREATE PROCEDURE [dbo].[es_get_action_by_id] @Action_id int AS
begin

select Action_id, Action_desc, Create_date from Action where Action_id=@Action_id

end
GO
