USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_promotion]    Script Date: 02/14/2014 13:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion
CREATE PROCEDURE [dbo].[efrcrm_update_promotion] @Promotion_id int, @Promotion_type_code varchar(4), @Targeted_market_id int, @Advertising_support_id int, @Advertisement_id int, @Partner_id int, @Advertiser_id int, @Transfer_status_id tinyint, @Advertisment_type_id int, @Destination_id int, @Advertiser_partner_id int, @Grabber_id int, @Description varchar(100), @Script_name varchar(100), @Contact_name varchar(100), @Visibility varchar(50), @Tracking_serial varchar(35), @Nb_impression_bought int, @Is_active bit, @Cookie_content varchar(255), @Is_predictive bit, @Keyword varchar(255), @Is_displayable bit AS
begin

update Promotion set Promotion_type_code=@Promotion_type_code, Targeted_market_id=@Targeted_market_id, Advertising_support_id=@Advertising_support_id, Advertisement_id=@Advertisement_id, Partner_id=@Partner_id, Advertiser_id=@Advertiser_id, Transfer_status_id=@Transfer_status_id, Advertisment_type_id=@Advertisment_type_id, Destination_id=@Destination_id, Advertiser_partner_id=@Advertiser_partner_id, Grabber_id=@Grabber_id, Description=@Description, Script_name=@Script_name, Contact_name=@Contact_name, Visibility=@Visibility, Tracking_serial=@Tracking_serial, Nb_impression_bought=@Nb_impression_bought, Is_active=@Is_active, Cookie_content=@Cookie_content, Is_predictive=@Is_predictive, Keyword=@Keyword, Is_displayable=@Is_displayable where Promotion_id=@Promotion_id

end
GO
