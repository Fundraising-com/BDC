USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_package_by_id]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_package
CREATE PROCEDURE [dbo].[efrstore_get_partner_package_by_id] @Partner_id int AS
begin

select Partner_id, Package_id from Partner_package where Partner_id=@Partner_id

end
GO
