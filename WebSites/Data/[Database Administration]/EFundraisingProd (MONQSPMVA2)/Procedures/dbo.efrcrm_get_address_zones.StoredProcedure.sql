USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_address_zones]    Script Date: 02/14/2014 13:03:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Address_zone
CREATE PROCEDURE [dbo].[efrcrm_get_address_zones] AS
begin

select Address_zone_id, Description from Address_zone

end
GO
