USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_destinations2]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Destinations2
CREATE PROCEDURE [dbo].[efrcrm_update_destinations2] @Destination_ID int, @Web_Site_Id int, @URL varchar(200) AS
begin

update Destinations2 set Web_Site_Id=@Web_Site_Id, URL=@URL where Destination_ID=@Destination_ID

end
GO
