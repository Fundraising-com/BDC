USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_actions]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_actions] AS
begin

select Action_id, Action_desc, Create_date from Action

end
GO
