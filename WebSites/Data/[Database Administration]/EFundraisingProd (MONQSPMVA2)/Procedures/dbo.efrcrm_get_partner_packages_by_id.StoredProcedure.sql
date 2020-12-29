USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_packages_by_id]    Script Date: 02/14/2014 13:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Partner_packages
CREATE PROCEDURE [dbo].[efrcrm_get_partner_packages_by_id] @Partner_id int AS
begin

select Partner_id, Package_id from Partner_packages where Partner_id=@Partner_id

end
GO
