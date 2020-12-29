USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_type_culture]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_type_culture
CREATE PROCEDURE [dbo].[efrstore_insert_partner_type_culture] @Partner_type_id int OUTPUT, @Culture_code nvarchar(10), @Name varchar(255), @Create_date datetime AS
begin

insert into Partner_type_culture(Culture_code, Name, Create_date) values(@Culture_code, @Name, @Create_date)

select @Partner_type_id = SCOPE_IDENTITY()

end
GO
