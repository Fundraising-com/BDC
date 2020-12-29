USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_by_id]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner
CREATE PROCEDURE [dbo].[efrstore_get_partner_by_id] @Partner_id int AS
begin

select Partner_id, Partner_type_id, Partner_name, Has_collection_site, Guid, Create_date from Partner where Partner_id=@Partner_id

end
GO
