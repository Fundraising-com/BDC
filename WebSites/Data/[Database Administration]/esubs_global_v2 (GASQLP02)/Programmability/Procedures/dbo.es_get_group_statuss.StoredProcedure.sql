USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_statuss]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_status
CREATE PROCEDURE [dbo].[es_get_group_statuss] AS
begin

select Group_status_id, Description from Group_status

end
GO
