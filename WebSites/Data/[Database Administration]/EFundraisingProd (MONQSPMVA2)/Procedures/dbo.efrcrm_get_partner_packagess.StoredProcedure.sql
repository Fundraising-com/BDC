USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_packagess]    Script Date: 02/14/2014 13:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_packages
CREATE PROCEDURE [dbo].[efrcrm_get_partner_packagess] AS
begin

select Partner_id, Package_id from Partner_packages

end
GO
