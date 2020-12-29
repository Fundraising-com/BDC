USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_external_accounts]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for External_account
CREATE PROCEDURE [dbo].[es_get_external_accounts] AS
begin

select External_account_id, Food_account_id, Fsm_id, Online_account_id, Event_participation_id, Create_date from External_account

end
GO
