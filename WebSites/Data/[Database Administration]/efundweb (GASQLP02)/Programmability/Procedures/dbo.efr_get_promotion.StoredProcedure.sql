USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_promotion]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [dbo].[efr_get_promotion] 1

CREATE PROCEDURE [dbo].[efr_get_promotion]
	@promotion_id int
AS
SELECT
	[promotion_id],
	[promotion_type_code],
	[targeted_market_id],
	[advertising_support_id],
	[advertisement_id],
	[partner_id],
	[advertiser_id],
	[advertisment_type_id],
	[destination_id],
	[advertiser_partner_id],
	[grabber_id],
	[description],
	[script_name],
	[contact_name],
	[visibility],
	[tracking_serial],
	[nb_impression_bought],
	[is_active],
	[cookie_content],
	[is_predictive],
	[keyword]
FROM
	promotion WITH (NOLOCK)
WHERE 
	promotion_id = @promotion_id
GO
