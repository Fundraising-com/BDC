USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_online_user_registered_info]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	june 8, 2004
Description:	this gets all the info for an online user (personal info plus order info)
*/
--[dbo].[get_online_user_info] 100,'en-us'
CREATE     PROCEDURE [dbo].[get_online_user_registered_info] 
	@intOnlineUserID INT
	, @strCultureName VARCHAR(10)
AS

-- get rest of the user's information
SELECT
	o.order_number,
	cct.credit_card_type_name AS card_type 
	, REPLACE( STR( cc.month_expire, 2 ), ' ', '0' ) + '/' + CONVERT( CHAR(4), cc.year_expire ) AS exp_date,
	cc.last_digits, 
	cc.credit_card, 
	o.tax_total,
	o.order_total,
	o.shipping_total, 
	pa.partner_name, 
	c.first_name + ' ' + c.last_name  AS client_name,
        convert(char(12),o.date_created,109)as date_created,
	c.email
FROM         dbo.Promotion p INNER JOIN
                      dbo.client c ON p.Promotion_ID = c.promotion_id INNER JOIN
                      dbo.partner pa ON p.Partner_ID = pa.partner_id INNER JOIN
                      dbo.online_users ou ON c.client_sequence_code = ou.client_sequence_code AND 
                      c.client_id = ou.client_id INNER JOIN
                      dbo.orders o INNER JOIN
                      dbo.credit_cards cc ON o.credit_card_id = cc.credit_card_id INNER JOIN
                      dbo.credit_card_types cct ON cc.credit_card_type_id = cct.credit_card_type_id ON ou.online_user_id = o.online_user_id
WHERE     o.online_user_id =  @intOnlineUserID
GO
