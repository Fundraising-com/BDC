USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_package]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_package
CREATE PROCEDURE [dbo].[efrstore_insert_partner_package] @Partner_id int OUTPUT, @Package_id tinyint AS
begin

insert into Partner_package(Package_id) values(@Package_id)

select @Partner_id = SCOPE_IDENTITY()

end
GO
