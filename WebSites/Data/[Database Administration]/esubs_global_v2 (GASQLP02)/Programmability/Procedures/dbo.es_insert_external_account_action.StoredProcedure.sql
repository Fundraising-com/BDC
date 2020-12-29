USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_external_account_action]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for External_account_action
CREATE PROCEDURE [dbo].[es_insert_external_account_action] @External_account_action_id int OUTPUT, @External_account_id int, @Action_id int, @Create_date datetime AS
begin

insert into External_account_action(External_account_id, Action_id, Create_date) values(@External_account_id, @Action_id, @Create_date)

select @External_account_action_id = SCOPE_IDENTITY()

end
GO
