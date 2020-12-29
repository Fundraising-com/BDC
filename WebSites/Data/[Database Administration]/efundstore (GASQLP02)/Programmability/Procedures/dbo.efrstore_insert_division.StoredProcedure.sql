USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_division]    Script Date: 02/14/2014 13:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Division
CREATE PROCEDURE [dbo].[efrstore_insert_division] @Division_id int OUTPUT, @Name varchar(50), @Logo text, @Short_name varchar(10) AS
begin

insert into Division(Name, Logo, Short_name) values(@Name, @Logo, @Short_name)

select @Division_id = SCOPE_IDENTITY()

end
GO
