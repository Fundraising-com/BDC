USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_group_type_descs]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_type_desc
CREATE PROCEDURE [dbo].[efrstore_get_group_type_descs] AS
begin

select Group_type_id, Culture_code, Description from Group_type_desc

end
GO
