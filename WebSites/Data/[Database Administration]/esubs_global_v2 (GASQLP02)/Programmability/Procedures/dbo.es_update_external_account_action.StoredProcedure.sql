USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_external_account_action]    Script Date: 02/14/2014 13:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for External_account_action
CREATE PROCEDURE [dbo].[es_update_external_account_action] @External_account_action_id int, @External_account_id int, @Action_id int, @Create_date datetime AS
begin

update External_account_action set External_account_id=@External_account_id, Action_id=@Action_id, Create_date=@Create_date where External_account_action_id=@External_account_action_id

end
GO
