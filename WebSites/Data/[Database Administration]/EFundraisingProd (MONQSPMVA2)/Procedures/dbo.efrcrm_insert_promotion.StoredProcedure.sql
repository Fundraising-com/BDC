USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotion]    Script Date: 02/14/2014 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion
CREATE PROCEDURE [dbo].[efrcrm_insert_promotion] @Promotion_id int OUTPUT, @Promotion_type_code varchar(4), @Targeted_market_id int, @Advertising_support_id int, @Advertisement_id int, @Partner_id int, @Advertiser_id int, @Transfer_status_id tinyint, @Advertisment_type_id int, @Destination_id int, @Advertiser_partner_id int, @Grabber_id int, @Description varchar(100), @Script_name varchar(100), @Contact_name varchar(100), @Visibility varchar(50), @Tracking_serial varchar(35), @Nb_impression_bought int, @Is_active bit, @Cookie_content varchar(255), @Is_predictive bit, @Keyword varchar(255), @Is_displayable bit AS

declare @id int
exec @id = sp_NewID  'Promotion_ID', 'All'

begin

insert into Promotion(Promotion_id, Promotion_type_code, Targeted_market_id, Advertising_support_id, Advertisement_id, Partner_id, Advertiser_id, Transfer_status_id, Advertisment_type_id, Destination_id, Advertiser_partner_id, Grabber_id, Description, Script_name, Contact_name, Visibility, Tracking_serial, Nb_impression_bought, Is_active, Cookie_content, Is_predictive, Keyword, Is_displayable) values(@id, @Promotion_type_code, @Targeted_market_id, @Advertising_support_id, @Advertisement_id, @Partner_id, @Advertiser_id, @Transfer_status_id, @Advertisment_type_id, @Destination_id, @Advertiser_partner_id, @Grabber_id, @Description, @Script_name, @Contact_name, @Visibility, @Tracking_serial, @Nb_impression_bought, @Is_active, @Cookie_content, @Is_predictive, @Keyword, @Is_displayable)

end
GO
