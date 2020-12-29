USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_statuss]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_status
create PROCEDURE [dbo].[es_get_event_statuss] AS
begin

select event_status_id, event_status_name, create_date from event_status

end

grant execute on [dbo].[es_get_event_statuss] TO db_stored_proc_exec
grant execute on [dbo].[es_get_event_statuss] TO proc_exec
GO
