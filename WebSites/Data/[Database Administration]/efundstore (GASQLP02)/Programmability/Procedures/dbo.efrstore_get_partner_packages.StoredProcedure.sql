USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_partner_packages]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_package
CREATE PROCEDURE [dbo].[efrstore_get_partner_packages] AS
begin

select Partner_id, Package_id from Partner_package

end
GO
