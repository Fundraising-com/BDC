USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_types]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_status
create PROCEDURE [dbo].[es_get_event_types] AS
begin

select event_type_id, event_type_name, create_date from event_type

end
GO
