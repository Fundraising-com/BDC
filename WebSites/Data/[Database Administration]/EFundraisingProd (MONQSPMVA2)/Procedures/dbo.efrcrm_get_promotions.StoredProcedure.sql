USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotions]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_promotions] AS
begin

select Promotion_id, Promotion_type_code, Targeted_market_id, Advertising_support_id, Advertisement_id, Partner_id, Advertiser_id, Transfer_status_id, Advertisment_type_id, Destination_id, Advertiser_partner_id, Grabber_id, Description, Script_name, Contact_name, Visibility, Tracking_serial, Nb_impression_bought, Is_active, Cookie_content, Is_predictive, Keyword, Is_displayable from Promotion

end
GO
