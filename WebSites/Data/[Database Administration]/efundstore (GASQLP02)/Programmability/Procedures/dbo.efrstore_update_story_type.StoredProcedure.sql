USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_story_type]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Story_type
CREATE PROCEDURE [dbo].[efrstore_update_story_type] @Story_type_id int, @Name varchar(50) AS
begin

update Story_type set Name=@Name where Story_type_id=@Story_type_id

end
GO
