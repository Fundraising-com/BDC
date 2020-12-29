USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_destinations2_by_id]    Script Date: 02/14/2014 13:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Destinations2
CREATE PROCEDURE [dbo].[efrcrm_get_destinations2_by_id] @Destination_ID int AS
begin

select Destination_ID, Web_Site_Id, URL from Destinations2 where Destination_ID=@Destination_ID

end
GO
