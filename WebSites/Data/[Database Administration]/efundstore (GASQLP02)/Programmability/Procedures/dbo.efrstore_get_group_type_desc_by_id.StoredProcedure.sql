USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_group_type_desc_by_id]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Group_type_desc
CREATE PROCEDURE [dbo].[efrstore_get_group_type_desc_by_id] @Group_type_id int AS
begin

select Group_type_id, Culture_code, Description from Group_type_desc where Group_type_id=@Group_type_id

end
GO
