USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_external_account_action_by_id]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for External_account_action
CREATE PROCEDURE [dbo].[es_get_external_account_action_by_id] @External_account_action_id int AS
begin

select External_account_action_id, External_account_id, Action_id, Create_date from External_account_action where External_account_action_id=@External_account_action_id

end
GO
