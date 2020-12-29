USE [eFundweb]
GO
/****** Object:  View [dbo].[promotion]    Script Date: 02/14/2014 13:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE               VIEW [dbo].[promotion]
AS
	
SELECT p.promotion_id
	, promotion_type_code
	, promotion_name as description
	, '' as visibility
	, '' as contact_name
	, NULL as tracking_serial
	, 0 as nb_impression_bought
	, active as is_active
	, NULL as targeted_market_id
	, NULL as advertising_support_id
	, NULL as advertisement_id
	, pp.partner_id
	, cookie_content
	, NULL as grabber_id
	, 0 as is_predictive
	, NULL as advertiser_id
	, keyword
	, script_name
	, NULL as advertisment_type_id
	, promotion_destination_id as destination_id
	, NULL as advertiser_partner_id
	, is_displayable
	--, 1 as transfer_status_id
	from efrcommon..promotion p
	inner join efrcommon..partner_promotion pp on p.promotion_id = pp.promotion_id
GO
