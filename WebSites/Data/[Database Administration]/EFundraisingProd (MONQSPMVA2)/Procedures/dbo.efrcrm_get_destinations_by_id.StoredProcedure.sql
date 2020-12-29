USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_destinations_by_id]    Script Date: 02/14/2014 13:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Destinations
CREATE PROCEDURE [dbo].[efrcrm_get_destinations_by_id] @Destination_ID int AS
begin

select Destination_ID, Web_Site_ID, URL from Destinations where Destination_ID=@Destination_ID

end
GO
