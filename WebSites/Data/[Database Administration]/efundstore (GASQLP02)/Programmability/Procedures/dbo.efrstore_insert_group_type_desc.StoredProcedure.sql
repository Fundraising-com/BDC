USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_group_type_desc]    Script Date: 02/14/2014 13:05:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Group_type_desc
CREATE PROCEDURE [dbo].[efrstore_insert_group_type_desc] @Group_type_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

insert into Group_type_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Group_type_id = SCOPE_IDENTITY()

end
GO
