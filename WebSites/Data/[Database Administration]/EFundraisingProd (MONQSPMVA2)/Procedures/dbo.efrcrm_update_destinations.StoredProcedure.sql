USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_destinations]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Destinations
CREATE PROCEDURE [dbo].[efrcrm_update_destinations] @Destination_ID int, @Web_Site_ID int, @URL varchar(200) AS
begin

update Destinations set Web_Site_ID=@Web_Site_ID, URL=@URL where Destination_ID=@Destination_ID

end
GO
