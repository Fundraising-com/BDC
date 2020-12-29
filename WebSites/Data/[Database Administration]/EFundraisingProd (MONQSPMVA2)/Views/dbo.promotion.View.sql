USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[promotion]    Script Date: 02/14/2014 13:02:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE               VIEW [dbo].[promotion]
AS
	
SELECT p.promotion_id
	, promotion_type_code
	, NULL as targeted_market_id
	, NULL as advertising_support_id
	, NULL as advertisement_id
	, pp.partner_id
	, NULL as advertiser_id
	, 1 as transfer_status_id
	, NULL as advertisment_type_id
	, promotion_destination_id as destination_id
	, NULL as advertiser_partner_id
	, NULL as grabber_id
	, promotion_name as description
	, script_name
	, '' as contact_name
	, '' as visibility
	, NULL as tracking_serial
	, 0 as nb_impression_bought
	, active as is_active
	, cookie_content
	, 0 as is_predictive
	, keyword
	, is_displayable
	from efrcommon..promotion p
	inner join efrcommon..partner_promotion pp on p.promotion_id = pp.promotion_id
GO
