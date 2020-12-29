USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_story_type_by_id]    Script Date: 02/14/2014 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Story_type
CREATE PROCEDURE [dbo].[efrstore_get_story_type_by_id] @Story_type_id int AS
begin

select Story_type_id, Name from Story_type where Story_type_id=@Story_type_id

end
GO
