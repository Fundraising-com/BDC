USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertiser]    Script Date: 02/14/2014 13:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertiser
CREATE PROCEDURE [dbo].[efrcrm_update_advertiser] @Advertiser_ID int, @Advertisment_Type_ID int, @Contact_ID int, @Advertiser_Name varchar(200) AS
begin

update Advertiser set Advertisment_Type_ID=@Advertisment_Type_ID, Contact_ID=@Contact_ID, Advertiser_Name=@Advertiser_Name where Advertiser_ID=@Advertiser_ID

end
GO
