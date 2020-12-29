USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_group_type]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Group_type
CREATE PROCEDURE [dbo].[efrstore_insert_group_type] @Group_type_id int OUTPUT, @Party_type_id tinyint, @Description varchar(50) AS
begin

insert into Group_type(Party_type_id, Description) values(@Party_type_id, @Description)

select @Group_type_id = SCOPE_IDENTITY()

end
GO
