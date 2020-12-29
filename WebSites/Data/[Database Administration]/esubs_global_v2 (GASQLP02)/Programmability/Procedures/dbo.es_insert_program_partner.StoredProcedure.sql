USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_program_partner]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Program_partner
CREATE PROCEDURE [dbo].[es_insert_program_partner] @Program_id int OUTPUT, @Partner_id int, @Program_url varchar(255), @Create_date datetime AS
begin

insert into Program_partner(Partner_id, Program_url, Create_date) values(@Partner_id, @Program_url, @Create_date)

select @Program_id = SCOPE_IDENTITY()

end
GO
