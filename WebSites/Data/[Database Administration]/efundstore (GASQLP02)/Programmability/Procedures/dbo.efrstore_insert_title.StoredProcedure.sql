USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_title]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Title
CREATE PROCEDURE [dbo].[efrstore_insert_title] @Title_id int OUTPUT, @Party_type_id tinyint, @Title_desc varchar(50) AS
begin

insert into Title(Party_type_id, Title_desc) values(@Party_type_id, @Title_desc)

select @Title_id = SCOPE_IDENTITY()

end
GO
