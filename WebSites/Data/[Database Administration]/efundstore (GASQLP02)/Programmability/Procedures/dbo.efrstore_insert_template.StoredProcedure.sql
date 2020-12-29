USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_template]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Template
CREATE PROCEDURE [dbo].[efrstore_insert_template] @Template_id int OUTPUT, @Name varchar(100), @Path varchar(1000), @Create_date datetime AS
begin

insert into Template(Name, Path, Create_date) values(@Name, @Path, @Create_date)

select @Template_id = SCOPE_IDENTITY()

end
GO
