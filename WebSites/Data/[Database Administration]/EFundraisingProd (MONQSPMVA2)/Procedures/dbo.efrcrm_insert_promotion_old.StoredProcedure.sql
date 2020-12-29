USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotion_old]    Script Date: 02/14/2014 13:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion_old
CREATE PROCEDURE [dbo].[efrcrm_insert_promotion_old] @Promotion_ID int OUTPUT, @Promotion_Type_Code varchar(4), @Description varchar(50), @Visibility varchar(50), @Contact_Name varchar(100), @Tracking_Serial varchar(35), @Nb_Impression_Bought int, @Is_Active bit, @Targeted_Market_ID int, @Advertising_Support_ID int, @Advertisement_Id int, @Partner_ID int, @Cookie_Content varchar(255), @Grabber_Id int, @Is_Predictive bit, @Advertiser_ID int, @Keyword varchar(255), @Script_Name varchar(100), @Advertisment_Type_ID int, @Destination_ID int, @Advertiser_Partner_ID int AS
begin

insert into Promotion_old(Promotion_Type_Code, Description, Visibility, Contact_Name, Tracking_Serial, Nb_Impression_Bought, Is_Active, Targeted_Market_ID, Advertising_Support_ID, Advertisement_Id, Partner_ID, Cookie_Content, Grabber_Id, Is_Predictive, Advertiser_ID, Keyword, Script_Name, Advertisment_Type_ID, Destination_ID, Advertiser_Partner_ID) values(@Promotion_Type_Code, @Description, @Visibility, @Contact_Name, @Tracking_Serial, @Nb_Impression_Bought, @Is_Active, @Targeted_Market_ID, @Advertising_Support_ID, @Advertisement_Id, @Partner_ID, @Cookie_Content, @Grabber_Id, @Is_Predictive, @Advertiser_ID, @Keyword, @Script_Name, @Advertisment_Type_ID, @Destination_ID, @Advertiser_Partner_ID)

select @Promotion_ID = SCOPE_IDENTITY()

end
GO
