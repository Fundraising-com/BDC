USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partners]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner
CREATE PROCEDURE [dbo].[efrstore_get_partners] AS
begin

select Partner_id, Partner_type_id, Partner_name, Has_collection_site, Guid, Create_date from Partner

end
GO
