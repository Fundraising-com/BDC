USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_address_zone]    Script Date: 02/14/2014 13:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Address_zone
CREATE PROCEDURE [dbo].[efrcrm_update_address_zone] @Address_zone_id int, @Description varchar(255) AS
begin

update Address_zone set Description=@Description where Address_zone_id=@Address_zone_id

end
GO
