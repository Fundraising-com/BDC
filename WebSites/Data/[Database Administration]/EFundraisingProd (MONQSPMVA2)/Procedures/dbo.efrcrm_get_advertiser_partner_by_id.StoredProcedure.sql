USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertiser_partner_by_id]    Script Date: 02/14/2014 13:03:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Advertiser_Partner
CREATE PROCEDURE [dbo].[efrcrm_get_advertiser_partner_by_id] @Advertiser_Partner_ID int AS
begin

select Advertiser_Partner_ID, Advertiser_ID, Description from Advertiser_Partner where Advertiser_Partner_ID=@Advertiser_Partner_ID

end
GO
