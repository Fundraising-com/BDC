USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_story_type]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Story_type
CREATE PROCEDURE [dbo].[efrstore_insert_story_type] @Story_type_id int OUTPUT, @Name varchar(50) AS
begin

insert into Story_type(Name) values(@Name)

select @Story_type_id = SCOPE_IDENTITY()

end
GO
