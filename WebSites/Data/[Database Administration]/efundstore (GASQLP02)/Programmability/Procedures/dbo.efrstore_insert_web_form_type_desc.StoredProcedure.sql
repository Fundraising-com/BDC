USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_web_form_type_desc]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Web_form_type_desc
CREATE PROCEDURE [dbo].[efrstore_insert_web_form_type_desc] @Web_form_type_id int OUTPUT, @Culture_code nvarchar(10), @Description varchar(256) AS
begin

insert into Web_form_type_desc(Culture_code, Description) values(@Culture_code, @Description)

select @Web_form_type_id = SCOPE_IDENTITY()

end
GO
