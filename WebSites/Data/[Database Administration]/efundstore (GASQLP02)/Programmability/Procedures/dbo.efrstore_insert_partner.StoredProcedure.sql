USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner
CREATE PROCEDURE [dbo].[efrstore_insert_partner] @Partner_id int OUTPUT, @Partner_type_id int, @Partner_name varchar(50), @Has_collection_site bit, @Guid uniqueidentifier, @Create_date datetime AS
begin

insert into Partner(Partner_type_id, Partner_name, Has_collection_site, Guid, Create_date) values(@Partner_type_id, @Partner_name, @Has_collection_site, @Guid, @Create_date)

select @Partner_id = SCOPE_IDENTITY()

end
GO
