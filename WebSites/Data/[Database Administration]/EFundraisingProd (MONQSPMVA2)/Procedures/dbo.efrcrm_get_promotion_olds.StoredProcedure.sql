USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_olds]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_old
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_olds] AS
begin

select Promotion_ID, Promotion_Type_Code, Description, Visibility, Contact_Name, Tracking_Serial, Nb_Impression_Bought, Is_Active, Targeted_Market_ID, Advertising_Support_ID, Advertisement_Id, Partner_ID, Cookie_Content, Grabber_Id, Is_Predictive, Advertiser_ID, Keyword, Script_Name, Advertisment_Type_ID, Destination_ID, Advertiser_Partner_ID from Promotion_old

end
GO
