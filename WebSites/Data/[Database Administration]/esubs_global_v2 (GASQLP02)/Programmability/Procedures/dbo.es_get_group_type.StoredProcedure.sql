USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_type]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_group_type] AS
begin

select group_type_id, description, create_date from group_type

end
GO
