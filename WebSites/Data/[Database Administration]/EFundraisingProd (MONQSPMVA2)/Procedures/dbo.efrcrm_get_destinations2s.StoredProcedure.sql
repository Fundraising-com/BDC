USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_destinations2s]    Script Date: 02/14/2014 13:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Destinations2
CREATE PROCEDURE [dbo].[efrcrm_get_destinations2s] AS
begin

select Destination_ID, Web_Site_Id, URL from Destinations2

end
GO
