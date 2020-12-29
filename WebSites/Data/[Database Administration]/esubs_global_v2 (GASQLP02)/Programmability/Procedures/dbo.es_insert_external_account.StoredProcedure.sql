USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_external_account]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for External_account
CREATE PROCEDURE [dbo].[es_insert_external_account] @External_account_id int OUTPUT, @Food_account_id int, @Fsm_id int, @Online_account_id int, @Event_participation_id int, @Create_date datetime AS
begin

insert into External_account(Food_account_id, Fsm_id, Online_account_id, Event_participation_id, Create_date) values(@Food_account_id, @Fsm_id, @Online_account_id, @Event_participation_id, @Create_date)

select @External_account_id = SCOPE_IDENTITY()

end
GO
