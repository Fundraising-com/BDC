USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_program]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Program
CREATE PROCEDURE [dbo].[es_insert_program] @Program_id int OUTPUT, @Program_name varchar(50), @Create_date datetime AS
begin

insert into Program(Program_name, Create_date) values(@Program_name, @Create_date)

select @Program_id = SCOPE_IDENTITY()

end
GO
