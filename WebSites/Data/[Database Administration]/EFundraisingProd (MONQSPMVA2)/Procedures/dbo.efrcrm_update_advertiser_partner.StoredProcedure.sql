USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertiser_partner]    Script Date: 02/14/2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertiser_Partner
CREATE PROCEDURE [dbo].[efrcrm_update_advertiser_partner] @Advertiser_Partner_ID int, @Advertiser_ID int, @Description varchar(200) AS
begin

update Advertiser_Partner set Advertiser_ID=@Advertiser_ID, Description=@Description where Advertiser_Partner_ID=@Advertiser_Partner_ID

end
GO
