USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_story]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Story
CREATE PROCEDURE [dbo].[efrstore_update_story] @Story_id int, @Story_type_id int, @Group_type_id int, @Story_text text AS
begin

update Story set Story_type_id=@Story_type_id, Group_type_id=@Group_type_id, Story_text=@Story_text where Story_id=@Story_id

end
GO
