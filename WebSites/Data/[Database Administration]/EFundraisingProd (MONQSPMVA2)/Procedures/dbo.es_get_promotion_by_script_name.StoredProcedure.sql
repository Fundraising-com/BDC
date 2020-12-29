USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_get_promotion_by_script_name]    Script Date: 02/14/2014 13:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_promotion_by_script_name]
	@script_name varchar(100)
AS
SELECT
	
	[promotion_id],
	[promotion_type_code],
	[targeted_market_id],
	[advertising_support_id],
	[advertisement_id],
	[partner_id],
	[advertiser_id],
	[transfer_status_id],
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
	efundraisingprod.dbo.promotion
WHERE 
	script_name = @script_name
GO
