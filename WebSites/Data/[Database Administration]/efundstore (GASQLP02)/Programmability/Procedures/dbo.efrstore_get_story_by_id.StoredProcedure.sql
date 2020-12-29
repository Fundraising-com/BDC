USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_story_by_id]    Script Date: 02/14/2014 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Story
CREATE PROCEDURE [dbo].[efrstore_get_story_by_id] @Story_id int AS
begin

select Story_id, Story_type_id, Group_type_id, Story_text from Story where Story_id=@Story_id

end
GO
