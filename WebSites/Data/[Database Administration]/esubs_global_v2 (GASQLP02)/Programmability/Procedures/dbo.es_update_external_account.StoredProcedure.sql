USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_external_account]    Script Date: 02/14/2014 13:07:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for External_account
CREATE PROCEDURE [dbo].[es_update_external_account] @External_account_id int, @Food_account_id int, @Fsm_id int, @Online_account_id int, @Event_participation_id int, @Create_date datetime AS
begin

update External_account set Food_account_id=@Food_account_id, Fsm_id=@Fsm_id, Online_account_id=@Online_account_id, Event_participation_id=@Event_participation_id, Create_date=@Create_date where External_account_id=@External_account_id

end
GO
