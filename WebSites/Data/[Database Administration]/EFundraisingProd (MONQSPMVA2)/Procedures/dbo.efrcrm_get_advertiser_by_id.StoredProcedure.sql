USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertiser_by_id]    Script Date: 02/14/2014 13:03:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Advertiser
CREATE PROCEDURE [dbo].[efrcrm_get_advertiser_by_id] @Advertiser_ID int AS
begin

select Advertiser_ID, Advertisment_Type_ID, Contact_ID, Advertiser_Name from Advertiser where Advertiser_ID=@Advertiser_ID

end
GO
