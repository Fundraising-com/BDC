USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_program]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Program
CREATE PROCEDURE [dbo].[efrstore_insert_program] @Program_id int OUTPUT, @Name varchar(50), @Create_date datetime AS
begin

insert into Program(Name, Create_date) values(@Name, @Create_date)

select @Program_id = SCOPE_IDENTITY()

end
GO
