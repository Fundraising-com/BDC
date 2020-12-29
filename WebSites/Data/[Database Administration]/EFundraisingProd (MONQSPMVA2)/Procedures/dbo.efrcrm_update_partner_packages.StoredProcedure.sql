USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_partner_packages]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Partner_packages
CREATE PROCEDURE [dbo].[efrcrm_update_partner_packages] @Partner_id int, @Package_id tinyint AS
begin

update Partner_packages set Package_id=@Package_id where Partner_id=@Partner_id

end
GO
