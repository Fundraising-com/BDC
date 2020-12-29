USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_story]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Story
CREATE PROCEDURE [dbo].[efrstore_insert_story] @Story_id int OUTPUT, @Story_type_id int, @Group_type_id int, @Story_text text AS
begin

insert into Story(Story_type_id, Group_type_id, Story_text) values(@Story_type_id, @Group_type_id, @Story_text)

select @Story_id = SCOPE_IDENTITY()

end
GO
