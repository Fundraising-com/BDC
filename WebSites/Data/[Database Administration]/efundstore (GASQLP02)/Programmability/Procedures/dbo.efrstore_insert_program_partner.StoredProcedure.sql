USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_program_partner]    Script Date: 02/14/2014 13:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Program_partner
CREATE PROCEDURE [dbo].[efrstore_insert_program_partner] @Program_id int OUTPUT, @Partner_id int, @Program_url varchar(255), @Create_date datetime AS
begin

insert into Program_partner(Partner_id, Program_url, Create_date) values(@Partner_id, @Program_url, @Create_date)

select @Program_id = SCOPE_IDENTITY()

end
GO
