USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_destinationss]    Script Date: 02/14/2014 13:04:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Destinations
CREATE PROCEDURE [dbo].[efrcrm_get_destinationss] AS
begin

select Destination_ID, Web_Site_ID, URL from Destinations

end
GO
