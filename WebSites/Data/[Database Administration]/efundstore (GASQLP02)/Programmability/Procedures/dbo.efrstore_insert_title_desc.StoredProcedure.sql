USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_title_desc]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Title_desc
CREATE PROCEDURE [dbo].[efrstore_insert_title_desc] @Title_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(100) AS
begin

insert into Title_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Title_id = SCOPE_IDENTITY()

end
GO
