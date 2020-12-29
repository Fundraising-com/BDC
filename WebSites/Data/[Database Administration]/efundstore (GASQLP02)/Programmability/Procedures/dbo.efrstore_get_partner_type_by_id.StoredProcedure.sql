USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_type_by_id]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_type
CREATE PROCEDURE [dbo].[efrstore_get_partner_type_by_id] @Partner_type_id int AS
begin

select Partner_type_id, Name, Create_date from Partner_type where Partner_type_id=@Partner_type_id

end
GO
