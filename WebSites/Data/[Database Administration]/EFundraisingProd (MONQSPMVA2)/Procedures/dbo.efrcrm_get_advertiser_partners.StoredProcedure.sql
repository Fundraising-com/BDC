USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertiser_partners]    Script Date: 02/14/2014 13:03:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Advertiser_Partner
CREATE PROCEDURE [dbo].[efrcrm_get_advertiser_partners] AS
begin

select Advertiser_Partner_ID, Advertiser_ID, Description from Advertiser_Partner

end
GO
